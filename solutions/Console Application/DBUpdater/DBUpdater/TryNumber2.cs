using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{

    public static List<string> tickers;
    public static List<string> tickerList;

    public class CompanyInfo
    {
        public string tickersymbol { get; set; }
    }

    public class inventoryturnover
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
        }


        static void Main(string[] args)
        {
            Task T = new Task(GetRatio);
            T.Start();
            Console.WriteLine("Json data........");
            Console.ReadLine();
        }
        static async void GetTickers()
        {
            List<string> tickers = new List<string>();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync("http://web.engr.oregonstate.edu/~jonesty/api.php/Investments");

                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();



                    var articles = JsonConvert.DeserializeObject<List<CompanyInfo>>(responseBody);



                    foreach (var Emp in articles)
                    {
                        Console.WriteLine("{0}\t", Emp.tickersymbol);
                        tickers.Add(Emp.tickersymbol);
                    }

                }

            }
        }
        static async void GetBalanceSheet()
        {
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync("http://web.engr.oregonstate.edu/~jonesty/api.php/Investments");

                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();



                    var articles = JsonConvert.DeserializeObject<List<CompanyInfo>>(responseBody);



                    foreach (var Emp in articles)
                    {
                        Console.WriteLine("{0}\t", Emp.tickersymbol);
                        tickers.Add(Emp.tickersymbol);
                    }

                }

            }
        }

        static async void FetchTickers()
        {
            var client = new HttpClient();
            List<string> tickerList = new List<string>();
            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/Investments";

            string content = await client.GetStringAsync(Url);
            JArray tickerlistC = JArray.Parse(content);


            if (tickerlistC.Count == 0)
            {
                Console.WriteLine("null returned");
                //return null;
                //Debug.WriteLine("null returned");
            }

            else
            {
                for (var i = 0; i < tickerlistC.Count; i++)

                {
                    //Debug.WriteLine((string)portfolios[i]["username"]);

                    tickerList.Add((string)tickerlistC[i]["tickersymbol"]);
                    Console.Write(tickerList[i]);


                }
                //Console.Write()
                //return tickerList;

            }

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



            using (HttpContent content = response.Content)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);


                var articles = JsonConvert.DeserializeObject<dynamic>(responseBody);
                I.name = articles.InventoryTurnover.Name;
                JObject historicaltemp = articles.InventoryTurnover.Recent;


                Console.WriteLine(I.name);
                Console.WriteLine(historicaltemp);
                Console.WriteLine(historicaltemp["TTM"]);

                //foreach (KeyValuePair<DateTime, float> kvp in Dictionary) ;
                //Console.WriteLine(articles.InventoryTurnover.Historical[0]);
                //data.Add(Emp.datasymbol);


            }

        }
    }


}

