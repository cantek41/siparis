using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class HomeController : BaseController
    {



        public ActionResult Index()
        {
            VdbSoftEntities db = new VdbSoftEntities();
            var model = from d in db.STOKCARDs
                        select d;
            var stokgrup=db.STOKGROUPs;
            return View(new Tuple<IEnumerable<STOKCARD>, IEnumerable<STOKGROUP>>(model, stokgrup));
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