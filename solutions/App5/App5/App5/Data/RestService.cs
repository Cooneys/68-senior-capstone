using App5.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace App5.Data
{
    public class RestService
    {
        public HttpClient client;
        public WebClient wclient;
        public List<User> mUserInfo;
        public List<Portfolio> portfolioList;
        public List<Investment> investmentList;
        public List<CompanyInfo> companyinfolist;
        public JArray data;
        public string json;
        public Portfolio tempPortfolio;
        public Investment tempInvestment;

        public RestService()
        {
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            wclient = new WebClient();
            mUserInfo = new List<User>();
            data = new JArray();
            portfolioList = new List<Portfolio>();
            //tempPortfolio = new Portfolio();
            Portfolio tempPortfolio = new Portfolio();
            investmentList = new List<Investment>();
            companyinfolist = new List<CompanyInfo>();


        }

        public Uri APIURLBuilder(string varPath)
        {
            UriBuilder tempBuilder = new UriBuilder();
            tempBuilder.Scheme = "http";
            tempBuilder.Host = "web.engr.oregonstate.edu";
            tempBuilder.Path = "~jonesty";
            tempBuilder.Path = "api.php";
            tempBuilder.Path = varPath;

            Uri uri = tempBuilder.Uri;

            Debug.WriteLine(uri);
            return uri;
        }

        void mDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            //string json = Encoding.UTF8.GetString(e.Result);
            //Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
            //object data = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(e.Result));
            data = JArray.Parse(Encoding.UTF8.GetString(e.Result));
            //Debug.WriteLine(data);


            //});
        }

        public async Task<List<Portfolio>> FetchPortfolios(User user)
        {
            client = new HttpClient();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");*/

            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView";

            //List<Portfolio> portfolioList = new List<Portfolio>();
            //Portfolio tempPortfolio = new Portfolio();
            //await wclient.DownloadDataAsync(mUrl);

            /*wclient.DownloadDataCompleted += mDownloadDataCompleted;
            wclient.DownloadDataAsync(mUrl);*/

            string content = await client.GetStringAsync(Url);
            JArray portfolios = JArray.Parse(content);//JsonConvert.DeserializeObject<List<Portfolio>>(content);

            //Debug.WriteLine(portfolios[0][);

            //data = JArray.Parse(Encoding.UTF8.GetString(portfolios));
            //_portfolios = new ObservableCollection<Portfolio>(portfolios);


            if (portfolios.Count == 0)
            {
                Debug.WriteLine("null returned");
                return null;
                //Debug.WriteLine("null returned");
            }

            else
            {
                for (var i = 0; i < portfolios.Count; i++)

                {
                    //Debug.WriteLine((string)portfolios[i]["username"]);
                    if (user.Username.Equals((string)portfolios[i]["username"]))
                    {
                        tempPortfolio = new Portfolio();
                        //tempPortfolio.Owners.Add((string)data[i]["username"]);
                        tempPortfolio.TotalValue = ((int)portfolios[i]["totalvalue"]);
                        tempPortfolio.Name = (string)portfolios[i]["portfolioname"];
                        tempPortfolio.sharperatio = ((double)portfolios[i]["sharperatio"]);
                        tempPortfolio.expectedreturn = ((double)portfolios[i]["expectedreturn"]);
                        tempPortfolio.alpha = ((double)portfolios[i]["alpha"]);

                        Debug.WriteLine(tempPortfolio.TotalValue);
                        Debug.WriteLine(tempPortfolio.Name);

                        portfolioList.Add(tempPortfolio);


                    }

                }
                Debug.WriteLine("break");
                for (var i = 0; i < portfolioList.Count; i++)
                {
                    Debug.WriteLine(portfolioList[i].TotalValue);
                    Debug.WriteLine(portfolioList[i].Name);
                }
                return portfolioList;

            }

        }

        public async Task<Boolean> Login(User user, string parameter)
        {
            //string confirmString;

            // wclient = new WebClient();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/" + parameter);

            // wclient.DownloadDataCompleted += mDownloadDataCompleted;
            //wclient.DownloadDataAsync(mUrl);

            client = new HttpClient();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");*/

            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/UserInfo";

            //List<Portfolio> portfolioList = new List<Portfolio>();
            //Portfolio tempPortfolio = new Portfolio();
            //await wclient.DownloadDataAsync(mUrl);

            /*wclient.DownloadDataCompleted += mDownloadDataCompleted;
            wclient.DownloadDataAsync(mUrl);*/

            string content = await client.GetStringAsync(Url);
            JArray users = JArray.Parse(content);//JsonConvert.DeserializeObject<List<Portfolio>>(content);



            if (users.Count == 0)
            {
                return false;
            }

            else
            {
                for (var i = 0; i < users.Count; i++)
                //Debug.WriteLine((string)data[i]["username"]);
                {
                    //Debug.WriteLine((string)data[i]["username"]);
                    if (user.Username.Equals((string)users[i]["username"]) && user.Password.Equals((string)users[i]["password"]))
                    {
                        return true;
                    }

                }

                return false;

            }

        }

        public async Task<List<Investment>> FetchPortfolioDetails(Portfolio portfolio)
        {
            client = new HttpClient();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");*/

            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/PortfoliosInvestments";

            //List<Portfolio> portfolioList = new List<Portfolio>();
            //Portfolio tempPortfolio = new Portfolio();
            //await wclient.DownloadDataAsync(mUrl);

            /*wclient.DownloadDataCompleted += mDownloadDataCompleted;
            wclient.DownloadDataAsync(mUrl);*/

            string content = await client.GetStringAsync(Url);
            JArray investments = JArray.Parse(content);//JsonConvert.DeserializeObject<List<Portfolio>>(content);

            if (investments.Count == 0)
            {
                return null;
            }

            else
            {
                for (var i = 0; i < investments.Count; i++)
                //Debug.WriteLine((string)data[i]["username"]);
                {
                    //Debug.WriteLine((string)data[i]["username"]);
                    if (portfolio.Name.Equals((string)investments[i]["portfolioname"]))
                    {
                        tempInvestment = new Investment();
                        //tempPortfolio.Owners.Add((string)data[i]["username"]);
                        tempInvestment.tickersymbol = ((string)investments[i]["tickersymbol"]);
                        tempInvestment.numberofshares = (int)investments[i]["numshares"];
                        tempInvestment.pricepurchased = (int)investments[i]["pricepurchased"];

                        investmentList.Add(tempInvestment);
                    }

                }

                return investmentList;

            }

        }

        public async Task<CompanyInfo> FetchCompanyDetails(string ticker)
        {
            client = new HttpClient();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");*/

            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/Investments";

            //List<Portfolio> portfolioList = new List<Portfolio>();
            //Portfolio tempPortfolio = new Portfolio();
            //await wclient.DownloadDataAsync(mUrl);

            /*wclient.DownloadDataCompleted += mDownloadDataCompleted;
            wclient.DownloadDataAsync(mUrl);*/

            string content = await client.GetStringAsync(Url);
            JArray investments = JArray.Parse(content);//JsonConvert.DeserializeObject<List<Portfolio>>(content);

            if (investments.Count == 0)
            {
                return null;
            }

            else
            {
                for (var i = 0; i < investments.Count; i++)
                //Debug.WriteLine((string)data[i]["username"]);
                {
                    if (ticker.Equals((string)investments[i]["tickersymbol"]))
                    {
                        //Debug.WriteLine((string)data[i]["username"]);
                        CompanyInfo info = new CompanyInfo();
                        //tempPortfolio.Owners.Add((string)data[i]["username"]);
                        info.tickersymbol = ((string)investments[i]["tickersymbol"]);
                        info.currentprice = (double)investments[i]["currentprice"];
                        info.ReturnOnAssets = (double)investments[i]["ReturnOnAssets"];
                        info.ReturnOnEquity = (double)investments[i]["ReturnOnEquity"];
                        info.EBTMargin = (double)investments[i]["EBTMargin"];
                        info.AssetTurnover = (double)investments[i]["AssetTurnover"];
                        info.ReceivablesTurnover = (double)investments[i]["ReceivablesTurnover"];
                        info.NetIncome = (double)investments[i]["NetIncome"];
                        info.EarningsPerShare = (double)investments[i]["EarningsPerShare"];
                        info.InterestCoverage = (double)investments[i]["InterestCoverage"];
                        info.TotalCurrentAssets = (double)investments[i]["TotalCurrentAssets"];
                        info.TaxRate = (double)investments[i]["TaxRate"];
                        info.InventoryTurnover = (double)investments[i]["InventoryTurnover"];
                        info.FreeCashFlow = (double)investments[i]["FreeCashFlow"];
                        info.Revenue = (double)investments[i]["Revenue"];

                        return info;
                    }


                }

                //return companyinfolist;
                throw new Exception(string.Format("Cannot find company info for ticker: {0}", ticker));

            }

        }

        public async Task<List<CompanyInfo>> FetchAllCompanyDetails()
        {
            client = new HttpClient();
            List<CompanyInfo> infoList = new List<CompanyInfo>();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");*/

            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/Investments";

            //List<Portfolio> portfolioList = new List<Portfolio>();
            //Portfolio tempPortfolio = new Portfolio();
            //await wclient.DownloadDataAsync(mUrl);

            /*wclient.DownloadDataCompleted += mDownloadDataCompleted;
            wclient.DownloadDataAsync(mUrl);*/

            string content = await client.GetStringAsync(Url);
            JArray investments = JArray.Parse(content);//JsonConvert.DeserializeObject<List<Portfolio>>(content);

            if (investments.Count == 0)
            {
                return null;
            }

            else
            {
                for (var i = 0; i < investments.Count; i++)
                //Debug.WriteLine((string)data[i]["username"]);
                {
                    //Debug.WriteLine((string)data[i]["username"]);
                    CompanyInfo info = new CompanyInfo();
                    //tempPortfolio.Owners.Add((string)data[i]["username"]);
                    info.tickersymbol = ((string)investments[i]["tickersymbol"]);
                    info.currentprice = (double)investments[i]["currentprice"];
                    info.ReturnOnAssets = (double)investments[i]["ReturnOnAssets"];
                    info.ReturnOnEquity = (double)investments[i]["ReturnOnEquity"];
                    info.EBTMargin = (double)investments[i]["EBTMargin"];
                    info.AssetTurnover = (double)investments[i]["AssetTurnover"];
                    info.ReceivablesTurnover = (double)investments[i]["ReceivablesTurnover"];
                    info.NetIncome = (double)investments[i]["NetIncome"];
                    info.EarningsPerShare = (double)investments[i]["EarningsPerShare"];
                    info.InterestCoverage = (double)investments[i]["InterestCoverage"];
                    info.TotalCurrentAssets = (double)investments[i]["TotalCurrentAssets"];
                    info.InventoryTurnover = (double)investments[i]["InventoryTurnover"];
                    info.TaxRate = (double)investments[i]["TaxRate"];
                    info.FreeCashFlow = (double)investments[i]["FreeCashFlow"];
                    info.Revenue = (double)investments[i]["Revenue"];

                    infoList.Add(info);

                }

                return infoList;

            }

        }
    }


}
