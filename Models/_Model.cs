using Symphony.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Symphony.Models
{

    public class _Global
    {
        static public string RegexPattern = "<head.*?>(.|\n)*?</head>";
    }
    public partial class A1
    {
        [Display(Name = "ساز و نوازنده")]
        public string IS
        {
            get
            {
                return Instrument.Name + "-" + Stringer.Fullname;
            }
        }
        [Display(Name = "سریال")]
        public int Id { get; set; }

        [Display(Name = "ساز")]
        [Required(ErrorMessage = "*")]
        public System.Guid InstrumentId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "نوازنده")]
        public System.Guid StringerId { get; set; }
    }

    public partial class A2
    {
        [Display(Name = "سریال")]
        public int Id { get; set; }

        [Display(Name = "ساز و نوازنده")]
        [Required(ErrorMessage = "*")]
        public int A1Id { get; set; }

        [Display(Name = "قطعه")]
        [Required(ErrorMessage = "*")]
        public System.Guid TrackId { get; set; }
    }

    public partial class A3
    {
        [Display(Name = "سریال")]
        public int Id { get; set; }

        [Display(Name = "قطعه")]
        [Required(ErrorMessage = "*")]
        public System.Guid TrackId { get; set; }

        [Display(Name = "کنسرت")]
        [Required(ErrorMessage = "*")]
        public System.Guid ConcertId { get; set; }
    }

    public partial class A4
    {
        [Display(Name = "سریال")]
        public int Id { get; set; }

        [Display(Name = "کنسرت")]
        [Required(ErrorMessage = "*")]
        public System.Guid ConcertId { get; set; }

        [Display(Name = "رهبر")]
        [Required(ErrorMessage = "*")]
        public System.Guid LeaderId { get; set; }

    }

    public partial class Admin
    {
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "پست الکترونیک")]
        public string Email { get; set; }
    }

    public partial class Adverties
    {
        [Display(Name = "شناسه ردیف")]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public byte[] Image { get; set; }

        [Display(Name = "مقصد")]
        public string Url { get; set; }
    }

    public partial class Composer
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "*")]
        public string Fullname { get; set; }

        private string _Text;
        [Required(ErrorMessage = "*")]
        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        public virtual string Text
        {

            get
            {

                try
                {
                    return Regex.Replace(_Text, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Text;
                }
            }
            set
            {
                _Text = value;
            }
        }


        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }
    }

    public partial class Concert
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "زمان")]
        public string Date { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "مکان")]
        public string Address { get; set; }

        private string _Description;
        [Required(ErrorMessage = "*")]
        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        public virtual string Description
        {

            get
            {

                try
                {
                    return Regex.Replace(_Description, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Description;
                }
            }
            set
            {
                _Description = value;
            }
        }

        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }

    }

    public partial class Genus
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        private string _Text;
        [Required(ErrorMessage = "*")]
        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        public virtual string Text
        {

            get
            {

                try
                {
                    return Regex.Replace(_Text, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Text;
                }
            }
            set
            {
                _Text = value;
            }
        }


        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }
    }

    public partial class Instrument
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        private string _Text;
        [Required(ErrorMessage = "*")]
        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        public virtual string Text
        {

            get
            {

                try
                {
                    return Regex.Replace(_Text, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Text;
                }
            }
            set
            {
                _Text = value;
            }
        }


        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }

        [Display(Name = "نوع صدا")]
        [Required(ErrorMessage = "*")]
        public System.Guid GenusId { get; set; }
    }

    public partial class Leader
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [Display(Name = "تولد")]
        [Required(ErrorMessage = "*")]
        public string BirthYear { get; set; }

        private string _Text;
        [Required(ErrorMessage = "*")]
        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        public virtual string Text
        {

            get
            {

                try
                {
                    return Regex.Replace(_Text, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Text;
                }
            }
            set
            {
                _Text = value;
            }
        }

        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }


        [Display(Name = "نام و نام خانوادگی")]
        public string Fullname
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

    public partial class News
    {
        [Display(Name = "ردیف")]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "چکیده")]
        [Required(ErrorMessage = "*")]
        public string Abstract { get; set; }

        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }

        private string _Text;
        [Required(ErrorMessage = "*")]
        [Display(Name = "متن")]
        [DataType(DataType.MultilineText)]
        public virtual string Text
        {

            get
            {

                try
                {
                    return Regex.Replace(_Text, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Text;
                }
            }
            set
            {
                _Text = value;
            }
        }
    }

    public partial class Picture
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "توضیح")]
        public string Description { get; set; }

        public byte[] Thumb { get; set; }

        public byte[] Bytes { get; set; }

        [Display(Name = "کنسرت")]
        public Nullable<System.Guid> ConcertId { get; set; }

        [Display(Name = "ساز")]
        public Nullable<System.Guid> InstrumentId { get; set; }

        [Display(Name = "قطعه")]
        public Nullable<System.Guid> TrackId { get; set; }

        [Display(Name = "نوازنده")]
        public Nullable<System.Guid> StringerId { get; set; }

        [Display(Name = "خبر")]
        public Nullable<int> NewsId { get; set; }

        [Display(Name = "رهبر")]
        public Nullable<System.Guid> LeaderId { get; set; }

        [Display(Name = "صدا")]
        public Nullable<System.Guid> GenusId { get; set; }

        [Display(Name = "آهنگساز")]
        public Nullable<System.Guid> ComposerId { get; set; }
    }

    public partial class Stringer
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [Display(Name = "تولد")]
        [Required(ErrorMessage = "*")]
        public string BirthYear { get; set; }

        private string _Text;
        [Required(ErrorMessage = "*")]
        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        public virtual string Text
        {

            get
            {

                try
                {
                    return Regex.Replace(_Text, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Text;
                }
            }
            set
            {
                _Text = value;
            }
        }


        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }


        [Display(Name = "نام و نام خانوادگی")]
        public string Fullname
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }


    public partial class Track
    {
        [Display(Name = "سریال")]
        public System.Guid Id { get; set; }

        [Display(Name = "ردیف")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "عنوان")]
        public string Name { get; set; }

        private string _Text;
        [Required(ErrorMessage = "*")]
        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        public virtual string Text
        {

            get
            {

                try
                {
                    return Regex.Replace(_Text, _Global.RegexPattern, String.Empty).Replace("<!DOCTYPE html>", String.Empty).ToString();
                }
                catch
                {

                    return _Text;
                }
            }
            set
            {
                _Text = value;
            }
        }

        [Display(Name = "تصویر")]
        public byte[] Thumbnail { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "آهنگساز")]
        public System.Guid ComposerId { get; set; }
    }

    public partial class File
    {
        [Display(Name = "نام")]
        public System.Guid Name { get; set; }

        [Display(Name = "نوع")]
        public string Type { get; set; }

        [Display(Name = "اندازه")]
        public int Lenght { get; set; }

        [Display(Name="مقدار")]
        public byte[] Bytes { get; set; }

        [Display(Name = "پوشه")]
        public int FolderId { get; set; }
    }
    public partial class Folder
    {
        [Display(Name="شناسه")]
        public int Id { get; set; }

        [Display(Name = "والد")]
        public int Parent { get; set; }
     
        [Display(Name = "نام")]
        public string Name { get; set; }
    }

}