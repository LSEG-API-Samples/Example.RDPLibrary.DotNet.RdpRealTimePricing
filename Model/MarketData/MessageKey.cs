using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RdpRealTimePricing.Converter;
using RdpRealTimePricing.Model.Enum;

namespace RdpRealTimePricing.Model.MarketData
{
    public class MessageKey
    {
        [Newtonsoft.Json.JsonProperty("Elements", DefaultValueHandling = DefaultValueHandling.Ignore,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public IDictionary<string, object> Elements { get; set; }
        [Newtonsoft.Json.JsonProperty("Filter", DefaultValueHandling = DefaultValueHandling.Ignore,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public IList<string> Filter { get; set; }
        [Newtonsoft.Json.JsonProperty("Identifier", DefaultValueHandling = DefaultValueHandling.Ignore,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public int? Identifier { get; set; }

        [Newtonsoft.Json.JsonProperty("Name", DefaultValueHandling = DefaultValueHandling.Ignore,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(SingleOrListElementConverter<string>))]
        public List<string> Name { get; set; }
        [Newtonsoft.Json.JsonProperty("NameType", DefaultValueHandling = DefaultValueHandling.Ignore,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public NameTypeEnum? NameType { get; set; }
        [Newtonsoft.Json.JsonProperty("Service", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public string Service { get; set; }
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public static MessageKey FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<MessageKey>(data);
        }
    };
}