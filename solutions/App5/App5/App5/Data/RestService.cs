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
        public JArray data;
        public string json;
        public Portfolio tempPortfolio;

        public RestService()
        {
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            wclient = new WebClient();
            mUserInfo = new List<User>();
            data = new JArray();
            portfolioList = new List<Portfolio>();
            tempPortfolio = new Portfolio();

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
                Debug.WriteLine(data);
     

            //});
        }

        public List<Portfolio> FetchPortfolios(User user)
        {
            wclient = new WebClient();
            Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");

            //List<Portfolio> portfolioList = new List<Portfolio>();
            //Portfolio tempPortfolio = new Portfolio();
            wclient.DownloadDataAsync(mUrl);
            wclient.DownloadDataCompleted += mDownloadDataCompleted;

            if (data.Count == 0)
            {
                return null;
            }

            else
            {
                for (var i = 0; i < data.Count; i++)

                {
                    Debug.WriteLine((string)data[i]["username"]);
                    if (user.Username.Equals((string)data[i]["username"]))
                    {
                        //tempPortfolio.Owners.Add((string)data[i]["username"]);
                        tempPortfolio.TotalValue = ((int)data[i]["totalvalue"]);
                        tempPortfolio.Name = (string)data[i]["portfolioname"];

                        //Debug.WriteLine(tempPortfolio.TotalValue);

                        portfolioList.Add(tempPortfolio);
                    }

                }

                return portfolioList;

            }

        }

        public async Task<Boolean> Login(User user, string parameter)
        {
            //string confirmString;

            wclient = new WebClient();
            Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/"+ parameter);

            wclient.DownloadDataAsync(mUrl);
            wclient.DownloadDataCompleted += mDownloadDataCompleted;



            if(data.Count == 0)
            {
                return false;
            }

            else
            {
                for (var i = 0; i < data.Count; i++)
                //Debug.WriteLine((string)data[i]["username"]);
                {
                    //Debug.WriteLine((string)data[i]["username"]);
                    if (user.Username.Equals((string)data[i]["username"]))
                    {
                        return true;
                    }
                    
                }

                return false;
                
            }


        
            //displayText.Text = mContacts[3].username;

        }

        /*public async Task<Token> RegisterUser(User user)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/registerNewUser.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("Name", user.Username);
            parameters.Add("Number", user.Password);

            //client.UploadValuesCompleted += client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, parameters);
        }*/
        
    }
}
