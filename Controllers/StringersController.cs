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
    public class StringersController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Stringers
        public ActionResult Index(int Id = 10)
        {
            var stringers = db.Stringers.AsParallel().Select(h => new Stringer { OrderId = h.OrderId, Id = h.Id, FirstName = h.FirstName, LastName = h.LastName, BirthYear = h.BirthYear }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(stringers);
            return View(stringers);
        }

        // GET: Stringers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Stringer stringer = await db.Stringers.FindAsync(id);
            if (stringer == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(stringer);
            return View(stringer);
        }

        // GET: Stringers/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,BirthYear,Text")] Stringer stringer,HttpPostedFileBase thumbnail)
        {
            stringer.Thumbnail = Thumbnail.Create(thumbnail);
            if (ModelState.IsValid)
            {
                stringer.Id = Guid.NewGuid();
                db.Stringers.Add(stringer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(stringer);
            return View(stringer);
        }

        // GET: Stringers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Stringer stringer = await db.Stringers.FindAsync(id);
            if (stringer == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(stringer);
            return View(stringer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,BirthYear,Text")] Stringer stringer, HttpPostedFileBase thumbnail)
        {
            db.Entry(stringer).State = EntityState.Modified;
            if (thumbnail != null)
            {
                stringer.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(stringer).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(stringer);
            return View(stringer);
        }

        // GET: Stringers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Stringer stringer = await db.Stringers.FindAsync(id);
            if (stringer == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(stringer);
            return View(stringer);
        }

        // POST: Stringers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Stringer stringer = await db.Stringers.FindAsync(id);
            db.Stringers.Remove(stringer);
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
