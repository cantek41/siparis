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
        //
        // GET: /Product/
        public ActionResult ProductDetail(int urunID)
        {
            ViewDataDoldur();
            STOKCARD model = getProduct(urunID);
            return View(model);
        }

        public void ViewDataDoldur()
        {
            VdbSoftEntities db = new VdbSoftEntities();
           
                                
            ViewData["Color"] = (from d in db.STOKCOLORs
                                 select new { Key = d.COLOR_CODE, Text = d.NAME_TR });
            
            ViewData["Body"] = (from d in db.STOKBODies
                                select new { Key = d.BODY_CODE, Text = d.NAME_TR });
           
           

        }

    }
}