using OfficeOpenXml;
using OfficeOpenXml.Style;
using QPricingManager.Core.DAL;
using QPricingManager.Core.Entities;
using QPricingManager.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web.Mvc;

namespace QPricingManager.Web
{
    [Authorize]
    public class PricingController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Pricing
        public ActionResult Index()
        {
            return View(db.Pricings.ToList());
        }

        // GET: Pricing/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pricing pricing = db.Pricings.Find(id);
            if (pricing == null)
            {
                return HttpNotFound();
            }
            return View(pricing);
        }

        // GET: Pricing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pricing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Pricing pricing)
        {
            if (ModelState.IsValid)
            {
                pricing.CreateTime = DateTime.Now;
                db.Pricings.Add(pricing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pricing);
        }

        // GET: Pricing/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pricing pricing = db.Pricings.Find(id);
            if (pricing == null)
            {
                return HttpNotFound();
            }
            return View(pricing);
        }

        // POST: Pricing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description, CreateTime")] Pricing pricing)
        {
            if (ModelState.IsValid)
            {
                pricing.UpdateTime = DateTime.Now;
                db.Entry(pricing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pricing);
        }

        // GET: Pricing/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pricing pricing = db.Pricings.Find(id);
            if (pricing == null)
            {
                return HttpNotFound();
            }
            return View(pricing);
        }

        // POST: Pricing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pricing pricing = db.Pricings.Find(id);
            db.Pricings.Remove(pricing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult GetPdf(string tableHtml, int id)
        {
            Pricing pricing = db.Pricings.Find(id);
            tableHtml = tableHtml.Replace("<span class=\"glyphicon glyphicon-ok\"></span>", "+");
            tableHtml = tableHtml.Replace("<span class=\"glyphicon glyphicon-remove\"></span>", "-");
            string fileName = "pricingDownload-" + DateTime.Now.ToString("u").Replace(':', '-').Replace('.', '-').Replace(' ', '-') + ".pdf";
            var result = PdfConvertor.ConvertReport(tableHtml, fileName);
            return File(result, MediaTypeNames.Application.Pdf, fileName);
        }

        // GET: Pricing Index to Excel export

        public ActionResult PricingIndexExcelExport()
        {
            var pricings = db.Pricings.ToList();

            string folderPath = String.Format(@"~\App_Data\Content\Excel");

            if (!System.IO.Directory.Exists(Server.MapPath(folderPath)))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            string fileName = "pricingDownload-" + DateTime.Now.ToString("u").Replace(':', '-').Replace('.', '-').Replace(',', '-') + ".xlsx";
            string path = Server.MapPath(folderPath + "\\" + fileName);

            FileStream file = new FileStream(path, FileMode.Create);

            ExcelPackage pck = new ExcelPackage();
            //pitati za smisao linije 144
            ExcelWorksheet sheet = pck.Workbook.Worksheets.Add("Pricings");

            sheet.Cells[1, 1].Style.Font.Bold = true;
            sheet.Cells[1, 2].Style.Font.Bold = true;
            sheet.Cells[1, 3].Style.Font.Bold = true;
            sheet.Cells[1, 4].Style.Font.Bold = true;
            sheet.Cells[1, 1].Value = "Name";
            sheet.Cells[1, 2].Value = "Description";
            sheet.Cells[1, 3].Value = "Tiers";
            sheet.Cells[1, 4].Value = "Number of Tiers";

            var i = 2;

            foreach (var pricing in pricings)
            {
                List<PricingTier> tiers = db.PricingTiers.Where(x => x.PricingId == pricing.Id).ToList();

                sheet.Cells[i, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                if (i % 2 == 0)
                {
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
                    sheet.Cells[i, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[i, 1].Style.Fill.BackgroundColor.SetColor(colFromHex);
                }
                sheet.Cells[i, 1].Value = pricing.Name;

                sheet.Cells[i, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                if (i % 2 == 0)
                {
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
                    sheet.Cells[i, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[i, 2].Style.Fill.BackgroundColor.SetColor(colFromHex);
                }
                sheet.Cells[i, 2].Value = pricing.Description;

                sheet.Cells[i, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                if (i % 2 == 0)
                {
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
                    sheet.Cells[i, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[i, 3].Style.Fill.BackgroundColor.SetColor(colFromHex);
                }
                sheet.Cells[i, 3].Value = string.Join(",", tiers.Select(x => x.Name));

                sheet.Cells[i, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                if (i % 2 == 0)
                {
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
                    sheet.Cells[i, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[i, 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                }
                sheet.Cells[i, 4].Value = pricing.PricingTiers.Count().ToString();
                i++;
            }

            var maxColumnNumber = 5;

            for (int k = 1; k < maxColumnNumber; k++)
            {
                sheet.Column(k).AutoFit();
            }

            pck.SaveAs(file);
            file.Close();

            HttpContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            return File(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult PricingExcelExport(int id)
        {
            Pricing pricing = db.Pricings.Find(id);

            string folderPath = String.Format(@"~\App_Data\Content\Excel");
            if (!System.IO.Directory.Exists(Server.MapPath(folderPath)))
                System.IO.Directory.CreateDirectory(Server.MapPath(folderPath));

            string fileName = "pricingDownload-" + DateTime.Now.ToString("u").Replace(':', '-').Replace('.', '-').Replace(' ', '-') + ".xlsx";
            string path = Server.MapPath(folderPath + "\\" + fileName);

            FileStream file = new FileStream(path, FileMode.Create);
            ExcelPackage pck = new ExcelPackage();

            ExcelWorksheet sheet = pck.Workbook.Worksheets.Add("Pricings");

            var column = 2;

            foreach (var pricingTier in pricing.PricingTiers)
            {
                sheet.Cells[1, column].Style.Font.Bold = true;
                sheet.Cells[1, column].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[1, column++].Value = pricingTier.Name;
            }

            int i = 2;

            foreach (var pricingGroup in pricing.PricingGroups)
            {
                sheet.Cells[i, 1].Style.Font.Bold = true;
                sheet.Cells[i, 1].Value = pricingGroup.Name;
                sheet.Cells[i, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                sheet.Cells[i, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);

                i++;

                foreach (var pricingItem in pricingGroup.PricingItems)
                {
                    sheet.Cells[i, 1].Value = pricingItem.Name;

                    column = 2;

                    foreach (var pricingUnit in pricingItem.PricingUnits)
                    {
                        sheet.Cells[i, column].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        if (pricingUnit.PricingUnitType == 0)
                        {
                            sheet.Cells[i, column++].Value = pricingUnit.Text;
                        }
                        else if (pricingUnit.IsIncluded)
                        {
                            sheet.Cells[i, column++].Value = "+";
                        }
                        else
                        {
                            sheet.Cells[i, column++].Value = "-";
                        }
                    }

                    i++;
                }
            }

            var maxRowNumber = i;

            var secondcolumn = 2;

            foreach (var pricingTier in pricing.PricingTiers)
            {
                sheet.Cells[maxRowNumber, 1].Style.Font.Bold = true;
                sheet.Cells[maxRowNumber, 1].Value = "Price";
                sheet.Cells[maxRowNumber, secondcolumn].Style.Font.Bold = true;
                sheet.Cells[maxRowNumber, secondcolumn++].Value = pricingTier.Price;
            }

            var maxColumnNumber = column;

            for (int k = 1; k < maxColumnNumber; k++)
            {
                sheet.Column(k).AutoFit();
            }

            pck.SaveAs(file);
            file.Close();

            HttpContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            return File(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
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