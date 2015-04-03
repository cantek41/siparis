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
            STOKCARD model = getProduct(urunID);
            return View(model);
        }

    }
}