﻿using System;
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

        #region arayüzler
        public ActionResult Index()
        {
            RolePrincipal r = (RolePrincipal)User;
            string[] rol = r.GetRoles();
            ActionResult returnValue;
            switch (rol.ElementAt(0))
            {
                case "Admin":
                    returnValue = RedirectToAction("Opportunity");
                    break;
                case "Temsilci":
                    returnValue = RedirectToAction("Opportunity");
                    break;
                case "Bayi":
                    returnValue = RedirectToAction("Draft");
                    break;
                case "Depo":
                    returnValue = RedirectToAction("Processed");
                    break;
                default:
                    returnValue = RedirectToAction("Opportunity");
                    break;
            }
            return returnValue;
        }

        public ActionResult Opportunity()
        {
            RolePrincipal r = (RolePrincipal)User;
            string[] rol = r.GetRoles();
            if (!rol.Contains("Admin") || !rol.Contains("Temsilci"))
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


        //public ActionResult Shipping()
        //{
        //    return View(getShipping(1));//sevk ediliyor
        //}
        public ActionResult ShippingConfirm()
        {

            return View(getShippingMaster(1));//sevk hazırlanıyopr tip 1
        }
        public ActionResult ShippingWait()
        {
            return View(getShippingMaster(2));//sevk toplu onay Bekliyor 2
        }
        public ActionResult Shipping()
        {

            return View(getShippingMaster(3));//sevk ediliyor tip tip 3
        }
        public ActionResult Shipped()
        {
            return View(getShippingMaster(4));//sevk edildi tip 4
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

        #endregion

        #region grid işlemler

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
                        case "Temsilci":
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
                                    Session.Remove("Sepet");
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
        public ActionResult CustomButtonClick(string clickedButton, string shipping_type = "-1")
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
                                case 15:
                                    Session.Remove("Sepet");
                                    opp.DOCUMENT_TYPE = 3;
                                    break;
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

                        case "Temsilci":
                            switch (eskiSayfa)
                            {
                                case 15:
                                    Session.Remove("Sepet");
                                    opp.DOCUMENT_TYPE = 3;
                                    break;
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
                                    Session.Remove("Sepet");
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
                            int newShippingType = -1;
                            switch (shipping_type)
                            {
                                case "1":
                                    newShippingType = 2;
                                    break;
                                case "2":
                                    newShippingType = 3;
                                    break;
                                case "3":
                                    newShippingType = 4;
                                    break;
                                case "4":
                                    newShippingType = 5;//fix me sevk ediledikten sopnra tipi ne olacak
                                    break;
                                default:
                                    break;
                            }
                            approvedShipping(OPPORTUNITY_CODE, Convert.ToInt32(shipping_type), newShippingType);
                            break;
                        default:
                            break;
                    }
                    if (!rol[0].Equals("Depo"))//depo işlemi değilse documan tipi değişsin                        
                    {
                        oppSave(db, opp, eskiSayfa);
                    }
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
        public void approvedShipping(int oppCode, int oldShippingType, int newShippingType)
        {
            if (oldShippingType != -1 || newShippingType != -1)
            {
                int userCode = getUserCode();
                int userWareHouseID;
                using (VdbSoftEntities db = new VdbSoftEntities(dbName))
                {
                    userWareHouseID = Convert.ToInt32(db.USERS.Find(userCode).USER_RIGHT);
                    // opp kodu ve warehouse u ve oldshippingtype belkli olan kayıtları değiştir.
                    string sorgu = String.Format("update  STOKACTUALORDER set SHIPPING_TYPE={0} where OPPORTUNITY_CODE={1} and SHIPPING_TYPE={2} and WAREHOUSE={3}", newShippingType, oppCode, oldShippingType, userWareHouseID);
                    db.Database.ExecuteSqlCommand(sorgu);
                    db.SaveChanges();
                    int hazirlanmasiBeklenenUrunSayisi = db.STOKACTUALORDERs.Where(x => x.OPPORTUNITY_CODE == oppCode).Where(x => x.SHIPPING_TYPE == 1).Count();
                    if (hazirlanmasiBeklenenUrunSayisi == 0)
                    {
                        sorgu = String.Format("update  STOKACTUALORDER set SHIPPING_TYPE={0} where OPPORTUNITY_CODE={1} and SHIPPING_TYPE={2} ", 3, oppCode, 2);
                        db.Database.ExecuteSqlCommand(sorgu);
                    }
                    db.SaveChanges();
                }

            }


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

        #endregion

        #region order depo toplu onaylama
        [ValidateInput(false)]
        public ActionResult OrderAllProductWareHouseSubmit(int oppMasterCode, string wareHouse)
        {
            int wareHouseID = Convert.ToInt32(wareHouse.Trim());
            WareHouseController.saveProductuAllWareHouse(oppMasterCode, wareHouseID);
            return RedirectToAction("Order");
        }

        #endregion

        #region genel gridler
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

            ViewData["OPPORTUNITY_CODE"] = customerID;
            int cID = Convert.ToInt32(customerID);
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            List<OppDetail> model = (from d in db.OPPORTUNITYDETAILs
                                     //join picture in db.STOKCARDPICTUREs on d.STOK_ID equals picture.STOK_ID
                                     where d.OPPORTUNITY_CODE == cID // && picture.TYPE == 2
                                     select new OppDetail
                                     {
                                         OPPORTUNITY_CODE = d.OPPORTUNITY_CODE,
                                         ROW_ORDER_NO = d.ROW_ORDER_NO,
                                         PRODUCT_NAME = d.PRODUCT_NAME,
                                         QUANTITY = d.QUANTITY,
                                         TOTAL = d.TOTAL,
                                         CUR_TYPE = d.CUR_TYPE,
                                         UNIT_PRICE = d.UNIT_PRICE,
                                         STOK_ID = d.STOK_ID
                                         //  Picture = picture.PATH
                                     }).ToList();

            List<OppDetail> modelPicture = new List<OppDetail>();
            foreach (var item in model)
            {
                item.Picture = db.STOKCARDPICTUREs.Where(x => x.STOK_ID == item.STOK_ID).Where(x => x.TYPE == 2).Select(x => x.PATH).FirstOrDefault();
                modelPicture.Add(item);
            }



            TempData["DOCUMENT_TYPE"] = db.OPPORTUNITYMASTERs.Find(cID).DOCUMENT_TYPE;

            return PartialView("MasterDetailDetailPartial", modelPicture.ToArray());
        }
        #endregion

        #region order gridleri
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
            //var model = from d in db.OPPORTUNITYDETAILs
            //            where d.OPPORTUNITY_CODE == cID
            //            select d;


            List<OppDetail> model = (from d in db.OPPORTUNITYDETAILs
                                     where d.OPPORTUNITY_CODE == cID
                                     select new OppDetail
                                     {
                                         OPPORTUNITY_CODE = d.OPPORTUNITY_CODE,
                                         ROW_ORDER_NO = d.ROW_ORDER_NO,
                                         PRODUCT_NAME = d.PRODUCT_NAME,
                                         QUANTITY = d.QUANTITY,
                                         TOTAL = d.TOTAL,
                                         CUR_TYPE = d.CUR_TYPE,
                                         UNIT_PRICE = d.UNIT_PRICE,
                                         STOK_ID = d.STOK_ID
                                     }).ToList();



            List<OppDetail> modelPicture = new List<OppDetail>();
            foreach (var item in model)
            {
                item.Picture = db.STOKCARDPICTUREs.Where(x => x.STOK_ID == item.STOK_ID).Where(x => x.TYPE == 2).Select(x => x.PATH).FirstOrDefault();
                modelPicture.Add(item);
            }

            return PartialView("_OrderDetailGrid", modelPicture.ToList());
        }


        public ActionResult orderPartial(string clickedButton = null)
        {
            //int oppCode = 1, rowCode = 1;
            //if (clickedButton != null)
            //{
            //    string[] keys = clickedButton.Split('|');
            //    rowCode = Convert.ToInt32(keys[0]);
            //    oppCode = Convert.ToInt32(keys[1]);
            //}
            //TempData["parametre"] = String.Format("{0}|{1}", oppCode, rowCode);
            //Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL> param = orderWareHouseCal(oppCode, rowCode);
            //List<StokWareHouseViewModel> depolar = param.Item1;
            //return PartialView("_orderPartial", param);

            int oppCode = 1, rowCode = 1;
            Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL> param = new Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL>(new List<StokWareHouseViewModel>(),new OPPORTUNITYDETAIL());

            if (clickedButton != null)
            {
                string[] keys = clickedButton.Split('|');
                rowCode = Convert.ToInt32(keys[0]);
                oppCode = Convert.ToInt32(keys[1]);
                param = orderWareHouseCal(oppCode, rowCode);
            }
            TempData["parametre"] = String.Format("{0}|{1}", oppCode, rowCode);
         //   Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL> param = orderWareHouseCal(oppCode, rowCode);
         //   List<StokWareHouseViewModel> depolar = param.Item1;
            return PartialView("_orderPartial", param);
        }

        #endregion

        #region sevk gridleri
        /// <summary>
        /// dökümantipi olarak bütün shipingler 24 olsun
        /// ama shipingtipleri farklı olmalı ki ne durumda olduğunu görebilelim.
        /// sevkhaıorlanıyor=1 sevkonay bekliyor=2 sevk ediliyor=3 sevkedildi=4
        /// shippingConfirm=1 shipingWait=2 shipping=3 shipped=4
        /// </summary>
        /// <param name="DOCUMENT_TYPE">shipingType</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShippingConfirmMasterPartial(int shipping_type)
        {
            TempData["DOCUMENT_TYPE"] = shipping_type;
            return PartialView("_ShippingConfirmMasterPartial", getShippingMaster(shipping_type));
        }

        public ActionResult ShippingConfirmDetailPartial(int shipping_type, int oppCode)
        {
            TempData["DOCUMENT_TYPE"] = shipping_type;
            ViewData["OPPORTUNITY_CODE"] = oppCode;
            IEnumerable<ShippingViewModel> resultModel;
            if (User.IsInRole("Depo"))
            {
                int userCode = getUserCode();
                using (VdbSoftEntities db = new VdbSoftEntities(dbName))
                {
                    int userWareHouseID = Convert.ToInt32(db.USERS.Find(userCode).USER_RIGHT);
                    resultModel = getShipping(shipping_type, userWareHouseID, oppCode);
                }

            }
            else
                resultModel = getShipping(shipping_type, oppCode);
            return PartialView("_ShippingConfirmDetailPartial", resultModel);
        }

        public IEnumerable<OrderMasterViewModel> getShippingMaster(int shippingType)
        {
            IEnumerable<OrderMasterViewModel> shipping = getOpp(24);
            TempData["DOCUMENT_TYPE"] = shippingType;
            List<OrderMasterViewModel> resultModel = new List<OrderMasterViewModel>();
            if (User.IsInRole("Depo"))
            {
                int userCode = getUserCode();
                using (VdbSoftEntities db = new VdbSoftEntities(dbName))
                {

                    int userWareHouseID = Convert.ToInt32(db.USERS.Find(userCode).USER_RIGHT);
                    foreach (OrderMasterViewModel item in shipping)
                    {
                        int count = getShipping(shippingType, userWareHouseID, item.OPPORTUNITY_CODE).Count();
                        if (count > 0)
                        {
                            resultModel.Add(item);
                        }
                    }
                }
            }
            else
            {
                foreach (OrderMasterViewModel item in shipping)
                {
                    int count = getShipping(shippingType, item.OPPORTUNITY_CODE).Count();
                    if (count > 0)
                    {
                        resultModel.Add(item);
                    }
                }
            }

            return resultModel;
        }


        [ValidateInput(false)]

        public IEnumerable<ShippingViewModel> getShipping(int oppShippingType, int wareHouseID, int oppMasterCode)
        {
            TempData["DOCUMENT_TYPE"] = oppShippingType;
            VdbSoftEntities db = db = new VdbSoftEntities(dbName);
            List<ShippingViewModel> model = (from d in db.STOKACTUALORDERs
                                             join stk in db.STOKCARDs on d.STOK_CODE equals stk.CODE
                                             join ware in db.STOKWAREHOUSEs on d.WAREHOUSE equals ware.ID
                                             where d.SHIPPING_TYPE == oppShippingType && d.WAREHOUSE == wareHouseID && d.OPPORTUNITY_CODE == oppMasterCode
                                             select new ShippingViewModel
                                             {
                                                 ID = d.ID,
                                                 OPPORTUNITY_CODE = (int)d.OPPORTUNITY_CODE,
                                                 ROW_ORDER_NO = (int)d.ROW_ORDER_NO,
                                                 QUANTITY = d.QUANTITY,
                                                 PRODUCT_NAME = stk.NAME_TR,
                                                 WAREHOUSE_NAME = ware.NAME,
                                                 WAREHOUSE_ID = ware.ID,
                                                 STOK_ID = stk.ID,
                                                 STOK_CODE = stk.CODE
                                             }).Distinct().ToList();

            List<ShippingViewModel> modelPicture = new List<ShippingViewModel>();
            foreach (var item in model)
            {
                item.Picture = db.STOKCARDPICTUREs.Where(x => x.STOK_ID == item.STOK_ID).Where(x => x.TYPE == 2).Select(x => x.PATH).FirstOrDefault();
                modelPicture.Add(item);
            }
            return modelPicture;
        }
        public IEnumerable<ShippingViewModel> getShipping(int oppMasterType, int oppMasterCode)
        {

            VdbSoftEntities db = db = new VdbSoftEntities(dbName);
            List<ShippingViewModel> model = (from d in db.STOKACTUALORDERs
                                             join stk in db.STOKCARDs on d.STOK_CODE equals stk.CODE
                                             join ware in db.STOKWAREHOUSEs on d.WAREHOUSE equals ware.ID
                                             where d.SHIPPING_TYPE == oppMasterType && d.OPPORTUNITY_CODE == oppMasterCode
                                             select new ShippingViewModel
                                             {
                                                 ID = d.ID,
                                                 OPPORTUNITY_CODE = (int)d.OPPORTUNITY_CODE,
                                                 ROW_ORDER_NO = (int)d.ROW_ORDER_NO,
                                                 QUANTITY = d.QUANTITY,
                                                 PRODUCT_NAME = stk.NAME_TR,
                                                 WAREHOUSE_NAME = ware.NAME,
                                                 WAREHOUSE_ID = ware.ID,
                                                 STOK_ID = stk.ID,
                                                 STOK_CODE = stk.CODE
                                             }).Distinct().ToList();

            List<ShippingViewModel> modelPicture = new List<ShippingViewModel>();
            foreach (var item in model)
            {
                item.Picture = db.STOKCARDPICTUREs.Where(x => x.STOK_ID == item.STOK_ID).Where(x => x.TYPE == 2).Select(x => x.PATH).FirstOrDefault();
                modelPicture.Add(item);
            }
            return modelPicture;
        }
        #endregion
    }
}
