using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanvasAPIWrapper
{
    partial class Wrapper
    {
        public class InternalDiscussionTopics
        {
            private Wrapper Canvas;
            public InternalDiscussionTopics(Wrapper w)
            {
                Canvas = w;
            }

            // Get_a_single_topic
            public Task<DiscussionTopic> Show(string courseId, string topicId, string parameters)
            { 
                return Canvas.Http.Get<DiscussionTopic>("courses/" + courseId + "/discussion_topics/" + topicId + parameters); 
            }
        }
    }
}