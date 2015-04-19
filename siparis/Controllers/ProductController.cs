using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class ProductController : BaseController
    {
        // GET: /Product/
        public ActionResult ProductDetail(int urunID)
        {
            STOKCARD model = getProduct(urunID);
            GetColor(model);
            return View(model);
        }

        [HttpPost]
        public void GetColor(STOKCARD model) 
        {
            VdbSoftEntities db = new VdbSoftEntities();
            ViewData["Color"] = (from s in db.STOKCARDs
                                 join c in db.STOKCOLORs on s.COLOR_CODE equals c.COLOR_CODE
                                 where s.UPPER_CODE == model.UPPER_CODE                             
                                 select new { Key = c.COLOR_CODE, Text = c.NAME_TR }).Distinct();
        }


        public ActionResult GetBody(int color,string upper)
        {
            VdbSoftEntities db = new VdbSoftEntities();
            var model= (from s in db.STOKCARDs
                                 where s.UPPER_CODE == upper && s.COLOR_CODE==color
                                 join b in db.STOKBODies on s.BODY_CODE equals b.BODY_CODE
                                 select new { Key = b.BODY_CODE, Text = b.NAME_TR }).Distinct();
            return Json(model);


        }

        public ActionResult GetProduct(string upper, int color, int body)
        {
            VdbSoftEntities db = new VdbSoftEntities();
            var model = (from s in db.STOKCARDs
                         where s.UPPER_CODE == upper && s.COLOR_CODE == color && s.BODY_CODE==body
                         select s.ID).FirstOrDefault();
            return Json(model);

        }


    }
}