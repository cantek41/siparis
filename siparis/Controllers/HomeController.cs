using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DevExpress.Web.Mvc;
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
            ///sayfalama bilgisi yukleniyor
            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "ID";
            info.SortDirection = "ascending";
            info.PageSize = 6;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(db.STOKCARDs.Where(x => x.UPPER_CODE == x.CODE).Count()
                           / info.PageSize)));
            info.CurrentPageIndex = 0;
            ViewBag.SortingPagingInfo = info;
            /// sayfalama bilgiswi bitti
            return View(stokViewList(info));


        }

        /// <summary>
        /// sayfa atlatma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(SortingPagingInfo info)
        {
            return View(stokViewList(info));
        }


        public IndexDataViewModel stokViewList(SortingPagingInfo info)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            
                IQueryable<STOKCARD> query = null;
                switch (info.SortField)
                {
                    case "ID":
                        query = (info.SortDirection == "ascending" ?
                                 db.STOKCARDs.Where(x => x.ID != 0).Where(x => x.UPPER_CODE == x.CODE).OrderBy(c => c.ID) :
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
                return data;


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

        public ActionResult CategorizedProducts(int Id)
        {
            TempData["Category"] = Id;
            return View(getDetailCategory(Id));
        }



        /// <summary>
        /// sayfa atlatma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult CategorizedProducts([ModelBinder(typeof(DevExpressEditorsBinder))] IndexDataViewModel model, int Id)
        {
            int CategoryID = Id;
            int[] season = CheckBoxListExtension.GetSelectedValues<int>("SEASON");
            int[] color = CheckBoxListExtension.GetSelectedValues<int>("COLOR");
            int[] size = CheckBoxListExtension.GetSelectedValues<int>("SIZE");
            int[] brands = CheckBoxListExtension.GetSelectedValues<int>("BRAND");
            //int price = (TextBoxExtension.GetValue<int>("PRICE") == null) ? 0 : TextBoxExtension.GetValue<int>("PRICE");
            VdbSoftEntities db = new VdbSoftEntities(dbName);

            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = (from stk in db.STOKCARDs
                             where stk.CATEGORY_CODE == CategoryID
                             select stk).ToList();
            data.stokcard = (from s in data.stokcard
                             join c in brands on s.BRAND_CODE equals c
                             select s).ToList();
            data.stokcard = (from s in data.stokcard
                             join c in color on s.COLOR_CODE equals c
                             select s).ToList();
            data.stokcard = (from s in data.stokcard
                             join c in season on s.SEASON_CODE equals c
                             select s).ToList();
            data.stokcard = (from s in data.stokcard
                             join c in size on s.BODY_CODE equals c
                             select s).ToList();
            //if (price > 0)
            //{
            //    data.stokcard = (from s in data.stokcard
            //                     where s.UNIT_PRICE < price
            //                     select s).ToList();
            //}

            //stok.brand ve stok.group data ya atanmalı
            data.stokbrand = BrandFilter(data);
            data.stokcolor = ColorFilter(data);
            data.stokseason = SeasonFilter(data);
            data.stokBody = BodyFilter(data);
            data.stokgroup = db.STOKCATEGORies.Where(x => x.STOK_GROUP_CODE == CategoryID).ToList();
            return View(data);

        }


        public List<STOKBRAND> BrandFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokbrand = (from brand in data.stokcard
                              join x in db.STOKBRANDs on brand.BRAND_CODE equals x.BRAND_CODE
                              select x).Distinct().ToList();
            return data.stokbrand;
        }

        public List<STOKCOLOR> ColorFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokcolor = (from brand in data.stokcard
                              join x in db.STOKCOLORs on brand.COLOR_CODE equals x.COLOR_CODE
                              select x).Distinct().ToList();
            return data.stokcolor;
        }

        public List<STOKSEASON> SeasonFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokseason = (from brand in data.stokcard
                               join x in db.STOKSEASONs on brand.SEASON_CODE equals x.SEASON_CODE
                               select x).Distinct().ToList();
            return data.stokseason;
        }
        public List<STOKBODY> BodyFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokBody = (from brand in data.stokcard
                             join x in db.STOKBODies on brand.BODY_CODE equals x.BODY_CODE
                             select x).Distinct().ToList();
            return data.stokBody;
        }


        public IndexDataViewModel getDetailCategory(int CategoryId)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = db.STOKCARDs.Where(x => x.CATEGORY_CODE == CategoryId).OrderBy(c => c.ID).ToList();
            data.stokgroup = db.STOKCATEGORies.Where(x => x.STOK_GROUP_CODE == CategoryId).ToList();
            data.stokbrand = (from brand in db.STOKBRANDs
                              join x in db.STOKCARDs on brand.BRAND_CODE equals x.BRAND_CODE
                              where x.CATEGORY_CODE == CategoryId
                              select brand).Distinct().ToList();
            data.stokBody = (from brand in db.STOKBODies
                             join x in db.STOKCARDs on brand.BODY_CODE equals x.BODY_CODE
                             where x.CATEGORY_CODE == CategoryId
                             select brand).Distinct().ToList();

            data.stokcolor = (from brand in db.STOKCOLORs
                              join x in db.STOKCARDs on brand.COLOR_CODE equals x.COLOR_CODE
                              where x.CATEGORY_CODE == CategoryId
                              select brand).Distinct().ToList();
            data.stokseason = (from brand in db.STOKSEASONs
                               join x in db.STOKCARDs on brand.SEASON_CODE equals x.SEASON_CODE
                               where x.CATEGORY_CODE == CategoryId
                               select brand).Distinct().ToList();

            return data;

        }

    }


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