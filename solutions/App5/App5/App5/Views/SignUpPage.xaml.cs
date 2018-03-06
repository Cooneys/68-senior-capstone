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
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}

        /*void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Result));
            });

        }*/

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            WebClient client = new WebClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/SignUp.php");

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("username", user.Username);
            parameters.Add("password", user.Password);

            client.UploadValuesAsync(uri, parameters);
            /*HttpClient client = new HttpClient();
            //Uri mUrl = new Uri("http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView");

            string Url = "http://web.engr.oregonstate.edu/~jonesty/SignUp.php";

            //string content = JsonConvert.SerializeObject(portfolio);
            var stringContent = new StringContent(JsonConvert.SerializeObject(user));//, Encoding.UTF8, "application/json");

            Debug.WriteLine(stringContent);

            await client.PostAsync(Url, stringContent);*/


            // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.currentUser = user;
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                messageLabel.Text = "Sign up failed";
            }
        }

        bool AreDetailsValid(User user)
        {
            return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password));
        }
    }
}