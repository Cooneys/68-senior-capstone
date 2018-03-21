using App5.Models;
using Newtonsoft.Json.Linq;
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

        public AddInvestmenttoPortfolio()


        {
            //List<string> tickerlist = new List<string>();
            //tickerlist = await FetchPossibleTickers();
            Picker picker = new Picker
            {
                Title = "Investment Type",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in assetTypes.Keys)
            {
                picker.Items.Add(colorName);
            }

            InitializeComponent();
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
            parameters.Add("numshares", purchasepriceEntry.Text);
            parameters.Add("pricepurchased", numSharesEntry.Text);

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
       
        async Task<List<string>> FetchPossibleTickers()
        {
            HttpClient client = new HttpClient();
            List<string> tickerlist = new List<string>();

            string Url = "http://web.engr.oregonstate.edu/~jonesty/api.php/Investments";

            string content = await client.GetStringAsync(Url);
            JArray tickers = JArray.Parse(content);//JsonConvert.DeserializeObject<List<Portfolio>>(content);

            //Debug.WriteLine(portfolios[0][);

            //data = JArray.Parse(Encoding.UTF8.GetString(portfolios));
            //_portfolios = new ObservableCollection<Portfolio>(portfolios);


            if (tickers.Count == 0)
            {
                Debug.WriteLine("null returned");
                return null;
            }

            else
            {
                for (var i = 0; i < tickers.Count; i++)

                {
                    tickerlist.Add((string)tickers[i]["tickersymbol"]);

                }
                return tickerlist;
            }
            
        }
    }
}