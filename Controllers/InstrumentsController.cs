using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Symphony.Models;
using Symphony.App_Code;

namespace Symphony.Controllers
{
    [Secure]
    public class InstrumentsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Instruments
        public async Task<ActionResult> Index(int Id = 10)
        {
            var instruments = db.Instruments.AsParallel().Select(m => new Instrument { Name = m.Name, Genu = m.Genu, GenusId = m.GenusId, Id = m.Id, OrderId = m.OrderId }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(instruments);
            return View(instruments);
        }

        // GET: Instruments/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Instrument instrument = await db.Instruments.FindAsync(id);
            if (instrument == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(instrument);
            return View(instrument);
        }

        // GET: Instruments/Create
        public ActionResult Create()
        {
            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title");
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Text,GenusId")] Instrument instrument,HttpPostedFileBase thumbnail)
        {
            instrument.Thumbnail = Thumbnail.Create(thumbnail);
            if (ModelState.IsValid)
            {
                instrument.Id = Guid.NewGuid();
                db.Instruments.Add(instrument);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title");
            if (Request.IsAjaxRequest())
                return PartialView(instrument);
            return View(instrument);
        }

        // GET: Instruments/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title");
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Instrument instrument = await db.Instruments.FindAsync(id);
            if (instrument == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(instrument);
            return View(instrument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Text,GenusId")] Instrument instrument, HttpPostedFileBase thumbnail)
        {
            db.Entry(instrument).State = EntityState.Modified;
            if (thumbnail != null)
            {
                instrument.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(instrument).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title");

            if (Request.IsAjaxRequest())
                return PartialView(instrument);
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Instrument instrument = await db.Instruments.FindAsync(id);
            if (instrument == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(instrument);
            return View(instrument);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Instrument instrument = await db.Instruments.FindAsync(id);
            db.Instruments.Remove(instrument);
            await db.SaveChangesAsync();
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
