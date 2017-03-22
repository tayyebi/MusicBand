using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Symphony.Controllers
{
    public class About
    {
        static public string Root = HttpContext.Current.Request.Url.Scheme.ToString() + "://" + HttpContext.Current.Request.Url.Host.ToString() + ":" + HttpContext.Current.Request.Url.Port.ToString() + "/";
        static public string Name = "ارکستر الوند همدان";
    }
}