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
    public class FoldersController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Folders
        public ActionResult Index(int Id = 10)
        {
            var folders = db.Folders.AsParallel().Select(m => new Folder { Parent = m.Parent, Id = m.Id, Name = m.Name }).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(folders);
            return View(folders);
        }

        // GET: Folders/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: Folders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Parent,Name")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                db.Folders.Add(folder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(folder);
            return View(folder);
        }

        // GET: Folders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Folder folder = await db.Folders.FindAsync(id);
            if (folder == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(folder);
            return View(folder);
        }

        // POST: Folders/Edit/5
        [ValidateInput(false)]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Parent,Name")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(folder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(folder);
            return View(folder);
        }

        // GET: Folders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Folder folder = await db.Folders.FindAsync(id);
            if (folder == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(folder);
            return View(folder);
        }

        // POST: Folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Folder folder = await db.Folders.FindAsync(id);
            db.Folders.Remove(folder);
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
