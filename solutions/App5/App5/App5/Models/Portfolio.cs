using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Models
{
    public class Portfolio
    {
        //public List<string> Owners { get; set; }
        public string Name { get; set; }
        public int TotalValue { get; set; }
        public float returns { get; set; }
        public double sharperatio { get; set; }
        public double expectedreturn { get; set; }
        public double alpha { get; set; }

        public Portfolio() { }

        public Portfolio(/*List<string> owners,*/ string name, int totalValue, float Returns, double Sharperatio, double Expectedreturn, double Alpha)
        {
            //this.Owners = owners;
            this.Name= name;
            this.TotalValue = totalValue;
            this.returns = Returns;
            this.sharperatio = Sharperatio;
            this.expectedreturn = Expectedreturn;
            this.alpha = Alpha;
        }
    }
}
