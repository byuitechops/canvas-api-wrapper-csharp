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

            public async Task<CoursesObject> Index(string id)
            {
                string json = await parent.http.getJsonAsync("courses/" + id);
                return JsonConvert.DeserializeObject<CoursesObject>(json);
            }
        }
    }
}