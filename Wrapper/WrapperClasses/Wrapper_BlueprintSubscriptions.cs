using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanvasAPIWrapper
{
    partial class Wrapper
    {
        public class InternalBlueprintSubscriptions
        {
            public Wrapper wrapper;
            public InternalBlueprintSubscriptions(Wrapper w)
            {
                wrapper = w;
            }

            public Task<string> SubscriptionsIndex(string id) 
            { 
                return wrapper.Http.Get<string>("courses/" + id + "/blueprint_subscriptions"); 
            }
        }
    }
}