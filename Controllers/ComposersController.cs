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
    public class ComposersController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Composers
        public ActionResult Index(int Id = 10)
        {
            var composer = db.Composers.AsParallel().Select(m => new Composer { Fullname = m.Fullname, Id = m.Id }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(composer);
            return View(composer);
        }

        // GET: Composers/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Composer composer = await db.Composers.FindAsync(id);
            if (composer == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(composer);
            return View(composer);
        }

        // GET: Composers/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Fullname,Text")] Composer composer,HttpPostedFileBase thumbnail)
        {
            composer.Thumbnail = Thumbnail.Create(thumbnail);
            if (ModelState.IsValid)
            {
                composer.Id = Guid.NewGuid();
                db.Composers.Add(composer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(composer);
            return View(composer);
        }

        // GET: Composers/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Composer composer = await db.Composers.FindAsync(id);
            if (composer == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(composer);
            return View(composer);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Fullname,Text")] Composer composer,HttpPostedFileBase thumbnail)
        {
            db.Entry(composer).State = EntityState.Modified;
            if (thumbnail != null)
            {
                composer.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(composer).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(composer);
            return View(composer);
        }

        // GET: Composers/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Composer composer = await db.Composers.FindAsync(id);
            if (composer == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(composer);
            return View(composer);
        }

        // POST: Composers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Composer composer = await db.Composers.FindAsync(id);
            db.Composers.Remove(composer);
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
