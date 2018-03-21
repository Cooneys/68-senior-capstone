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
	public partial class AddMoreAsset : ContentPage
	{

        Investment newInvestment = new Investment();

        public AddMoreAsset ()
		{
            Title = App.currentInvestment.tickersymbol;
            InitializeComponent ();
		}

        async void OnDoneNewInvestmentClicked(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://web.engr.oregonstate.edu/~jonesty/AddInvestment.php");

            newInvestment.tickersymbol = App.currentInvestment.tickersymbol;
            newInvestment.pricepurchased = float.Parse(purchasepriceEntry.Text, CultureInfo.InvariantCulture.NumberFormat);
            newInvestment.numberofshares = float.Parse(numSharesEntry.Text, CultureInfo.InvariantCulture.NumberFormat);

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("portfolioname", App.currentPortfolio.Name);
            parameters.Add("tickersymbol", App.currentInvestment.tickersymbol);
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
            await Navigation.PopAsync();
        }
    }
}