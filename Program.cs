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
            
            var url1 = "/courses/50960/pages";
            Console.WriteLine(url1);
            Task<string> result1 = canvas.Http.Get(url1);
            result1.Wait();
            Console.WriteLine(result1.Result);

            var courses = new List<Task<CourseObject>>();
            courses.Add(canvas.Courses.Show("40958"));
            courses.Add(canvas.Courses.Show("40960"));
            courses.Add(canvas.Courses.Show("40962"));
            courses.Add(canvas.Courses.Show("40964"));
            courses.Add(canvas.Courses.Show("40966"));

            Task<CourseObject[]> t = Task.WhenAll(courses.ToArray());
            t.Wait();

            foreach (CourseObject item in t.Result)
            {
                Console.Write(item.Id + " ");
                Console.WriteLine(item.Name);
            }

            // System.IO.File.WriteAllText("results.json", JsonHelper.FormatJson(result1));
        }
    }
}
