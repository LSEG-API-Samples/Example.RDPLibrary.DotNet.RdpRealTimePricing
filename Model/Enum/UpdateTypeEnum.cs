namespace RdpRealTimePricing.Model.Enum
{
    public enum UpdateTypeEnum
    {
        ClosingRun, //Closing run.
        Correction, //Correction.
        MarketDigest, //Market digest.
        Multiple, //Update event with filtering and conflation applied.
        NewsAlert, //News alert.
        OrderIndication, //Order indication.
        Quote, //Quote.
        QuotesTrade, //Quotes followed by a Trade.
        Trade, //Trade.
        Unspecified, //Unspecified update event.
        Verify, //Fields may have changed.
        VolumeAlert //Volume alert.
    };
}