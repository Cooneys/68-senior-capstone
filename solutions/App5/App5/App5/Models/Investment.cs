using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App5.Models
{
    public class Investment
    {

        public string tickersymbol { get; set; }
        public float numberofshares { get; set; }
        public float pricepurchased { get; set; }
        public float totalvalue { get; set; }
        public float recentprice { get; set; }
        public string type { get; set; }
        public Color color { get; set; }
        public float percentChangeDaily { get; set; }
        public float percentChangeTotal { get; set; }
        public Color changeColorDaily { get; set; }
        public Color changeColorTotal { get; set; }

        public Investment() { }

        public Investment(string tckr, float numshares, float purchase, string typeT)
        {
            this.tickersymbol = tckr;
            this.numberofshares = numshares;
            this.pricepurchased = purchase;
            this.type = typeT;
        }
    }
}
