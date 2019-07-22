using System;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Polly;
using System.Text;

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
        public int retryCount;
        public bool debug;

        public HTTPHandler(HttpClient c)
        {
            retryCount = 0;
            ShowRetries = false;
            debug = false;
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

        private async Task<HttpResponseMessage> LimitedCallAsync(string type, string url, string SerializedCanvasObject)
        {
            while (Runners >= MaxRunners) { System.Threading.Thread.Sleep(1); }
            Runners += 1;

            var stringContent = new StringContent(SerializedCanvasObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            
            if (type.ToLower() == "delete") response = await Client.DeleteAsync(url);
            else if (type.ToLower() == "get") response = await Client.GetAsync(url);
            else if (type.ToLower() == "post") response = await Client.PostAsync(url, stringContent);
            else if (type.ToLower() == "put") response = await Client.PutAsync(url, stringContent);
            else throw new ArgumentException("type must be \"delete\" or \"get\" or \"post\" or \"put\"");
            
            Runners -= 1;
            return response;
        }

        private async Task<string> ApiCallAsync(string type, string path, string input)
        {
            var response = await LimitedCallAsync(type, APIContext + path, input);
            
            // check if the request was denied because we're over our API limit
            // then try until you succeed
            var status = response.Headers.GetValues("Status").FirstOrDefault();
            var retries = 1;

            // keep trying until APIRetryLimit
            while (status == "403 Forbidden")
            {
                if (ShowRetries)
                {
                    Console.Error.Write(" | " + response.Headers.GetValues("Status").FirstOrDefault());
                    Console.Error.Write(" | " + path);
                    Console.Error.WriteLine(" | retrying " + retries);
                }

                // wait a bit between each call
                System.Threading.Thread.Sleep(retries * 1000);

                if (debug) { Console.Write("*"); } // retry
                retryCount += 1;

                // wait for an open space and then make the call
                response = await LimitedCallAsync(type, APIContext + path, input);

                // check if the call was successful
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

                    if (debug) { Console.Write("!"); } // too many retries

                    return await response.Content.ReadAsStringAsync();
                }
            }

            // failed API Call, not just over the rate limit.
            if (response.Headers.GetValues("Status").FirstOrDefault() != "200 OK")
            {
                Console.Error.WriteLine("");
                Console.Error.Write(response.Headers.GetValues("Status").FirstOrDefault());
                Console.Error.WriteLine(" :at: " + path);
                Console.Error.WriteLine(response);

                if (debug) { Console.Write("?"); } // real failure

                return await response.Content.ReadAsStringAsync();
            }

            if (debug) { Console.Write("."); } // success
            
            // parse the headers to use in throttling
            var limit = response.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault();
            var cost = response.Headers.GetValues("X-Request-Cost").FirstOrDefault();
            RecalculateMaxRunners(float.Parse(cost), float.Parse(limit));

            var results = await response.Content.ReadAsStringAsync();

            // check for pagination (only in get requests)
            if(type.ToLower() == "get" && response.Headers.Contains("Link"))
            {
                var link = response.Headers.GetValues("Link").FirstOrDefault();
                var pages = link.Split(';', ',', '"');
                // index 6 will be either "next" (more to do) or "first" (only 1 page) or "prev" (currently last page)
                if (pages[6] == "next")
                {
                    string newpath = path.Split("?page=")[0];
                    newpath += "?page=" + (path.Split("?page=").Length == 1 ? "2" : (int.Parse(path.Split("?page=")[1]) + 1).ToString());
                    results = results.Substring(0, results.Length - 1) + ",";
                    results += (await ApiCallAsync(type, newpath, input)).Substring(1);
                }
            }

            return results;
        }
        public async Task<T> Get<T>(string api_call)
        {
            if (api_call[0] == '/') api_call = api_call.Substring(1);
            string json = await ApiCallAsync("get", api_call, "");
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<string> Get(string api_call)
        {
            if (api_call[0] == '/') api_call = api_call.Substring(1);
            return await ApiCallAsync("get", api_call, "");
        }

        // public async Task<T> Delete<T>(string api_call)
        // {
        //     string json = await ApiCallAsync("delete", api_call, "");
        //     return JsonConvert.DeserializeObject<T>(json);
        // }

        // public async Task<string> Delete(string api_call)
        // {
        //     return await ApiCallAsync("delete", api_call, "");
        // }

        // public async Task<T> Post<T>(string api_call, T InputObject)
        // {
        //     string json = await ApiCallAsync("post", api_call, JsonConvert.SerializeObject(InputObject));
        //     return JsonConvert.DeserializeObject<T>(json);
        // }

        // public async Task<string> Post(string api_call, object InputObject)
        // {
        //     return await ApiCallAsync("post", api_call, JsonConvert.SerializeObject(InputObject));
        // }

        // public async Task<T> Put<T>(string api_call, T InputObject)
        // {
        //     string json = await ApiCallAsync("put", api_call, JsonConvert.SerializeObject(InputObject));
        //     return JsonConvert.DeserializeObject<T>(json);
        // }

        // public async Task<string> Put(string api_call, object InputObject)
        // {
        //     return await ApiCallAsync("put", api_call, JsonConvert.SerializeObject(InputObject));
        // }
    }
}
