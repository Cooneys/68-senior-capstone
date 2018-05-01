using App5.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreatePortfolioPage : ContentPage
	{
		public CreatePortfolioPage ()
		{
			InitializeComponent ();
		}

        public async void OnNewPortfolioButtonClicked()
        {
            var portfolio = new Portfolio()
            {
                Name = Entry_Name.Text,
                TotalValue = 0,
                sharperatio = 0,
                alpha = 0,
                expectedreturn = 0

            };


            //WebClient client = new WebClient();
            //Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/makeNewPortfolio.php");
            HttpClient client2 = new HttpClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/makeNewPortfolio.php");

            /*NameValueCollection parameters = new NameValueCollection();
            parameters.Add("portfolioname", portfolio.Name);
            parameters.Add("totalvalue", portfolio.TotalValue.ToString());
            parameters.Add("username", App.currentUser.Username);

            client.UploadValuesAsync(uri, parameters);*/
            Debug.WriteLine("POOPOPOPOP");
            var postData = new List<KeyValuePair<string, string>>();
            Debug.WriteLine("POOPOPOPOP2");
            postData.Add(new KeyValuePair<string, string>("portfolioname", portfolio.Name.ToString()));
            Debug.WriteLine("POOPOPOPOP3");
            postData.Add(new KeyValuePair<string, string>("totalvalue", portfolio.TotalValue.ToString()));
            Debug.WriteLine("POOPOPOPOP4");
            postData.Add(new KeyValuePair<string, string>("username", App.currentUser.Username.ToString()));
            Debug.WriteLine("POOPOPOPOP5");
            postData.Add(new KeyValuePair<string, string>("expectedreturn", portfolio.expectedreturn.ToString()));
            Debug.WriteLine("POOPOPOPO6");
            postData.Add(new KeyValuePair<string, string>("sharperatio", portfolio.sharperatio.ToString()));
            Debug.WriteLine("POOPOPOPOP7");
            postData.Add(new KeyValuePair<string, string>("alpha", portfolio.alpha.ToString()));
            Debug.WriteLine("POOPOPOPOP8");

            //Debug.WriteLine(App.currentPortfolio.Name.ToString());
            Debug.WriteLine(portfolio.TotalValue.ToString());
            Debug.WriteLine(App.currentUser.Username.ToString());
            Debug.WriteLine(portfolio.expectedreturn.ToString());
            Debug.WriteLine(portfolio.sharperatio.ToString());
            Debug.WriteLine(portfolio.alpha.ToString());


            HttpContent content = new FormUrlEncodedContent(postData);

            var response = await client2.PostAsync(uri, content);
            //client.UploadValuesAsync(uri, parameters);

            /*var rootPage = Navigation.NavigationStack.FirstOrDefault();
            if (rootPage != null)
            {
                Navigation.InsertPageBefore(new PortfolioDetails(App.currentPortfolio), Navigation.NavigationStack.First());
                await Navigation.PopToRootAsync();
            }*/
            // await Navigation.PushAsync(new PortfolioDetails(App.currentPortfolio));
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count() - 2]);
            //Navigation.InsertPageBefore(new PortfolioDetails(App.currentPortfolio), Navigation.NavigationStack[Navigation.NavigationStack.Count() - 1]);
            //await Navigation.PopAsync();

            //HttpClient client = new HttpClient();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");*/

            //string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/Portfolios";

            //string content = JsonConvert.SerializeObject(portfolio);
            //var stringContent = new StringContent(JsonConvert.SerializeObject(portfolio));//, Encoding.UTF8, "application/json");

            //Debug.WriteLine(stringContent);

            //await client.PostAsync(Url, stringContent);

            // Sign up logic goes here

            //var signUpSucceeded = AreDetailsValid(user);

            var rootPage = Navigation.NavigationStack.FirstOrDefault();
            if (rootPage != null)
            {
                Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                await Navigation.PopToRootAsync();
            }
            //await Navigation.PushAsync(new MainPage());
        }

        //protected override async 
	}
}