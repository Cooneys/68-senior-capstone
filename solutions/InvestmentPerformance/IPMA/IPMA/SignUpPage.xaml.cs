using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/*
Code referenced from:
https://github.com/xamarin/xamarin-forms-samples/blob/master/Navigation/LoginFlow/LoginNavigation/SignUpPage.xaml.cs
*/

namespace IPMA {
	public partial class SignUpPage : ContentPage {
        public SignUpPage(){
            InitializeComponent();
        }
        async void OnSignUpButtonClicked(object sender, EventArgs e) {
            var user = new User() {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text
            };

            // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded) {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null) {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new AssetView(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else {
                messageLabel.Text = "Sign up failed";
            }
        }

        bool AreDetailsValid(User user)
        {
            return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
        }
    }
}
