using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Linq;


namespace RdpRealTimePricing.Model.Data
{
    public class AppData
    {
        public AppData()
        {
            AppMenuTxt = "Login";
            DataCache = new ConcurrentDictionary<string, MarketPriceData>();
        }
        public string AppMenuTxt { get; set; }
        public bool UseRDP { get; set; } = true;
        public string CurrentUserName { get; set; }
        public string MinWidth { get; set; } = "60%";
        public string MaxWidth { get; set; } = "95%"; 
        public ConcurrentDictionary<string, MarketPriceData> DataCache{ get; set; }
    }
}
