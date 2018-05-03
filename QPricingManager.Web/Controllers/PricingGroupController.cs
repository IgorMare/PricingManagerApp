using QPricingManager.Core.DAL;
using QPricingManager.Core.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QPricingManager.Web
{
    [Authorize]
    public class PricingGroupController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PricingGroup
        public ActionResult Index()
        {
            var pricingGroups = db.PricingGroups.Include(p => p.Pricing);
            return View(pricingGroups.ToList());
        }

        // GET: PricingGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingGroup pricingGroup = db.PricingGroups.Find(id);
            if (pricingGroup == null)
            {
                return HttpNotFound();
            }
            return View(pricingGroup);
        }

        // GET: PricingGroup/Create
        public ActionResult Create()
        {
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name");
            return View();
        }

        // POST: PricingGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,OrderInPricing,PricingId")] PricingGroup pricingGroup)
        {
            if (ModelState.IsValid)
            {
                pricingGroup.CreateTime = DateTime.Now;
                db.PricingGroups.Add(pricingGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", pricingGroup.PricingId);
            return View(pricingGroup);
        }

        // GET: PricingGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingGroup pricingGroup = db.PricingGroups.Find(id);
            if (pricingGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", pricingGroup.PricingId);
            return View(pricingGroup);
        }

        // POST: PricingGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OrderInPricing,PricingId, CreateTime")] PricingGroup pricingGroup)
        {
            if (ModelState.IsValid)
            {
                pricingGroup.UpdateTime = DateTime.Now;
                db.Entry(pricingGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", pricingGroup.PricingId);
            return View(pricingGroup);
        }

        // GET: PricingGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingGroup pricingGroup = db.PricingGroups.Find(id);
            if (pricingGroup == null)
            {
                return HttpNotFound();
            }
            return View(pricingGroup);
        }

        // POST: PricingGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PricingGroup pricingGroup = db.PricingGroups.Find(id);
            db.PricingGroups.Remove(pricingGroup);
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