using Newtonsoft.Json;

namespace RdpRealTimePricing.Model.Data
{
    public class CompanyName
    {
        [JsonProperty("companyTitle")]
        public string companyTitle{ get; set; }
        [JsonProperty("sector")]
        public string sector{ get; set; }
        [JsonProperty("market")]
        public string market { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
    }
}