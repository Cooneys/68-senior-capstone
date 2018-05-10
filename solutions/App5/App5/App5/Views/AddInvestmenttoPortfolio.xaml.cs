using App5.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            newInvestment.tickersymbol = tickerPicker.Item[tickerPicker.SelectedIndex].Text;
            newInvestment.pricepurchased = float.Parse(purchasepriceEntry.Text, CultureInfo.InvariantCulture.NumberFormat);
            newInvestment.numberofshares = float.Parse(numSharesEntry.Text, CultureInfo.InvariantCulture.NumberFormat);

            var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("portfolioname", App.currentPortfolio.Name.ToString()));
            postData.Add(new KeyValuePair<string, string>("tickersymbol", tickerPicker.Item[tickerPicker.SelectedIndex].ToString()));
            postData.Add(new KeyValuePair<string, string>("numshares", numSharesEntry.Text.ToString()));
            postData.Add(new KeyValuePair<string, string>("pricepurchased", purchasepriceEntry.Text.ToString()));

            HttpContent content = new FormUrlEncodedContent(postData);

            var response = await client2.PostAsync(uri, content);

            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count()-2]);
            Navigation.InsertPageBefore(new PortfolioDetails(App.currentPortfolio), Navigation.NavigationStack[Navigation.NavigationStack.Count()-1]);
            await Navigation.PopAsync();
        }
    }
}