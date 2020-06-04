using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RdpRealTimePricing.Model.Enum;

namespace RdpRealTimePricing.Model.MarketData
{
    public class StatusMessage : IMessage
    {
        [Newtonsoft.Json.JsonProperty("ClearCache", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ClearCache { get; set; }

        [Newtonsoft.Json.JsonProperty("Domain", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public override DomainEnum? Domain { get; set; }

        [Newtonsoft.Json.JsonProperty("ExtHdr", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ExtHdr { get; set; }

        [Newtonsoft.Json.JsonProperty("ID", DefaultValueHandling = DefaultValueHandling.Ignore,
            Required = Newtonsoft.Json.Required.AllowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public override int? ID { get; set; }

        [Newtonsoft.Json.JsonProperty("Type", DefaultValueHandling = DefaultValueHandling.Ignore,
            Required = Newtonsoft.Json.Required.AllowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public override MessageTypeEnum? MsgType { get; set; }

        [Newtonsoft.Json.JsonProperty("Key", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public MessageKey Key { get; set; }

        [Newtonsoft.Json.JsonProperty("PermData", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PermData { get; set; }


        [Newtonsoft.Json.JsonProperty("State", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public StateInfo State { get; set; }

       

        [Newtonsoft.Json.JsonProperty("Qualified", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Qualified { get; set; }
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }
        public static StatusMessage FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(data);
        }
    };
}