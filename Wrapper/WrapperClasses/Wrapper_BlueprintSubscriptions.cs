using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanvasAPIWrapper
{
    partial class Wrapper
    {
        public class InternalBlueprintSubscriptions
        {
            private Wrapper Canvas;
            public InternalBlueprintSubscriptions(Wrapper w)
            {
                Canvas = w;
            }

            public Task<BlueprintCourseObject> SubscriptionsIndex(string id) 
            { 
                return Canvas.Http.Get<BlueprintCourseObject>("courses/" + id + "/blueprint_subscriptions"); 
            }
        }
    }
}