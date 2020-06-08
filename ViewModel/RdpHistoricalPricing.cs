using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RdpRealTimePricing.Model.Data;
using Refinitiv.DataPlatform.Core;
using Refinitiv.DataPlatform.Delivery.Request;

namespace RdpRealTimePricing.ViewModel
{
    public class RdpHistoricalPricing
    {
        public async Task<DataTable> GetDailyInterDayPricingAsync(ISession session, string ricname, int count = 60)
        {
            var endpoint = new StringBuilder();
            endpoint.Append("https://api.refinitiv.com/data/historical-pricing/v1/views/interday-summaries/");
            endpoint.Append(ricname);
            endpoint.Append($"?interval=P1D&count={count}&fields=MID_PRICE,TRDPRC_1,TRNOVR_UNS");

            var response = await Endpoint.SendRequestAsync(session, endpoint.ToString()).ConfigureAwait(true);
            if (!response.IsSuccess) return new DataTable();
            if (response.Data.Raw == null) return new DataTable();
            var headers = response.Data?.Raw[0]?["headers"];

            var headersData = headers?.ToObject<List<TabularHeaders>>();
            var data = response.Data?.Raw[0]?["data"];
            if (data == null) return new DataTable();
            var rowData = data.ToObject<IList<IList<dynamic>>>();
            return ToDataTable(headersData, rowData);

        }

        public static bool TryGetSeriesFromDataTable(DataTable data_table,out List<MidpriceItem> midpriceItems, out List<TrdprcItem> tradepriceItems,out List<TrnovrunsData> trnovrunsDataItems)
        {
                midpriceItems = new List<MidpriceItem>();
                tradepriceItems = new List<TrdprcItem>();
                trnovrunsDataItems = new List<TrnovrunsData>();
                if (data_table.Rows.Count <= 0) return false;
                foreach (DataRow row in data_table.Rows)
                {
                    var midPrice = new MidpriceItem();
                    var tradePrice = new TrdprcItem();
                    var trnOvrunsData = new TrnovrunsData();
                    if (data_table.Columns.Contains("Date"))
                    {
                        midPrice.Date = (DateTime) row["Date"];
                        tradePrice.Date = (DateTime) row["Date"];
                        trnOvrunsData.Date = (DateTime) row["Date"];
                    }


                    if (data_table.Columns.Contains("MID_PRICE"))
                    {
                        if (!Convert.IsDBNull(row["MID_PRICE"]))
                        {
                            midPrice.MID_PRICE = (double) row["MID_PRICE"];
                            midpriceItems.Add(midPrice);
                        }
                    }

                    if (data_table.Columns.Contains("TRNOVR_UNS"))
                    {
                        if (!Convert.IsDBNull(row["TRNOVR_UNS"]))
                        {
                            trnOvrunsData.TRNOVR_UNS = (double) row["TRNOVR_UNS"];
                            trnovrunsDataItems.Add(trnOvrunsData);
                        }
                    }


                    if (!data_table.Columns.Contains("TRDPRC_1")) continue;
                    if (Convert.IsDBNull(row["TRDPRC_1"])) continue;
                    tradePrice.TRDPRC_1 = (double) row["TRDPRC_1"];
                    tradepriceItems.Add(tradePrice);
                }

                ;
                return tradepriceItems.Any() || midpriceItems.Any();

        }
        private static DataTable ToDataTable(IList<TabularHeaders> headers,IEnumerable<IList<dynamic>> data)
        {
          
            var table = new DataTable();
            foreach(var colums in headers)
            {
                switch (colums.type)
                {
                    case "string":
                        table.Columns.Add(colums.name, colums.name.ToLower() == "date" ? typeof(DateTime) : typeof(string));
                        break;
                    case "number" when colums.decimalChar!=null:
                        table.Columns.Add(colums.name, typeof(double));
                        break;
                    case "number" when colums.decimalChar == null:
                        table.Columns.Add(colums.name, typeof(long));
                        break;
                }
            }

            foreach (var rowItem in data)
            {
                var dtRow = table.NewRow();
                var i = 0;
                foreach (var colums in headers)
                {
                    dtRow[colums.name] = i == 0 ? DateTime.Parse(rowItem[i++]) ?? DBNull.Value : rowItem[i++] ?? DBNull.Value;
                }

                table.Rows.Add(dtRow);
            }
            return table;
        }
    }

   

}
