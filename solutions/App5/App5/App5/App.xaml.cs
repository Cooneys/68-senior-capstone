using App5.Data;
using App5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App5
{
	public partial class App : Application
	{

        static RestService restService;

        public static User currentUser { get; set; }
        public static Portfolio currentPortfolio { get; set; }
        public static bool IsUserLoggedIn { get; set; }

		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new Views.LoginPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();
                }
                return restService;
            
            }
        }
	}
}
