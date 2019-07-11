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
        private HttpClient Client;
        private int Runners;
        private int MaxRunners;
        private float APICostLimit;
        private float APIUpfrontCost;
        private List<float> Costs;
        private string Domain;
        private string APIPrefix;
        private string APIContext;
        private float BigCost;
        public bool ShowRetries;
        public int APIRetryLimit;
        public int ErrorCount; // :T:

        public HTTPHandler(HttpClient c)
        {
            ShowRetries = false;
            ErrorCount = 0; // :T:
            Client = c;

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
            APICostLimit = 700F; // API rate limit is 700
            APIUpfrontCost = 15F; // canvas seems to automatically apply a cost of 15 to each
            BigCost = 12F; // this is the biggest API call I've ever seen

            Costs = new List<float>();
            Costs.Add(BigCost);
            
            MaxRunners = 5; // start off small
            Runners = 0;

            APIRetryLimit = 10;

            // Ensure proper authentication
            string token = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
            if (token == null) { throw new System.ArgumentException("Canvas api token was not set"); }
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Domain = "https://byui.instructure.com";
            APIPrefix = "/api/v1/";
            APIContext = Domain + APIPrefix;
        }

        // Set to "true" if you want to see how many retries you are making (defaults to "false")
        public bool Show403Retries(bool n)
        {
            ShowRetries = n;
            return ShowRetries;
        }

        // determine how many concurrent API calls we can make, based on the 15-unit-holding-fee that Canvas has.
        private void RecalculateMaxRunners(float cost, float limit)
        {
            // var old = MaxRunners; // :T:

            Costs.Add(cost);
            var SmallCallMax = (APICostLimit / APIUpfrontCost);
            MaxRunners = Convert.ToInt32((APICostLimit - SmallCallMax * Costs.ToArray().Average()) / APIUpfrontCost);

            // API limit = 700 / 15 concurrent, so 46 concurrent calls plus some change.
            // Setting it to 45 max saves us if we hit a big call after a bunch of small ones.
            if (MaxRunners > 45) MaxRunners = 45; // small api calls can overrun
            if (MaxRunners < 5) MaxRunners = 5; // in case the penalty gets too large we don't want to stop.

            // the average quickly gets out of touch when it remembers the past
            // if (Costs.Count > 30) Costs.RemoveAt(0);

            // if (old != MaxRunners) Console.Write("." + MaxRunners.ToString()); // :T:
        }

        private async Task<string> getJsonAsync(string path)
        {
            // limit the number of concurrent api calls
            // 700 rate-limit, 50 credit-hold, 14 api calls at once reduce rate-limit to 0
            while (Runners >= MaxRunners)
            {
                System.Threading.Thread.Sleep(1); // cpu intensive but it's not doing anything else.
            }

            Runners += 1;

            // make the actual api call and wait for it
            // luckily this is threaded so other stuff can happen at the same time
            var response = await Client.GetAsync(APIContext + path);
            
            // done running so free up a spot for other threads
            Runners -= 1;
            
            // check if the request was denied because we're over our API limit
            // then try until you succeed
            var status = response.Headers.GetValues("Status").FirstOrDefault();
            var retries = 1;

            // keep trying until APIRetryLimit
            // just 
            while (status == "403 Forbidden")
            {
                // Console.Error.Write("*"); // :T:
                ErrorCount += 1; // :T:

                if (ShowRetries)
                {
                    Console.Error.Write(" | " + response.Headers.GetValues("Status").FirstOrDefault());
                    Console.Error.Write(" | " + path);
                    Console.Error.WriteLine(" | retrying " + retries);
                }

                System.Threading.Thread.Sleep(retries * 1000);

                while (Runners >= MaxRunners) { System.Threading.Thread.Sleep(1); }
                Runners += 1;
                response = await Client.GetAsync(APIContext + path);
                Runners -= 1;

                status = response.Headers.GetValues("Status").FirstOrDefault();
                var limit403 = response.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault();
                RecalculateMaxRunners(BigCost * 4, float.Parse(limit403)); // extra expensive to slow things down

                retries += 1;

                if (status != "200 OK" && retries >= APIRetryLimit)
                {
                    Console.Error.WriteLine("");
                    Console.Error.Write("too many " + status);
                    Console.Error.WriteLine(" at: " + path);
                    Console.Error.WriteLine("Probably an invalid call");
                    Console.Error.WriteLine(response);

                    return await response.Content.ReadAsStringAsync();
                }
            }

            if (response.Headers.GetValues("Status").FirstOrDefault() != "200 OK")
            {
                Console.Error.WriteLine("");
                Console.Error.Write(response.Headers.GetValues("Status").FirstOrDefault());
                Console.Error.WriteLine(" :at: " + path);
                Console.Error.WriteLine(response);
                return await response.Content.ReadAsStringAsync();
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
    }
}
