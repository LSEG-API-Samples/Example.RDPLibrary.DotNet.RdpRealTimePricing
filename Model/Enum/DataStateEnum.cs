namespace RdpRealTimePricing.Model.Enum
{
    public enum DataStateEnum
    {
        NoChange, //There is not change in the current state of the data.
        Ok, //All data associated with the stream is healthy and current.
        Suspect //Some or all of the data on a stream is out-of-date (or that it cannot be confirmed as current, e.g., the service is down). If an application does not allow suspect data, a stream might change from Open to Closed or ClosedRecover as a result.
    };
}