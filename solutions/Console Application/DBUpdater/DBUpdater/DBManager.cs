using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DBUpdater
{
    public class DBManager
    {
        public HttpClient client;
        public JArray tickerlistC;
        public List<String> tickerlist;


        public DBManager()
        {

        }

        public async Task<List<String>> FetchTickers()
        {
            client = new HttpClient();

            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/Investments";

            string content = await client.GetStringAsync(Url);
            JArray tickerlistC = JArray.Parse(content);


            if (tickerlistC.Count == 0)
            {
                Console.WriteLine("null returned");
                return null;
                //Debug.WriteLine("null returned");
            }

            else
            {
                for (var i = 0; i < tickerlistC.Count; i++)

                {
                    //Debug.WriteLine((string)portfolios[i]["username"]);

                    tickerlist.Add((string)tickerlistC[i]["tickersymbol"]);
                    Console.Write(tickerlist[i]);


                }
                //Console.Write()
                return tickerlist;

            }

        }
    }
}
