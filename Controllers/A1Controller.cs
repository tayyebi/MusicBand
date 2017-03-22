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
    public class A1Controller : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: A1
        public ActionResult Index(int Id = 10)
        {
            var a1 = db.A1.AsParallel().Select(x => new A1 { Instrument = x.Instrument, Id = x.Id, Stringer = x.Stringer }).OrderByDescending(m => m.Id).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(a1);
            return View(a1);
        }

        // GET: A1/Create
        public ActionResult Create()
        {
            ViewBag.InstrumentId = new SelectList(db.Instruments, "Id", "Name");
            ViewBag.StringerId = new SelectList(db.Stringers, "Id", "Fullname");
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: A1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,InstrumentId,StringerId")] A1 a1)
        {
            if (ModelState.IsValid)
            {
                db.A1.Add(a1);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.InstrumentId = new SelectList(db.Instruments, "Id", "Name");
            ViewBag.StringerId = new SelectList(db.Stringers, "Id", "Fullname");
            if (Request.IsAjaxRequest())
                return PartialView(a1);
            return View(a1);
        }

        // GET: A1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            A1 a1 = await db.A1.FindAsync(id);
            if (a1 == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(a1);
            return View(a1);
        }

        // POST: A1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            A1 a1 = await db.A1.FindAsync(id);
            db.A1.Remove(a1);
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
