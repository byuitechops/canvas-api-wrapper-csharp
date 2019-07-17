using System;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanvasAPIWrapper
{
    partial class Wrapper
    {
        public HTTPHandler Http;

        // add new internal classes here
        public InternalCourses Courses;

        public Wrapper()
        {
            Http = new HTTPHandler(new HttpClient());

            // internal classes for the Canvas.Course.Get() syntax            
            Courses = new InternalCourses(this);
        }
    }
}
