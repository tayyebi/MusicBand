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
    public class GenusController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Genus
        public ActionResult Index(int Id = 10)
        {
            var genus = db.Genus.AsParallel().Select(j => new Genus { Id = j.Id, OrderId = j.OrderId, Title = j.Title }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(genus);
            return View(genus);
        }

        // GET: Genus/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Genus genus = await db.Genus.FindAsync(id);
            if (genus == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(genus);
            return View(genus);
        }

        // GET: Genus/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: Genus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Text")] Genus genus,HttpPostedFileBase thumbnail)
        {
            genus.Thumbnail = Thumbnail.Create(thumbnail);
            if (ModelState.IsValid)
            {
                genus.Id = Guid.NewGuid();
                db.Genus.Add(genus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(genus);
            return View(genus);
        }

        // GET: Genus/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Genus genus = await db.Genus.FindAsync(id);
            if (genus == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(genus);
            return View(genus);
        }

        // POST: Genus/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Text")] Genus genus,HttpPostedFileBase thumbnail)
        {
            db.Entry(genus).State = EntityState.Modified;
            if (thumbnail != null)
            {
                genus.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(genus).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            if (Request.IsAjaxRequest())
                return PartialView(genus);
            return View(genus);
        }

        // GET: Genus/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Genus genus = await db.Genus.FindAsync(id);
            if (genus == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(genus);
            return View(genus);
        }

        // POST: Genus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Genus genus = await db.Genus.FindAsync(id);
            db.Genus.Remove(genus);
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
