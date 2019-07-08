using System;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanvasAPIWrapper
{
    partial class HTMLHandler
    {
        public HttpClient client;
        public int chill;
        public int runners;
        public float buffer_limit;
        public int chill_time;
        private string domain;
        private string api_prefix;
        private string api_context;

        public HTMLHandler(HttpClient c)
        {
            client = c;
            // Polly -> Retry
            chill = 0; // Polly -> Circuit Breaker
            runners = 14; // Polly -> Bulkhead Isolation
            buffer_limit = 300.0F;
            chill_time = 100;

            string token = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
            if (token == null) { throw new System.ArgumentException("Canvas api token was not set"); }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            domain = "https://byui.instructure.com";
            api_prefix = "/api/v1/";
            api_context = domain + api_prefix;
        }

        public async Task<string> getJsonAsync(string path)
        {
            // limit the number of concurrent api calls
            // 700 rate-limit, 50 credit-hold, 14 api calls at once reduce rate-limit to 0
            while (runners <= 0)
            {
                System.Threading.Thread.Sleep(1); // cpu intensive but it's not doing anything else.
            }

            runners -= 1;

            // if someone came back with a low rate limit then the new runners have to wait
            // for the track to cool down
            // This is important if there is a big API call
            while (chill > 0)
            {
                // TODO: figure out what a good sleep time would be here...
                Console.WriteLine(":=:chill: " + chill);
                System.Threading.Thread.Sleep(chill_time);
                chill -= 1;
                if (chill < 0) chill = 1;
            }

            // make the actual api call and wait for it
            // luckily this is threaded so other stuff can happen at the same time
            var response = await client.GetAsync(api_context + path);
            
            // parse some response
            var limit = response.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault();
            var cost = response.Headers.GetValues("X-Request-Cost").FirstOrDefault();
            Console.WriteLine($"Path: {path.PadLeft(13)} | Limit: {float.Parse(limit)}");
            
            // check if it's getting hot in here
            if (float.Parse(limit) < buffer_limit) 
            {
                // NOBODY NEW CAN GO UNTIL THEY CHILL OUT A BIT
                chill += 1;
            }

            // done running so free up a spot for other threads
            runners += 1;
            
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
