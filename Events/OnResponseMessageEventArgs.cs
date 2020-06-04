using RdpRealTimePricing.Model.Enum;
using RdpRealTimePricing.Model.MarketData;

namespace RdpRealTimePricing.Events
{
    public class OnResponseMessageEventArgs
    {
        public MessageTypeEnum MessageType { get; set; }
        public IMessage RespMessage { get; set; }
    };
}