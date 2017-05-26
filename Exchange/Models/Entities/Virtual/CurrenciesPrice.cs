using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Entities.Virtual
{
    public class CurrenciesPrice
    {
        public CurrenciesPrice()
        {
            this.Items = new List<Item>();
        }
        public DateTime publicationDate { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public decimal cash { get; set; }


    }
}