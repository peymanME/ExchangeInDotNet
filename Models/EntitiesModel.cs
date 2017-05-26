namespace Exchange.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Exchange.Models.Entities;

    public partial class EntitiesModel : DbContext
    {
        public EntitiesModel()
            : base("name=EntitiesModel")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users", "dbo");
            modelBuilder.Entity<Users>()
                .ToTable("Users", "dbo");

            modelBuilder.Entity<Users>()
                        .HasOptional(u => u.Members)
                        .WithRequired(m => m.Users);

            modelBuilder.Entity<Members>()
                        .HasOptional(u => u.Wallet)
                        .WithRequired(w => w.Members);

            modelBuilder.Entity<Members>()
                        .HasMany<Currencies>(c => c.Currencies)
                        .WithMany(u => u.Members)
                        .Map(uc =>
                        {
                            uc.MapLeftKey("Currencies_Users_Id");
                            uc.MapRightKey("Users_Currencies_Id");
                            uc.ToTable("Users_Currencies");
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

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<LogsSales> LogsSales { get; set; }
        public virtual DbSet<Wallet> Wallet
        {
            get; set;
        }
    }
}
