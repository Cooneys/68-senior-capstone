using App5.Models;
using Microcharts;
using OxyPlot;
using OxyPlot.Series;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED;
using Avapi;
using System.Globalization;
using System.Net;
using System.Collections.Specialized;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioDetails : ContentPage
    {
        Data.RestService restService = new Data.RestService();
        List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
        public PlotModel PieModel { get; set; }

        public PortfolioDetails (Portfolio selectedPortfolio)
		{
            Title = selectedPortfolio.Name;
            //PieModel = CreatePieChart();
            InitializeComponent ();
            //PieModel = CreatePieChart();
            App.currentPortfolio = selectedPortfolio;
            //MyChart.Chart = new DonutChart() { Entries = entires };
            GetPortfolioDetails();
		}

        public async void GetPortfolioDetails()
        {
            Task<List<Investment>> investmentListT = restService.FetchPortfolioDetails(App.currentPortfolio);
            await investmentListT;
            //restService.FetchPortfolios(App.currentUser);
            List<Investment> investmentList = investmentListT.Result;
            //var selectedPortfolio = sender as Portfolio;
            //investmentList = restService.FetchPortfolioDetails(App.currentPortfolio);

            Task<List<CompanyInfo>> companyInfoT = restService.FetchAllCompanyDetails();
            await companyInfoT;
            List<CompanyInfo> companyInfo = companyInfoT.Result;

            //This is a pretty brute force method of doing this, rather than a double for loop can we override equality operator? 
            //Restructure classes? May be good enough for now
            float totalportfoliovalue = 0;
            App.currentPortfolio.returns = 0;

            if (investmentList != null)
            {
                for (var i = 0; i < investmentList.Count; i++)
                {
                    for (var j = 0; j < companyInfo.Count; j++)
                    {
                        if (investmentList[i].tickersymbol.Equals(companyInfo[j].tickersymbol))
                        {
                            investmentList[i].recentprice = (float)companyInfo[j].currentprice;
                        }
                    }
                    investmentList[i].totalvalue = investmentList[i].numberofshares * investmentList[i].recentprice;
                    //investmentList[i].percentChangeDaily = ((investmentList[i].recentprice - investmentList[i].pricepurchased) / investmentList[i].pricepurchased);
                    totalportfoliovalue = totalportfoliovalue + (investmentList[i].totalvalue);

                    investmentList[i].percentChangeTotal = ((investmentList[i].recentprice - investmentList[i].pricepurchased) / investmentList[i].pricepurchased) * 100;
                    investmentList[i].percentChangeDaily = investmentList[i].recentprice * investmentList[i].numberofshares;

                    investmentList[i].type = investmentList[i].percentChangeTotal.ToString("0.000");


                    App.currentPortfolio.returns += (investmentList[i].recentprice - investmentList[i].pricepurchased) * investmentList[i].numberofshares;

                    int n = 200 / investmentList.Count;
                    investmentList[i].color = CustColors.grabColor(i * n);
                    if (investmentList[i].percentChangeDaily >= 0)
                    {
                        investmentList[i].changeColorDaily = Color.FromRgb(0, 0, 0);
                    }
                    else
                    {
                        investmentList[i].changeColorDaily = Color.FromRgb(255, 0, 0);
                    }

                    if (investmentList[i].percentChangeTotal >= 0) {
                        investmentList[i].changeColorTotal = Color.FromRgb(0, 0, 0);
                    }
                    else {
                        investmentList[i].changeColorTotal = Color.FromRgb(255, 0, 0);
                    }

                    Microcharts.Entry tempEntry = new Microcharts.Entry(investmentList[i].totalvalue)
                    {
                        Label = investmentList[i].tickersymbol,
                        Color = investmentList[i].color.ToSKColor()
                    };
                    entries.Add(tempEntry);
                }
                App.currentPortfolioTotalValue = totalportfoliovalue;
                var chart = new DonutChart() { Entries = entries };
                this.chartView.Chart = chart;
                investmentListView.ItemsSource = investmentList;
                returnsLabel.Text = App.currentPortfolio.returns.ToString("0.00");
                sharpeLabel.Text = App.currentPortfolio.sharperatio.ToString("0.000");
                alphaLabel.Text = App.currentPortfolio.alpha.ToString("0.00");
                investmentListView.ItemTapped += async (sender, args) =>
                {
                    var item = args.Item as Investment;
                    if (item == null) return;
                    await Navigation.PushAsync(new AssetDetails(item));
                    investmentListView.SelectedItem = null;
                };
            }
            else
            {
                Debug.WriteLine("PL returned null");
            }
            WebClient client = new WebClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~cooneys/capstone/updatePortfolioValue.php");
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("portfolioname", App.currentPortfolio.Name);
            parameters.Add("currentuser", App.currentUser.Username);
            parameters.Add("totalvalue", App.currentPortfolioTotalValue.ToString());
            client.UploadValuesAsync(uri, parameters);
        }

        async void OnInvestmentButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddInvestmenttoPortfolio());
        }

        async void OnTrashButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Do you want to delete this Portfolio?", "Yes", "No");
            if (answer)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://web.engr.oregonstate.edu/~cooneys/capstone/deletePortfolio.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("portfolioname", App.currentPortfolio.Name);
                parameters.Add("currentuser", App.currentUser.Username);
                client.UploadValuesAsync(uri, parameters);

                await Navigation.PopAsync();
            }
        }
    }
}
