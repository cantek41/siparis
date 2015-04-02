using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
namespace siparis.Controllers
{
    public class BaseController : Controller
    {
        public void checkCart()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
            int sepetID = (from d in db.OPPORTUNITYMASTERs
                           where d.OPEN_CLOSE == 0
                           select d.OPPORTUNITY_CODE).FirstOrDefault();
            Session.Add("Sepet", sepetID);

        }
        public static STOKCARD getProduct(int ID = 1)
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
            STOKCARD st = (from d in db.STOKCARDs
                           where d.ID == ID
                           select d).FirstOrDefault();
            return st;
        }

        public static IEnumerable<STOKCARD> getProductAll()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
            var model = from d in db.STOKCARDs
                        select d;
            return model;
        }
    }
}