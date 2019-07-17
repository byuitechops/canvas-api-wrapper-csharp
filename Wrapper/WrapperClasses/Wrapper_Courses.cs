using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanvasAPIWrapper
{
    partial class Wrapper
    {
        public class InternalCourses
        {
            public Wrapper parent;
            public InternalCourses(Wrapper w)
            {
                parent = w;
            }

            public async Task<CourseObject> Show(string id, string parameters = "")
            {
                string json = await parent.Http.Get("courses/" + id + parameters);
                return JsonConvert.DeserializeObject<CourseObject>(json);
            }

            public async Task<List<UserObject>> ListUsersInCourse(string id, string parameters = "")
            {
                string json = await parent.Http.Get("courses/" + id + "/users" + parameters);
                return JsonConvert.DeserializeObject<List<UserObject>>(json);
            }
        }
    }
}