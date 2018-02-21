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
	public partial class AllPortfolios : ContentPage
	{
        Data.RestService restService = new Data.RestService();
        public AllPortfolios (User user)
		{
         
           // UsernameLabel.Text = user.Username;
            InitializeComponent ();

            List<Portfolio> portfolioList = new List<Portfolio>();
            portfolioList = restService.FetchPortfolios(user);
            //Debug.WriteLine(user.Username);
            //Debug.WriteLine(portfolioList);
            portfolioListView.ItemsSource = portfolioList;
        }
	}
}