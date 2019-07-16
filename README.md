# Canvas API Wrapper in C#

```c#
using CanvasAPIWrapper;

var myhttp = new HttpClient();
Wrapper canvas = new Wrapper(myhttp);

CoursesObject MyCourse = await canvas.Courses.Show("61116");
CoursesObject MyCourseWithParameters = await canvas.Courses.Show("61116", "?include[]=term&include[]=syllabus_body");

var sectionparams = new CoursesParametersObject();
sectionparams.include.public_description = true;
sectionparams.include.account = true;
sectionparams.include.concluded = true;
sectionparams.include.observed_users = true;
sectionparams.include.syllabus_body = true;
sectionparams.include.teachers = true;
sectionparams.teacher_limit = 12;

CoursesObject MyCourseWithParametersFromObject = await canvas.Courses.Show("61116", sectionparams.GetParameterString());

Console.WriteLine(MyCourseWithParameters.SyllabusBody);
```
Make sure your canvas API token is set in the command line!