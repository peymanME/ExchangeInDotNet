using Exchange.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Entities
{
    public class LogsSales:EntityBase
    {
        public bool Sell { get; set; }
        public decimal Worth { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateRegidter { get; set; }

        public int Members_id { get; set; }
        public int Currencies_id { get; set; }

        public virtual Members Members { get; set; }
        public virtual Currencies Currencies { get; set; }

    }
}