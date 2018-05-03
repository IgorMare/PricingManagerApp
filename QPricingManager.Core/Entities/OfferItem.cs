using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPricingManager.Core.Entities
{
    public class OfferItem
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public virtual PricingUnit PricingUnit { get; set; }

        public int? PricingUnitId { get; set; }

        public virtual Offer Offer { get; set; }

        public int OfferId { get; set; }
    }
}
