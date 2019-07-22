# Canvas API Wrapper in C#
A wrapper function for HTTP calls in Canvas that provides these key features:
- ***Rate Limiting***: Canvas limits the number of concurrent API calls that a single user can make. This checks to see if you went over your rate limit then waits, slows down the rate of API calls, and retries the API call that exceeded the rate limit.
- ***Automatic Pagination Handling***: If there are multiple pages in a GET API call then this will get all of the pages. You don't have to add any pagination parameters, it will get everything for you! (currently doesn't work with any parameters)
- ***Limited Retries***: If an individual api call returns 403 forbidden more than 10 times (customizable via Wrapper.Http.APIRetryLimit) then the call will fail. If that happens you likely have too many api calls happening all at once and should add an "await" or two between some calls (this probably won't happen)
- ***Other exceptions***: If the status code is anything other than 200 or 403, then it will fail and display the API call that failed, along with the response.
- ***POCOs***: Some POCOs have been added based on the API reference, more are planned in the future. In the meantime you'll have to work with the JSON string response *(for unsupported objects)*.
-  ***CRUD***: Currently only read *(GET)* is supported.

## Important!
Make sure your canvas API token is set in the command line!

The domain and "/api/v1/" are taken care of for you, you only need to add the endpoint, or choose the sub-class that matches.

The best way to use this system is to make similar sized API calls together, and then await. If you want to make 1000 calls that cost 0.5 points, and then 300 calls that cost 12.0 points, you should make all of one cost first, await all of those, and then make all the rest.

## Working with POCOs
```c#
using CanvasAPIWrapper;
Wrapper Canvas = new Wrapper();

CoursesObject MyCourse = await Canvas.Courses.Show("61116");
CoursesObject MyCourseWithParameters = await Canvas.Courses.Show("61116", "?include[]=term&include[]=syllabus_body");

Console.WriteLine(MyCourseWithParameters.SyllabusBody);
```

## Custom Get (Working with JSON directly)
```c#
using CanvasAPIWrapper;
Wrapper Canvas = new Wrapper();

string CourseFeatures = await Canvas.Http.Get("/courses/61116/features/enabled");

Console.WriteLine(CourseFeatures);

// then you can parse the json to your own POCO
// or you can just work with the string
```

## Running concurrent API calls
```c#
using CanvasAPIWrapper;
Wrapper Canvas = new Wrapper();

var courses = new List<Task<CourseObject>>();
courses.Add(Canvas.Courses.Show("40958"));
courses.Add(Canvas.Courses.Show("40960"));
courses.Add(Canvas.Courses.Show("40962"));
courses.Add(Canvas.Courses.Show("40964"));
courses.Add(Canvas.Courses.Show("40966"));

Task<CourseObject[]> t = Task.WhenAll(courses.ToArray());
t.Wait();

foreach (CourseObject item in t.Result)
{
    Console.Write(item.Id + " ");
    Console.WriteLine(item.Name);
}
```

## Supported API Calls 
- `Http.Get("custom_api_call")`
- Courses
- BlueprintSubscriptions.SubscriptionsIndex = courses/ + id + /blueprint_subscriptions


We are following the canvas api documentation for our naming convention: https://canvas.instructure.com/doc/api/index.html

# Improvement Ideas
- Should we add Polly.Cache? (reduce the number of calls to the server)
    - https://github.com/App-vNext/Polly/wiki/Cache
    - does this ONLY cache 200s? or will it cache the 403s?
    - we don't want it to cache 403s.
- We may be able to replace my concurrent API call limiter with Polly.Bulkhead
    - We should compare the performance of both options before deleting any code
    - Polly.Bulkhead may not have all the features we require
    - https://github.com/App-vNext/Polly/wiki/Bulkhead
        - make sure to use async!
        - expects threads


