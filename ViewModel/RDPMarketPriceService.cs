using Refinitiv.DataPlatform.Delivery.Stream;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refinitiv.DataPlatform.Delivery;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using RdpRealTimePricing.Events;
using RdpRealTimePricing.Extensions;
using RdpRealTimePricing.Model.Enum;
using RdpRealTimePricing.Model.MarketData;
using RdpRealTimePricing.Model.Data;


namespace RdpRealTimePricing.ViewModel
{
    public class RdpMarketPriceService
    {
        private readonly ConcurrentDictionary<string, IStream> _streamCache;
        public async Task OpenItemAsync(string item, Refinitiv.DataPlatform.Core.ISession RdpSession,Type modelType)
        {
           var fieldnameList = modelType.GetProperties()
                    .SelectMany(p => p.GetCustomAttributes(typeof(JsonPropertyAttribute))
                        .Cast<JsonPropertyAttribute>())
                    .Select(prop => prop.PropertyName)
                    .ToArray();

            if (RdpSession != null)
                await Task.Run(() =>
                {
                    ItemStream.Params itemParams;
                    if (!fieldnameList.Any())
                    {
                        // First, prepare our item stream details including the fields of interest and where to capture events...
                        itemParams = new ItemStream.Params().Session(RdpSession)
                            .OnRefresh(processOnRefresh)
                            .OnUpdate(processOnUpdate)
                            .OnStatus(processOnStatus);
                    }
                    else
                    {
                        // First, prepare our item stream details including the fields of interest and where to capture events...
                        itemParams = new ItemStream.Params().Session(RdpSession)
                            .WithFields(fieldnameList)
                            .OnRefresh(processOnRefresh)
                            .OnUpdate(processOnUpdate)
                            .OnStatus(processOnStatus);
                    }

                    var stream = DeliveryFactory.CreateStream(itemParams.Name(item));
                    if (_streamCache.TryAdd(item, stream))
                    {
                        stream.OpenAsync();
                    }
                    else
                    {
                        var msg = $"Unable to open new stream for item {item}.";
                        RaiseOnError(msg);
                    }
                });
            else
                throw new ArgumentNullException("RDP Session is null.");
        }

        public RdpMarketPriceService()
        {
            _streamCache = new ConcurrentDictionary<string, IStream>();
        }
        public Task CloseItemStreamAsync(string item)
        {
            return Task.WhenAll(Task.Run(() =>
            {
                if (_streamCache.TryGetValue(item,out var stream))
                {
                    stream.CloseAsync();
                    if (_streamCache.TryRemove(item, out var removedItem))
                    {
                        removedItem = null;
                    }
                }
            }));
        }

        public IMarketData FieldListToMarketPriceData<T>(Dictionary<string, dynamic> data)
        {
            return typeof(T) != typeof(MarketPriceData) ? null : GenerateMarketPriceData(data);
        }

        private MarketPriceData GenerateMarketPriceData(Dictionary<string, dynamic> data)
        {
            var fxRateData = data.ToObject<MarketPriceData>();
            fxRateData.BID_Status = MarketPriceData.PriceChangeEnum.NoChange;

            if (fxRateData.OPEN_BID == null)
            {
                if (fxRateData.OPEN_PRC != null)
                {
                    if (fxRateData.BID > fxRateData.OPEN_PRC)
                        fxRateData.BID_Status = MarketPriceData.PriceChangeEnum.Up;
                    else if (fxRateData.BID < fxRateData.OPEN_PRC)
                        fxRateData.BID_Status = MarketPriceData.PriceChangeEnum.Down;
                }
            }
            else
            {
                if (fxRateData.BID > fxRateData.OPEN_BID)
                    fxRateData.BID_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (fxRateData.BID < fxRateData.OPEN_BID)
                    fxRateData.BID_Status = MarketPriceData.PriceChangeEnum.Down;
            }

            fxRateData.ASK_Status = MarketPriceData.PriceChangeEnum.NoChange;
            if (fxRateData.OPEN_ASK == null)
            {
                if (fxRateData.OPEN_PRC != null)
                {
                    if (fxRateData.ASK > fxRateData.OPEN_PRC)
                        fxRateData.ASK_Status = MarketPriceData.PriceChangeEnum.Up;
                    else if (fxRateData.ASK < fxRateData.OPEN_PRC)
                        fxRateData.ASK_Status = MarketPriceData.PriceChangeEnum.Down;
                }
            }
            else
            {
                if (fxRateData.ASK > fxRateData.OPEN_BID)
                    fxRateData.ASK_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (fxRateData.ASK < fxRateData.OPEN_ASK)
                    fxRateData.ASK_Status = MarketPriceData.PriceChangeEnum.Down;
            }

            fxRateData.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.NoChange;
            if (fxRateData.TRDPRC_1 > fxRateData.OPEN_PRC)
                fxRateData.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Up;
            else if (fxRateData.TRDPRC_1 < fxRateData.OPEN_PRC)
                fxRateData.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Down;
            return fxRateData;
        }

