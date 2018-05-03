using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QPricingManager.Core.DAL;
using QPricingManager.Core.Entities;
using System.Web.Mvc;

namespace QPricingManager.Web
{

    [RequireHttps]
    [Authorize]
    public class HomeController : Controller
    {

        public virtual ActionResult Index()
        {
            using (var context = new AppDbContext())
            {
                //var roleStore = new RoleStore<IdentityRole>(context);
                //var roleManager = new RoleManager<IdentityRole>(roleStore);

                //roleManager.Create(new IdentityRole("Admin"));

                //var userStore = new UserStore<AppUser, >(context);
                //var userManager = new UserManager<AppUser>(userStore);

                //var user = userManager.FindByEmail("my.email@somewhere.com");
                //userManager.AddToRole(user.Id, "Admin");
                //context.SaveChanges();
            }
            return RedirectToAction("Index", "Pricing");
        }
    }
}
