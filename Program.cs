using System;
using Polly;
using Refit;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace CanvasWrapperRefit
{
    class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private string Token;
        public AuthenticatedHttpClientHandler(string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            this.Token = token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            Console.WriteLine("BEFORE THE API CALL HAPPENS");

            HttpResponseMessage result = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Forbidden)
                .RetryAsync(3, onRetry: (exception, retryCount) =>
                {
                    Console.WriteLine(exception.Result.StatusCode.ToString() + retryCount.ToString());
                    System.Threading.Thread.Sleep(retryCount * 1000);
                })
                .ExecuteAsync(async () => 
                { 
                    Console.WriteLine("Hello:");
                    return await base.SendAsync(request, cancellationToken);
                });

            Console.WriteLine(result.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault());

            return result;
        }
    }

    interface CourseRestService
    {
        [Get("/api/v1/courses/{id}")]
        Task<Course> GetCourseById(string id);
    }

    interface BlueprintInfoRestService
    {
        [Get("/api/v1/courses/{id}")]
        Task<string> GetCourseById(string id);
    }

    class Wrapper
    {
        private string _token;
        private HttpClient _client;
        public CourseRestService Courses;
        public BlueprintInfoRestService BlueprintInfo;
        private void SetupRestServices()
        {
            Courses = RestService.For<CourseRestService>(_client);
            BlueprintInfo = RestService.For<BlueprintInfoRestService>(_client);
        }
        public Wrapper()
        {

            _token = Environment.GetEnvironmentVariable("CANVAS_API_TOKEN");
            var auth = new AuthenticatedHttpClientHandler(_token);
            _client = new HttpClient(auth) { BaseAddress = new Uri("https://byui.instructure.com/") };

            SetupRestServices();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var Canvas = new Wrapper();

            var t = Canvas.Courses.GetCourseById("61116");
            t.Wait();
            var CameronsCourse = t.Result;

            Console.WriteLine(CameronsCourse.Id);
        }
    }
}
