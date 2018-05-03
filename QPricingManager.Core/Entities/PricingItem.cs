using System.Collections.Generic;

namespace QPricingManager.Core.Entities
{
    public class PricingItem : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? OrderInGroup { get; set; }

        public virtual PricingGroup PricingGroup { get; set; }

        public int? PricingGroupId { get; set; }

        public virtual ICollection<PricingUnit> PricingUnits { get; set; }
    }
}
