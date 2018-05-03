using PagedList;
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
    public class PricingItemController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PricingItem
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            var pricingItems = db.PricingItems.Include(p => p.PricingGroup);

            if (searchString != null)
            {
                page = 1;
                pricingItems = pricingItems.Where(x => x.Name.Contains(searchString));
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(pricingItems.OrderBy(x =>x.Id).ToPagedList(pageNumber, pageSize));
        }

        // GET: PricingItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingItem pricingItem = db.PricingItems.Find(id);
            if (pricingItem == null)
            {
                return HttpNotFound();
            }
            return View(pricingItem);
        }

        // GET: PricingItem/Create
        public ActionResult Create()
        {
            ViewBag.PricingGroupId = new SelectList(db.PricingGroups, "Id", "Name");
            return View();
        }

        // POST: PricingItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,OrderInGroup,PricingGroupId")] PricingItem pricingItem)
        {
            if (ModelState.IsValid)
            {
                pricingItem.CreateTime = DateTime.Now;
                db.PricingItems.Add(pricingItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PricingGroupId = new SelectList(db.PricingGroups, "Id", "Name", pricingItem.PricingGroupId);
            return View(pricingItem);
        }

        // GET: PricingItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingItem pricingItem = db.PricingItems.Find(id);
            if (pricingItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.PricingGroupId = new SelectList(db.PricingGroups, "Id", "Name", pricingItem.PricingGroupId);
            return View(pricingItem);
        }

        // POST: PricingItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OrderInGroup,PricingGroupId, CreateTime")] PricingItem pricingItem)
        {
            if (ModelState.IsValid)
            {
                pricingItem.UpdateTime = DateTime.Now;
                db.Entry(pricingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PricingGroupId = new SelectList(db.PricingGroups, "Id", "Name", pricingItem.PricingGroupId);
            return View(pricingItem);
        }

        // GET: PricingItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricingItem pricingItem = db.PricingItems.Find(id);
            if (pricingItem == null)
            {
                return HttpNotFound();
            }
            return View(pricingItem);
        }

        // POST: PricingItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PricingItem pricingItem = db.PricingItems.Find(id);
            db.PricingItems.Remove(pricingItem);
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