using System;

namespace QPricingManager.Core.Entities
{
    public abstract class BaseEntity
    {
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

    }
}