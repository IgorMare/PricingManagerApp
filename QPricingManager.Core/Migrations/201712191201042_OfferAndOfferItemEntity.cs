namespace QPricingManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OfferAndOfferItemEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OfferItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        PricingUnitId = c.Int(),
                        OfferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offer", t => t.OfferId, cascadeDelete: true)
                .ForeignKey("dbo.PricingUnit", t => t.PricingUnitId)
                .Index(t => t.PricingUnitId)
                .Index(t => t.OfferId);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OfferFor = c.String(),
                        OfferForAddress = c.String(),
                        PricingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pricing", t => t.PricingId, cascadeDelete: true)
                .Index(t => t.PricingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OfferItem", "PricingUnitId", "dbo.PricingUnit");
            DropForeignKey("dbo.Offer", "PricingId", "dbo.Pricing");
            DropForeignKey("dbo.OfferItem", "OfferId", "dbo.Offer");
            DropIndex("dbo.Offer", new[] { "PricingId" });
            DropIndex("dbo.OfferItem", new[] { "OfferId" });
            DropIndex("dbo.OfferItem", new[] { "PricingUnitId" });
            DropTable("dbo.Offer");
            DropTable("dbo.OfferItem");
        }
    }
}
