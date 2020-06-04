using RdpRealTimePricing.Model.Enum;

namespace RdpRealTimePricing.Model.MarketData
{
    public abstract class IMessage
    {
        public abstract DomainEnum? Domain { get; set; }
        public abstract int? ID { get; set; }
        public abstract MessageTypeEnum? MsgType { get; set; }
    }
}