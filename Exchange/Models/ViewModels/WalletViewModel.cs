using Exchange.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exchange.Models.ViewModels
{
    public class WalletViewModel
    {
        private readonly List<Currencies> _currencies;

        public WalletViewModel()
        {
            using (var db = new ApplicationDbContext()) { 
                _currencies = (from o in db.Currencies select o).ToList();
            }
        }


        [Required]
        public int[] SelectedCurrencyId { get; set; }
        public IEnumerable<SelectListItem> Currencies {
            get { return new MultiSelectList(_currencies, "Id", "Name"); }           
        }

        [Required]
        [Display (Name = "Default cash amount :")]
        [Range(100, float.MaxValue, ErrorMessage = "Please enter valid your cash more than 100 zł")]
        public decimal Cash { get; set; }
    }
}