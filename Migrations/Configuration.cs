namespace Exchange.Migrations
{
    using Exchange.Models.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Exchange.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Exchange.Models.ApplicationDbContext";
        }

        protected override void Seed(Exchange.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Currencies.AddOrUpdate(
                new Currencies() { Name = "US Dollar", Code = "USD" },
                new Currencies() { Name = "Euro", Code = "EUR" },
                new Currencies() { Name = "Swiss Franc", Code = "CHF" },
                new Currencies() { Name = "Russian ruble", Code = "RUB" },
                new Currencies() { Name = "Czech koruna", Code = "CZK" },
                new Currencies() { Name = "Pound sterling", Code = "GBP" }
            );
            //
        }
    }
}
