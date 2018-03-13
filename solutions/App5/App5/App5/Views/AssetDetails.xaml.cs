using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Models;

namespace App5.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssetDetails : ContentPage
	{
		public AssetDetails (Investment selectedInvestment)
		{
            Title = selectedInvestment.tickersymbol;
            InitializeComponent();
            App.currentInvestment = selectedInvestment;
            GetAssetDetails();
		}

        private void GetAssetDetails()
        {
            //string iname = this.FindByName<Label>("investmentName").Text;
            //iname = App.currentInvestment.tickersymbol;
            this.FindByName<Label>("investmentName").Text = App.currentInvestment.tickersymbol;
            this.FindByName<Label>("investmentCount").Text = App.currentInvestment.numberofshares.ToString();
            this.FindByName<Label>("investmentPrice").Text = App.currentInvestment.pricepurchased.ToString();
        }
    }
}