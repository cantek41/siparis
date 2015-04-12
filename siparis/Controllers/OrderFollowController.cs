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
            return RedirectToAction("Opportunity");
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

        public ActionResult Shipping()
        {
            return View(getOpp(24));
        }
        public ActionResult ShippingConfirm()
        {
            return View(getOpp(25));
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
                    switch (rol[0])
                    {
                        case "Admin":
                            switch (eskiSayfa)
                            {
                                case 3:
                                    opp.DOCUMENT_TYPE = 22;
                                    break;
                                case 19:
                                    opp.DOCUMENT_TYPE = 22;
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case "Bayi":
                            switch (eskiSayfa)
                            {
                                case 15:
                                    opp.DOCUMENT_TYPE = 22;
                                    break;
                                case 19:
                                    opp.DOCUMENT_TYPE = 22;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Depo":
                            switch (eskiSayfa)
                            {
                                case 20:
                                    opp.DOCUMENT_TYPE = 19;
                                    break;
                                default:
                                    break;
                            }
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
            return RedirectToAction(gidecegisayfa(eskiSayfa));

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

            return RedirectToAction(gidecegisayfa(eskiSayfa));

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
                            switch (eskiSayfa)
                            {
                                case 3:
                                    opp.DOCUMENT_TYPE = 18;
                                    break;
                                case 17:
                                    opp.DOCUMENT_TYPE = 18;
                                    break;
                                case 19:
                                    opp.DOCUMENT_TYPE = 3;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Bayi":
                            switch (eskiSayfa)
                            {
                                case 15:
                                    opp.DOCUMENT_TYPE = 3;
                                    break;
                                case 19:
                                    opp.DOCUMENT_TYPE = 3;
                                    break;
                                case 23:
                                    opp.DOCUMENT_TYPE = 3;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Depo":
                            switch (eskiSayfa)
                            {
                                case 18:
                                    opp.DOCUMENT_TYPE = 20;
                                    break;
                                case 20:
                                    opp.DOCUMENT_TYPE = 24;
                                    break;
                                case 24:
                                    opp.DOCUMENT_TYPE = 21;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }

                    oppSave(db, opp, eskiSayfa);
                    //db.OPPORTUNITYMASTERs.Attach(opp);
                    //var entry = db.Entry(opp);
                    //entry.Property(e => e.DOCUMENT_TYPE).IsModified = true; 
                    //db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return RedirectToAction(gidecegisayfa(eskiSayfa));

        }

        private void oppSave(VdbSoftEntities db, OPPORTUNITYMASTER opp, int eskiSayfa)
        {
            OPPORTUNITYHISTORY history = new OPPORTUNITYHISTORY();
            history.ACTUEL_DOCUMENT_TYPE = opp.DOCUMENT_TYPE;
            history.LAST_DOCUMENT_TYPE = eskiSayfa;
            history.OPPORTUNITY_CODE = opp.OPPORTUNITY_CODE;
            history.LAST_UPDATE = DateTime.Now;
            history.LAST_UPDATE_USER = getUserCode();
            db.OPPORTUNITYHISTORies.Add(history);
            db.OPPORTUNITYMASTERs.Attach(opp);
            var entry = db.Entry(opp);
            entry.Property(e => e.DOCUMENT_TYPE).IsModified = true;
            db.SaveChanges();
        }


        public string gidecegisayfa(int eskiSayfa)
        {
            string sayfa = null;
            switch (eskiSayfa)
            {
                case 1:
                    sayfa = "Opportunity";
                    break;
                case 2:
                    sayfa = "Offer";
                    break;
                case 3:
                    sayfa = "Order";
                    break;
                case 4:
                    sayfa = "DispatchNote";
                    break;
                case 5:
                    sayfa = "Invoice";
                    break;
                case 6:
                    sayfa = "Sample";
                    break;
                case 15:
                    sayfa = "Draft";
                    break;
                case 16:
                    sayfa = "InReview";
                    break;
                case 17:
                    sayfa = "Pending";
                    break;
                case 18:
                    sayfa = "Approved";
                    break;
                case 19:
                    sayfa = "Edited";
                    break;
                case 20:
                    sayfa = "Processed";
                    break;
                case 21:
                    sayfa = "Shipped";
                    break;
                case 22:
                    sayfa = "Cancelled";
                    break;
                case 23:
                    sayfa = "LineSheets";
                    break;
                case 24:
                    sayfa = "Shipping";
                    break;
                case 25:
                    sayfa = "ShippingConfirm";
                    break;
                default:
                    sayfa = "Opportunity";
                    break;
            }
            return sayfa;
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



        [ValidateInput(false)]
        public ActionResult OrderMasterGrid(int DOCUMENT_TYPE)
        {

            return View(getOpp(DOCUMENT_TYPE));
        }
        [ValidateInput(false)]
        public ActionResult OrderMasterGridPartial(int DOCUMENT_TYPE)
        {
            return PartialView("_OrderMasterGrid", getOpp(DOCUMENT_TYPE));
        }
        [ValidateInput(false)]
        public ActionResult OrderDetailGrid(string customerID)
        {
            ViewData["COURSE_CODE"] = customerID;
            int cID = Convert.ToInt32(customerID);
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            var model = from d in db.OPPORTUNITYDETAILs
                        where d.OPPORTUNITY_CODE == cID
                        select d;
            return PartialView("_OrderDetailGrid", model.ToArray());
        }


        public ActionResult orderPartial(string clickedButton)
        {
            int oppCode = 1, rowCode = 1;
            if (clickedButton != null)
            {
                string[] keys = clickedButton.Split('|');
                rowCode = Convert.ToInt32(keys[0]);
                oppCode = Convert.ToInt32(keys[1]);
            }
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            OPPORTUNITYDETAIL model = db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == oppCode).Where(x => x.ROW_ORDER_NO == rowCode).FirstOrDefault();

            List<StokWareHouseViewModel> depolar = (from product in db.STOKACTUALs
                                                    join warehouse in db.STOKWAREHOUSEs on product.DEPOT_ID equals warehouse.ID
                                                    where product.STOK_CODE == model.STOK_CODE
                                                    select new StokWareHouseViewModel
                                                      {
                                                          WAREHOUSE_NAME = warehouse.NAME,
                                                          STOK_CODE = product.STOK_CODE,
                                                          TOTAL_QUANTITIY = product.QUANTITY
                                                      }).ToList();
            int i = 0;
            bool stokTamam = false;
            foreach (StokWareHouseViewModel item in depolar)
            {
                int miktar = db.STOKACTUALORDERs.Where(x => x.STOK_CODE == item.STOK_CODE).Select(x => x.QUANTITY).FirstOrDefault() ?? 0;
                item.QUANTITY = item.TOTAL_QUANTITIY - miktar;
                if (!stokTamam)
                    if (item.QUANTITY > model.QUANTITY)
                    {
                        depolar.ElementAt(i).CHOSE = (int)model.QUANTITY;
                        stokTamam = true;
                    }
                depolar.ElementAt(i).QUANTITY = item.QUANTITY;
                i++;
            }


            return PartialView("_orderPartial", new Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL>(depolar, model));
        }

        public ActionResult updateWareHouse(MVCxGridViewBatchUpdateValues<StokWareHouseViewModel, int> updateValues)
        {
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                {
                    int i;
                    // UpdateProduct(product, updateValues);
                }
            }
            return OrderMasterGrid(3);
        }
    }
}
