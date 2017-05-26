using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Exchange.Models.Entities;
using System;

namespace Exchange.Models
{
    public class ApplicationDbContext : IdentityDbContext<Users>, IDisposable
    {
        public ApplicationDbContext()
            : base("ExchangeContext", throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationDbContext>(null);
            //Database.SetInitializer(new CurrencyDBInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users", "dbo");
            modelBuilder.Entity<Users>()
                .ToTable("Users", "dbo");

            modelBuilder.Entity<Users>()
                        .HasKey(e => e.UsersId);

            modelBuilder.Entity<Users>()
                        .HasOptional(u => u.Members)
                        .WithRequired(m => m.Users);

            modelBuilder.Entity<Members>()
                        .HasOptional(m => m.Wallet)
                        .WithRequired(w => w.Members);

            modelBuilder.Entity<Members>()
                        .HasMany<Currencies>(c => c.Currencies)
                        .WithMany(m => m.Members)
                        .Map(cm =>
                        {
                            cm.MapLeftKey("Currencies_Members_Id");
                            cm.MapRightKey("Members_Currencies_Id");
                            cm.ToTable("Members_Currencies");
                        });

            modelBuilder.Entity<Members>()
                        .HasMany<LogsSales>(l => l.LogsSales)
                        .WithRequired(l => l.Members)
                        .HasForeignKey(l => l.Members_id);

            modelBuilder.Entity<Currencies>()
                        .HasMany<LogsSales>(c => c.LogsSales)
                        .WithRequired(l => l.Currencies)
                        .HasForeignKey(l => l.Currencies_id);

            modelBuilder.Entity<Members>()
                        .HasMany<Members>(u => u.Childeren)
                        .WithOptional(u => u.Parents)
                        .HasForeignKey(u => u.Parent);
        }

        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<LogsSales> LogsSales { get; set; }
        public virtual DbSet<Wallet> Wallet { get; set; }
    }
}