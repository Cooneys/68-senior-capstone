using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Models;
using Avapi;
using Avapi.AvapiTIME_SERIES_MONTHLY_ADJUSTED;
using OxyPlot;
using System.Globalization;
using Microcharts;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetDetails : ContentPage
    {
        List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
        //public PlotModel PieModel { get; set; }
        public AssetDetails(Investment selectedInvestment)
        {
            Title = selectedInvestment.tickersymbol;
            InitializeComponent();
            App.currentInvestment = selectedInvestment;
            GetAssetDetails();
        }

        async void OnInvestmentButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMoreAsset());
        }

        async void OnTrashButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Do you want to delete this Asset?", "Yes", "No");
            if (answer)
            {

            }
        }

        private void GetAssetDetails()
        {
            //string iname = this.FindByName<Label>("investmentName").Text;
            //iname = App.currentInvestment.tickersymbol;
            //this.FindByName<Label>("investmentName").Text = App.currentInvestment.tickersymbol;
            //this.FindByName<Label>("investmentCount").Text = App.currentInvestment.numberofshares.ToString();
            //this.FindByName<Label>("investmentPrice").Text = App.currentInvestment.pricepurchased.ToString();
            DisplayPricingData(App.currentInvestment.tickersymbol);
        }

        private async void DisplayPricingData(string ticker)
        {
            List<double> priceslastyear = new List<double>();
            List<string> dateslastyear = new List<string>();
            // Creating the connection object
            IAvapiConnection connection = AvapiConnection.Instance;
            IAvapiResponse_TIME_SERIES_MONTHLY_ADJUSTED m_time_series_monthly_adjustedResponse;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect("7NIMRBR8G8UB7P8C");

            // Get the TIME_SERIES_MONTHLY_ADJUSTED query object
            Int_TIME_SERIES_MONTHLY_ADJUSTED time_series_monthly_adjusted =
                connection.GetQueryObject_TIME_SERIES_MONTHLY_ADJUSTED();

            // Perform the TIME_SERIES_MONTHLY_ADJUSTED request and get the result
            m_time_series_monthly_adjustedResponse = await time_series_monthly_adjusted.QueryPrimitiveAsync(
                 ticker);

            var data = m_time_series_monthly_adjustedResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
            }
            else
            {

                int counter = 0;
                foreach (var timeseries in data.TimeSeries)
                {
                    //Grab 1 year worth of months data
                    if (counter < 12)
                    {
                        //add raw prices and dates to arrays
                        priceslastyear.Add(double.Parse(timeseries.adjustedclose, CultureInfo.InvariantCulture.NumberFormat));
                        Debug.WriteLine(float.Parse(timeseries.adjustedclose, CultureInfo.InvariantCulture.NumberFormat));
                        Debug.WriteLine(timeseries.DateTime);
                        Microcharts.Entry tempEntry = new Microcharts.Entry(float.Parse(timeseries.adjustedclose, CultureInfo.InvariantCulture.NumberFormat))
                        {
                            Label = timeseries.DateTime,
                            ValueLabel = timeseries.adjustedclose//Color = investmentList[i].color.ToSKColor()
                        };
                        entries.Add(tempEntry);
                    }
                    counter = counter + 1;
                }
            }
            var chart = new LineChart() { Entries = entries, LineMode = LineMode.Straight, LineSize = 8, PointSize =18, LabelTextSize = 25};
            this.chartView.Chart = chart;
        }
    }
}