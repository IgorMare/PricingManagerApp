namespace QPricingManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Identity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        AppRole_Id = c.Int(),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppRole", t => t.AppRole_Id)
                .ForeignKey("dbo.AppUser", t => t.AppUser_Id)
                .Index(t => t.AppRole_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AppUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AppUserLogin",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AppUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUser", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUserRole", "AppUser_Id", "dbo.AppUser");
            DropForeignKey("dbo.AppUserLogin", "AppUser_Id", "dbo.AppUser");
            DropForeignKey("dbo.AppUserClaim", "AppUser_Id", "dbo.AppUser");
            DropForeignKey("dbo.AppUserRole", "AppRole_Id", "dbo.AppRole");
            DropIndex("dbo.AppUserLogin", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserClaim", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserRole", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserRole", new[] { "AppRole_Id" });
            DropTable("dbo.AppUserLogin");
            DropTable("dbo.AppUserClaim");
            DropTable("dbo.AppUser");
            DropTable("dbo.AppUserRole");
            DropTable("dbo.AppRole");
        }
    }
}
