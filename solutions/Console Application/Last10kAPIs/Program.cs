using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Last10kAPIs
{
    class Program
    {

        private static List<string> data;
        private static List<string> datalist;

        public class inventoryturnover
        {
            [JsonProperty(PropertyName = "Name")]
            public string name { get; set; }
            [JsonProperty(PropertyName = "Historical")]
            public Dictionary<DateTime, float> historical { get; set; }
            [JsonProperty(PropertyName = "Recent")]
            public Dictionary<string, float> recent { get; set; }

        }

        public class company 
        {
            [JsonProperty(PropertyName = "Company")]
            public string name { get; set; }
        }
        public class returnonassets
        {
            [JsonProperty(PropertyName = "Name")]
            public string name { get; set; }
            [JsonProperty(PropertyName = "Historical")]
            public Dictionary<DateTime, float> historical { get; set; }
            [JsonProperty(PropertyName = "Recent")]
            public Dictionary<string, float> recent { get; set; }
        }

        public class returnonequity
        {
            [JsonProperty(PropertyName = "Name")]
            public string name { get; set; }
            [JsonProperty(PropertyName = "Historical")]
            public Dictionary<DateTime, float> historical { get; set; }
            [JsonProperty(PropertyName = "Recent")]
            public Dictionary<string, float> recent { get; set; }
        }

        public class ebtmargin
        {
            [JsonProperty(PropertyName = "Name")]
            public string name { get; set; }
            [JsonProperty(PropertyName = "Historical")]
            public Dictionary<DateTime, float> historical { get; set; }
            [JsonProperty(PropertyName = "Recent")]
            public Dictionary<string, float> recent { get; set; }
        }

        public class Ratios
        {
            [JsonProperty(PropertyName = "InventoryTurnover")]
            public inventoryturnover inventory { get; set; }
            [JsonProperty(PropertyName = "ReturnOnAssets")]
            public returnonassets assets { get; set; }
            [JsonProperty(PropertyName = "ReturnOnEquity")]
            public returnonequity equity { get; set; }
            [JsonProperty(PropertyName = "EBTMargin")]
            public ebtmargin ebt { get; set; }
        }

        public class BalanceSheets
        {
            //currently having it output all balancesheet data will specify what is needed in next git push
        }


        public class CashFlows
        {
            //same as balancesheet public class. specific cash flow will be coming soon for global variable usage
        }

        public class Income
        {
            //same as balancesheet and cash flow public class. specific income data will be coming soon for global variable usage
        }

        static void Main(string[] args)
        {
            Task T = new Task(GetRatio);
            T.Start();
            Console.WriteLine("Financial Ratios for Return on Assets, Return on Equity, EBITDA Margin, and Inventory Turnover ");
            Console.ReadLine();

            Task T1 = new Task(GetBalanceSheet);
            T1.Start();
            Console.WriteLine("Balance Sheet");
            Console.ReadLine();

            Task T2 = new Task(GetCashFlow);
            T2.Start();
            Console.WriteLine("Cash Flow");
            Console.ReadLine();

            Task T3 = new Task(GetIncome);
            T3.Start();
            Console.WriteLine("Income Statement");
            Console.ReadLine();
        }


        static async void GetRatio()
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                HttpResponseMessage response = await client.GetAsync("https://services.last10k.com/v1/company/AAPL/ratios?10-Q");

                response.EnsureSuccessStatusCode();

                Ratios R = new Ratios();
                inventoryturnover I = new inventoryturnover();
                returnonassets A = new returnonassets();
                returnonequity E = new returnonequity();
                ebtmargin M = new ebtmargin();




                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);


                    var articlesIT = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    I.name = articlesIT.InventoryTurnover.Name;
                    JObject historicalIT = articlesIT.InventoryTurnover.Recent;

                    var articlesROA = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    A.name = articlesROA.ReturnOnAssets.Name;
                    JObject historicalROA = articlesROA.ReturnOnAssets.Recent;

                    var articlesROE = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    E.name = articlesROE.ReturnOnEquity.Name;
                    JObject historicalROE = articlesROE.ReturnOnEquity.Recent;

                    var articlesEBT = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    M.name = articlesEBT.EBTMargin.Name;
                    JObject historicalEBT = articlesEBT.EBTMargin.Recent;

                    Console.WriteLine(I.name);
                    Console.WriteLine(historicalIT);
                    Console.WriteLine(historicalIT["TTM"]);

                    Console.WriteLine(A.name);
                    Console.WriteLine(historicalROA);
                    Console.WriteLine(historicalROA["TTM"]);

                    Console.WriteLine(E.name);
                    Console.WriteLine(historicalROE);
                    Console.WriteLine(historicalROE["TTM"]);

                    Console.WriteLine(M.name);
                    Console.WriteLine(historicalEBT);
                    Console.WriteLine(historicalEBT["TTM"]);


                    //foreach (KeyValuePair<DateTime, float> kvp in Dictionary) ;
                    //Console.WriteLine(articles.InventoryTurnover.Historical[0]);
                    //data.Add(Emp.datasymbol);


                }

            }
        }

        static async void GetBalanceSheet()
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                HttpResponseMessage response = await client.GetAsync("https://services.last10k.com/v1/company/AAPL/balancesheet?10-Q");

                response.EnsureSuccessStatusCode();
                BalanceSheets B = new BalanceSheets();


                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);



                }

            }




        }

        static async void GetCashFlow()
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                HttpResponseMessage response = await client.GetAsync("https://services.last10k.com/v1/company/AAPL/cashflows?10-Q");

                response.EnsureSuccessStatusCode();
                CashFlows C = new CashFlows();


                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);



                }

            }


        }
        static async void GetIncome()
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                HttpResponseMessage response = await client.GetAsync("https://services.last10k.com/v1/company/AAPL/income?10-Q");

                response.EnsureSuccessStatusCode();
                Income Ic = new Income();


                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);



                }

            }


        }
    }
}


