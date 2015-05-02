using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace siparis
{
    public class sessiDataFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionlang = HttpContext.Current.Session["lang"];
            if (sessionlang != null)
            {
                CultureInfo ci = CultureInfo.GetCultureInfo(sessionlang.ToString());
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }


            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower().Trim();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower().Trim();


            if (!actionName.StartsWith("login") && !actionName.StartsWith("logoff") && !actionName.StartsWith("changelanguage"))
            {
                var session = HttpContext.Current.Session["profilim"];
                HttpContext ctx = HttpContext.Current;
                //Redirects user to login screen if session has timed out
                if (session == null)
                {
                    base.OnActionExecuting(filterContext);


                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "LogOff"
                    }));

                }
            }
        }

    }
}