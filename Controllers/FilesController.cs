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
    public class FilesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Files
        public ActionResult Index(int Id = 10)
        {
            var files = db.Files.AsParallel().Select(m => new File { Folder = m.Folder, Type = m.Type, Lenght = m.Lenght, Name = m.Name }).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(files);
            return View(files);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.FolderId = new SelectList(db.Folders, "Id", "Name");
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,FolderId")] File file, HttpPostedFileBase Parvandeh)
        {
            var _Bytes = new byte[Parvandeh.ContentLength];
            Parvandeh.InputStream.Read(_Bytes, 0, Parvandeh.ContentLength);
            file.Bytes = _Bytes;
            file.Lenght = Parvandeh.ContentLength;
            file.Type = Parvandeh.ContentType;

            if (ModelState.IsValid)
            {
                file.Name = Guid.NewGuid();
                db.Files.Add(file);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FolderId = new SelectList(db.Folders, "Id", "Name", file.FolderId);
            if (Request.IsAjaxRequest())
                return PartialView(file);
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            File file = await db.Files.FindAsync(id);
            if (file == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(file);
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            File file = await db.Files.FindAsync(id);
            db.Files.Remove(file);
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
