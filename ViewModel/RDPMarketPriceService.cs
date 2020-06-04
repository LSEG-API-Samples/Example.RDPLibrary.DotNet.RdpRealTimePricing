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

        public static MarketPriceData FieldListToMarketPriceData(Dictionary<string, dynamic> data)
        {
            var fxrateData = data.ToObject<MarketPriceData>();
            fxrateData.BID_Status = MarketPriceData.PriceChangeEnum.NoChange;

            if (fxrateData.OPEN_BID == null)
            {
                if (fxrateData.OPEN_PRC != null)
                {
                    if (fxrateData.BID > fxrateData.OPEN_PRC)
                        fxrateData.BID_Status = MarketPriceData.PriceChangeEnum.Up;
                    else if (fxrateData.BID < fxrateData.OPEN_PRC)
                        fxrateData.BID_Status = MarketPriceData.PriceChangeEnum.Down;
                }
            }
            else
            {
                if (fxrateData.BID > fxrateData.OPEN_BID)
                    fxrateData.BID_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (fxrateData.BID < fxrateData.OPEN_BID)
                    fxrateData.BID_Status = MarketPriceData.PriceChangeEnum.Down;
            }

            fxrateData.ASK_Status = MarketPriceData.PriceChangeEnum.NoChange;
            if (fxrateData.OPEN_ASK == null)
            {
                if (fxrateData.OPEN_PRC != null)
                {
                    if (fxrateData.ASK > fxrateData.OPEN_PRC)
                        fxrateData.ASK_Status = MarketPriceData.PriceChangeEnum.Up;
                    else if (fxrateData.ASK < fxrateData.OPEN_PRC)
                        fxrateData.ASK_Status = MarketPriceData.PriceChangeEnum.Down;
                }
            }
            else
            {
                if (fxrateData.ASK > fxrateData.OPEN_BID)
                    fxrateData.ASK_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (fxrateData.ASK < fxrateData.OPEN_ASK)
                    fxrateData.ASK_Status = MarketPriceData.PriceChangeEnum.Down;
            }

            fxrateData.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.NoChange;
            if (fxrateData.TRDPRC_1 > fxrateData.OPEN_PRC)
                fxrateData.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Up;
            else if (fxrateData.TRDPRC_1 < fxrateData.OPEN_PRC)
                fxrateData.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Down;
            return fxrateData;
        }

        public static void UpdateFieldListWithFieldUpdate(Dictionary<string,dynamic> source,ref MarketPriceData destination)
        {
            if(source.ContainsKey("DSPLY_NAME"))
               destination.DSPLY_NAME = (string)source["DSPLY_NAME"];

            if (source.ContainsKey("BID"))
            {
                if (source["BID"] > destination.BID)
                    destination.BID_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (source["BID"] < destination.BID)
                    destination.BID_Status = MarketPriceData.PriceChangeEnum.Down;

                destination.BID = source["BID"];
            }

            if (source.ContainsKey("ASK"))
            {
                if (source["ASK"] > destination.ASK)
                    destination.ASK_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (source["ASK"] < destination.ASK)
                    destination.ASK_Status = MarketPriceData.PriceChangeEnum.Down;

                destination.ASK = source["ASK"];
            }

            if (source.ContainsKey("HIGH_1"))
                destination.HIGH_1 = source["HIGH_1"];

            if (source.ContainsKey("TRDPRC_1"))
            {
                if (source["TRDPRC_1"] > destination.TRDPRC_1)
                    destination.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Up;
                else if (source["TRDPRC_1"] < destination.TRDPRC_1)
                    destination.TRDPRC_1_Status = MarketPriceData.PriceChangeEnum.Down;
                        
                destination.TRDPRC_1 = source["TRDPRC_1"];
            }

            if (source.ContainsKey("TRDVOL_1"))
                destination.TRDVOL_1 = source["TRDVOL_1"];

            if (source.ContainsKey("PCTCHNG"))
                destination.PCTCHNG = source["PCTCHNG"];

            if (source.ContainsKey("OPEN_PRC"))
                destination.OPEN_PRC = source["OPEN_PRC"];

            if (source.ContainsKey("HST_CLOSE"))
                destination.HST_CLOSE = source["HST_CLOSE"];

            if (source.ContainsKey("CURRENCY"))
                destination.CURRENCY = source["CURRENCY"];
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
