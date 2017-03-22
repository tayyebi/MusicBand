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
    public class TracksController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Tracks
        public ActionResult Index(int Id = 10)
        {
            var tracks = db.Tracks.AsParallel().Select(f => new Track { OrderId = f.OrderId , Name = f.Name , Composer = f.Composer , Id = f.Id }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(tracks);
            return View(tracks);
        }

        // GET: Tracks/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Track track = await db.Tracks.FindAsync(id);
            if (track == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(track);
            return View(track);
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname");
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Text,ComposerId")] Track track,HttpPostedFileBase thumbnail)
        {
            track.Thumbnail = Thumbnail.Create(thumbnail);
            if (ModelState.IsValid)
            {
                track.Id = Guid.NewGuid();
                db.Tracks.Add(track);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname", track.ComposerId);
            if (Request.IsAjaxRequest())
                return PartialView(track);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Track track = await db.Tracks.FindAsync(id);
            if (track == null)
                return HttpNotFound();
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname", track.ComposerId);
            if (Request.IsAjaxRequest())
                return PartialView(track);
            return View(track);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Text,ComposerId")] Track track, HttpPostedFileBase thumbnail)
        {
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname", track.ComposerId);

            db.Entry(track).State = EntityState.Modified;
            if (thumbnail != null)
            {
                track.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(track).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(track);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Track track = await db.Tracks.FindAsync(id);
            if (track == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(track);
            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Track track = await db.Tracks.FindAsync(id);
            db.Tracks.Remove(track);
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
