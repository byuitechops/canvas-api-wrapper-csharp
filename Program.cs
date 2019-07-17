using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CanvasAPIWrapper;
using Newtonsoft.Json;

namespace APITesting
{
    class Program
    {
        public static Task<string> BigAPICall(string page_number, Wrapper wrapper)
        {
            string call = "accounts/1/courses?include[]=needs_grading_count&include[]=syllabus_body&include[]=public_description&include[]=total_scores&include[]=current_grading_period_scores&include[]=term&include[]=account&include[]=course_progress&include[]=sections&include[]=storage_quota_used_mb&include[]=total_students&include[]=passback_status&include[]=favorites&include[]=teachers&include[]=observed_users&include[]=course_image&include[]=concluded&per_page=100&page=";
            return wrapper.Http.Get(call + page_number);
        }

        public static Task<string> SmallAPICall(string page_number, Wrapper wrapper)
        {
            string call = "accounts/1/courses?page=";
            return wrapper.Http.Get(call + page_number);
        }

        static async Task<string> test()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            Wrapper canvas = new Wrapper();
            var myStrings = new List<Task<string>>();
            var myCourses = new List<Task<CourseObject>>();

            // var section8params = new CourseParametersObject();
            // section8params.include.public_description = true;
            // section8params.include.account = true;
            // section8params.include.concluded = true;
            // section8params.include.observed_users = true;
            // section8params.include.syllabus_body = true;
            // section8params.include.teachers = true;
            // section8params.teacher_limit = 12;

            // Console.WriteLine(section8params.GetParameterString());

            // canvas.http.Show403Retries(true); // show everytime a course is retried
            // canvas.http.debug = true;

            // // expensive API calls
            // for (int i = 1; i <= 268; i++)
            // {
            //     myStrings.Add(BigAPICall(i.ToString(), canvas));
            // }

            // // cheap API calls
            // for (int i = 1; i <= 1245; i++)
            // {
            //     myStrings.Add(SmallAPICall(i.ToString(), canvas));
            // }

            myCourses.Add(canvas.Courses.Show("40654"));
            myCourses.Add(canvas.Courses.Show("40654", "?include[]=syllabus_body"));
            myCourses.Add(canvas.Courses.Show("40654", "?include[]=total_students&include[]=total_scores&include[]=permissions"));

            // these occur concurrently and out of order, so they break the API limiting system
            // and cause interesting errors

            // everything comes back in the expected order
            // Console.WriteLine("");
            CourseObject[] results = await Task.WhenAll(myCourses.ToArray()); // wait for everything to finish
            // Array.ForEach(results, x => Console.WriteLine(JsonHelper.FormatJson(Newtonsoft.Json.JsonConvert.SerializeObject(x))));
            
            var courseUsers = await canvas.Courses.ListUsersInCourse("50728");

            Console.WriteLine("");
            Array.ForEach(courseUsers.ToArray(), x => Console.WriteLine(JsonHelper.FormatJson(JsonConvert.SerializeObject(x))));
            Console.WriteLine("");

            // string[] results = await Task.WhenAll(myStrings.ToArray()); // wait for everything to finish
            // long total = 0;
            // Array.ForEach(results, x => Console.WriteLine(JsonConvert.SerializeObject(x)));
            // Console.WriteLine(total / results.Length);


            timer.Stop();
            return timer.Elapsed + " - " + canvas.Http.retryCount + "/" + myCourses.Count;
        }

        static async Task Main(string[] args)
        {
            List<string> results = new List<string>();
            for (int i = 0; i < 1; i++)
            {
                results.Add(await test());
            }
            
            Console.WriteLine("");

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }
    }
}
