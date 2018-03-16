using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avapi.AvapiTIME_SERIES_MONTHLY_ADJUSTED;
using Avapi;
using System.Globalization;

namespace IPMAConsole
{
    public class Investment
    {
        public int numshares { get; set; }
        public string tickersymbol { get; set; }
        public int purchaseprice { get; set; }
    }
    public class Portfolio : IEquatable<Portfolio>
    {
        //[JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        public List<Investment> contents { get; set; }

        public bool Equals(Portfolio obj)
        {
            // Check if the object is a RecommendationDTO.
            // The initial null check is unnecessary as the cast will result in null
            // if obj is null to start with.
            var portfolio= obj as Portfolio;

            if (portfolio == null)
            {
                // If it is null then it is not equal to this instance.
                return false;
            }

            // Instances are considered equal if the ReferenceId matches.
            return this.name == portfolio.name;
        }

    }
    public class inventoryturnover
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        /*[JsonProperty(PropertyName = "Historical")]
        public Dictionary<DateTime, float> historical { get; set; }*/
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }

    public class company
    {
        //[JsonProperty(PropertyName = "Company")]
        public string tickersymbol { get; set; }
    }
    public class returnonassets
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        /*[JsonProperty(PropertyName = "Historical")]
        public Dictionary<DateTime, float> historical { get; set; }*/
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class returnonequity
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        /*[JsonProperty(PropertyName = "Historical")]
        public Dictionary<DateTime, float> historical { get; set; }*/
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class ebtmargin
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        /*[JsonProperty(PropertyName = "Historical")]
        public Dictionary<DateTime, float> historical { get; set; }*/
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
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

        public string companyname { get; set; }
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

    class Program
    {

        //public static List<string> tickers = null;
        //public static List<string> tickerList;



        static void Main(string[] args)
        {       //List<string> tickers = new List<string>();
            //List<string> tickerList;

            RunAsync().Wait();
            //Console.WriteLine(Program.tickers[0]);
            //await RunAsync;
            Console.WriteLine("Jobs Completed! Press Enter to Exit.");
            Console.ReadLine();
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
        static async Task<List<string>> GetTickers()
        {
            List<string> tickers = new List<string>();
            using (var client = new HttpClient())
            {
                //List<string> tickers = new List<string>();
                HttpResponseMessage response = await client.GetAsync("http://web.engr.oregonstate.edu/~jonesty/api.php/Investments");

                response.EnsureSuccessStatusCode();
                Console.WriteLine("here");

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    


                    var articles = JsonConvert.DeserializeObject<List<company>>(responseBody);



                    foreach (var Emp in articles)
                    {
                        
                        tickers.Add(Emp.tickersymbol);
                        
                       
                    }

                }

            }
            return tickers;
        }

        static async Task<Ratios> GetRatio(string ticker)
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);


            Ratios R = new Ratios();
            inventoryturnover I = new inventoryturnover();
            returnonassets A = new returnonassets();
            returnonequity E = new returnonequity();
            ebtmargin M = new ebtmargin();

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                string url = "https://services.last10k.com/v1/company/" + ticker + "/ratios?10-Q";
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();



                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);


                    var articlesIT = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    I.name = articlesIT.InventoryTurnover.Name;
                    JObject historicalIT = articlesIT.InventoryTurnover.Recent;

                    if (historicalIT["TTM"] != null)
                    {
                        I.recent = (float)historicalIT["TTM"];
                    }
                    else
                    {
                        I.recent = 0;
                    }
                    var articlesROA = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    A.name = articlesROA.ReturnOnAssets.Name;
                    JObject historicalROA = articlesROA.ReturnOnAssets.Recent;
                    //A.recent = (float)historicalROA["TTM"];

                    if (historicalROA["TTM"] != null)
                    {
                        A.recent = (float)historicalROA["TTM"];
                    }
                    else
                    {
                        A.recent = 0;
                    }

                    var articlesROE = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    E.name = articlesROE.ReturnOnEquity.Name;
                    JObject historicalROE = articlesROE.ReturnOnEquity.Recent;
                    //E.recent = (float)historicalROE["TTM"];

                    if (historicalROE["TTM"] != null)
                    {
                        E.recent = (float)historicalROE["TTM"];
                    }
                    else
                    {
                        E.recent = 0;
                    }
                    var articlesEBT = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    M.name = articlesEBT.EBTMargin.Name;
                    JObject historicalEBT = articlesEBT.EBTMargin.Recent;
                    //M.recent = (float)historicalEBT["TTM"];

                    if (historicalEBT["TTM"] != null)
                    {
                        M.recent = (float)historicalEBT["TTM"];
                    }
                    else
                    {
                        M.recent = 0;
                    }
                    /*Console.WriteLine(I.name);
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
                    Console.WriteLine(historicalEBT["TTM"]);*/

                    R.assets = A;
                    Console.WriteLine(R.assets.recent);
                    R.ebt = M;
                    Console.WriteLine(R.ebt.recent);
                    R.equity = E;
                    Console.WriteLine(R.equity.recent);
                    R.inventory = I;
                    Console.WriteLine(R.inventory.recent);
                    R.companyname = ticker;
                    Console.WriteLine(R.companyname);

                    return R;


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

        static async void SharpeRatioCalculator(string ticker)
        {
            // Creating the connection object
            List<string> tickers = new List<string>();
            using (var client = new HttpClient())
            {
                //List<string> tickers = new List<string>();
                HttpResponseMessage response = await client.GetAsync("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=" + ticker + "&apikey=7NIMRBR8G8UB7P8C");

                response.EnsureSuccessStatusCode();
                Console.WriteLine("here");

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();


                    var jObj = JObject.Parse(responseBody);
                    var metadata = jObj["Meta Data"].ToObject<Dictionary<string, string>>();
                    var timeseries = jObj["Monthly Adjusted Time Series"].ToObject<Dictionary<string, Dictionary<string, string>>>();

                    Console.WriteLine(timeseries);

                    //var articles = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    //articles.Monthly

                    //foreach (var Emp in articles)
                    //{

                    //tickers.Add(Emp.tickersymbol);


                    //}

                }

            }

        }

        static async Task<double> CalculateSharpeRatioForCompany(string ticker)


        {
            List<double> priceslastyear = new List<double>();
            List<double> monthlyreturnpercentages = new List<double>();
            // Creating the connection object
            IAvapiConnection connection = AvapiConnection.Instance;
            IAvapiResponse_TIME_SERIES_MONTHLY_ADJUSTED m_time_series_monthly_adjustedResponse;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect("7NIMRBR8G8UB7P8C");

            // Get the TIME_SERIES_MONTHLY_ADJUSTED query object
            Int_TIME_SERIES_MONTHLY_ADJUSTED time_series_monthly_adjusted =
                connection.GetQueryObject_TIME_SERIES_MONTHLY_ADJUSTED();

            // Perform the TIME_SERIES_MONTHLY_ADJUSTED request and get the result
            m_time_series_monthly_adjustedResponse = await time_series_monthly_adjusted.QueryPrimitiveAsync(
                 ticker);

            Console.WriteLine("******** STRUCTURED DATA TIME_SERIES_MONTHLY_ADJUSTED ********");
            var data = m_time_series_monthly_adjustedResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
                return 0;
            }
            else
            {

                int counter = 0;
                foreach (var timeseries in data.TimeSeries)
                {
                    //Grab 5 years worth of months data
                    if (counter < 60)
                    {
                        //add raw prices to array
                        priceslastyear.Add(double.Parse(timeseries.adjustedclose, CultureInfo.InvariantCulture.NumberFormat));

                        if(counter > 0)
                        {
                            //take pricing data and calculate percent change between each pair of months
                            //Console.Write(((priceslastyear[counter] / priceslastyear[counter - 1]) - 1) * 100);
                            monthlyreturnpercentages.Add(((priceslastyear[counter] / priceslastyear[counter - 1]) - 1)*100);
                        }
                        counter = counter + 1;
                    }
                }

                double HistAvgMonthlyReturn = 0;

                for (var i = 0; i < monthlyreturnpercentages.Count; i++)
                {
                    //Console.WriteLine(monthprice);
                    HistAvgMonthlyReturn = HistAvgMonthlyReturn + monthlyreturnpercentages[i];
                }

                
                //Calculate the average monthly return
                HistAvgMonthlyReturn = HistAvgMonthlyReturn / monthlyreturnpercentages.Count;

                //Console.WriteLine(HistAvgMonthlyReturn);


                //Here we need to calculate the Historical Monthly standard deviation based on the Std dev
                // of the monthlyreturnpercentages
                double Summation = 0;

                for (var i = 0; i<monthlyreturnpercentages.Count; i++)
                {
                    Summation = Summation + Math.Pow(monthlyreturnpercentages[i] - HistAvgMonthlyReturn, 2);
                }
                Summation = Summation / monthlyreturnpercentages.Count;

                double HistAvgMonthlyStdDev = Math.Sqrt(Summation);

                //Annualize our standard deviation
                double AnnualStdDev = HistAvgMonthlyStdDev * Math.Sqrt(12);
                double AnnualReturn = ((Math.Pow((100 + HistAvgMonthlyReturn), 12)) - 100);

                double Riskfreerate = 3;

                //Console.WriteLine(HistAvgMonthlyReturn);
                Console.WriteLine(ticker);
                Console.WriteLine(AnnualReturn);
                //Console.WriteLine(Riskfreerate);
                //Console.WriteLine(AnnualStdDev);

                //Calculate the SharpeRatio for the Company
                double SharpeRatioforCompany = (AnnualReturn - Riskfreerate) / AnnualStdDev;

                //Console.WriteLine(SharpeRatioforCompany);
                
                return (AnnualReturn);



            }
        }

        static async Task<List<Portfolio>> FetchPortfoliosandContents()
        {
            var client = new HttpClient();
            List<Portfolio> portfolioList = new List<Portfolio>();
            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/PortfoliosInvestments";

            string content = await client.GetStringAsync(Url);
            JArray portfoliolistC = JArray.Parse(content);


            if (portfoliolistC.Count == 0)
            {
                Console.WriteLine("null returned");
                return null;
                //Debug.WriteLine("null returned");
            }

            else
            {
                //Console.WriteLine(portfoliolistC);
                for (var i = 0; i < portfoliolistC.Count; i++)

                {
                    //Debug.WriteLine((string)portfolios[i]["username"]);
                    Portfolio tempportfolio = new Portfolio();
                    List<Investment> portfoliocontents = new List<Investment>();
                    Investment tempinvestment = new Investment();
                    tempportfolio.name = (string)portfoliolistC[i]["portfolioname"];
                    tempportfolio.contents = portfoliocontents;
                    if (!portfolioList.Contains(tempportfolio))
                    {
                        //Console.WriteLine("New Portfolio Scanned, adding to list");
                        tempinvestment.tickersymbol = (string)portfoliolistC[i]["tickersymbol"];
                        tempinvestment.numshares = (int)portfoliolistC[i]["numshares"];
                        tempinvestment.purchaseprice = (int)portfoliolistC[i]["pricepurchased"];


                        tempportfolio.contents.Add(tempinvestment);
                       // Console.WriteLine((string)portfoliolistC[i]["tickersymbol"]);
                        portfolioList.Add(tempportfolio);
                    }
                    else
                    {
                        for (var j = 0; j<portfolioList.Count; j++)
                        {
                            if (portfolioList[j].name == tempportfolio.name)
                            {
                                //Console.WriteLine("wooooooo");
                                //Console.WriteLine((string)portfoliolistC[i]["tickersymbol"]);
                                tempinvestment.tickersymbol = (string)portfoliolistC[i]["tickersymbol"];
                                tempinvestment.numshares = (int)portfoliolistC[i]["numshares"];
                                tempinvestment.purchaseprice = (int)portfoliolistC[i]["pricepurchased"];

                                portfolioList[j].contents.Add(tempinvestment);
                            }
                        }
                    }
                    //tickerList.Add(portfoliolistC[i]["tickersymbol"]);
                    //Console.Write(tickerList[j].name);


                }
                //Console.Write()

                /*for (var k = 0; k<portfolioList.Count; k++)
                {
                    for(var h =0; h<portfolioList[k].contents.Count; h++)
                    {
                        Console.WriteLine(portfolioList[k].contents[h].tickersymbol);
                        Console.WriteLine(portfolioList[k].contents[h].numshares);
                        Console.WriteLine(portfolioList[k].contents[h].purchaseprice);
                    }
                    Console.WriteLine(portfolioList[k].contents[0].tickersymbol);
                }*/
                return portfolioList;

            }

        }



        static private async Task RunAsync()
        {
            //Grab our ticker List
            List<string> tickers = new List<string>();
            tickers = await GetTickers();

            //Grab ratios for all companies in ticker list and store the results in a list of Ratios


            /*List<Ratios> ratiosfortickers = new List<Ratios>();
            foreach(string ticker in tickers)
            {
                Ratios tempRatio = new Ratios();
                tempRatio = await GetRatio(ticker);
                ratiosfortickers.Add(tempRatio);
            }*/

            //Grab yearly average returns for the companies for calculating sharperatio
            List<double> SharpeRatios = new List<double>();
            foreach(string ticker in tickers)
            {
                Console.Write("here");
                double SharpeRatioForcompany = await CalculateSharpeRatioForCompany(ticker);
                SharpeRatios.Add(SharpeRatioForcompany);
            }

            List<Portfolio> portfolioList = new List<Portfolio>();
            portfolioList = await FetchPortfoliosandContents();

            //Now calculate Sharpe Ratio for Portfolio!

            //1) Calculate the Expected Return of the Portfolio based on weighted average of the
            // expected return of the investments within it. Make a list to store the expected return for all of our returned portfolios

            List<double> ExpectedReturnPortfolioList = new List<double>();
            double weights = 0;

            for (var i = 0; i<portfolioList.Count; i++)
            {
                for(var j = 0; j <portfolioList[i].contents.Count; j++)
                {
                    weights =  portfolioList[i].contents[j].purchaseprice * portfolioList[i].contents[j].numshares;
                }
            }
            


        }

    }
}


