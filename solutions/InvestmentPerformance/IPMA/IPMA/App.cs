using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IPMA {
    public class App : Application {
        public static bool IsUserLoggedIn { get; set; }

        public App() {
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
