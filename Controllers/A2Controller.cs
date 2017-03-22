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
    public class A2Controller : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: A2
        public ActionResult Index(int Id = 10)
        {
            var a2 = db.A2.AsParallel().Select(m => new A2 { Track = m.Track, A1 = m.A1 }).OrderByDescending(m => m.Id).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(a2);
            return View(a2);
        }

        // GET: A2/Create
        public ActionResult Create()
        {
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name");
            ViewBag.A1Id = new SelectList(db.A1, "Id", "IS");
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: A2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TrackId,A1Id")] A2 a2)
        {
            if (ModelState.IsValid)
            {
                db.A2.Add(a2);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name");
            ViewBag.A1Id = new SelectList(db.A1, "Id", "IS");
            if (Request.IsAjaxRequest())
                return PartialView(a2);
            return View(a2);
        }

        // GET: A2/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            A2 a2 = await db.A2.FindAsync(id);
            if (a2 == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(a2);
            return View(a2);
        }

        // POST: A2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            A2 a2 = await db.A2.FindAsync(id);
            db.A2.Remove(a2);
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
