using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace siparis.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View("Firsat");
        }

        public ActionResult Firsat()
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
    }
}