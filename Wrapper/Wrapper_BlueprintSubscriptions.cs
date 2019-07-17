using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanvasAPIWrapper
{
    partial class Wrapper
    {
        public class InternalBlueprintSubscriptions
        {
            public Wrapper parent;
            public InternalBlueprintSubscriptions(Wrapper w)
            {
                parent = w;
            }

            public async Task<InternalBlueprintSubscriptions> SubscriptionsIndex(string id)
            {
                string json = await parent.Http.Get("courses/" + id + "/blueprint_subscriptions");
                return JsonConvert.DeserializeObject<InternalBlueprintSubscriptions>(json);
            }
        }
    }
}