        public static IMarketData UpdateFieldListWithFieldUpdate<T>(Dictionary<string,dynamic> source,IMarketData destination)
        {
       
            if (typeof(T) != typeof(MarketPriceData)) return destination;

            var tempObj = (MarketPriceData)destination;
            if (source.ContainsKey("DSPLY_NAME"))
                tempObj.DSPLY_NAME = (string)source["DSPLY_NAME"];

            if (source.ContainsKey("BID"))
            {
                if (source["BID"] > tempObj.BID)
                    tempObj.BID_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (source["BID"] < tempObj.BID)
                    tempObj.BID_Status = MarketPriceData.PriceChangeEnum.Down;

                tempObj.BID = source["BID"];
            }

            if (source.ContainsKey("ASK"))
            {
                if (source["ASK"] > tempObj.ASK)
                    tempObj.ASK_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (source["ASK"] < tempObj.ASK)
                    tempObj.ASK_Status = MarketPriceData.PriceChangeEnum.Down;

                tempObj.ASK = source["ASK"];
            }

            if (source.ContainsKey("HIGH_1"))
                tempObj.HIGH_1 = source["HIGH_1"];

            if (source.ContainsKey("TRDPRC_1"))
            {
                if (source["TRDPRC_1"] > tempObj.TRDPRC_1)
                    tempObj.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (source["TRDPRC_1"] < tempObj.TRDPRC_1)
                    tempObj.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Down;

                tempObj.TRDPRC_1 = source["TRDPRC_1"];
            }

            if (source.ContainsKey("TRDVOL_1"))
                tempObj.TRDVOL_1 = source["TRDVOL_1"];

            if (source.ContainsKey("PCTCHNG"))
                tempObj.PCTCHNG = source["PCTCHNG"];

            if (source.ContainsKey("OPEN_PRC"))
                tempObj.OPEN_PRC = source["OPEN_PRC"];

            if (source.ContainsKey("HST_CLOSE"))
                tempObj.HST_CLOSE = source["HST_CLOSE"];

            if (source.ContainsKey("CURRENCY"))
                tempObj.CURRENCY = source["CURRENCY"];

            return (MarketPriceData)tempObj;
        }
        #region ItemEventProcessing
        private void processOnRefresh(IStream s, JObject msg)
       {
           var refreshMsg = msg.ToObject<MarketPriceRefreshMessage>();
           RaiseOnMessage(MessageTypeEnum.Refresh, refreshMsg);
       }
       private void processOnUpdate(IStream s, JObject msg)
       {
           var updateMsg = msg.ToObject<MarketPriceUpdateMessage>();
           RaiseOnMessage(MessageTypeEnum.Update, updateMsg);
       }
       private void processOnStatus(IStream s, JObject msg)
       {
           var statusMsg = msg.ToObject<StatusMessage>();
           var itemName = statusMsg.Key.Name.FirstOrDefault();
           if (statusMsg.State.Stream == StreamStateEnum.Closed ||
               statusMsg.State.Stream == StreamStateEnum.ClosedRecover)
               _streamCache.TryRemove(itemName, out var temp);
           RaiseOnMessage(MessageTypeEnum.Status, statusMsg);

       }
        #endregion
        // Event Handler
        #region EventHandler
        public event EventHandler<OnErrorEventArgs> OnErrorEvents;
       public event EventHandler<OnResponseMessageEventArgs> OnResponeMessageEvents;
       protected void RaiseOnMessage(MessageTypeEnum msgtype, IMessage message)
       {
           var messageEvent = new OnResponseMessageEventArgs() { MessageType = msgtype, RespMessage = message };
           OnRespMessage(messageEvent);
       }
       protected virtual void OnRespMessage(OnResponseMessageEventArgs e)
       {
           var handler = OnResponeMessageEvents;
           handler?.Invoke(this, e);
       }
       protected void RaiseOnError(string message)
       {
           var errorEvent = new OnErrorEventArgs() { Message = message };
           OnError(errorEvent);
       }
       protected virtual void OnError(OnErrorEventArgs e)
       {
           var handler = OnErrorEvents;
           handler?.Invoke(this, e);
       }
        #endregion EventHandler
    }
}
