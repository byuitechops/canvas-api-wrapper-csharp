using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using CanvasAPIWrapper;

namespace APITesting
{
    class Program
    {
        public static Task<string> BigAPICall(string page_number, Wrapper wrapper)
        {
            string call = "accounts/1/courses?include[]=needs_grading_count&include[]=syllabus_body&include[]=public_description&include[]=total_scores&include[]=current_grading_period_scores&include[]=term&include[]=account&include[]=course_progress&include[]=sections&include[]=storage_quota_used_mb&include[]=total_students&include[]=passback_status&include[]=favorites&include[]=teachers&include[]=observed_users&include[]=course_image&include[]=concluded&per_page=100&page=";
            return wrapper.http.Get(call + page_number);
        }

        public static Task<string> SmallAPICall(string page_number, Wrapper wrapper)
        {
            string call = "accounts/1/courses?page=";
            return wrapper.http.Get(call + page_number);
        }

        static async Task<string> test()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var myhttp = new HttpClient();
            Wrapper canvas = new Wrapper(myhttp);
            var myCourses = new List<Task<string>>();

            // canvas.http.Show403Retries(true); // show everytime a course is retried

            // expensive API calls
            for (int i = 1; i <= 268; i++)
            {
                myCourses.Add(BigAPICall(i.ToString(), canvas));
            }

            // cheap API calls
            for (int i = 1; i <= 1245; i++)
            {
                myCourses.Add(SmallAPICall(i.ToString(), canvas));
            }

            // these occur concurrently and out of order, so they break the API limiting system
            // and cause interesting errors

            string[] results = await Task.WhenAll(myCourses.ToArray()); // wait for everything to finish

            // everything comes back in the expected order
            // Console.WriteLine("");
            // Array.ForEach(results, x => Console.Write((x.Count() % 10000).ToString().PadLeft(4) + " | "));

            timer.Stop();
            return timer.Elapsed + " - " + canvas.http.ErrorCount + "/" + myCourses.Count;
        }

        static async Task Main(string[] args)
        {
            List<string> results = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                results.Add(await test());
            }
            
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }
    }
}
