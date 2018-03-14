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


            public class Ratios
            {
                [JsonProperty(PropertyName = "InventoryTurnover")]
                public inventoryturnover inventory { get; set; }
            }


            static void Main(string[] args)
            {
                Task T = new Task(GetRatio);
                T.Start();
                Console.WriteLine("Financial Ratios for ROA, ROE, EBITDA Margin, Inventory Turnover, and others ");
                Console.ReadLine();
            }


            static async void GetRatio()
            {   
                var client = new HttpClient();
                var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

                using (client)
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                    HttpResponseMessage response = await client.GetAsync("https://services.last10k.com/v1/company/AAPL/ratios");

                    response.EnsureSuccessStatusCode();

                    Ratios R = new Ratios();
                    inventoryturnover I = new inventoryturnover();



                    using (HttpContent content = response.Content)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(responseBody);


                        var articles = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        I.name = articles.InventoryTurnover.Name;
                        I.historical = articles.InventoryTurnover.Historical;


                            Console.WriteLine(I.name);

                        //foreach (KeyValuePair<DateTime, float> kvp in Dictionary) ;
                        //Console.WriteLine(articles.InventoryTurnover.Historical[0]);
                            //data.Add(Emp.datasymbol);


                    }

                }
            }




        }


    }
}
