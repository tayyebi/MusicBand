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
    public class AdvertiesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Adverties
        public ActionResult Index(int Id = 10)
        {
            var adv = db.Adverties.AsParallel().Select(m => new Adverties{ Id = m.Id , Title = m.Title , Url = m.Url }).OrderByDescending(m => m.Id).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(adv);
            return View(adv);
        }

        // GET: Adverties/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Adverties adverties = await db.Adverties.FindAsync(id);
            if (adverties == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(adverties);
            return View(adverties);
        }

        // GET: Adverties/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Url")] Adverties adverties, HttpPostedFileBase Image)
        {
            var _Bytes = new byte[Image.ContentLength];
            Image.InputStream.Read(_Bytes, 0, Image.ContentLength);

                adverties.Image = _Bytes;

            if (ModelState.IsValid)
            {
                db.Adverties.Add(adverties);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(adverties);
            return View(adverties);
        }

        // GET: Adverties/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Adverties adverties = await db.Adverties.FindAsync(id);
            if (adverties == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(adverties);
            return View(adverties);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Url")] Adverties adverties, HttpPostedFileBase Image)
        {

            db.Entry(adverties).State = EntityState.Modified;
            if (Image != null)
            {
                var _Bytes = new byte[Image.ContentLength];
                Image.InputStream.Read(_Bytes, 0, Image.ContentLength);
                adverties.Image = _Bytes;
            }
            else
            {
                db.Entry(adverties).Property(x => x.Image).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            if (Request.IsAjaxRequest())
                return PartialView(adverties);
            return View(adverties);
        }

        // GET: Adverties/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Adverties adverties = await db.Adverties.FindAsync(id);
            if (adverties == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(adverties);
            return View(adverties);
        }

        // POST: Adverties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Adverties adverties = await db.Adverties.FindAsync(id);
            db.Adverties.Remove(adverties);
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
