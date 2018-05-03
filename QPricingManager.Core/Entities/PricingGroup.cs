using System.Collections.Generic;

namespace QPricingManager.Core.Entities
{
    public class PricingGroup : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? OrderInPricing { get; set; }

        public virtual Pricing Pricing { get; set; }

        public int PricingId { get; set; }

        public virtual ICollection<PricingItem> PricingItems { get; set; }
    }
}
