using App5.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
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
            WebClient client = new WebClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/AddInvestment.php");

            newInvestment.tickersymbol = tickerEntry.Text;
            newInvestment.pricepurchased = float.Parse(purchasepriceEntry.Text, CultureInfo.InvariantCulture.NumberFormat);
            newInvestment.numberofshares = float.Parse(numSharesEntry.Text, CultureInfo.InvariantCulture.NumberFormat);

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("portfolioname", App.currentPortfolio.Name);
            parameters.Add("tickersymbol", tickerEntry.Text);
            parameters.Add("numshares", numSharesEntry.Text);
            parameters.Add("pricepurchased", purchasepriceEntry.Text);

            client.UploadValuesAsync(uri, parameters);

            /*var rootPage = Navigation.NavigationStack.FirstOrDefault();
            if (rootPage != null)
            {
                Navigation.InsertPageBefore(new PortfolioDetails(App.currentPortfolio), Navigation.NavigationStack.First());
                await Navigation.PopToRootAsync();
            }*/
            // await Navigation.PushAsync(new PortfolioDetails(App.currentPortfolio));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count()-2]);
            Navigation.InsertPageBefore(new PortfolioDetails(App.currentPortfolio), Navigation.NavigationStack[Navigation.NavigationStack.Count()-1]);
            await Navigation.PopAsync();
        }
    }
}