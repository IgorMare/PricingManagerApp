using QPricingManager.Core.DAL;
using QPricingManager.Core.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QPricingManager.Web.Controllers
{
    [Authorize]
    public class PricingTierController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PricingTier
        public ActionResult Index()
        {
            var pricingTiers = db.PricingTiers.Include(p => p.Pricing);
            return View(pricingTiers.ToList());
        }

        // GET: PricingTier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingTier pricingTier = db.PricingTiers.Find(id);
            if (pricingTier == null)
            {
                return HttpNotFound();
            }
            return View(pricingTier);
        }

        // GET: PricingTier/Create
        public ActionResult Create()
        {
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name");
            return View();
        }

        // POST: PricingTier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,OrderInPricing,PricingId")] PricingTier pricingTier)
        {
            if (ModelState.IsValid)
            {
                pricingTier.CreateTime = DateTime.Now;
                db.PricingTiers.Add(pricingTier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", pricingTier.PricingId);
            return View(pricingTier);
        }

        // GET: PricingTier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingTier pricingTier = db.PricingTiers.Find(id);
            if (pricingTier == null)
            {
                return HttpNotFound();
            }
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", pricingTier.PricingId);
            return View(pricingTier);
        }

        // POST: PricingTier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,OrderInPricing,PricingId, CreateTime")] PricingTier pricingTier)
        {
            if (ModelState.IsValid)
            {
                pricingTier.UpdateTime = DateTime.Now;
                db.Entry(pricingTier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", pricingTier.PricingId);
            return View(pricingTier);
        }

        // GET: PricingTier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingTier pricingTier = db.PricingTiers.Find(id);
            if (pricingTier == null)
            {
                return HttpNotFound();
            }
            return View(pricingTier);
        }

        // POST: PricingTier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PricingTier pricingTier = db.PricingTiers.Find(id);
            db.PricingTiers.Remove(pricingTier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}