using App5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage


	{
        Data.RestService restService = new Data.RestService();
		public LoginPage ()
		{
			InitializeComponent ();
		}

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string userparam = "UserInfo";
            Debug.WriteLine("here");
            //Task<bool> correctT = restService.Login(App.currentUser, userparam);
            //Debug.WriteLine("here2");
            //await correctT;
           // Debug.WriteLine("here3");

            //bool correct = new bool();
            //Debug.WriteLine("here4");
            //correct = correctT.Result;
            //Debug.WriteLine("here5");

            //string userparam = "UserInfo";
            var user = new User
            {
                Username = Entry_Username.Text,
                Password = Entry_Password.Text
            };

            Task<bool> correctT = restService.Login(user, userparam);
            Debug.WriteLine("here2");
            await correctT;
            Debug.WriteLine("here3");

            bool correct = new bool();
            Debug.WriteLine("here4");
            correct = correctT.Result;

            //var isValid = AreCredentialsCorrect(user, userparam);
            if (correct == true)
            {
                App.IsUserLoggedIn = true;
                App.currentUser = user;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
                Entry_Password.Text = string.Empty;
            }
        }

        /*async bool AreCredentialsCorrect(User user, String userparam)
        {
            Task<bool> correctT = restService.Login(App.currentUser, userparam);
            await correctT;

            bool correct = correctT.Result;
            return correct;
        }*/

        /*async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            string userparam = "UserInfo";
            
            if (user.CheckInformation())
            {
          
                if (await restService.Login(user, userparam))
                {
                    //DisplayAlert("Login", "Login Success", "Okay");
                    //var allPortfolios = new AllPortfolios(user);
                    //allPortfolios.BindingContext = user;
                    Navigation.PushAsync(new AllPortfolios(user));
                }
                else
                {
                    await DisplayAlert("Login", "Login Failed: Invalid Username or Password", "Okay");
                }
            }
            else
            {
                 await DisplayAlert("Login", "Login Incorrect: Please enter a username or password", "Okay");
            }
        }*/
	}
}