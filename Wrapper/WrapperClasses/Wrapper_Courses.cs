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
            public Wrapper wrapper;
            public InternalCourses(Wrapper w)
            {
                wrapper = w;
            }

            // List_your_courses
            public Task<List<CourseObject>> Index(string parameters = "")
            {
                return wrapper.Http.Get<List<CourseObject>>("courses" + parameters);
            }

            // List_courses_for_a_user
            public  Task<List<CourseObject>> UserIndex(string userId, string parameters = "")
            {
                return wrapper.Http.Get<List<CourseObject>>("users/" + userId + "/courses" + parameters);
            }

            // List_users_in_course
            public Task<List<UserObject>> Users(string courseId, string parameters = "") 
            { 
                return wrapper.Http.Get<List<UserObject>>("courses/" + courseId + "/users" + parameters); 
            }

            // List_recently_logged_in_students
            public Task<List<UserObject>> RecentStudents(string courseId)
            {
                return wrapper.Http.Get<List<UserObject>>("courses/" + courseId + "/recent_students");
            }

            // Get_single_user
            public Task<UserObject> User(string courseId, string userId, string parameters = "")
            {
                return wrapper.Http.Get<UserObject>("courses/" + courseId + "/users/" + userId + parameters);
            }

            // Course_activity_stream
            public Task<string> ActivityStream(string courseId)
            {
                return wrapper.Http.Get("courses/" + courseId + "/activity_stream");
            }

            // Course_activity_stream_summary
            public Task<string> ActivityStreamSummary(string courseId)
            {
                return wrapper.Http.Get("courses/" + courseId + "/activity_stream/summary");
            }

            // Course_TODO_items
            public Task<string> TodoItems(string courseId)
            {
                return wrapper.Http.Get("courses/" + courseId + "/todo");
            }

            // Get_course_settings
            public Task<CourseSettingsObject> ApiSettings(string courseId)
            {
                return wrapper.Http.Get<CourseSettingsObject>("courses/" + courseId + "/settings");
            }

            // Get_a_single_course
            public Task<CourseObject> Show(string courseId, string parameters = "") 
            { 
                return wrapper.Http.Get<CourseObject>("courses/" + courseId + parameters); 
            }

            // Get_effective_due_dates
            public Task<string> EffectiveDueDates(string courseId, string parameters = "") 
            { 
                return wrapper.Http.Get("courses/" + courseId + "/effective_due_dates" + parameters); 
            }

            // Permissions
            public Task<string> Permissions(string courseId) 
            { 
                return wrapper.Http.Get("courses/" + courseId + "/permissions");
            }
        }
    }
}