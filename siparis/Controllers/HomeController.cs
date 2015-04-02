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
            checkCart();
            //VdbSoftEntities db = new VdbSoftEntities();
            //var model = from d in db.STOKCARDs
            //            select d;
            //return View(model);

            VdbSoftEntities db = new VdbSoftEntities();


            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "ID";
            info.SortDirection = "ascending";
            info.PageSize = 6;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(db.STOKCARDs.Count()
                           / info.PageSize)));
            info.CurrentPageIndex = 0;
            var query = db.STOKCARDs.OrderBy(c => c.ID).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;
            List<STOKCARD> model = query.ToList();
            var stokgrup = db.STOKGROUPs;
            return View(new Tuple<IEnumerable<STOKCARD>, IEnumerable<STOKGROUP>>(model, stokgrup));
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
            VdbSoftEntities db = new VdbSoftEntities();

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
            List<STOKCARD> model = query.ToList();
            var stokgrup = db.STOKGROUPs;
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