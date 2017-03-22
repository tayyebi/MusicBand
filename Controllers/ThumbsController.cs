using Symphony.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Symphony.Controllers
{
    public class ThumbsController : Controller
    {
        private ModelContainer db = new ModelContainer();
        public ActionResult Pic(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Pics.AsParallel().Select(m => new { Thumb = m.Thumb, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumb, "image/png");
        }
        public ActionResult News(int? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.News.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
        public ActionResult Instrument(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Instruments.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
        public ActionResult Stringer(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Stringers.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
        public ActionResult Track(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Tracks.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
        public ActionResult Concert(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Concerts.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
        public ActionResult Leader(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Leaders.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
        public ActionResult Genus(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Genus.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
        public ActionResult Composer(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            return File(db.Composers.AsParallel().Select(m => new { Thumbnail = m.Thumbnail, Id = m.Id }).Where(m => m.Id == Id).FirstOrDefault().Thumbnail, "image/png");
        }
    }
}