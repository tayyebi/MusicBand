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
    public class A3Controller : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: A3
        public ActionResult Index(int Id = 10)
        {
            var a3 = db.A3.AsParallel().Select(m => new A3 { Track = m.Track, Concert = m.Concert }).OrderByDescending(m => m.Id).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(a3);
            return View(a3);
        }

        // GET: A3/Create
        public ActionResult Create()
        {
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name");
            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date");
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: A3/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TrackId,ConcertId")] A3 a3)
        {
            if (ModelState.IsValid)
            {
                db.A3.Add(a3);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name");
            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date");
            if (Request.IsAjaxRequest())
                return PartialView(a3);
            return View(a3);
        }

        // GET: A3/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            A3 a3 = await db.A3.FindAsync(id);
            if (a3 == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(a3);
            return View(a3);
        }

        // POST: A3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            A3 a3 = await db.A3.FindAsync(id);
            db.A3.Remove(a3);
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
