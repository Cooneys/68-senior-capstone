using App5.Models;
using App5.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
        Data.RestService restService = new Data.RestService();
        private HttpClient _client = new HttpClient();
        private ObservableCollection<Portfolio> _portfolios;
        string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/UsersPortfoliosView";
        public MainPage()
		{
            Title = "Welcome, " + App.currentUser.Username;

            InitializeComponent();
            //TestLabel.Text = App.currentUser.Username;

            //TestLabel.Text = portfolioList[0].Name;

            PageFetchPortfolios();

        }

        protected override async void OnAppearing()
        {
            //PageFetchPortfolios();
            base.OnAppearing();


        }

        async void OnNewPortfolioButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePortfolioPage());
        }

        public async void PageFetchPortfolios()
        {

            Task<List<Portfolio>> portfoliolisttask = restService.FetchPortfolios(App.currentUser);
            await portfoliolisttask;
            
            List<Portfolio> portfolioList = portfoliolisttask.Result;
            Debug.WriteLine("In Display:");
            //Debug.WriteLine(portfolioList[0].Name);


            //List<Portfolio> portfolioList = new List<Portfolio>();
            //restService.FetchPortfolios(App.currentUser);
            //portfolioList = restService.FetchPortfolios(App.currentUser);
            Debug.WriteLine("check under here");
            if (portfolioList != null)

            {
                //Debug.WriteLine(portfolioList[0].Name);
                portfolioListView.ItemsSource = portfolioList;
                portfolioListView.ItemTapped += async (sender, args) =>
                {
                    var item = args.Item as Portfolio;
                    if (item == null) return;
                    await Navigation.PushAsync(new PortfolioDetails(item));
                    portfolioListView.SelectedItem = null;
                };
            }
            else
            {
                Debug.WriteLine("PL returned null");
            }
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;
        }
	}
}
