using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DevExpress.Xpo;
using siparis.Models;

namespace siparis.Controllers
{
   
    public class HomeController : BaseController
    {
        
        
        public ActionResult Index()
        {
            checkCart();           
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "ID";
            info.SortDirection = "ascending";
            info.PageSize = 6;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(db.STOKCARDs.Count()
                           / info.PageSize)));
            info.CurrentPageIndex = 0;
            var query = db.STOKCARDs.Where(x=>x.ID!=0).OrderBy(c => c.ID).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;
           /// List<STOKCARD> model = query.ToList();
           
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = query.ToList();
            data.stokgroup=db.STOKCATEGORies.ToList();
            data.stokbrand=db.STOKBRANDs.ToList();
            return View(data);
            //   return View(model);

        }

        /// <summary>
        /// sayfa atlatma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(SortingPagingInfo info)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);

            IQueryable<STOKCARD> query = null;
            switch (info.SortField)
            {
                case "ID":
                    query = (info.SortDirection == "ascending" ?
                             db.STOKCARDs.OrderBy(c => c.ID) :
                             db.STOKCARDs.OrderByDescending(c => c.ID));
                    break;
                //case "CompanyName":
                //    query = (info.SortDirection == "ascending" ?
                //             db.STOKCARDs.OrderBy(c => c.CompanyName) :
                //             db.STOKCARDs.OrderByDescending(c => c.CompanyName));
                //    break;
                //case "ContactName":
                //    query = (info.SortDirection == "ascending" ?
                //             db.STOKCARDs.OrderBy(c => c.ContactName) :
                //             db.STOKCARDs.OrderByDescending(c => c.ContactName));
                //    break;
                //case "Country":
                //    query = (info.SortDirection == "ascending" ?
                //             db.STOKCARDs.OrderBy(c => c.Country) :
                //             db.STOKCARDs.OrderByDescending(c => c.Country));
                //break;
            }
            query = query.Skip(info.CurrentPageIndex
                  * info.PageSize).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = query.ToList();
            data.stokgroup = db.STOKCATEGORies.ToList();
            data.stokbrand = db.STOKBRANDs.ToList();
            return View(data);

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            // ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }


    }


    public class sessiDataFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if ((HttpContext.Current.User.Identity.IsAuthenticated==true) && (HttpContext.Current.Session["FirmaAdi"]==null))
            //{
               
            //    AccountController ac = new AccountController();
            //    ac.LogOff();
            //}
            //base.OnActionExecuting(filterContext);

            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower().Trim();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower().Trim();

            if (!actionName.StartsWith("login") && !actionName.StartsWith("logoff"))
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