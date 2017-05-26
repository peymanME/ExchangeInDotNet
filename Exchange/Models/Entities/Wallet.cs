using Exchange.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Exchange.Models.Entities
{
    public class Wallet
    {
        
        public int Id { get; set; }

        public decimal Cash { get; set; }

        public virtual Members Members { get; set; }

    }
}