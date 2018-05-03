using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QPricingManager.Core.DAL;
using QPricingManager.Core.Entities;
using System.Globalization;

namespace QPricingManager.Web.Controllers
{
    public class OfferController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Offer
        public ActionResult Index()
        {
            var offers = db.Offers.Include(o => o.Pricing);
            return View(offers.ToList());
        }

        // GET: Offer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: Offer/Create
        public ActionResult Create()
        {
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name");
            return View();
        }

        // POST: Offer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,OfferFor,OfferForAddress,PricingId")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", offer.PricingId);
            return View(offer);
        }

        [HttpPost]
        public ActionResult CreateFromPricing(Offer offerModel, FormCollection collection)
        {
            var offer = new Offer();
            offer.Name = collection["Offer Name"];
            offer.Description = collection["Offer Description"];
            offer.OfferFor = collection["Offer For"];
            offer.OfferForAddress = collection["Offer For Address"];
            offer.PricingId = Convert.ToInt16(collection["PricingId"]);
            db.Offers.Add(offer);

            foreach (string key in collection.Keys)
            {
                if(key.Contains("multiplier"))
                {
                    var value = collection[key];

                    var parts = key.Split('-').ToList().LastOrDefault();

                    var offerItem = new OfferItem();
                    if (value != string.Empty)
                    {
                        CultureInfo cult = new CultureInfo("de-DE");

                        offerItem.Value = double.Parse(value, cult);
                    }
                    else
                    {
                        offerItem.Value = 0;
                    }
                    offerItem.OfferId = offer.Id;
                    offerItem.PricingUnitId = Convert.ToInt16(parts);
                    db.OfferItems.Add(offerItem);
                }             
            }
            db.SaveChanges();
            return View(offer);
        }

        // GET: Offer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", offer.PricingId);
            return View(offer);
        }

        // POST: Offer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,OfferFor,OfferForAddress,PricingId")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PricingId = new SelectList(db.Pricings, "Id", "Name", offer.PricingId);
            return View(offer);
        }

        // GET: Offer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
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
