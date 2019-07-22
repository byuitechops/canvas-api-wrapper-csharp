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

        public Wrapper()
        {
            Http = new HTTPHandler(new HttpClient());
            SetupInternal();
        }

        public Wrapper(HttpClient client)
        {
            Http = new HTTPHandler(client);
            SetupInternal();
        }

        // NOTE: add new internal classes here
        public InternalCourses Courses;
        public InternalBlueprintSubscriptions BlueprintSubscriptions;

        // internal classes for the Canvas.Course.Get() syntax
        private void SetupInternal()
        {
            // NOTE: instantiate your new internal classes here
            Courses = new InternalCourses(this);
            BlueprintSubscriptions = new InternalBlueprintSubscriptions(this);

        }
    }
}
