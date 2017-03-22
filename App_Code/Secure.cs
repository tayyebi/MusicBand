using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Symphony.Models;

namespace Symphony.App_Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]

    class SecureAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool CanAccess = true;
            ModelContainer db = new ModelContainer();

            string Username = string.Empty;
            string Email = string.Empty;
            try
            {

                    Username = filterContext.HttpContext.Session["Username"].ToString();
                    Email = filterContext.HttpContext.Session["Email"].ToString();
                
            }
            catch
            {
                CanAccess = false;
            }


            if (CanAccess)
            {
                try
                {
                    var Administrator = db.Admins.Find(Username);
                    if (Administrator.Username.ToLower() != Username.ToLower())
                        CanAccess = false;
                }
                catch
                {
                    CanAccess = false;
                }
            }

            if (!CanAccess)
                filterContext.Result = new HttpStatusCodeResult(404);
        }
    }
}
