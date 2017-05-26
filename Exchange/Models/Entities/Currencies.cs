using Exchange.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Entities
{
    public class Currencies:EntityBase
    {

        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Members> Members { get; set; }
        public virtual ICollection<LogsSales> LogsSales { get; set; }
    }
}