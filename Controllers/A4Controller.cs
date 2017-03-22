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
    public class A4Controller : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: A4
        public async Task<ActionResult> Index(int Id = 10)
        {
            var a4 = db.A4.Select(m => new A4 { Leader = m.Leader, Concert = m.Concert }).OrderByDescending(m => m.Id).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(a4);
            return View(a4);
        }

        // GET: A4/Create
        public ActionResult Create()
        {
            ViewBag.LeaderId = new SelectList(db.Leaders, "Id", "Fullname");
            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date");
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: A4/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,LeaderId,ConcertId")] A4 a4)
        {
            if (ModelState.IsValid)
            {
                db.A4.Add(a4);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LeaderId = new SelectList(db.Tracks, "Id", "Fullname");
            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date");
            if (Request.IsAjaxRequest())
                return PartialView(a4);
            return View(a4);
        }

        // GET: A4/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            A4 a4 = await db.A4.FindAsync(id);
            if (a4 == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(a4);
            return View(a4);
        }

        // POST: A4/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            A4 a4 = await db.A4.FindAsync(id);
            db.A4.Remove(a4);
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
