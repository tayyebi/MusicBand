using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using Symphony.Models;
using System.Drawing.Imaging;

namespace Symphony.Controllers
{
    public class Thumbnail
    {
        private ModelContainer db = new ModelContainer();

        static public byte[] ConvertImageToByte(System.Drawing.Image ImageToConvert)
        {
            MemoryStream ms = new MemoryStream();
            ImageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        static public byte[] Create(HttpPostedFileBase FileUploader)
        {
            if (FileUploader == null)
                return null;
            if (!FileUploader.ContentType.StartsWith("image"))
                return null;

            var _Bytes = new byte[FileUploader.ContentLength];
            FileUploader.InputStream.Read(_Bytes, 0, FileUploader.ContentLength);

            using (var ms = new MemoryStream(_Bytes))
            {
                Bitmap imgIn = new Bitmap(ms);
                double y = imgIn.Height;
                double x = imgIn.Width;
                int height = 100;
                int width = 200;
                int Radius = 100;

                double factor = 1;
                if (width > 0)
                {
                    factor = width / x;
                }
                else if (height > 0)
                {
                    factor = height / y;
                }
                System.IO.MemoryStream outStream = new System.IO.MemoryStream();
                Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));
                imgOut.SetResolution(72, 72);
                Graphics g = Graphics.FromImage(imgOut);
                g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)), new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);
                Brush brush = new System.Drawing.SolidBrush(Color.Transparent);
                for (int i = 0; i < 4; i++)
                {
                    Point[] CornerUpLeft = new Point[3];
                    CornerUpLeft[0].X = 0;
                    CornerUpLeft[0].Y = 0;
                    CornerUpLeft[1].X = Radius;
                    CornerUpLeft[1].Y = 0;
                    CornerUpLeft[2].X = 0;
                    CornerUpLeft[2].Y = Radius;
                    System.Drawing.Drawing2D.GraphicsPath pathCornerUpLeft =
                       new System.Drawing.Drawing2D.GraphicsPath();
                    pathCornerUpLeft.AddArc(CornerUpLeft[0].X, CornerUpLeft[0].Y,
                        Radius, Radius, 180, 90);
                    pathCornerUpLeft.AddLine(CornerUpLeft[0].X, CornerUpLeft[0].Y,
                        CornerUpLeft[1].X, CornerUpLeft[1].Y);
                    pathCornerUpLeft.AddLine(CornerUpLeft[0].X, CornerUpLeft[0].Y,
                        CornerUpLeft[2].X, CornerUpLeft[2].Y);
                    g.FillPath(brush, pathCornerUpLeft);
                    pathCornerUpLeft.Dispose();
                    imgOut.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                imgOut.Save(outStream, ImageFormat.Png);
                return outStream.ToArray();
            }
        }
    }
}