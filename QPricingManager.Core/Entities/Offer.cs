using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPricingManager.Core.Entities
{
    public class Offer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OfferFor { get; set; }
        
        public string OfferForAddress { get; set; }

        public virtual Pricing Pricing { get; set; }

        public int PricingId { get; set; }

        public virtual ICollection<OfferItem> OfferItems { get; set; }
    }
}
