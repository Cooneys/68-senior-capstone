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
using System.Net;
using System.Globalization;
using Microcharts;
using System.Collections.Specialized;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetDetails : ContentPage
    {
        List<Microcharts.Entry> entries = new List<Microcharts.Entry>();

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
            var answer = await DisplayAlert("Delete", "Do you want to delete " + App.currentPortfolio.Name + " " + App.currentInvestment.tickersymbol, "Yes", "No");
            if (answer)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://web.engr.oregonstate.edu/~cooneys/capstone/deleteInvestment.php");
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("portfolioname", App.currentPortfolio.Name);
                parameters.Add("tickersymbol", App.currentInvestment.tickersymbol);
                client.UploadValuesAsync(uri, parameters);

                await Navigation.PopAsync();
            }
        }

        private async void GetAssetDetails()
        {
            DisplayPricingData(App.currentInvestment.tickersymbol);


            //CompanyInfo info = App.RestService.FetchCompanyDetails(App.currentInvestment.tickersymbol).Result;
            Task <CompanyInfo>  AssetDetailsA = App.RestService.FetchCompanyDetails(App.currentInvestment.tickersymbol);
            await AssetDetailsA;
            CompanyInfo AssetDetails = AssetDetailsA.Result;


            App.currentInvestment.tickersymbol = "0";

            if (AssetDetails != null)
            {
                
                    this.freeCF.Text = string.Format("{0} billion USD ", AssetDetails.FreeCashFlow);
                    this.rOA.Text = string.Format("{0}%", AssetDetails.ReturnOnAssets);
                    this.rOE.Text = string.Format("{0}%", AssetDetails.ReturnOnEquity);
                    this.IT.Text = string.Format("{0}%", AssetDetails.InventoryTurnover);
                    this.AT.Text = string.Format("{0}%", AssetDetails.AssetTurnover);
                    this.EBITM.Text = string.Format("{0}%", AssetDetails.EBTMargin);
                    this.TCA.Text = string.Format("{0} thousand USD ", AssetDetails.TotalCurrentAssets);
                    this.RT.Text = string.Format("{0}%", AssetDetails.ReceivablesTurnover);
                    this.NI.Text = string.Format("{0} billion USD ", AssetDetails.NetIncome);
                    this.EPS.Text = string.Format("{0} billion USD ", AssetDetails.EarningsPerShare);
                    this.REV.Text = string.Format("{0} billion USD ", AssetDetails.Revenue);
                    this.IC.Text = string.Format("{0}%", AssetDetails.InterestCoverage);
                    this.TR.Text = string.Format("{0}%", AssetDetails.TaxRate);

            }


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
            entries.Reverse();
            var chart = new LineChart() { Entries = entries, LineMode = LineMode.Straight, LineSize = 8, PointSize =18, LabelTextSize = 25};
            this.chartView.Chart = chart;
        }
    }
}