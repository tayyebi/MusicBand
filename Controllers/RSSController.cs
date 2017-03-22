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
using System.Linq.Expressions;

namespace Symphony.Controllers
{
    public class RSSController : Controller
    {
        private ModelContainer db = new ModelContainer();
        #region Gallery
        public ActionResult Gallery(int Id = 10)
        {
            var pic = db.Pics.AsParallel().Select(x => new Picture { Title = x.Title, Id = x.Id, Description = x.Description }).OrderBy(x => new Random().Next()).Take(Id).ToList();
            if (pic.Count == 0)
                return HttpNotFound();
            return View(pic);
        }
        public ActionResult Picture(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            var bytes = db.Pics.AsParallel().Select(x => new Picture { Id = x.Id, Bytes = x.Bytes }).Where(x => x.Id == Id).FirstOrDefault().Bytes;
            return File(bytes, "image/png");
        }
        #endregion
        #region Adverties
        public ActionResult AdvPic(int Id)
        {
            if (Id == 0) 
                return HttpNotFound();
            var bytes = db.Adverties.AsParallel().Select(x => new Adverties { Image = x.Image, Id = x.Id }).Where(x => x.Id == Id).FirstOrDefault().Image;
            return File(bytes, "image/png");
        }
        public ActionResult Adverties()
        {
            var adv = db.Adverties.OrderByDescending(m => m.Id).Take(10).ToList();
            if (adv.Count == 0)
                return HttpNotFound();
            return View(adv);
        }
        #endregion
        #region Genus
        public ActionResult Genus()
        {
            var genus = db.Genus.AsParallel().Select(x => new Genus { Id = x.Id, Thumbnail = x.Thumbnail, Title = x.Title }).ToList();
            if (genus.Count == 0)
                return HttpNotFound();
            return View(genus);
        }
        public ActionResult Genus_Details(Guid? Id = null)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var genus = db.Genus.AsParallel().Select(x => new Genus { Id = x.Id, Title = x.Title, Text = x.Text }).Where(x => x.Id == Id).FirstOrDefault();
            if (genus == null)
                return HttpNotFound();
            return View(genus);
        }
        #endregion
        #region Track
        public ActionResult Tracks(int Id = 10)
        {
            var tracks = db.Tracks.Include("Composer").AsParallel().Select(x => new Track { Composer = new Composer { Fullname = x.Composer.Fullname }, Id = x.Id, Name = x.Name, ComposerId = x.ComposerId }).OrderBy(x => new Random().Next()).Take(Id).ToList();
            return View(tracks);
        }
        public ActionResult Track_Details(Guid? Id = null)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var track = db.Tracks.Include("Composer").AsParallel().Select(x => new Track {Text = x.Text, Composer = new Composer { Fullname = x.Composer.Fullname }, Id = x.Id, Name = x.Name, ComposerId = x.ComposerId }).Where(x => x.Id == Id).FirstOrDefault();
            if (track == null)
                return HttpNotFound();

            return View(track);
        }
        #endregion
        #region Concert
        public ActionResult Concerts()
        {
            var concert = db.Concerts.AsParallel().Select(x => new Concert { Id = x.Id, Date = x.Date }).OrderByDescending(m => m.OrderId).Take(3).ToList();
            if (concert.Count == 0)
                return HttpNotFound();
            return View(concert);
        }
        public ActionResult Concert_Details(Guid? Id = null)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Concert concert = db.Concerts.AsParallel().Select(x => new Concert { Id = x.Id , Date = x.Date , Address = x.Address , Description = x.Description }).Where(x => x.Id == Id).FirstOrDefault();
            if (concert == null)
                return HttpNotFound();

            return View(concert);
        }
        #endregion
        #region Composer
        public ActionResult Composer_Details(Guid? Id = null)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Composer composer = db.Composers.AsParallel().Select(x => new Composer { Fullname = x.Fullname, Text = x.Text, Id = x.Id }).Where(x => x.Id == Id).FirstOrDefault();
            if (composer == null)
                return HttpNotFound();

            return View(composer);
        }
        #endregion
        #region Leader
        public ActionResult Leader()
        {
            var leader = db.Leaders.AsParallel().Select(x => new Leader { Id = x.Id, Text = x.Text, FirstName = x.FirstName, LastName = x.LastName, A4 = x.A4, BirthYear = x.BirthYear, OrderId = x.OrderId }).OrderByDescending(m => m.OrderId).FirstOrDefault();
            if (leader == null)
                return HttpNotFound();
            else
                return View(leader);
        }
        #endregion
        #region Stringer
        public ActionResult Stringers(Guid? Id = null)
        {
            if (Id != null)
            {
                List<Stringer> stringers = new List<Stringer>();
                foreach (var item in db.A1.AsParallel().Select(x => new A1 { StringerId = x.StringerId, InstrumentId = x.InstrumentId }).Where(m => m.InstrumentId == Id))
                {
                    try
                    {
                        var stringerItem = db.Stringers.AsParallel().Select(x => new Stringer { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, BirthYear = x.BirthYear }).Where(x => x.Id == item.StringerId).FirstOrDefault();
                        stringers.Add(stringerItem);
                    }
                    catch
                    {

                    }
                }
                if (stringers.Count == 0)
                    return HttpNotFound();
                return View(stringers);
            }
            else
            {
                var stringers = db.Stringers.AsParallel().Select(x => new Stringer { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, BirthYear = x.BirthYear }).ToList();
                if (stringers.Count == 0)
                    return HttpNotFound();
                return View(stringers);
            }
        }
        public ActionResult Stringer_Details(Guid? Id = null)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Stringer stringer = db.Stringers.AsParallel().Select(x => new Stringer { FirstName = x.FirstName, LastName = x.LastName, BirthYear = x.BirthYear, Text = x.Text, Id = x.Id }).Where(x => x.Id == Id).FirstOrDefault();
            if (stringer == null)
                return HttpNotFound();

            return View(stringer);
        }
        #endregion
        #region News
        public ActionResult News_Top(int Id = 10)
        {
            try
            {
                return View(db.News.AsParallel().Select(x => new News { Id = x.Id, Title = x.Title, Date = x.Date, Abstract = x.Abstract }).OrderByDescending(m => m.Id).Take(Id).ToList());
            }
            catch
            {
                return HttpNotFound();
            }
        }
        public ActionResult News_Details(int Id = 0)
        {
            if (Id == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            News news = db.News.AsParallel().Select(x => new News { Id = x.Id, Title = x.Title, Text = x.Text, Date = x.Date }).Where(x => x.Id == Id).FirstOrDefault();
            if (news == null)
                return HttpNotFound();

            return View(news);
        }
        #endregion
        #region Instruments
        public ActionResult Instrument_Details(Guid? Id)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Instrument instrument = db.Instruments.Include("Genu").AsParallel().Select(x => new Instrument { Name = x.Name, GenusId = x.GenusId, Genu = new Genus { Title = x.Genu.Title }, Text = x.Text, Id = x.Id }).Where(x => x.Id == Id).FirstOrDefault();
            if (instrument == null)
                return HttpNotFound();

            return View(instrument);
        }
        public ActionResult Instruments(Guid Id)
        {
            try
            {
                var instrument = db.Instruments.Where(m => m.GenusId == Id).OrderByDescending(m => m.Id).AsParallel().Select(x => new Instrument { Id = x.Id, GenusId = x.GenusId, Name = x.Name }).ToList();
                return View(instrument);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        #endregion
    }
}