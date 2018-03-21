using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Last10kAPIs
{

    public class Returnoncommonequity
    {
        

        //Placeholder equation: N / Eps
         


    }


    public class Debtcoverageserviceratio
    {
        // placeholder equation: M / ( IC + ( Tc/[1-Tr]) )

    }



    public class Inventoryturnover
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }

    public class Company
    {
        [JsonProperty(PropertyName = "Company")]
        public string tickersymbol { get; set; }
    }

    public class Returnonassets
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class Returnonequity
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class Ebtmargin
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }


    public class Assetturnover
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class Totalassets
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }

    public class Receivablesturnover
    {

        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class Netincome
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class Earningspershare
    {

        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }

    public class Interestcoverage
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }
    }

    public class Totalcurrentassets
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }

    public class Taxrate
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }

    public class Freecashflow
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }

    public class Revenue
    {
        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }
        //[JsonProperty(PropertyName = "Historical")]
        //public Dictionary<DateTime, float> historical { get; set; }
        [JsonProperty(PropertyName = "Recent")]
        public float recent { get; set; }

    }



    public class Ratios
    {
        [JsonProperty(PropertyName = "InventoryTurnover")]
        public Inventoryturnover inventory { get; set; }
        [JsonProperty(PropertyName = "ReturnOnAssets")]
        public Returnonassets assets { get; set; }
        [JsonProperty(PropertyName = "ReturnOnEquity")]
        public Returnonequity equity { get; set; }
        [JsonProperty(PropertyName = "EBTMargin")]
        public Ebtmargin ebt { get; set; }
        [JsonProperty(PropertyName = "AssetTurnover")]
        public Assetturnover ast { get; set; }
        [JsonProperty(PropertyName = "TotalAssets")]
        public Totalassets toas { get; set; }
        [JsonProperty(PropertyName = "ReceivablesTurnover")]
        public Receivablesturnover rec { get; set; }
        [JsonProperty(PropertyName = "NetIncome")]
        public Netincome netinc { get; set; }
        [JsonProperty(PropertyName = "EarningsPerShare")]
        public Earningspershare eps { get; set; }
        [JsonProperty(PropertyName = "InterestCoverage")]
        public Interestcoverage intcov { get; set; }
        [JsonProperty(PropertyName = "TotalCurrentAssets")]
        public Totalcurrentassets tca { get; set; }
        [JsonProperty(PropertyName = "TaxRate")]
        public Taxrate tax { get; set; }
        [JsonProperty(PropertyName = "FreeCashFlow")]
        public Freecashflow fcf { get; set; }
        [JsonProperty(PropertyName = "Revenue")]
        public Revenue rev { get; set; }

        public string Company { get; set; }

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

        static void Main(string[] args)
        {
            RunAsync().Wait();
            Console.WriteLine("Financial Data from Ratios, Balance Sheet, Cash Flow, and Income Statement");
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
                Console.WriteLine("Beginning Calculations");

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();



                    var articles = JsonConvert.DeserializeObject<List<Company>>(responseBody);



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
            Inventoryturnover I = new Inventoryturnover();
            Returnonassets A = new Returnonassets();
            Returnonequity E = new Returnonequity();
            Ebtmargin M = new Ebtmargin();
            Assetturnover At = new Assetturnover();
            Totalassets Ta = new Totalassets();
            Receivablesturnover Rt = new Receivablesturnover();
            Netincome N = new Netincome();
            Earningspershare Ep = new Earningspershare();
            Interestcoverage Ic = new Interestcoverage();
            Totalcurrentassets Tc = new Totalcurrentassets();
            Taxrate Tr = new Taxrate();
            Freecashflow Fc = new Freecashflow();
            Revenue Re = new Revenue();
            Company C = new Company();

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                HttpResponseMessage response = await client.GetAsync("https://services.last10k.com/v1/company/AAPL/ratios?10-Q");

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

                    if (historicalEBT["TTM"] != null)
                    {
                        M.recent = (float)historicalEBT["TTM"];
                    }
                    else
                    {
                        M.recent = 0;
                    }

                    var articlesN = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    N.name = articlesN.NetIncome.Name;
                    JObject historicalN = articlesN.N.Recent;

                    if (historicalN["TTM"] != null)
                    {
                        N.recent = (float)historicalN["TTM"];
                    }
                    else
                    {
                        N.recent = 0;
                    }

                    var articlesEP = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Ep.name = articlesEP.EarningsPerShare.Name;
                    JObject historicalEP = articlesEP.EarningsPerShare.Recent;

                    if (historicalEP["TTM"] != null)
                    {
                        Ep.recent = (float)historicalEP["TTM"];
                    }
                    else
                    {
                        Ep.recent = 0;
                    }

                    var articlesIC = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Ic.name = articlesIC.InterestCoverage.Name;
                    JObject historicalIC = articlesIC.InterestCoverage.Recent;

                    if (historicalIC["TTM"] != null)
                    {
                        Ic.recent = (float)historicalIC["TTM"];
                    }
                    else
                    {
                        Ic.recent = 0;
                    }

                    var articlesTC = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Tc.name = articlesTC.TotalCurrentAssets.Name;
                    JObject historicalTC = articlesTC.TotalCurrentAssets.Recent;

                    if (historicalTC["Latest Qtr"] != null)
                    {
                        Tc.recent = (float)historicalTC["Latest Qtr"];
                    }
                    else
                    {
                        Tc.recent = 0;
                    }

                    var articlesTR = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Tr.name = articlesTR.TaxRate.Name;
                    JObject historicalTR = articlesTR.TaxRate.Recent;

                    if (historicalTR["TTM"] != null)
                    {
                        Tr.recent = (float)historicalTR["TTM"];
                    }
                    else
                    {
                        Tr.recent = 0;
                    }

                    var articlesAT = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    At.name = articlesAT.AssetTurnover.Name;
                    JObject historicalAT = articlesAT.AssetTurnover.Recent;

                    if (historicalAT["TTM"] != null)
                    {
                        At.recent = (float)historicalAT["TTM"];
                    }
                    else
                    {
                        At.recent = 0;
                    }

                    var articlesTA = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Ta.name = articlesTA.TotalAssets.Name;
                    JObject historicalTA = articlesTA.TotalAssets.Recent;

                    if (historicalTA["Latest Qtr"] != null)
                    {
                        Ta.recent = (float)historicalTA["Latest Qtr"];
                    }
                    else
                    {
                        Ta.recent = 0;
                    }

                    var articlesRT = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Rt.name = articlesRT.ReceivablesTurnover.Name;
                    JObject historicalRT = articlesRT.ReceivablesTurnover.Recent;

                    if (historicalRT["TTM"] != null)
                    {
                        Rt.recent = (float)historicalRT["TTM"];
                    }
                    else
                    {
                        Rt.recent = 0;
                    }

                    var articlesFC = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Fc.name = articlesFC.FreeCashFlow.Name;
                    JObject historicalFC = articlesFC.FreeCashFlow.Recent;

                    if (historicalFC["TTM"] != null)
                    {
                        Fc.recent = (float)historicalFC["TTM"];
                    }
                    else
                    {
                        Fc.recent = 0;
                    }

                    var articlesRE = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    Re.name = articlesRE.Revenue.Name;
                    JObject historicalRE = articlesRE.Revenue.Recent;

                    if (historicalRE["TTM"] != null)
                    {
                        Re.recent = (float)historicalRE["TTM"];
                    }
                    else
                    {
                        Re.recent = 0;
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
                    R.netinc = N;
                    Console.WriteLine(R.netinc.recent);
                    R.eps = Ep;
                    Console.WriteLine(R.eps.recent);
                    R.intcov = Ic;
                    Console.WriteLine(R.intcov.recent);
                    R.tca = Tc;
                    Console.WriteLine(R.tca.recent);
                    R.tax = Tr;
                    Console.WriteLine(R.tax.recent);
                    R.ast = At;
                    Console.WriteLine(R.ast.recent);
                    R.toas = Ta;
                    Console.WriteLine(R.toas.recent);
                    R.rec = Rt;
                    Console.WriteLine(R.rec.recent);
                    R.rev = Re;
                    Console.WriteLine(R.rev.recent);



                    R.Company = ticker;
                    Console.WriteLine(R.Company);

                    return R;

                    //foreach (KeyValuePair<DateTime, float> kvp in Dictionary) ;
                    //Console.WriteLine(articles.InventoryTurnover.Historical[0]);
                    //data.Add(Emp.datasymbol);


                }

            }
        }

        static async Task<BalanceSheets> GetBalanceSheet(string ticker)
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);


            BalanceSheets B = new BalanceSheets();

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                string url = "https://services.last10k.com/v1/company/" + ticker + "/balancesheet?10-Q";
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();



                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);

                    return B;





                }

            }
        }

        static async Task<Income> GetIncome(string ticker)
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);


            Income I = new Income();

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                string url = "https://services.last10k.com/v1/company/" + ticker + "/income?10-Q";
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();



                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);

                    return I;





                }

            }
        }

        static async Task<CashFlows> GetCashFlows(string ticker)
        {
            var client = new HttpClient();
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);


            CashFlows Cf = new CashFlows();

            using (client)
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");
                string url = "https://services.last10k.com/v1/company/" + ticker + "/cashflows?10-Q";
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();



                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);

                    return Cf;





                }

            }
        }




        static private async Task RunAsync()
        {
            //Grab our ticker List;
            List<string> tickers = new List<string>();
            tickers = await GetTickers();

            //Grab ratios for all companies in ticker list and store the results in a list of Ratios
            List<Ratios> ratiosfortickers = new List<Ratios>();
            foreach (string ticker in tickers)
            {
                Ratios tempRatio = new Ratios();
                tempRatio = await GetRatio(ticker);
                ratiosfortickers.Add(tempRatio);
            }

            //Grab all balance sheet
            List<BalanceSheets> balancesheetfortickers = new List<BalanceSheets>();
            foreach (string ticker in tickers)
            {

                BalanceSheets tempBalanceSheets = new BalanceSheets();
                tempBalanceSheets = await GetBalanceSheet(ticker);
                //balancesheetsfortickers.Add(tempBalanceSheets);

            }

            //Grab all Cash Flow
            List<CashFlows> cashflowsfortickers = new List<CashFlows>();
            foreach (string ticker in tickers)
            {
                CashFlows tempCashFlow = new CashFlows();
                tempCashFlow = await GetCashFlows(ticker);
                cashflowsfortickers.Add(tempCashFlow);
            }



            //Grab all Income Statement
            List<Income> incomefortickers = new List<Income>();
            foreach (string ticker in tickers)
            {
                Income tempIncome = new Income();
                tempIncome = await GetIncome(ticker);
                incomefortickers.Add(tempIncome);
            }
        }

    }


}   
