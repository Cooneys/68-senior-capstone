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
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED;

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
        public int totalvalue { get; set; }

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
        public double currentprice { get; set; }
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

            //RunAsync().Wait();
            Action completionMethod = () => Console.WriteLine("Completed!");

            RunAsync().GetAwaiter().OnCompleted(completionMethod);
            //System.Threading.Thread.Sleep(90000);

            //Console.WriteLine(Program.tickers[0]);
            //await RunAsync;
            //Console.WriteLine("Jobs Completed! Press Enter to Exit.");
            Console.ReadLine();
        }

        static async Task<List<company>> FetchTickers()
        {
            var client = new HttpClient();
            List<company> portfolioList = new List<company>();
            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/Investments";

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
                    company tempcompany = new company();
                    
                    
                    tempcompany.tickersymbol = (string)portfoliolistC[i]["tickersymbol"];
                    tempcompany.currentprice = (double)portfoliolistC[i]["currentprice"];

                    //tempportfolio.totalvalue = (int)portfoliolistC[i]["totalvalue"];
                    portfolioList.Add(tempcompany);
                }

                return portfolioList;
            }


        }

        // Return the standard deviation of an array of Doubles.
        //
        // If the second argument is True, evaluate as a sample.
        // If the second argument is False, evaluate as a population.
        public static double StdDev(IEnumerable<double> values,
            bool as_sample)
        {
            // Get the mean.
            double mean = values.Sum() / values.Count();

            // Get the sum of the squares of the differences
            // between the values and the mean.
            var squares_query =
                from double value in values
                select (value - mean) * (value - mean);
            double sum_of_squares = squares_query.Sum();

            if (as_sample)
            {
                return Math.Sqrt(sum_of_squares / (values.Count() - 1));
            }
            else
            {
                return Math.Sqrt(sum_of_squares / values.Count());
            }
        }

        public static double Covariance(List<double> set1, List<double> set2)
        {
            double xbar = 0;
            double ybar = 0;

            for(var i=0; i<set1.Count(); i++)
            {
                xbar = xbar + set1[i];
                ybar = ybar + set2[i];
            }

            xbar = xbar / set1.Count();
            ybar = ybar / set2.Count();

            double sum = 0;

            for (var i=0; i<set1.Count(); i++)
            {
                sum = sum + ((set1[i] - xbar) * (set2[i] - ybar));
            }

            sum = sum / (set1.Count()-1);

            return sum;
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
                    //tempportfolio.totalvalue = (int)portfoliolistC[i]["totalvalue"];
                    tempportfolio.contents = portfoliocontents;
                    if (!portfolioList.Contains(tempportfolio))
                    {
                        //Console.WriteLine("New Portfolio Scanned, adding to list");
                        tempinvestment.tickersymbol = (string)portfoliolistC[i]["tickersymbol"];
                        tempinvestment.numshares = (int)portfoliolistC[i]["numshares"];
                        tempinvestment.purchaseprice = (int)portfoliolistC[i]["pricepurchased"];
                        


                        tempportfolio.contents.Add(tempinvestment);
                        //tempportfolio.totalvalue = (int)portfoliolistC[i]["totalvalue"];
                        //Console.WriteLine(tempportfolio.totalvalue);
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

        static async void UploadSharpeRatio(string portfolioname, double sharperatio)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/UploadSharpeRatio.php");

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("portfolioname", portfolioname);
            parameters.Add("sharperatio", sharperatio.ToString());

            client.UploadValuesAsync(uri, parameters);
            Console.WriteLine(sharperatio.ToString());
            Console.WriteLine(portfolioname);
            Console.WriteLine("Data Added");
        }

        static async void CalculateRSquaredforPortfolio()
        {

        }

        static async Task<int> FetchandUpdateCurrentPriceDataforCompany(string ticker)
        {
                IAvapiConnection connection = AvapiConnection.Instance;
                float recentprice = 0;
                IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED m_time_series_daily_adjustedResponse;

                // Set up the connection and pass the API_KEY provided by alphavantage.co
                connection.Connect("7NIMRBR8G8UB7P8C");

                // Get the TIME_SERIES_MONTHLY_ADJUSTED query object
                Int_TIME_SERIES_DAILY_ADJUSTED time_series_daily_adjusted =
                    connection.GetQueryObject_TIME_SERIES_DAILY_ADJUSTED();

                // Perform the TIME_SERIES_MONTHLY_ADJUSTED request and get the result
                m_time_series_daily_adjustedResponse = await time_series_daily_adjusted.QueryAsync(
                     ticker);

                var data = m_time_series_daily_adjustedResponse.Data;

                WebClient client = new WebClient();
                Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/UpdateCompanyInfo.php");

                NameValueCollection parameters = new NameValueCollection();
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
                        if (counter < 1)
                        {
                            
                            recentprice = float.Parse(timeseries.adjustedclose, CultureInfo.InvariantCulture.NumberFormat);
                            
                        }
                    counter = counter + 1;
                    }
                }
                Console.WriteLine(recentprice);
                Console.WriteLine(ticker);
                parameters.Add("tickersymbol", ticker);
                parameters.Add("currentprice", recentprice.ToString());

                client.UploadValuesAsync(uri, parameters);
                return 1;
        }


        static async Task<Tuple<double, double, List<double>>> SR_FetchPricingDataandExpectedReturn(string ticker)
        {
            //******************************************************************************************
            //The purpose of this function is to do all calculations related to sharperatio that involve pricing data
            // for a single company. This includes STDdev, and expected return. These will be stored in a dictionary
            //******************************************************************************************
            

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
                return null;
            }
            else
            {
                Console.WriteLine("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
                int counter = 0;
                foreach (var timeseries in data.TimeSeries)
                {
                    //Grab 5 years worth of months data
                    if (counter < 60)
                    {
                        //add raw prices to array
                        priceslastyear.Add(double.Parse(timeseries.adjustedclose, CultureInfo.InvariantCulture.NumberFormat));

                        if (counter > 0)
                        {
                            //take pricing data and calculate percent change between each pair of months
                            //Console.Write(((priceslastyear[counter] / priceslastyear[counter - 1]) - 1) * 100);
                            monthlyreturnpercentages.Add(((priceslastyear[counter] / priceslastyear[counter - 1]) - 1) * 100);
                        }
                        counter = counter + 1;
                    }
                }

                //Setup counter to average pricing data
                double running_average = 0;
                //Calculate STDDev of data
                double std_dev = StdDev(monthlyreturnpercentages, true);

                //Sum all the values in our list
                for(var i = 0; i<monthlyreturnpercentages.Count(); i++)
                {
                    running_average = monthlyreturnpercentages[i] + running_average;
                }

                //Finishing taking average by dividing by the number of entries
                running_average = running_average / monthlyreturnpercentages.Count();

                //Annualize the average (multiply by 12 since monthly)
                running_average = running_average * 12;

                //Add the values to the tuple and print!
                Console.Write(ticker + ", ");
                Console.Write(std_dev + ", ");
                Console.Write(running_average + ", ");

                var pricingdata = Tuple.Create(std_dev, running_average, monthlyreturnpercentages);
                //Return this value
                return pricingdata;
            }
        }

        static async Task SR_CalculateSharpeforPortfolio(Portfolio currentportfolio, List<company> companies)
        {
            //*********************************************************************************************************
            //First, we need to fetch pricing data for all the companies in the portfolio
            // Typically, this is done with perhaps a 3 year horizon of daily prices, but we will do monthly over 5 years to start
            //*********************************************************************************************************

            Console.WriteLine("Calculating sharpe for: {0}", currentportfolio.name);

            //This list will hold all of our expected returns for all the companies in our portfolio
            List<double> expectedreturnforallcompanies  = new List<double>();
            //This list will hold all of our standard deviations for all the companies in our portfolio
            List<double> stddevforallcompanies = new List<double>();
            //This list will hold all of the raw pricing data for later covariance calculations
            List<List<double>> pricingdataforcovariance = new List<List<double>>();

            List<double> covariances = new List<double>();

            //var pricingdata = new Tuple<double, double, List<double>>();

            
            

            //We need to iterate through all the tickers in our portfolio, and add that expected return to our list above
            for (var i = 0; i < currentportfolio.contents.Count(); i++) {


                //Get pricing data for that company
                Console.WriteLine("Fetching pricing data for: {0}", currentportfolio.contents[i].tickersymbol);
                Tuple<double, double, List<double>> pricingdata = await SR_FetchPricingDataandExpectedReturn(currentportfolio.contents[i].tickersymbol);

                //grab values out of Tuple and assign to local variables
                double expectedreturnforsinglecompany = pricingdata.Item2;
                double stddevforcompany = pricingdata.Item1;

                //Add values to their appropriate lists for later use
                expectedreturnforallcompanies.Add(expectedreturnforsinglecompany);
                stddevforallcompanies.Add(stddevforcompany);
                pricingdataforcovariance.Add(pricingdata.Item3);
            }

            //Now we need to calculate the sample covariance for each pair of companies in our portfolio
            for (var i = 0; i< pricingdataforcovariance.Count()-1; i++)
            {
                for (var j = i+1; j<pricingdataforcovariance.Count(); j++)
                {
                    double temp = Covariance(pricingdataforcovariance[i], pricingdataforcovariance[j]);
                    Console.WriteLine("Comparing {0} & {1}", i, j);
                    covariances.Add(temp);
                }
            }

            // Sharpe Ratio calculation now that we have all of our pieces! :)

            // Sharpe Ratio (p) = (Expected Return(P) - Risk Free Rate)/Standard Deviation(P)

            // 1) Lets get Expected return of portfolio based on expected returns of assets (calculated already) times the 
            // weight that each asset holds ((number of shares * current price of stock)/total value of portfolio)

            // a) Make a list to hold our weights
            List<double> weights = new List<double>();
            double totalportfoliovalue = 0;
            for(var i=0; i < currentportfolio.contents.Count(); i++)
            {
                int numshares = currentportfolio.contents[i].numshares;
                for(var j = 0; j<companies.Count(); j++)
                {
                    if (currentportfolio.contents[i].tickersymbol.Equals(companies[j].tickersymbol))
                    {
                        totalportfoliovalue = totalportfoliovalue + (numshares * companies[j].currentprice);
                        weights.Add(numshares * companies[j].currentprice);
                    }
                }
            }

            //NOTE: there is likely a much more efficient way to do this, perhaps calculate total portfolio value earlier?

            for(var i=0; i<weights.Count(); i++)
            {
                weights[i] = weights[i] / totalportfoliovalue;
            }

            double portfolioexpectedreturn = 0;
            for (var i = 0; i<weights.Count(); i++){
                portfolioexpectedreturn = portfolioexpectedreturn + (weights[i] * expectedreturnforallcompanies[i]);
            }

            // 2) Now lets get standard deviation of the portfolio!
            

 

        }

        static private async Task RunAsync()
        {
            //Grab our ticker List
            /*List<string> tickers = new List<string>();
            tickers = await GetTickers();*/

            List<company> companies = new List<company>();
            companies = await FetchTickers();


            //Update our tickers with current price

            int updatersuccess = new int();
            foreach(var ticker in companies)
            {
                //Console.WriteLine("there");
                updatersuccess = await FetchandUpdateCurrentPriceDataforCompany(ticker.tickersymbol);
            }


            //Grab ratios for all companies in ticker list and store the results in a list of Ratios


            /*List<Ratios> ratiosfortickers = new List<Ratios>();
            foreach(string ticker in tickers)
            {
                Ratios tempRatio = new Ratios();
                tempRatio = await GetRatio(ticker);
                ratiosfortickers.Add(tempRatio);
            }*/

            List<Portfolio> portfolioList = new List<Portfolio>();
            portfolioList = await FetchPortfoliosandContents();

            Console.WriteLine(portfolioList[5].contents[0].tickersymbol);
            Console.WriteLine(portfolioList.Count());

            foreach (Portfolio portfolio in portfolioList)
            {
                await SR_CalculateSharpeforPortfolio(portfolio, companies);
            }
            /*for(var i = 0; i<portfolioList.Count(); i++)
            {

            }*/
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            /*
            
            
            //Grab yearly average returns for the companies for calculating sharperatio
            List<double> SharpeRatios = new List<double>();
            foreach(string ticker in tickers)
            {
                Console.Write("here");
                double SharpeRatioForcompany = await CalculateSharpeRatioForCompany(ticker);
                SharpeRatios.Add(SharpeRatioForcompany);
            }

            var ExpectedReturnOfCompanies = tickers.Zip(SharpeRatios, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);
        
            foreach (KeyValuePair<string, double> kvp in ExpectedReturnOfCompanies)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.Write(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }

            List<Portfolio> portfolioList = new List<Portfolio>();
            portfolioList = await FetchPortfoliosandContents();

            //Now calculate Sharpe Ratio for Portfolio!

            //1) Calculate the Expected Return of the Portfolio based on weighted average of the
            // expected return of the investments within it. Make a list to store the expected return for all of our returned portfolios

            Dictionary<string, double> ExpectedReturnPortfolioList = new Dictionary<string, double>();
            double weights = 0;
            double tempsum = 0;
            
            //Need to do this for each portfolio
            for (var i = 0; i<portfolioList.Count; i++)
            {
                //Need to do the calculation for each investment in the portfolio
                for(var j = 0; j <portfolioList[i].contents.Count; j++)
                {
                    weights = (portfolioList[i].contents[j].purchaseprice * portfolioList[i].contents[j].numshares);/// portfolioList[i].totalvalue;
                    //Console.WriteLine(ExpectedReturnOfCompanies[portfolioList[i].contents[j].tickersymbol]);
                    tempsum = tempsum + (weights* ExpectedReturnOfCompanies[portfolioList[i].contents[j].tickersymbol]);
                }
                //Console.WriteLine("ahasdfhasdfasdf");
                ExpectedReturnPortfolioList.Add(portfolioList[i].name, tempsum);
            }
            foreach (KeyValuePair<string, double> kvp in ExpectedReturnPortfolioList)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.Write(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }

            //Upload the SharpeRatios to our database!
            foreach(KeyValuePair<string, double> kvp in ExpectedReturnPortfolioList)
            {
                UploadSharpeRatio(kvp.Key, kvp.Value);
            }
            */

        }

    }
}


