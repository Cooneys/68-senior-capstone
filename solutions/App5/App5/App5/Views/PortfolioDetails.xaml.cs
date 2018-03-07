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

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioDetails : ContentPage
    {
        Data.RestService restService = new Data.RestService();
        List<Microcharts.Entry> entries = new List<Microcharts.Entry>
        {

            new Microcharts.Entry(200)
            {
                Color = SKColor.Parse("#000000"),
                Label = "january",
                ValueLabel = "200"
            },
            
            new Microcharts.Entry(300)
            {
                Color = SKColor.Parse("#00BFFF"),
                Label = "january1",
                ValueLabel = "300"
            },
            new Microcharts.Entry(400)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "january2",
                ValueLabel = "4+00"
            },
        };

        public PlotModel PieModel { get; set; }

        public PortfolioDetails (Portfolio selectedPortfolio)
		{
            PieModel = CreatePieChart();
            InitializeComponent ();
            //PieModel = CreatePieChart();
            App.currentPortfolio = selectedPortfolio;
            //MyChart.Chart = new DonutChart() { Entries = entires };
            GetPortfolioDetails();
            var chart = new PieChart() { Entries = entries };
            this.chartView.Chart = chart;
		}

        public async void GetPortfolioDetails()
        {
            Task<List<Investment>> investmentListT = restService.FetchPortfolioDetails(App.currentPortfolio);
            await investmentListT;
            //restService.FetchPortfolios(App.currentUser);
            List<Investment> investmentList = investmentListT.Result;
            //var selectedPortfolio = sender as Portfolio;
            //investmentList = restService.FetchPortfolioDetails(App.currentPortfolio);
            
            if (investmentList != null)

            {
                investmentListView.ItemsSource = investmentList;
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
    }
}