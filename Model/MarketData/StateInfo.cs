using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RdpRealTimePricing.Model.Enum;

namespace RdpRealTimePricing.Model.MarketData
{
    public class StateInfo
     {
         [Newtonsoft.Json.JsonProperty("Code", DefaultValueHandling = DefaultValueHandling.Include,
             NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
         [JsonConverter(typeof(StringEnumConverter))]
         public StateCodeEnum? Code { get; set; }

         [Newtonsoft.Json.JsonProperty("Stream", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
         [JsonConverter(typeof(StringEnumConverter))]
         public StreamStateEnum? Stream { get; set; }
         [Newtonsoft.Json.JsonProperty("Data", DefaultValueHandling = DefaultValueHandling.Include,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
         [JsonConverter(typeof(StringEnumConverter))]
         public DataStateEnum? Data { get; set; }


         [Newtonsoft.Json.JsonProperty("Text", DefaultValueHandling = DefaultValueHandling.Ignore,  NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

         public string Text { get; set; }
         public string ToJson()
         {
             return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
         }

         public static StateInfo FromJson(string data)
         {
             return Newtonsoft.Json.JsonConvert.DeserializeObject<StateInfo>(data);
         }

     }

}