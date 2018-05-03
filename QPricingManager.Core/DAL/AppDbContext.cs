using Microsoft.AspNet.Identity.EntityFramework;
using QPricingManager.Core.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace QPricingManager.Core.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppDbContext() : base("QPM.Test")
        {
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }

        public DbSet<PricingGroup> PricingGroups { get; set; }
        public DbSet<PricingItem> PricingItems { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<PricingTier> PricingTiers { get; set; }
        public DbSet<PricingUnit> PricingUnits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<AppUser>().ToTable("AppUser").HasKey(x => x.Id);
            modelBuilder.Entity<AppUserClaim>().ToTable("AppUserClaim").HasKey(x => x.Id);
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRole").HasKey(x => x.UserId);
            modelBuilder.Entity<AppRole>().ToTable("AppRole").HasKey(x => x.Id);
            modelBuilder.Entity<AppUserLogin>().ToTable("AppUserLogin").HasKey(x=>x.UserId); 


        }
    }
}