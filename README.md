# Canvas API Wrapper in C#

```c#
using CanvasAPIWrapper;

var myhttp = new HttpClient();
Wrapper canvas = new Wrapper(myhttp);

var MyCourse = await canvas.Courses.Show("61116");
var MyCourseWithParameters = await canvas.Courses.Show("61116", "?include[]=term&include[]=syllabus_body");
var MyCourseWithParameters = await canvas.Courses.Show("61116", "(include[]) term, syllabus_body (teacher_limit) 5");
```
Make sure your canvas API token is set in the command line!