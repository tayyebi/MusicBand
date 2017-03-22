using Symphony.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Symphony.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Leader()
        {
            return View();
        }
        public ActionResult NewsTop()
        {
            return View();
        }
        public ActionResult Adverties()
        {
            return View();
        }
        public ActionResult Concert()
        {
            return View();
        }
        public ActionResult Genus()
        {
            return View();
        }
        public ActionResult Instrument()
        {
            return View();
        }
        public ActionResult Composer()
        {
            return View();
        }
        public ActionResult Track()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
        public ActionResult Stringers()
        {
            return View();
        }
        public ActionResult Stringer(Guid Id)
        {
            ModelContainer db = new ModelContainer();
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Stringer stringer = db.Stringers.Find(Id);
            if (stringer == null)
                return HttpNotFound();

            return View(stringer);
        }
    }
}