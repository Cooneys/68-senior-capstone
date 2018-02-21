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

        public Portfolio() { }

        public Portfolio(/*List<string> owners,*/ string name, int totalValue)
        {
            //this.Owners = owners;
            this.Name= name;
            this.TotalValue = totalValue;
        }
    }
}
