using App5.Models;
using Avapi;
using Avapi.AvapiTIME_SERIES_DAILY;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddInvestmenttoPortfolio : ContentPage
	{
        Dictionary<string, string> assetTypes = new Dictionary<string, string>
        {
            { "Stock", "stock" }, { "Stock Option", "option" },

        };

        Investment newInvestment = new Investment();

        public AddInvestmenttoPortfolio ()


		{
            Picker picker = new Picker
            {
                Title = "Investment Type",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in assetTypes.Keys)
            {
                picker.Items.Add(colorName);
            }

            InitializeComponent ();
		}

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                newInvestment.type = picker.Items[selectedIndex];
            }
        }

        async void OnDoneNewInvestmentClicked(object sender, EventArgs e)
        {

            HttpClient client2 = new HttpClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/AddInvestment.php");

            newInvestment.tickersymbol = tickerPicker.Items[tickerPicker.SelectedIndex];//.Text;
            //newInvestment.pricepurchased = float.Parse(purchasepriceEntry.Text, CultureInfo.InvariantCulture.NumberFormat);
            newInvestment.numberofshares = float.Parse(numSharesEntry.Text, CultureInfo.InvariantCulture.NumberFormat);

            var postData = new List<KeyValuePair<string, string>>();
            double currentprice = 0;

            if (pricePicker.SelectedIndex == 0)
            {
                IAvapiConnection connection = AvapiConnection.Instance;
                IAvapiResponse_TIME_SERIES_DAILY m_time_series_daily_Response;

                // Set up the connection and pass the API_KEY provided by alphavantage.co
                connection.Connect("7NIMRBR8G8UB7P8C");

                // Get the TIME_SERIES_MONTHLY_ADJUSTED query object
                Int_TIME_SERIES_DAILY time_series_Daily =
                    connection.GetQueryObject_TIME_SERIES_DAILY();

                // Perform the TIME_SERIES_MONTHLY_ADJUSTED request and get the result

              
                Task <IAvapiResponse_TIME_SERIES_DAILY> tempResponse = time_series_Daily.QueryPrimitiveAsync(
                    newInvestment.tickersymbol);

                await tempResponse;

                m_time_series_daily_Response = tempResponse.Result;
                //m_time_series_daily_Response = await time_series_Daily.QueryPrimitiveAsync(
                   // newInvestment.tickersymbol);

                var data = m_time_series_daily_Response.Data;
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
                        if (counter < 1)
                        {

                            currentprice = double.Parse(timeseries.close, CultureInfo.InvariantCulture.NumberFormat);
                            Debug.WriteLine(currentprice);
                                //add raw prices and dates to arrays


                        }
                        counter = counter + 1;
                    }
                }
                Debug.WriteLine(currentprice.ToString());
                postData.Add(new KeyValuePair<string, string>("pricepurchased", currentprice.ToString()));

            }

            else
            {
                postData.Add(new KeyValuePair<string, string>("pricepurchased", purchasepriceEntry.Text.ToString()));
            }
            //var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("portfolioname", App.currentPortfolio.Name.ToString()));
            postData.Add(new KeyValuePair<string, string>("tickersymbol", tickerPicker.Items[tickerPicker.SelectedIndex].ToString()));
            postData.Add(new KeyValuePair<string, string>("numshares", numSharesEntry.Text.ToString()));
            //postData.Add(new KeyValuePair<string, string>("pricepurchased", purchasepriceEntry.Text.ToString()));

            HttpContent content = new FormUrlEncodedContent(postData);

            var response = await client2.PostAsync(uri, content);

            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count()-2]);
            Navigation.InsertPageBefore(new PortfolioDetails(App.currentPortfolio), Navigation.NavigationStack[Navigation.NavigationStack.Count()-1]);
            await Navigation.PopAsync();
        }
    }
}