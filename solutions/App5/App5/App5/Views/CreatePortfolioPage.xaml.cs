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
                TotalValue = 0
            };


            WebClient client = new WebClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/makeNewPortfolio.php");

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("portfolioname", portfolio.Name);
            parameters.Add("totalvalue", portfolio.TotalValue.ToString());
            parameters.Add("username", App.currentUser.Username);

            client.UploadValuesAsync(uri, parameters);

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