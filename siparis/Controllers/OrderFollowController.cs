using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DevExpress.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    [Authorize]
    public class OrderFollowController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {

            return View(getOpp(1));
        }

        public ActionResult Opportunity()
        {

            RolePrincipal r = (RolePrincipal)User;
            string[] rol = r.GetRoles();
            if (!rol.Contains("Admin"))
            {
                return View();
            }
            return View(getOpp(1));
        }
        public ActionResult Edited()
        {
            return View(getOpp(19));
        }
        public ActionResult Sample()
        {
            return View(getOpp(6));
        }
        public ActionResult Offer()
        {
            return View(getOpp(2));
        }
        public ActionResult Draft()
        {
            return View(getOpp(15));
        }
        public ActionResult Order()
        {
            return View(getOpp(3));
        }
        public ActionResult InReview()
        {
            return View(getOpp(16));
        }
        public ActionResult Pending()
        {
            return View(getOpp(17));
        }
        public ActionResult Approved()
        {
            return View(getOpp(18));
        }
        public ActionResult Processed()
        {
            return View(getOpp(20));
        }
        public ActionResult Shipped()
        {
            return View(getOpp(21));
        }
        public ActionResult Cancelled()
        {
            return View(getOpp(22));
        }
        public ActionResult DispatchNote()
        {
            return View(getOpp(4));
        }
        public ActionResult Invoice()
        {
            return View(getOpp(5));
        }
        public ActionResult LineSheets()
        {
            return View(getOpp(23));
        }
        public ActionResult girdMaster()
        {
            VdbSoftEntities db = db = new VdbSoftEntities(dbName);
            var model = from d in db.OPPORTUNITYMASTERs
                        select d;
            return View(model.ToArray());
        }

        public IEnumerable<OrderMasterViewModel> getOpp(int oppMasterType)
        {
            TempData["DOCUMENT_TYPE"] = oppMasterType;
            VdbSoftEntities db = db = new VdbSoftEntities(dbName);
            List<OrderMasterViewModel> model = (from d in db.OPPORTUNITYMASTERs
                                                join com in db.COMPANies on d.COMPANY_CODE equals com.COMPANY_CODE
                                                join con in db.CONTACTs on d.CONTACT_CODE equals con.CONTACT_CODE
                                                join user in db.USERS on d.APPOINTED_USER_CODE equals user.USER_CODE
                                                where d.DOCUMENT_TYPE == oppMasterType
                                                select new OrderMasterViewModel
                                                 {
                                                     OPPORTUNITY_CODE = d.OPPORTUNITY_CODE,
                                                     COMPANY_CODE = com.COMPANY_NAME,
                                                     DOCUMENT_DATE = (DateTime)d.DOCUMENT_DATE,
                                                     CONTACT_CODE = con.NAME + " " + con.SURNAME,
                                                     APPOINTED_USER_CODE = user.AUSER_NAME + " " + user.SURNAME,
                                                     CREATE_USER = user.AUSER_NAME + " " + user.SURNAME,
                                                     TOTAL = (float)d.TOTAL
                                                 }).ToList();
            return model;
        }


        [ValidateInput(false)]
        public ActionResult MasterDetail(int DOCUMENT_TYPE)
        {

            return View(getOpp(DOCUMENT_TYPE));
        }
        [ValidateInput(false)]
        public ActionResult MasterDetailMasterPartial(int DOCUMENT_TYPE)
        {
            return PartialView("MasterDetailMasterPartial", getOpp(DOCUMENT_TYPE));
        }
        [ValidateInput(false)]
        public ActionResult MasterDetailDetailPartial(string customerID)
        {

            ViewData["COURSE_CODE"] = customerID;
            int cID = Convert.ToInt32(customerID);
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            var model = from d in db.OPPORTUNITYDETAILs
                        where d.OPPORTUNITY_CODE == cID
                        select d;
            return PartialView("MasterDetailDetailPartial", model.ToArray());
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Cencel(System.Int32 OPPORTUNITY_CODE)
        {
            RolePrincipal r = (RolePrincipal)User;
            string[] rol = r.GetRoles();

            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
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
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
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
                    TotalHesapla(OPPORTUNITY_CODE);
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
                    return GridViewPartialDraft();
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
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
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

        [HttpPost]
        public ActionResult CustomButtonClick(string clickedButton)
        {
            RolePrincipal r = (RolePrincipal)User;
            string[] rol = r.GetRoles();

            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            int eskiSayfa = 0;
            int OPPORTUNITY_CODE = Convert.ToInt32(clickedButton);
            if (OPPORTUNITY_CODE >= 0)
            {
                try
                {
                    OPPORTUNITYMASTER opp = db.OPPORTUNITYMASTERs.Find(OPPORTUNITY_CODE);
                    eskiSayfa = (int)opp.DOCUMENT_TYPE;

                    switch (rol[0])
                    {
                        case "Admin":
                            break;
                        case "Bayi":
                            switch (eskiSayfa)
                            {
                                case 15:
                                    opp.DOCUMENT_TYPE = 3;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Depo":
                            break;
                        default:
                            break;
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
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 1);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialSample()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 6);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialOffer()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 2);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialDraft()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 15);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialOrder()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 3);
            return PartialView("_GridViewPartialOrder", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialInReview()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 16);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialPending()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 17);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialApproved()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 18);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialEdited()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 19);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialProcessed()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 20);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialShipped()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 21);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialCancelled()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 22);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialDispatchNote()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 4);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialInvoice()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 5);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialLinesheets()
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            var model = db.OPPORTUNITYMASTERs.Where(x => x.DOCUMENT_TYPE == 23);
            return PartialView("_GridViewPartialOpportunity", model.ToList());
        }

    }
}
