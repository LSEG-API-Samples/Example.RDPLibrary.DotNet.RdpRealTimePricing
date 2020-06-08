using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace RdpRealTimePricing.Model.Data
{
    public class MarketPriceData: IMarketData
    {
        public enum PriceChangeEnum
        {
            NoChange=0,
            Up=1,
            Down=2
        };
        public bool IsSelected{ get; set; }
        public string RicName { get; set; }
        public int? StreamId { get; set; }

        [Newtonsoft.Json.JsonProperty("DSPLY_NAME", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public string DSPLY_NAME { get; set; }

        [Newtonsoft.Json.JsonProperty("TRDPRC_1", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? TRDPRC_1
        {
            get;
            set;
        }
        public PriceChangeEnum TRDPRC_1_Status { get; set; }

        [Newtonsoft.Json.JsonProperty("TRDVOL_1", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public long? TRDVOL_1 { get; set; }

        [Newtonsoft.Json.JsonProperty("PCTCHNG", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? PCTCHNG { get; set; }

        [Newtonsoft.Json.JsonProperty("OPEN_PRC", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? OPEN_PRC { get; set; }

        [Newtonsoft.Json.JsonProperty("HST_CLOSE", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? HST_CLOSE { get; set; }

        [Newtonsoft.Json.JsonProperty("BID", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? BID { get; set; }
        [Newtonsoft.Json.JsonProperty("OPEN_BID", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? OPEN_BID { get; set; }
        public PriceChangeEnum BID_Status { get; set; }

        [Newtonsoft.Json.JsonProperty("BIDSIZE", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public long? BIDSIZE { get; set; }

        [Newtonsoft.Json.JsonProperty("ASK", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? ASK { get; set; }

        [Newtonsoft.Json.JsonProperty("OPEN_ASK", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? OPEN_ASK { get; set; }
        public PriceChangeEnum ASK_Status { get; set; }

        [Newtonsoft.Json.JsonProperty("ASKSIZE", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public long? ASKSIZE { get; set; }

        [Newtonsoft.Json.JsonProperty("HIGH_1", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? HIGH_1{ get; set; }
        
        [Newtonsoft.Json.JsonProperty("LOW_1", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public double? LOW_1{ get; set; }

        [Newtonsoft.Json.JsonProperty("CURRENCY", DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include)]
        public string CURRENCY { get; set; }

        [Newtonsoft.Json.JsonExtensionData]
        public IDictionary<string, JToken> _Attribute { get; set; }
    }
}