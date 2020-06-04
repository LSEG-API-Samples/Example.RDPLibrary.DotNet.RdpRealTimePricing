using Refinitiv.DataPlatform.Core;

namespace RdpRealTimePricing.Events
{
    public class OnStateChangedEventArgs
    {
        public Session.State State { get; set; }
        public string Message { get; set; }
    };
}