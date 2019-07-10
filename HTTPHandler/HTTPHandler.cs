using System;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Polly;

namespace CanvasAPIWrapper
{
    partial class HTTPHandler
    {
        public HttpClient client;
        public int runners;
        public int MaxRunners;
        private int RunnerPenalty;
        public float API_cost_limit;
        public float API_upfront_cost;
        public List<float> costs;
        private string domain;
        private string api_prefix;
        private string api_context;

        public HTTPHandler(HttpClient c)
        {
            client = c;

            // Polly -> Retry (In case of any real errors)
            Policy
                .Handle<HttpRequestException>()
                .WaitAndRetry(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(4)
                });

            // Limit the number of concurrent API calls
            API_cost_limit = 700F; // API rate limit is 700
            API_upfront_cost = 15F; // canvas seems to automatically apply a cost of 15 to each
            var big_cost = 12F; // this is the biggest API call I've ever seen

            costs = new List<float>();
            costs.Add(big_cost);
            
            var small_call_max = (API_cost_limit / API_upfront_cost);
            
            MaxRunners = Convert.ToInt32((API_cost_limit - small_call_max * big_cost) / API_upfront_cost);
            runners = 0;
            RunnerPenalty = 1;

            // Ensure proper authentication
            string token = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
            if (token == null) { throw new System.ArgumentException("Canvas api token was not set"); }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            domain = "https://byui.instructure.com";
            api_prefix = "/api/v1/";
            api_context = domain + api_prefix;
        }

        public void RecalculateMaxRunners(float cost, float limit)
        {
            costs.Add(cost);
            var small_call_max = (API_cost_limit / API_upfront_cost);
            MaxRunners = Convert.ToInt32(((API_cost_limit - small_call_max * costs.Average()) / API_upfront_cost) - RunnerPenalty);

            // API limit = 700 / 15 concurrent, so 46 concurrent calls plus some change.
            // Setting it to 45 max saves us if we hit a big call after a bunch of small ones.
            if (MaxRunners > 45) MaxRunners = 45; // small api calls can overrun
            if (MaxRunners < 5) MaxRunners = 5; // in case the penalty gets too large we don't want to stop.

            // the average quickly gets out of touch when it remembers the past
            if (costs.Count > 30) costs.RemoveAt(0);

            // Once we're moving fast again we can let more bandwidth through
            if (limit > 500) RunnerPenalty = 0;
        }

        public async Task<string> getJsonAsync(string path)
        {
            // limit the number of concurrent api calls
            // 700 rate-limit, 50 credit-hold, 14 api calls at once reduce rate-limit to 0
            while (runners >= MaxRunners)
            {
                System.Threading.Thread.Sleep(1); // cpu intensive but it's not doing anything else.
            }

            runners += 1;

            // make the actual api call and wait for it
            // luckily this is threaded so other stuff can happen at the same time
            var response = await client.GetAsync(api_context + path);
            
            // done running so free up a spot for other threads
            runners -= 1;
            
            // check if the request was denied because we're over our API limit
            // then try until you succeed
            if (response.Headers.GetValues("Status").FirstOrDefault() != "200 OK")
            {
                // Console.Error.WriteLine($"Failed:{path}: ... Retrying...");
                RunnerPenalty += 1;
                System.Threading.Thread.Sleep(500);

                return await getJsonAsync(path);
            }
            
            // parse some response
            var limit = response.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault();
            var cost = response.Headers.GetValues("X-Request-Cost").FirstOrDefault();

            RecalculateMaxRunners(float.Parse(cost), float.Parse(limit));

            var results = await response.Content.ReadAsStringAsync();

            return results;
        }
        public async Task<T> Get<T>(string api_call)
        {
            string json = await getJsonAsync(api_call);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<string> Get(string api_call)
        {
            return await getJsonAsync(api_call);
        }

        public async Task<string> BigAPICall(string page_number)
        {
            string call = "accounts/1/courses?include[]=needs_grading_count&include[]=syllabus_body&include[]=public_description&include[]=total_scores&include[]=current_grading_period_scores&include[]=term&include[]=account&include[]=course_progress&include[]=sections&include[]=storage_quota_used_mb&include[]=total_students&include[]=passback_status&include[]=favorites&include[]=teachers&include[]=observed_users&include[]=course_image&include[]=concluded&per_page=100&page=";
            return await getJsonAsync(call + page_number);
        }

        public async Task<string> SmallAPICall(string page_number)
        {
            string call = "accounts/1/courses?page=";
            return await getJsonAsync(call + page_number);
        }
    }
}
