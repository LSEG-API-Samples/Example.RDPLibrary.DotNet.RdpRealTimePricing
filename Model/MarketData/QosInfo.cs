using Newtonsoft.Json;
using RdpRealTimePricing.Model.Enum;

namespace RdpRealTimePricing.Model.MarketData
{
    public class QosInfo
    {
        [Newtonsoft.Json.JsonProperty("Dynamic", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Dynamic { get; set; }
        [Newtonsoft.Json.JsonProperty("Rate", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public RateEnum? Rate { get; set; }
        [Newtonsoft.Json.JsonProperty("RateInfo", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? RateInfo { get; set; }
        [Newtonsoft.Json.JsonProperty("Timeliness", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public TimelinessEnum? Timeliness { get; set; }
        [Newtonsoft.Json.JsonProperty("TimeInfo", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? TimeInfo { get; set; }
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public static QosInfo FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<QosInfo>(data);
        }
    };
}