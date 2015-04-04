﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class OrderFollowController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View("girdMaster");
        }
        public ActionResult Opportunity()
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
        public ActionResult InReview()
        {
            return View();
        }
        public ActionResult Pending()
        {
            return View();
        }
        public ActionResult Approved()
        {
            return View();
        }
        public ActionResult Processed()
        {
            return View();
        }
        public ActionResult Shipped()
        {
            return View();
        }
        public ActionResult Cancelled()
        {
            return View();
        }
        public ActionResult DispatchNote()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult LineSheets()
        {
            return View();
        }
        public ActionResult girdMaster()
        {
            VdbSoftEntities db = db = new VdbSoftEntities();
            var model = from d in db.OPPORTUNITYMASTERs
                        select d;
            return View(model.ToArray());
        }


        public ActionResult MasterDetail()
        {
            VdbSoftEntities db = db = new VdbSoftEntities();
            var model = from d in db.OPPORTUNITYMASTERs
                        select d;
            return View(model);
        }
        public ActionResult MasterDetailMasterPartial()
        {
            VdbSoftEntities db = new VdbSoftEntities();
            var model = from d in db.OPPORTUNITYMASTERs
                        select d;
            return PartialView("MasterDetailMasterPartial", model.ToArray());
        }
        public ActionResult MasterDetailDetailPartial(string customerID)
        {

            ViewData["COURSE_CODE"] = customerID;
            int cID = Convert.ToInt32(customerID);
            VdbSoftEntities db = new VdbSoftEntities();
            var model = from d in db.OPPORTUNITYDETAILs
                        where d.OPPORTUNITY_CODE == cID
                        select d;
            return PartialView("MasterDetailDetailPartial", model.ToArray());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Cencel(System.Int32 OPPORTUNITY_CODE)
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            int eskiSayfa = 0;
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    OPPORTUNITYMASTER opp = db.OPPORTUNITYMASTERs.Find(OPPORTUNITY_CODE);
                    eskiSayfa = (int)opp.DOCUMENT_TYPE;
                    opp.DOCUMENT_TYPE = 12;
                    db.OPPORTUNITYMASTERs.Attach(opp);
                    var entry = db.Entry(opp);
                    entry.Property(e => e.DOCUMENT_TYPE).IsModified = true;
                    //this.UpdateModel(entry);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return gidecegisayfa(eskiSayfa);

        }


          [HttpPost, ValidateInput(false)]
        public ActionResult CencelRow()
        {
            string[] keys = Request.Params["ROW_ORDER_NO;OPPORTUNITY_CODE"].Split('|');
            int ROW_ORDER_NO = Convert.ToInt32(keys[0]);
            int OPPORTUNITY_CODE = Convert.ToInt32(keys[1]);
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            int eskiSayfa = 0;
            if (ROW_ORDER_NO >= 0)
            {
                try
                {
                    OPPORTUNITYDETAIL opp = (from d in db.OPPORTUNITYDETAILs
                                             where d.OPPORTUNITY_CODE == OPPORTUNITY_CODE && d.ROW_ORDER_NO == ROW_ORDER_NO
                                             select d).SingleOrDefault();
                    
                    eskiSayfa = (int)db.OPPORTUNITYMASTERs.Find(OPPORTUNITY_CODE).DOCUMENT_TYPE;
                    db.OPPORTUNITYDETAILs.Remove(opp);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return gidecegisayfa(eskiSayfa);

        }

        
        public ActionResult gidecegisayfa(int eskiSayfa)
        {
            switch (eskiSayfa)
            {
                case 1:
                    return GridViewPartialOpportunity();
                    break;
                case 2:
                    return GridViewPartialSample();
                    break;
                case 3:
                    return GridViewPartialOffer();
                    break;
                case 4:
                    return GridViewPartialDraft();
                    break;
                case 5:
                    return GridViewPartialOrder();
                    break;
                case 6:
                    return GridViewPartialInReview();
                    break;
                case 7:
                    return GridViewPartialPending();
                    break;
                case 8:
                    return GridViewPartialApproved();
                    break;
                case 9:
                    return GridViewPartialEdited();
                    break;
                case 10:
                    return GridViewPartialProcessed();
                    break;
                case 11:
                    return GridViewPartialShipped();
                    break;
                case 12:
                    return GridViewPartialCancelled();
                    break;
                case 13:
                    return GridViewPartialDispatchNote();
                    break;
                case 14:
                    return GridViewPartialInvoice();
                    break;
                case 15:
                    return GridViewPartialLinesheets();
                    break;

                default:
                    return PartialView("_GridViewPartialOpportunity");
                    break;
            }
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Progress(System.Int32 OPPORTUNITY_CODE)
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            int eskiSayfa = 0;
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    OPPORTUNITYMASTER opp = db.OPPORTUNITYMASTERs.Find(OPPORTUNITY_CODE);
                    eskiSayfa = (int)opp.DOCUMENT_TYPE;
                    if (eskiSayfa < 15)
                    {
                        opp.DOCUMENT_TYPE++;
                    }
                    else
                    {
                        opp.DOCUMENT_TYPE = -1;
                    }


                    db.OPPORTUNITYMASTERs.Attach(opp);
                    var entry = db.Entry(opp);
                    entry.Property(e => e.DOCUMENT_TYPE).IsModified = true;
                    //this.UpdateModel(entry);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartialOpportunity");
        }

        public ActionResult CustomButtonClick(string clickedButton)
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            int eskiSayfa = 0;
            int OPPORTUNITY_CODE = Convert.ToInt32(clickedButton);
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    OPPORTUNITYMASTER opp = db.OPPORTUNITYMASTERs.Find(OPPORTUNITY_CODE);
                    eskiSayfa = (int)opp.DOCUMENT_TYPE;
                    if (eskiSayfa < 15)
                    {
                        opp.DOCUMENT_TYPE++;
                    }
                    else
                    {
                        opp.DOCUMENT_TYPE = -1;
                    }


                    db.OPPORTUNITYMASTERs.Attach(opp);
                    var entry = db.Entry(opp);
                    entry.Property(e => e.DOCUMENT_TYPE).IsModified = true;
                    //this.UpdateModel(entry);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return gidecegisayfa(eskiSayfa);

        }




        [ValidateInput(false)]
        public ActionResult GridViewPartialOpportunity()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 1);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialSample()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 6);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialOffer()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 2);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialDraft()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 15);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialOrder()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 3);
            return PartialView("_GridViewPartialOrder", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialInReview()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 16);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialPending()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 17);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialApproved()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 18);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialEdited()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 19);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialProcessed()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 20);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialShipped()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 21);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialCancelled()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 22);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialDispatchNote()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 4);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialInvoice()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 5);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialLinesheets()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities();
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 23);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }




    }
}
