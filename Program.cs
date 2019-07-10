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
        static async Task Main(string[] args)
        {
            var myhttp = new HttpClient();
            Wrapper canvas = new Wrapper(myhttp);

            // keep track of all the tasks
            var myCourses = new List<Task<string>>();

            for (int i = 1; i <= 400; i++)
            {
                myCourses.Add(canvas.http.BigAPICall(i.ToString()));
            }

            for (int i = 1; i <= 1000; i++)
            {
                myCourses.Add(canvas.http.SmallAPICall(i.ToString()));
            }

            // wait until everything finishes before doing the next code
            string[] results = await Task.WhenAll(myCourses.ToArray());

            Console.WriteLine("");

            // this shows that things come back in the correct (expected) order
            Array.ForEach(results, x => Console.Write(x.Count().ToString() + " | "));
        }
    }
}
