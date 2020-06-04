using Newtonsoft.Json;

namespace RdpRealTimePricing.Model.Data
{
    public class CompanyBusinessSummary
    {
        [JsonProperty("businessSummary")]
        public string businessSummary { get; set; }
    }
}