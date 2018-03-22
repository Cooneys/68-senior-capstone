using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Models
{
    public class CompanyInfo
    {
        public string tickersymbol { get; set; }
        public double currentprice { get; set; }

        public CompanyInfo() { }

        public CompanyInfo(string tckr, double currprice)
        {
            this.tickersymbol = tckr;
            this.currentprice = currprice;

        }
    }

}
