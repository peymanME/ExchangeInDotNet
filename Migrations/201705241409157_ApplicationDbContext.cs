namespace Exchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogsSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sell = c.Boolean(nullable: false),
                        Worth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateRegidter = c.DateTime(nullable: false),
                        Members_id = c.Int(nullable: false),
                        Currencies_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Members_id, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.Currencies_id, cascadeDelete: true)
                .Index(t => t.Members_id)
                .Index(t => t.Currencies_id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Parent = c.Int(),
                        Users_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Parent)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Parent)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UsersId = c.Int(nullable: false, identity: true),
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.UsersId, unique: true, name: "UserIdIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Cash = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Members_Currencies",
                c => new
                    {
                        Currencies_Members_Id = c.Int(nullable: false),
                        Members_Currencies_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Currencies_Members_Id, t.Members_Currencies_Id })
                .ForeignKey("dbo.Members", t => t.Currencies_Members_Id, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.Members_Currencies_Id, cascadeDelete: true)
                .Index(t => t.Currencies_Members_Id)
                .Index(t => t.Members_Currencies_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LogsSales", "Currencies_id", "dbo.Currencies");
            DropForeignKey("dbo.Wallets", "Id", "dbo.Members");
            DropForeignKey("dbo.Members", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.LogsSales", "Members_id", "dbo.Members");
            DropForeignKey("dbo.Members_Currencies", "Members_Currencies_Id", "dbo.Currencies");
            DropForeignKey("dbo.Members_Currencies", "Currencies_Members_Id", "dbo.Members");
            DropForeignKey("dbo.Members", "Parent", "dbo.Members");
            DropIndex("dbo.Members_Currencies", new[] { "Members_Currencies_Id" });
            DropIndex("dbo.Members_Currencies", new[] { "Currencies_Members_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Wallets", new[] { "Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Members", new[] { "Users_Id" });
            DropIndex("dbo.Members", new[] { "Parent" });
            DropIndex("dbo.LogsSales", new[] { "Currencies_id" });
            DropIndex("dbo.LogsSales", new[] { "Members_id" });
            DropTable("dbo.Members_Currencies");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Wallets");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Members");
            DropTable("dbo.LogsSales");
            DropTable("dbo.Currencies");
        }
    }
}
