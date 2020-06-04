namespace RdpRealTimePricing.Model.Enum
{
    public enum StreamStateEnum
    {
        Closed, //Data is not available on this service and connection is not likely to become available, though the data might be available on another service or connection.
        ClosedRecover, //State is closed, however data can be recovered on this service and connection at a later time.
        NonStreaming, //The stream is closed and updated data is not delivered without a subsequent re-request.
        Open, //Data is streaming, as data changes it is sent to the stream.
        Redirected //The current stream is closed and has new identifying information.The user can issue a new request for the data using the new message key data from the redirect message.
    };
}