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
        public ActionResult Draft()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Review()
        {
            return View();
        }
        public ActionResult Panding()
        {
            return View();
        }
        public ActionResult Approwed()
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
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 3);
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

        siparis.Models.VdbSoftEntities db1 = new siparis.Models.VdbSoftEntities();

        [ValidateInput(false)]
        public ActionResult GridView3PartialDraft()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 4);
            return PartialView("_GridView3PartialDraft", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView3PartialDraftDelete(System.Int32 OPPORTUNITY_CODE)
        {
            var model = db1.OPPORTUNITYMASTERs;
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.OPPORTUNITY_CODE == OPPORTUNITY_CODE);
                    if (item != null)
                        model.Remove(item);
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
    return PartialView("_GridView3PartialDraft", model.ToList());
}

        siparis.Models.VdbSoftEntities db2 = new siparis.Models.VdbSoftEntities();

        [ValidateInput(false)]
        public ActionResult GridView4PartialOrder()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 5);
            return PartialView("_GridView4PartialOrder", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView4PartialOrderDelete(System.Int32 OPPORTUNITY_CODE)
        {
            var model = db2.OPPORTUNITYMASTERs;
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.OPPORTUNITY_CODE == OPPORTUNITY_CODE);
                    if (item != null)
                        model.Remove(item);
                    db2.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
    return PartialView("_GridView4PartialOrder", model.ToList());
}

        siparis.Models.VdbSoftEntities db3 = new siparis.Models.VdbSoftEntities();

        [ValidateInput(false)]
        public ActionResult GridView5PartialReview()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 6);
            return PartialView("_GridView5PartialReview", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView5PartialReviewDelete(System.Int32 OPPORTUNITY_CODE)
        {
            var model = db3.OPPORTUNITYMASTERs;
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.OPPORTUNITY_CODE == OPPORTUNITY_CODE);
                    if (item != null)
                        model.Remove(item);
                    db3.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
    return PartialView("_GridView5PartialReview", model.ToList());
}

        siparis.Models.VdbSoftEntities db4 = new siparis.Models.VdbSoftEntities();

        [ValidateInput(false)]
        public ActionResult GridView6PartialPanding()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 7);
            return PartialView("_GridView6PartialPanding", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView6PartialPandingDelete(System.Int32 OPPORTUNITY_CODE)
        {
            var model = db4.OPPORTUNITYMASTERs;
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.OPPORTUNITY_CODE == OPPORTUNITY_CODE);
                    if (item != null)
                        model.Remove(item);
                    db4.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
    return PartialView("_GridView6PartialPanding", model.ToList());
}

        [ValidateInput(false)]
        public ActionResult GridView7PartialApprowed()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 8);
            return PartialView("_GridView7PartialApprowed", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView7PartialApprowedDelete(System.Int32 OPPORTUNITY_CODE)
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
    return PartialView("_GridView7PartialApprowed", model);
}
    }
}