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
        static void Main(string[] args)
        {
            Wrapper canvas = new Wrapper();

            Console.WriteLine("==============================================");
            Console.WriteLine("================ CONCURRENCY =================");
            Console.WriteLine("==============================================");
            Console.WriteLine("");

            Console.WriteLine("testing 30 calls to canvas");

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("Starting synchronous timer...");
            Stopwatch synchTimer = new Stopwatch();
            synchTimer.Start();

            canvas.Courses.Show("40958").Wait();
            canvas.Courses.Show("40960").Wait();
            canvas.Courses.Show("40962").Wait();
            canvas.Courses.Show("40964").Wait();
            canvas.Courses.Show("40966").Wait();
            canvas.Courses.Show("40958").Wait();
            canvas.Courses.Show("40960").Wait();
            canvas.Courses.Show("40962").Wait();
            canvas.Courses.Show("40964").Wait();
            canvas.Courses.Show("40966").Wait();
            canvas.Courses.Show("40958").Wait();
            canvas.Courses.Show("40960").Wait();
            canvas.Courses.Show("40962").Wait();
            canvas.Courses.Show("40964").Wait();
            canvas.Courses.Show("40966").Wait();
            canvas.Courses.Show("40958").Wait();
            canvas.Courses.Show("40960").Wait();
            canvas.Courses.Show("40962").Wait();
            canvas.Courses.Show("40964").Wait();
            canvas.Courses.Show("40966").Wait();
            canvas.Courses.Show("40958").Wait();
            canvas.Courses.Show("40960").Wait();
            canvas.Courses.Show("40962").Wait();
            canvas.Courses.Show("40964").Wait();
            canvas.Courses.Show("40966").Wait();
            canvas.Courses.Show("40958").Wait();
            canvas.Courses.Show("40960").Wait();
            canvas.Courses.Show("40962").Wait();
            canvas.Courses.Show("40964").Wait();
            canvas.Courses.Show("40966").Wait();

            synchTimer.Stop();
            Console.WriteLine("Time: " + synchTimer.Elapsed);

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("Starting asynchronous timer...");
            Stopwatch asynchTimer = new Stopwatch();
            asynchTimer.Start();

            var courses = new List<Task<CourseObject>>();
            courses.Add(canvas.Courses.Show("40958"));
            courses.Add(canvas.Courses.Show("40960"));
            courses.Add(canvas.Courses.Show("40962"));
            courses.Add(canvas.Courses.Show("40964"));
            courses.Add(canvas.Courses.Show("40966"));
            courses.Add(canvas.Courses.Show("40958"));
            courses.Add(canvas.Courses.Show("40960"));
            courses.Add(canvas.Courses.Show("40962"));
            courses.Add(canvas.Courses.Show("40964"));
            courses.Add(canvas.Courses.Show("40966"));
            courses.Add(canvas.Courses.Show("40958"));
            courses.Add(canvas.Courses.Show("40960"));
            courses.Add(canvas.Courses.Show("40962"));
            courses.Add(canvas.Courses.Show("40964"));
            courses.Add(canvas.Courses.Show("40966"));
            courses.Add(canvas.Courses.Show("40958"));
            courses.Add(canvas.Courses.Show("40960"));
            courses.Add(canvas.Courses.Show("40962"));
            courses.Add(canvas.Courses.Show("40964"));
            courses.Add(canvas.Courses.Show("40966"));
            courses.Add(canvas.Courses.Show("40958"));
            courses.Add(canvas.Courses.Show("40960"));
            courses.Add(canvas.Courses.Show("40962"));
            courses.Add(canvas.Courses.Show("40964"));
            courses.Add(canvas.Courses.Show("40966"));
            courses.Add(canvas.Courses.Show("40958"));
            courses.Add(canvas.Courses.Show("40960"));
            courses.Add(canvas.Courses.Show("40962"));
            courses.Add(canvas.Courses.Show("40964"));
            courses.Add(canvas.Courses.Show("40966"));

            Task<CourseObject[]> t2 = Task.WhenAll(courses.ToArray());
            t2.Wait();

            asynchTimer.Stop();
            Console.WriteLine("Time: " + asynchTimer.Elapsed);

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("An improvement of " + Math.Floor(synchTimer.Elapsed / asynchTimer.Elapsed * 100).ToString() + "%!");

            Console.WriteLine("");
            Console.WriteLine("==============================================");
            Console.WriteLine("================= PAGINATION =================");
            Console.WriteLine("==============================================");
            Console.WriteLine("");

            var t3 = canvas.Http.Get("courses/50960/pages?sort=title");
            t3.Wait();
            int count = t3.Result.Split(",").Length;

            Console.WriteLine("API Call: courses/50960/pages?sort=title");
            Console.WriteLine("Number of pages retrieved: " + count.ToString());

        }
    }
}
