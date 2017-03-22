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
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Symphony.Controllers
{
    [Secure]
    public class PicsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: Pics
        public ActionResult Index(int Id = 10)
        {
            var pics = db.Pics.AsParallel().Select(k => new Picture { Id = k.Id, OrderId = k.OrderId, Title = k.Title, Description = k.Description }).OrderByDescending(m => m.OrderId).Take(Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView( pics);
            return View( pics);
        }

        // GET: Pics/Create
        public ActionResult Create()
        {
            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date");
            ViewBag.StringerId = new SelectList(db.Stringers, "Id", "Fullname");
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name");
            ViewBag.LeaderId = new SelectList(db.Leaders, "Id", "Fullname");
            ViewBag.InstrumentId = new SelectList(db.Instruments, "Id", "Name");
            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title");
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname");
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title");

            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        static public Bitmap ResizeImage(Image ImageToResize, Size ResizedImageSize)
        {
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            int originalWidth = ImageToResize.Width;
            int originalHeight = ImageToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)ResizedImageSize.Width / (float)originalWidth);
            nPercentH = ((float)ResizedImageSize.Height / (float)originalHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((ResizedImageSize.Width -
                              (originalWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((ResizedImageSize.Height -
                              (originalHeight * nPercent)) / 2);
            }

            int destWidth = (int)(originalWidth * nPercent);
            int destHeight = (int)(originalHeight * nPercent);

            Bitmap bmp = new Bitmap(ResizedImageSize.Width, ResizedImageSize.Height, PixelFormat.Format32bppArgb);

            bmp.SetResolution(ImageToResize.HorizontalResolution, ImageToResize.VerticalResolution);
            using (Graphics Graphic = Graphics.FromImage(bmp))
            {
                Graphic.CompositingQuality = CompositingQuality.HighQuality;
                Graphic.Clear(Color.Transparent);
                Graphic.CompositingMode = CompositingMode.SourceCopy;
                Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Graphic.DrawImage(
                    ImageToResize,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, originalWidth, originalHeight),
                    GraphicsUnit.Pixel
                    );
                return bmp;
            }
        }

        static public byte[] ConvertImageToByte(Bitmap ImageToConvert)
        {
            MemoryStream ms = new MemoryStream();
            ImageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description")] Picture picture,HttpPostedFileBase Pic)
        {
            ModelContainer db = new ModelContainer();

            var _Bytes = new byte[Pic.ContentLength];
            Pic.InputStream.Read(_Bytes, 0, Pic.ContentLength);
            picture.Bytes = _Bytes;
            Image _Image;
            using (var ms = new MemoryStream(_Bytes))
            {
                _Image = Image.FromStream(ms);
            }
            picture.Thumb = ConvertImageToByte(ResizeImage(_Image, new Size(200, 100)));
            picture.Id = Guid.NewGuid();

            if (ModelState.IsValid)
            {
                db.Pics.Add(picture);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date", picture.ConcertId);
            ViewBag.StringerId = new SelectList(db.Stringers, "Id", "FirstName", picture.StringerId);
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name", picture.TrackId);
            ViewBag.LeaderId = new SelectList(db.Leaders, "Id", "FirstName", picture.LeaderId);
            ViewBag.InstrumentId = new SelectList(db.Instruments, "Id", "Name", picture.InstrumentId);
            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title", picture.GenusId);
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname", picture.ComposerId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title", picture.NewsId);

            if (Request.IsAjaxRequest())
                return PartialView(picture);
            return View(picture);
        }


        // GET: Pics/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Picture picture = await db.Pics.FindAsync(id);
            if (picture == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(picture);
            return View(picture);
        }

        // POST: Pics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Picture picture = await db.Pics.FindAsync(id);
            db.Pics.Remove(picture);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Pics/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Picture pic = await db.Pics.FindAsync(id);
            if (pic == null)
                return HttpNotFound();

            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date");
            ViewBag.StringerId = new SelectList(db.Stringers, "Id", "FirstName");
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name");
            ViewBag.LeaderId = new SelectList(db.Leaders, "Id", "FirstName");
            ViewBag.InstrumentId = new SelectList(db.Instruments, "Id", "Name");
            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title");
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname");
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title");

            if (Request.IsAjaxRequest())
                return PartialView(pic);
            return View(pic);
        }

        // POST: Pics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description")] Picture picture, HttpPostedFileBase Pic)
        {
            db.Entry(picture).State = EntityState.Modified;
            if (Pic != null)
            {
                var _Bytes = new byte[Pic.ContentLength];
                Pic.InputStream.Read(_Bytes, 0, Pic.ContentLength);
                picture.Bytes = _Bytes;
                Image _Image;
                using (var ms = new MemoryStream(_Bytes))
                {
                    _Image = Image.FromStream(ms);
                }
                picture.Thumb = ConvertImageToByte(ResizeImage(_Image, new Size(200, 100)));
            }
            else
            {
                db.Entry(picture).Property(x => x.Bytes).IsModified = false;
                db.Entry(picture).Property(x => x.Thumb).IsModified = false;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConcertId = new SelectList(db.Concerts, "Id", "Date");
            ViewBag.StringerId = new SelectList(db.Stringers, "Id", "FirstName");
            ViewBag.TrackId = new SelectList(db.Tracks, "Id", "Name");
            ViewBag.LeaderId = new SelectList(db.Leaders, "Id", "FirstName");
            ViewBag.InstrumentId = new SelectList(db.Instruments, "Id", "Name");
            ViewBag.GenusId = new SelectList(db.Genus, "Id", "Title");
            ViewBag.ComposerId = new SelectList(db.Composers, "Id", "Fullname");
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title");

            if (Request.IsAjaxRequest())
                return PartialView(picture);
            return View(picture);
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
