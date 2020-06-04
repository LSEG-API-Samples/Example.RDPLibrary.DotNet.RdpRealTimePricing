using Newtonsoft.Json.Linq;
using Refinitiv.DataPlatform.Core;

namespace RdpRealTimePricing.Events
{
    public class OnSessionEventArgs
    {
        public Session.EventCode EventCode { get; set; }
        public JObject Message { get; set; }
    };
}