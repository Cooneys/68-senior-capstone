using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Models
{
    public class CompanyInfo
    {
        public string tickersymbol { get; set; }
        public double currentprice { get; set; }
        public double InventoryTurnover { get; set; }
        public double ReturnOnAssets { get; set; }
        public double ReturnOnEquity { get; set; }
        public double EBTMargin { get; set; }
        public double AssetTurnover { get; set; }
        public double ReceivablesTurnover { get; set; }
        public double NetIncome { get; set; }
        public double EarningsPerShare { get; set; }
        public double InterestCoverage { get; set; }
        public double TotalCurrentAssets { get; set; }
        public double TaxRate {get; set;}
        public double FreeCashFlow { get; set; }
        public double Revenue { get; set; }



        public CompanyInfo() { }

        public CompanyInfo(string tckr, double currprice)
        {
            this.tickersymbol = tckr;
            this.currentprice = currprice;

        }
    }

}
