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


    static void Main(string[] args)
    {
        Task T = new Task(FetchTickers);
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
    static async void GetBalanceSheet(){
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

}

