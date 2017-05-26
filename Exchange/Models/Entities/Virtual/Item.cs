using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Entities.Virtual
{
    public class Item
    {
        public string name { get; set; }
        public string code { get; set; }
        public int unit { get; set; }
        public decimal purchasePrice { get; set; }
        public decimal sellPrice { get; set; }
        public decimal averagePrice { get; set; }
        public decimal amount { get; set; }

    }
}