using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace QPricingManager.Core.Entities
{
    public class AppRole : IdentityRole<int, AppUserRole>
    {
        public AppRole() : base() { }
        public AppRole(string name) : base() { }
        // extra properties here 
    }
}