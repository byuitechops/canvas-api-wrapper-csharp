# Canvas API Wrapper in C#
Make sure your canvas API token is set in the command line!

The domain and "/api/v1/" are taken care of for you, you only need to add the endpoint, or choose the sub-class that matches.

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

// then you can parse the json to your own POCO
// or you can just work with the string
```

## Supported API Calls
- Show : courses/ + id (+ parameters)
- ListUsersInCourse : courses/ + id + /users (+ parameters)
- SubscriptionsIndex : courses/ + id + /blueprint_subscriptions

All other GET calls are supported but you have to type the full api call and handle the return type yourself.