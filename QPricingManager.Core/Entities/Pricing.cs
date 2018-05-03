using System.Collections.Generic;

namespace QPricingManager.Core.Entities
{
    public class Pricing : BaseEntity
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<PricingGroup> PricingGroups { get; set; }

        public virtual ICollection<PricingTier> PricingTiers { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}