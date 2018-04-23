using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace FinancialRatios
{
    internal class Ratios
    {
        public string Ticker { get; set; }
        public string InventoryTurnover { get; set; } 
        public string ReturnOnAssets { get; set; }
        public string ReturnOnEquity { get; set; }
        public string EBTMargin { get; set; }
        public string AssetTurnover { get; set; }
        public string TotalAssets { get; set; }
        public string ReceivablesTurnover { get; set; }
        public string NetIncome { get; set; }
        public string EarningsPerShare { get; set; }
        public string InterestCoverage { get; set; }
        public string TotalCurrentAssets { get; set; }
        public string TaxRate { get; set; }
        public string FreeCashFlow { get; set; }
        public string Revenue { get; set; }


        public override string ToString()
        {
            return $"{Ticker,20} {InventoryTurnover} {ReturnOnAssets} {ReturnOnEquity} {EBTMargin} {AssetTurnover} {TotalAssets} {ReceivablesTurnover} {NetIncome} {EarningsPerShare} {InterestCoverage} {TotalCurrentAssets} {TaxRate} {FreeCashFlow} {Revenue}  ";

        }
    }



   

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("The following Financial Ratios are from Last10k as follows: Ticker Symbol, Inventory Turnover, ReturnOnAssets," +
                              " Return on Equity, EBTMargin, Asset Turnover, Total Assets, Receivables Turnover, Net Income, " +
                              "Earnings Per Share, Interest Coverage, Total Current Assets, Tax Rate, Free CashFlow, and Revenue");

            var webRequest = WebRequest.Create("http://web.engr.oregonstate.edu/~sinhaav/financialratios.html") as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            }



            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var ratiosAsJson = sr.ReadToEnd();
                    var ratios = JsonConvert.DeserializeObject<List<Ratios>>(ratiosAsJson);
                    ratios.ForEach(Console.WriteLine);
                }
            }

            Console.ReadLine();
        }

        static async void MakeRequest()
        {
            var client = new System.Net.Http.HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");


            var uri = "https://services.last10k.com/v1/company/AAPL/ratios?";

            var response = await client.GetAsync(uri);
        }
    }









}
