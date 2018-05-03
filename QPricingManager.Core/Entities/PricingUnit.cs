using System.Collections.Generic;

namespace QPricingManager.Core.Entities
{
    public class PricingUnit : BaseEntity
    {
        public int Id { get; set; }

        public double? Price { get; set; }

        public double? Multiplier { get; set; }

        public bool IsIncluded { get; set; }

        public string Text { get; set; }

        public PricingItem PricingItem { get; set; }

        public int PricingItemId { get; set; }

        public PricingTier PricingTier { get; set; }

        public int PricingTierId { get; set; }

        public virtual PricingUnitType PricingUnitType { get; set; }

        public virtual ICollection<OfferItem> OfferItems { get; set; }
    }
}
