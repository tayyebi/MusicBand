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
    public class LeadersController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Leaders
        public ActionResult Index(int Id = 10)
        {
            var leaders = db.Leaders.AsParallel().Select(m => new Leader { OrderId = m.OrderId, FirstName = m.FirstName, LastName = m.LastName, Id = m.Id, BirthYear = m.BirthYear }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(leaders);
            return View(leaders);
        }

        // GET: Leaders/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Leader leader = await db.Leaders.FindAsync(id);
            if (leader == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(leader);
            return View(leader);
        }

        // GET: Leaders/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,BirthYear,Text")] Leader leader,HttpPostedFileBase thumbnail)
        {
            leader.Thumbnail = Thumbnail.Create(thumbnail);
            if (ModelState.IsValid)
            {
                leader.Id = Guid.NewGuid();
                db.Leaders.Add(leader);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(leader);
            return View(leader);
        }

        // GET: Leaders/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Leader leader = await db.Leaders.FindAsync(id);
            if (leader == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
                return PartialView(leader);
            return View(leader);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,BirthYear,Text")] Leader leader, HttpPostedFileBase thumbnail)
        {
            db.Entry(leader).State = EntityState.Modified;
            if (thumbnail != null)
            {
                leader.Thumbnail = Thumbnail.Create(thumbnail);
            }
            else
            {
                db.Entry(leader).Property(x => x.Thumbnail).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(leader);
            return View(leader);
        }

        // GET: Leaders/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Leader leader = await db.Leaders.FindAsync(id);
            if (leader == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(leader);
            return View(leader);
        }

        // POST: Leaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Leader leader = await db.Leaders.FindAsync(id);
            db.Leaders.Remove(leader);
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
