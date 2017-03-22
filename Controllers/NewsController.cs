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
    public class NewsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: News
        public ActionResult Index(int Id = 10)
        {
            var news = db.News.AsParallel().Select(k => new News{ Id = k.Id , Title = k.Title , Date = k.Date }).OrderByDescending(m => m.Id).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(news);
            return View(news);
        }

        // GET: News/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            News news = await db.News.FindAsync(id);
            if (news == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(news);
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Abstract,Text")] News news, HttpPostedFileBase thumbnail)
        {
            news.Thumbnail = Thumbnail.Create(thumbnail);
            news.Date = DateTime.Now.ToLongDateString();
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(news);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            News news = await db.News.FindAsync(id);
            if (news == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(news);
            return View(news);
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Title,Abstract,Text")] News news, HttpPostedFileBase thumbnail)
        {
            db.Entry(news).State = EntityState.Modified;
            if (thumbnail != null)
            {
                news.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(news).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(news);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            News news = await db.News.FindAsync(id);
            if (news == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(news);
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
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
