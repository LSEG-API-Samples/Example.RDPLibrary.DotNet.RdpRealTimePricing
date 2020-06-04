using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RdpRealTimePricing.Model.Enum;

namespace RdpRealTimePricing.Model.MarketData
{
    public class MarketPriceRefreshMessage : IMessage
    {
        [Newtonsoft.Json.JsonProperty("Domain", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public override DomainEnum? Domain { get; set; }

        [Newtonsoft.Json.JsonProperty("ID", DefaultValueHandling = DefaultValueHandling.Ignore,
            Required = Newtonsoft.Json.Required.AllowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public override int? ID { get; set; }

        [Newtonsoft.Json.JsonProperty("Type", DefaultValueHandling = DefaultValueHandling.Ignore,
            Required = Newtonsoft.Json.Required.AllowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public override MessageTypeEnum? MsgType { get; set; }

        [Newtonsoft.Json.JsonProperty("ClearCache", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? ClearCache { get; set; }

        [Newtonsoft.Json.JsonProperty("Complete", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Complete { get; set; }

        [Newtonsoft.Json.JsonProperty("ExtHdr", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ExtHdr { get; set; }

        [Newtonsoft.Json.JsonProperty("Key", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public MessageKey Key { get; set; }

        [Newtonsoft.Json.JsonProperty("PartNumber", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int PartNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("PermData", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PermData { get; set; }

        [Newtonsoft.Json.JsonProperty("Private", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Private { get; set; }

        [Newtonsoft.Json.JsonProperty("QoS", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public QosInfo QoS { get; set; }

        [Newtonsoft.Json.JsonProperty("SeqNumber", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int SeqNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("Solicited", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Solicited { get; set; }

        [Newtonsoft.Json.JsonProperty("State", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public StateInfo State { get; set; }
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }
        [Newtonsoft.Json.JsonProperty("Fields", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Dictionary<string, dynamic> Fields { get; set; }
        
        public static MarketPriceRefreshMessage FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<MarketPriceRefreshMessage>(data);
        }

    };
}