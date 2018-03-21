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


            Task<List<CompanyInfo>> companyInfoT = restService.FetchCompanyDetails();
            await companyInfoT;
            List<CompanyInfo> companyInfo = companyInfoT.Result;

            //This is a pretty brute force method of doing this, rather than a double for loop can we override equality operator? 
            //Restructure classes? May be good enough for now


            float totalportfoliovalue = 0;
            if (investmentList != null)
            {
                for (var i = 0; i < investmentList.Count; i++)
                {
                    Debug.WriteLine(investmentList[i].numberofshares);
                    Debug.WriteLine(investmentList[i].pricepurchased);
                    float recentprice = 0;
                    //float recentprice = new float();
                    for (var j = 0; j < companyInfo.Count; j++)
                    {
                        if (investmentList[i].tickersymbol.Equals(companyInfo[j].tickersymbol))
                        {
                            recentprice = (float)companyInfo[i].currentprice;
                        }
                    }
                    //recentprice = await GetRecentPricingDataForCompany(investmentList[i].tickersymbol);
                    float tempvalue = investmentList[i].numberofshares * recentprice;
                    totalportfoliovalue = totalportfoliovalue + (recentprice * tempvalue);
                    Debug.WriteLine(recentprice);
                    Debug.WriteLine("test");
                    Debug.WriteLine(tempvalue);
                    int n = 200 / investmentList.Count;
                    investmentList[i].color = CustColors.grabColor(i * n);

                    //Temp solution for dynamically making colors. This should be improved and moved to its own class or function
                    
                    Microcharts.Entry tempEntry = new Microcharts.Entry(tempvalue)
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

            }
        }

        /*async Task<float> GetRecentPricingDataForCompany(string ticker)
        {
            IAvapiConnection connection = AvapiConnection.Instance;
            float recentprice = 0;
            IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED m_time_series_daily_adjustedResponse;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect("7NIMRBR8G8UB7P8C");

            // Get the TIME_SERIES_MONTHLY_ADJUSTED query object
            Int_TIME_SERIES_DAILY_ADJUSTED time_series_daily_adjusted =
                connection.GetQueryObject_TIME_SERIES_DAILY_ADJUSTED();

            // Perform the TIME_SERIES_MONTHLY_ADJUSTED request and get the result
            m_time_series_daily_adjustedResponse = await time_series_daily_adjusted.QueryAsync(
                 ticker);

            var data = m_time_series_daily_adjustedResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
            }
            else
            {

                int counter = 0;
                foreach (var timeseries in data.TimeSeries)
                {
                    if (counter < 1)
                    {
                        recentprice = float.Parse(timeseries.adjustedclose, CultureInfo.InvariantCulture.NumberFormat);
                        Debug.WriteLine(recentprice);
                    }
                }
            }

            return recentprice;
        } */

        /*
        private PlotModel CreatePieChart()
        {
            var model = new PlotModel { Title = "World population by continent" };

            var ps = new PieSeries
            {
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                AngleSpan = 360,
                StartAngle = 0
            };

            // http://www.nationsonline.org/oneworld/world_population.htm  
            // http://en.wikipedia.org/wiki/Continent  
            ps.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Americas", 929) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Asia", 4157));
            ps.Slices.Add(new PieSlice("Europe", 739) { IsExploded = false });
            ps.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = false });
            model.Series.Add(ps);
            return model;
        }
        */
    }
}
