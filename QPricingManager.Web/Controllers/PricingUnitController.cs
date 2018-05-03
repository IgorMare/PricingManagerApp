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
    public class PricingUnitController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PricingUnit
        public ActionResult Index()
        {
            var pricingUnits = db.PricingUnits.Include(p => p.PricingItem).Include(p => p.PricingTier);
            return View(pricingUnits.ToList());
        }

        // GET: PricingUnit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingUnit pricingUnit = db.PricingUnits.Find(id);
            if (pricingUnit == null)
            {
                return HttpNotFound();
            }
            return View(pricingUnit);
        }

        // GET: PricingUnit/Create
        public ActionResult Create()
        {
            ViewBag.PricingItemId = new SelectList(db.PricingItems, "Id", "Name");
            ViewBag.PricingTierId = new SelectList(db.PricingTiers, "Id", "Name");
            return View();
        }

        // POST: PricingUnit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Multiplier,IsIncluded,Text,PricingItemId,PricingTierId,PricingUnitType")] PricingUnit pricingUnit)
        {
            if (ModelState.IsValid)
            {
                pricingUnit.CreateTime = DateTime.Now;
                db.PricingUnits.Add(pricingUnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PricingItemId = new SelectList(db.PricingItems, "Id", "Name", pricingUnit.PricingItemId);
            ViewBag.PricingTierId = new SelectList(db.PricingTiers, "Id", "Name", pricingUnit.PricingTierId);
            return View(pricingUnit);
        }

        // GET: PricingUnit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingUnit pricingUnit = db.PricingUnits.Find(id);
            if (pricingUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.PricingItemId = new SelectList(db.PricingItems, "Id", "Name", pricingUnit.PricingItemId);
            ViewBag.PricingTierId = new SelectList(db.PricingTiers, "Id", "Name", pricingUnit.PricingTierId);
            return View(pricingUnit);
        }

        // POST: PricingUnit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Multiplier,IsIncluded,Text,PricingItemId,PricingTierId,PricingUnitType, CreateTime")] PricingUnit pricingUnit)
        {
            if (ModelState.IsValid)
            {
                pricingUnit.UpdateTime = DateTime.Now;
                db.Entry(pricingUnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PricingItemId = new SelectList(db.PricingItems, "Id", "Name", pricingUnit.PricingItemId);
            ViewBag.PricingTierId = new SelectList(db.PricingTiers, "Id", "Name", pricingUnit.PricingTierId);
            return View(pricingUnit);
        }

        // GET: PricingUnit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingUnit pricingUnit = db.PricingUnits.Find(id);
            if (pricingUnit == null)
            {
                return HttpNotFound();
            }
            return View(pricingUnit);
        }

        // POST: PricingUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PricingUnit pricingUnit = db.PricingUnits.Find(id);
            db.PricingUnits.Remove(pricingUnit);
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