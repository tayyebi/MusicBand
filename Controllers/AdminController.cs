using Symphony.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Symphony.Controllers
{
    public class AdminController : Controller
    {
        [Secure]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            WebClient webclient = new WebClient();
            string Code = string.Empty;
            string _Username = string.Empty;
            string Email = string.Empty;
            string WebApp = "http://abnt.ir/";
            string Name = "alvand-62324";
            string Key = "503bfb69-251c-40ee-8b91-35b5e08f143f";
            //string Name = "test-28338";
            //string Key = "c0d1540d-975c-4621-8f69-9cd0c9668f1e";
            //string WebApp = "http://localhost:2038/";

            try
            {
                Code = Session["Code"].ToString();
                _Username = Session["Username"].ToString();
            }
            catch { }
            if (Code != "" && Code != null)
            {
                string Url = WebApp + "[webapp]/en-US/WebAppUser" + "?Name=" + Name + "&Key=" + Key + "&Code=" + Code + "&Export=";

                try
                {
                    _Username = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Username"));
                }
                catch { }
                Session["Username"] = _Username;
                Session["Email"] = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Email"));
                ViewBag.Username = _Username;

                if (_Username == "")
                {
                    if (_Username != "" && _Username != null)
                    {
                    }
                    else
                    {
                        Response.Redirect(WebApp + "[webapp]/en-US/WebAppAllow/Index" + "?Name=" + Name + "&Code=" + Code);
                    }
                }
            }
            else
            {
                try
                {
                    Session["Code"] = webclient.DownloadString(HttpUtility.UrlDecode(WebApp + "[webapp]/en-US/WebAppCode")).ToString();
                }
                catch { }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Code"] = null;
            Session["Username"] = null;
            return Redirect("~/Admin/Login");
        }
    }
}