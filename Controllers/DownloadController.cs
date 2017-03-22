using Symphony.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphony.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        private ModelContainer db = new ModelContainer();
        public ActionResult File(Guid? Id)
        {
            var file = db.Files.Select(m => new File { Bytes = m.Bytes , Type = m.Type , Name = m.Name }).Where(y => y.Name == Id).FirstOrDefault();
            if (Id == null) return HttpNotFound();
            return File(file.Bytes, file.Type, file.Name.ToString());
        }
    }
}