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
    public class ConcertsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Concerts
        public ActionResult Index(int Id = 10)
        {
            var concerts = db.Concerts.AsParallel().Select(x => new Concert { Date = x.Date, OrderId = x.OrderId, Address = x.Address , Id = x.Id }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(concerts);
            return View(concerts);
        }

        // GET: Concerts/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Concert concert = await db.Concerts.FindAsync(id);
            if (concert == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(concert);
            return View(concert);
        }

        // GET: Concerts/Create
        public ActionResult Create()
        {
            Concert concert = new Concert();
            concert.Date = DateTime.Now.AddDays(7).ToLongDateString();
            if (Request.IsAjaxRequest())
                return PartialView(concert);
            return View(concert);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,Address,Description")] Concert concert,HttpPostedFileBase thumbnail)
        {
            concert.Thumbnail = Thumbnail.Create(thumbnail);
            if (ModelState.IsValid)
            {
                concert.Id = Guid.NewGuid();
                db.Concerts.Add(concert);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(concert);
            return View(concert);
        }

        // GET: Concerts/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Concert concert = await db.Concerts.FindAsync(id);
            if (concert == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(concert);
            return View(concert);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Address,Description")] Concert concert,HttpPostedFileBase thumbnail)
        {
            db.Entry(concert).State = EntityState.Modified;
            if (thumbnail != null)
            {
                concert.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(concert).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(concert);
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Concert concert = await db.Concerts.FindAsync(id);
            if (concert == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(concert);
            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Concert concert = await db.Concerts.FindAsync(id);
            db.Concerts.Remove(concert);
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
