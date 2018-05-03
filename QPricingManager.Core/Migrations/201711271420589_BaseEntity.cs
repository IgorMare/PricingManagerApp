namespace QPricingManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PricingGroup", "CreateTime", c => c.DateTime());
            AddColumn("dbo.PricingGroup", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.Pricing", "CreateTime", c => c.DateTime());
            AddColumn("dbo.Pricing", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.PricingTier", "CreateTime", c => c.DateTime());
            AddColumn("dbo.PricingTier", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.PricingUnit", "CreateTime", c => c.DateTime());
            AddColumn("dbo.PricingUnit", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.PricingItem", "CreateTime", c => c.DateTime());
            AddColumn("dbo.PricingItem", "UpdateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PricingItem", "UpdateTime");
            DropColumn("dbo.PricingItem", "CreateTime");
            DropColumn("dbo.PricingUnit", "UpdateTime");
            DropColumn("dbo.PricingUnit", "CreateTime");
            DropColumn("dbo.PricingTier", "UpdateTime");
            DropColumn("dbo.PricingTier", "CreateTime");
            DropColumn("dbo.Pricing", "UpdateTime");
            DropColumn("dbo.Pricing", "CreateTime");
            DropColumn("dbo.PricingGroup", "UpdateTime");
            DropColumn("dbo.PricingGroup", "CreateTime");
        }
    }
}
