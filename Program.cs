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
            var myCourses = new List<Task<CoursesObject>>();

            // each of these returns tasks
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

            // wait until everything finishes before doing the next code
            CoursesObject[] results = await Task.WhenAll(myCourses.ToArray());

            Console.WriteLine("");

            canvas.http.stopTimer();
            
            // this shows that things come back in the correct (expected) order
            // Array.ForEach(results, x => Console.WriteLine(x.ToString()));
        }
    }
}
