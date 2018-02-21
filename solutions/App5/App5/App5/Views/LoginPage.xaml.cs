using App5.Models;
using System;
using System.Collections.Generic;
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

        async void SignInProcedure(object sender, EventArgs e)
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
        }
	}
}