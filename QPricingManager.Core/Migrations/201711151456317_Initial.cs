namespace QPricingManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PricingGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OrderInPricing = c.Int(),
                        PricingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pricing", t => t.PricingId, cascadeDelete: true)
                .Index(t => t.PricingId);
            
            CreateTable(
                "dbo.Pricing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PricingTier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        OrderInPricing = c.Int(nullable: false),
                        PricingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pricing", t => t.PricingId, cascadeDelete: true)
                .Index(t => t.PricingId);
            
            CreateTable(
                "dbo.PricingUnit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(),
                        Multiplier = c.Double(),
                        IsIncluded = c.Boolean(nullable: false),
                        Text = c.String(),
                        PricingItemId = c.Int(nullable: false),
                        PricingTierId = c.Int(nullable: false),
                        PricingUnitType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PricingItem", t => t.PricingItemId, cascadeDelete: true)
                .ForeignKey("dbo.PricingTier", t => t.PricingTierId, cascadeDelete: true)
                .Index(t => t.PricingItemId)
                .Index(t => t.PricingTierId);
            
            CreateTable(
                "dbo.PricingItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OrderInGroup = c.Int(),
                        PricingGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PricingGroup", t => t.PricingGroupId)
                .Index(t => t.PricingGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PricingUnit", "PricingTierId", "dbo.PricingTier");
            DropForeignKey("dbo.PricingUnit", "PricingItemId", "dbo.PricingItem");
            DropForeignKey("dbo.PricingItem", "PricingGroupId", "dbo.PricingGroup");
            DropForeignKey("dbo.PricingTier", "PricingId", "dbo.Pricing");
            DropForeignKey("dbo.PricingGroup", "PricingId", "dbo.Pricing");
            DropIndex("dbo.PricingItem", new[] { "PricingGroupId" });
            DropIndex("dbo.PricingUnit", new[] { "PricingTierId" });
            DropIndex("dbo.PricingUnit", new[] { "PricingItemId" });
            DropIndex("dbo.PricingTier", new[] { "PricingId" });
            DropIndex("dbo.PricingGroup", new[] { "PricingId" });
            DropTable("dbo.PricingItem");
            DropTable("dbo.PricingUnit");
            DropTable("dbo.PricingTier");
            DropTable("dbo.Pricing");
            DropTable("dbo.PricingGroup");
        }
    }
}
