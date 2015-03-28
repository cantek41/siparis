using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace siparis.Controllers
{
    public class OrderFollowController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View("opportunity");
        }

        public ActionResult Firsat()
        {
            return View();
        }


        public ActionResult Edited()
        {
            return View();
        }
        public ActionResult Sample()
        {
            return View();
        }
        public ActionResult Offer()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialFirsat()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 1);
            return PartialView("_GridViewPartialFirsat", model.ToList());
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialFirsatDelete(System.Int32 OPPORTUNITY_CODE)
        {
            var model = new object[0];
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialFirsat", model);
        }




        [ValidateInput(false)]
        public ActionResult GridViewPartialEdited()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 9);
            return PartialView("_GridViewPartialEdited", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridView1PartialSample()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 2);
            return PartialView("_GridView1PartialSample", model);
        }

        siparis.Models.VdbSoftEntities db = new siparis.Models.VdbSoftEntities();

        [ValidateInput(false)]
        public ActionResult GridView2PartialOffer()
        {
            var model = db.OPPORTUNITYMASTERs;
            return PartialView("_GridView2PartialOffer", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialOfferDelete(System.Int32 OPPORTUNITY_CODE)
        {
            var model = db.OPPORTUNITYMASTERs;
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.OPPORTUNITY_CODE == OPPORTUNITY_CODE);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
    return PartialView("_GridView2PartialOffer", model.ToList());
}
    }
}