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
            Filter filter = new Filter(dbName);
            IndexDataViewModel data = filter.getMainMenu();
            data.stokcardView = filter.getStok(12);
            ViewData["Options"] = new ImageSliderSlideShowDemoOptions();
            return View(data);

        }
        public ActionResult FilterProduct(int main,int sub)
        {
            ViewData["Options"] = new ImageSliderSlideShowDemoOptions();
            Filter filter = new Filter(dbName);
            IndexDataViewModel data = filter.getFilterItem();
            data.stokMainGroup.ElementAt(0).ID = main;
            data.stokSubGroup.ElementAt(0).ID = sub;
            return View(data);
        }
        [HttpPost]
        public JsonResult FilterProduct(filterModel data)
        {
            ViewData["Options"] = new ImageSliderSlideShowDemoOptions();
            Filter filter = new Filter(dbName);
            return Json(filter.getFilterStok(data), JsonRequestBehavior.AllowGet);
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

}