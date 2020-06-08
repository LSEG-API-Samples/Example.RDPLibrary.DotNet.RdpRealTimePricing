using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace RdpRealTimePricing.Model.Data
{
    public interface IMarketData
    {
        public string RicName { get; set; }
        public int? StreamId { get; set; }
        [Newtonsoft.Json.JsonExtensionData]
        IDictionary<string, JToken> _Attribute { get; set; }
    }
}