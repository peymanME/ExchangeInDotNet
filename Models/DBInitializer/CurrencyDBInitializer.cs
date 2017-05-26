using Exchange.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace Exchange.Models.DBInitializer
{
    public class CurrencyDBInitializer: DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            IList<Currencies> defaultCurrencies = new List<Currencies>();

            defaultCurrencies.Add(new Currencies() { Name = "US Dollar"     , Code = "USD" });
            defaultCurrencies.Add(new Currencies() { Name = "Euro"          , Code = "EUR" });
            defaultCurrencies.Add(new Currencies() { Name = "Swiss Franc"   , Code = "CHF" });
            defaultCurrencies.Add(new Currencies() { Name = "Russian ruble" , Code = "RUB" });
            defaultCurrencies.Add(new Currencies() { Name = "Czech koruna"  , Code = "CZK" });
            defaultCurrencies.Add(new Currencies() { Name = "Pound sterling", Code = "GBP" });

            foreach (Currencies cur in defaultCurrencies)
                context.Currencies.Add(cur);

            base.Seed(context);
        }
    }
}