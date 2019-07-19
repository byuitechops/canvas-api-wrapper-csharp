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
        static async Task Main(string[] args)
        {
            Wrapper canvas = new Wrapper();
            
            var url1 = "/courses/50960/pages";
            Console.WriteLine(url1);
            string result1 = await canvas.Http.Get(url1);

            System.IO.File.WriteAllText("results.json", JsonHelper.FormatJson(result1));
        }
    }
}
