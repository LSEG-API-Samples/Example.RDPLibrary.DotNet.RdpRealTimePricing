using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RdpRealTimePricing.Model.Enum;

namespace RdpRealTimePricing.Model.MarketData
{
    public class MarketPriceUpdateMessage : IMessage
    {

        [Newtonsoft.Json.JsonProperty("ConflationInfo", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ConflationInfo ConflationInfo { get; set; }

        [Newtonsoft.Json.JsonProperty("Discardable", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Discardable { get; set; }

        [Newtonsoft.Json.JsonProperty("Domain", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public override DomainEnum? Domain { get; set; }

        [Newtonsoft.Json.JsonProperty("DoNotCache", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? DoNotCache { get; set; }

        [Newtonsoft.Json.JsonProperty("DoNotConflate", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? DoNotConflate { get; set; }

        [Newtonsoft.Json.JsonProperty("DoNotRipple", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? DoNotRipple { get; set; }

        [Newtonsoft.Json.JsonProperty("ID", DefaultValueHandling = DefaultValueHandling.Ignore,
            Required = Newtonsoft.Json.Required.AllowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public override int? ID { get; set; }

        [Newtonsoft.Json.JsonProperty("Type", DefaultValueHandling = DefaultValueHandling.Ignore,
            Required = Newtonsoft.Json.Required.AllowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public override MessageTypeEnum? MsgType { get; set; }

        [Newtonsoft.Json.JsonProperty("ExtHdr", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ExtHdr { get; set; }


        [Newtonsoft.Json.JsonProperty("PermData", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PermData { get; set; }

        [Newtonsoft.Json.JsonProperty("SeqNumber", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int SeqNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("Key", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public MessageKey Key { get; set; }

        [Newtonsoft.Json.JsonProperty("UpdateType", DefaultValueHandling = DefaultValueHandling.Ignore,
            Required = Newtonsoft.Json.Required.AllowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UpdateTypeEnum UpdateType { get; set; }
        [Newtonsoft.Json.JsonProperty("Fields", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Dictionary<string, dynamic> Fields { get; set; }
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public static MarketPriceUpdateMessage FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<MarketPriceUpdateMessage>(data);
        }
    };
}