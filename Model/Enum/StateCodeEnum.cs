namespace RdpRealTimePricing.Model.Enum
{
    public enum StateCodeEnum
    {
        AlreadyOpen, // Indicates that a stream is already open on the connection for the requested data.
        AppAuthorizationFailed, //Indicates that application authorization using the secure token has failed.
        DacsDown, //Indicates that the connection to DACS is down and users are not allowed to connect.
        Error, //Indicates an internal error from the sender.
        ExceededMaxMountsPerUser, //Indicates that the login was rejected because the user exceeded their maximum number of allowed mounts.
        FailoverCompleted, //Indicated that recovery from a failover condition has finished.
        FailoverStarted, //Indicates that the component is recovering due to a failover condition.User is notified when recovery finishes via FailoverCompleted.
        FullViewProvided, //Indicates that the full view (e.g., all available fields) is being provided, even though only a specific view was requested.
        GapDetected, //Indicates that a gap was detected between messages.
        GapFill, //Indicates that the received content is meant to fill a recognized gap.
        InvalidArgument, //Indicates that the request includes an invalid or unrecognized parameter.Specific information should be contained in Text.
        InvalidView, //Indicates that the requested view is invalid, possibly due to bad formatting.Additional information should be available in Text.
        JitConflationStarted, //Indicates that JIT conflation has started on the stream.User is notified when JIT conflation ends via RealtimeResume.
        MaxLoginsReached, //Indicates that the maximum number of logins has been reached.
        None, //Indicates that additional state code information is not required, nor present.
        NotEntitled, //Indicates that the request was denied due to permissioning.
        // Typically indicates that the requesting user does not have permission to request on the service, to receive requested data,
        // or to receive data at the requested QoS.
        NotFound, //Indicates that requested information was not found, though it might be available at a later time or through changing some parameters used in the request.
        Preempted, //Indicates the stream was preempted, possibly by caching device.Typically indicates the user has exceeded an item limit,
        //whether specific to the user or a component in the system.Relevant information should be contained in Text.
        NoBatchViewSupportInReq, //Indicates that the provider does not support batch and/or view functionality.
        NonUpdatingItem, //Indicates that a streaming request was made for non-updating data.
        NoResources, //Indicates that no resources are available to accommodate the stream.
        NotOpen, //Indicates that the stream was not opened.Additional information should be available in Text.
        RealtimeResumed, //Indicated that JIT conflation on the stream has finished.
        SourceUnknown, //Indicates that the requested service is not known though the service might be available at a later point in time.
        Timeout, //Indicates that the timeout occurred somewhere in the system while processing requested data.
        TooManyItems, //Indicates that a request cannot be processed because too many other streams are already open.
        UnableToRequestAsBatch, //Indicates that a batch request cannot be used for this request.The user can instead split the batched items into individual requests.
        UnsupportedViewType, //Indicates that the domain on which a request is made does not support the requests ViewType.
        UserAccessToAppDenied, //Indicates that the application is denied access to the system.
        UsageError, //Indicates invalid usage within the system.Specific information should be contained in Text.
        UserUnknownToPermSys //Indicates that the user is unknown to the permissioning system and is not allowed to connect.
    };
}