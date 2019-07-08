using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;

namespace CanvasAPIWrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("(Compiled Successfully)");

            // Stopwatch stopWatch = new Stopwatch();
            // stopWatch.Start();

            var myhttp = new HttpClient();
            Wrapper canvas = new Wrapper(myhttp);

            var myCourses = new List<Task<CoursesObject>>();

            // Course thing1 = await canvas.Get<Course>("courses/80");
            // var thing2 = canvas.Get<User>("user/80");
            // var thing3 = canvas.Get<Quiz>("quiz/80");

            myCourses.Add(canvas.Courses.Index("80"));
            myCourses.Add(canvas.Courses.Index("1659"));
            myCourses.Add(canvas.Courses.Index("14726"));
            myCourses.Add(canvas.Courses.Index("45440"));
            myCourses.Add(canvas.Courses.Index("54454"));
            myCourses.Add(canvas.Courses.Index("40882"));
            myCourses.Add(canvas.Courses.Index("43894"));
            myCourses.Add(canvas.Courses.Index("39918"));
            myCourses.Add(canvas.Courses.Index("39926"));
            myCourses.Add(canvas.Courses.Index("39960"));
            myCourses.Add(canvas.Courses.Index("44914"));
            myCourses.Add(canvas.Courses.Index("44282"));
            myCourses.Add(canvas.Courses.Index("44288"));
            myCourses.Add(canvas.Courses.Index("40554"));
            myCourses.Add(canvas.Courses.Index("43790"));
            myCourses.Add(canvas.Courses.Index("42710"));
            myCourses.Add(canvas.Courses.Index("41708"));
            myCourses.Add(canvas.Courses.Index("42154"));
            myCourses.Add(canvas.Courses.Index("45402"));
            myCourses.Add(canvas.Courses.Index("42772"));
            myCourses.Add(canvas.Courses.Index("43614"));
            myCourses.Add(canvas.Courses.Index("80"));
            myCourses.Add(canvas.Courses.Index("1659"));
            myCourses.Add(canvas.Courses.Index("14726"));
            myCourses.Add(canvas.Courses.Index("45440"));
            myCourses.Add(canvas.Courses.Index("54454"));
            myCourses.Add(canvas.Courses.Index("40882"));
            myCourses.Add(canvas.Courses.Index("43894"));
            myCourses.Add(canvas.Courses.Index("39918"));
            myCourses.Add(canvas.Courses.Index("39926"));
            myCourses.Add(canvas.Courses.Index("39960"));
            myCourses.Add(canvas.Courses.Index("44914"));
            myCourses.Add(canvas.Courses.Index("44282"));
            myCourses.Add(canvas.Courses.Index("44288"));
            myCourses.Add(canvas.Courses.Index("40554"));
            myCourses.Add(canvas.Courses.Index("43790"));
            myCourses.Add(canvas.Courses.Index("42710"));
            myCourses.Add(canvas.Courses.Index("41708"));
            myCourses.Add(canvas.Courses.Index("42154"));
            myCourses.Add(canvas.Courses.Index("45402"));
            myCourses.Add(canvas.Courses.Index("42772"));
            myCourses.Add(canvas.Courses.Index("43614"));
            myCourses.Add(canvas.Courses.Index("80"));
            myCourses.Add(canvas.Courses.Index("1659"));
            myCourses.Add(canvas.Courses.Index("14726"));
            myCourses.Add(canvas.Courses.Index("45440"));
            myCourses.Add(canvas.Courses.Index("54454"));
            myCourses.Add(canvas.Courses.Index("40882"));
            myCourses.Add(canvas.Courses.Index("43894"));
            myCourses.Add(canvas.Courses.Index("39918"));
            myCourses.Add(canvas.Courses.Index("39926"));
            myCourses.Add(canvas.Courses.Index("39960"));
            myCourses.Add(canvas.Courses.Index("44914"));
            myCourses.Add(canvas.Courses.Index("44282"));
            myCourses.Add(canvas.Courses.Index("44288"));
            myCourses.Add(canvas.Courses.Index("40554"));
            myCourses.Add(canvas.Courses.Index("43790"));
            myCourses.Add(canvas.Courses.Index("42710"));
            myCourses.Add(canvas.Courses.Index("41708"));
            myCourses.Add(canvas.Courses.Index("42154"));
            myCourses.Add(canvas.Courses.Index("45402"));
            myCourses.Add(canvas.Courses.Index("42772"));
            myCourses.Add(canvas.Courses.Index("43614"));
            myCourses.Add(canvas.Courses.Index("80"));
            myCourses.Add(canvas.Courses.Index("1659"));
            myCourses.Add(canvas.Courses.Index("14726"));
            myCourses.Add(canvas.Courses.Index("45440"));
            myCourses.Add(canvas.Courses.Index("54454"));
            myCourses.Add(canvas.Courses.Index("40882"));
            myCourses.Add(canvas.Courses.Index("43894"));
            myCourses.Add(canvas.Courses.Index("39918"));
            myCourses.Add(canvas.Courses.Index("39926"));
            myCourses.Add(canvas.Courses.Index("39960"));
            myCourses.Add(canvas.Courses.Index("44914"));
            myCourses.Add(canvas.Courses.Index("44282"));
            myCourses.Add(canvas.Courses.Index("44288"));
            myCourses.Add(canvas.Courses.Index("40554"));
            myCourses.Add(canvas.Courses.Index("43790"));
            myCourses.Add(canvas.Courses.Index("42710"));
            myCourses.Add(canvas.Courses.Index("41708"));
            myCourses.Add(canvas.Courses.Index("42154"));
            myCourses.Add(canvas.Courses.Index("45402"));
            myCourses.Add(canvas.Courses.Index("42772"));
            myCourses.Add(canvas.Courses.Index("43614"));
            myCourses.Add(canvas.Courses.Index("80"));
            myCourses.Add(canvas.Courses.Index("1659"));
            myCourses.Add(canvas.Courses.Index("14726"));
            myCourses.Add(canvas.Courses.Index("45440"));
            myCourses.Add(canvas.Courses.Index("54454"));
            myCourses.Add(canvas.Courses.Index("40882"));
            myCourses.Add(canvas.Courses.Index("43894"));
            myCourses.Add(canvas.Courses.Index("39918"));
            myCourses.Add(canvas.Courses.Index("39926"));
            myCourses.Add(canvas.Courses.Index("39960"));
            myCourses.Add(canvas.Courses.Index("44914"));
            myCourses.Add(canvas.Courses.Index("44282"));
            myCourses.Add(canvas.Courses.Index("44288"));
            myCourses.Add(canvas.Courses.Index("40554"));
            myCourses.Add(canvas.Courses.Index("43790"));
            myCourses.Add(canvas.Courses.Index("42710"));
            myCourses.Add(canvas.Courses.Index("41708"));
            myCourses.Add(canvas.Courses.Index("42154"));
            myCourses.Add(canvas.Courses.Index("45402"));
            myCourses.Add(canvas.Courses.Index("42772"));
            myCourses.Add(canvas.Courses.Index("43614"));

            CoursesObject[] results = await Task.WhenAll(myCourses.ToArray());

            Console.WriteLine("");

            // Console.WriteLine(thing1);

            // this shows that things come back in the correct (expected) order
            // Array.ForEach(results, x => Console.WriteLine(x.ToString()));

            // stopWatch.Stop();
            // TimeSpan ts = stopWatch.Elapsed;
            // Console.WriteLine("--");
            // Console.WriteLine("RunTime " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds));
        }
    }
}
