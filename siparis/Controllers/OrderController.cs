using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost]
        public bool AddCart(int stokID, int QUANTITY = 1)
        {
            try
            {
                using (VdbSoftEntities db = new VdbSoftEntities(dbName))
                {
                    #region Sepet Var mı
                    if (Session["Sepet"] == null)
                    {
                        ProfileInfo pf = (ProfileInfo)Session["profilim"];
                        OPPORTUNITYMASTER sepet = new OPPORTUNITYMASTER();
                        int sepetID = 0;
                        if (db.OPPORTUNITYMASTERs.Count() != 0)
                        {
                            sepetID = db.OPPORTUNITYMASTERs.Max(x => x.OPPORTUNITY_CODE);
                        }
                        sepetID++;
                        sepet.TOTAL = 0;
                        sepet.OPEN_CLOSE = 0;
                        sepet.EXPLANATION = "User alış veris.";
                        sepet.OPPORTUNITY_CODE = sepetID;
                        sepet.DOCUMENT_TYPE = 15;
                        sepet.VERSION = "V1";
                        sepet.COMPANY_CODE = pf.FirmaKodu;
                        sepet.CONTACT_CODE = 0;
                        sepet.DOCUMENT_DATE = DateTime.Now;
                        sepet.CREATE_DATE = DateTime.Now;
                        sepet.CREATE_USER = User.Identity.Name;
                        sepet.APPOINTED_USER_CODE = getUserCode();
                        db.OPPORTUNITYMASTERs.Add(sepet);
                        db.SaveChanges();
                        Session.Add("Sepet", sepetID);
                    }
                    #endregion

                    STOKCARD stokcart = db.STOKCARDs.Find(stokID);
                    OPPORTUNITYDETAIL opportunitdetails = new OPPORTUNITYDETAIL();

                    opportunitdetails.OPPORTUNITY_CODE = Convert.ToInt32(Session["Sepet"]);
                    int row;
                    if (db.OPPORTUNITYDETAILs.Count(x => x.OPPORTUNITY_CODE == opportunitdetails.OPPORTUNITY_CODE) == 0)
                    {
                        row = 0;
                    }
                    else row = db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == opportunitdetails.OPPORTUNITY_CODE).Max(x => x.ROW_ORDER_NO);
                    
                    ProfileInfo profil = (ProfileInfo)Session["profilim"];
                    stokcart.UNIT = db.STOKWAREHOUSEPRODUCTs.Where(x => x.STOK_ID == stokID).Sum(x => x.TOTAL_QUANTITIY);
                    var price = db.STOKCARDUSERPRICEs.Where(x => x.COMPANY_CODE == profil.FirmaKodu).Where(x => x.STOK_ID == stokID).FirstOrDefault();
                    stokcart.UNIT_PRICE = price.PRICE;
                    stokcart.CUR_TYPE = price.CUR_TYPE;
                    opportunitdetails.ROW_ORDER_NO = row + 1;
                    opportunitdetails.CUR_TYPE = stokcart.CUR_TYPE.Trim();
                    opportunitdetails.UNIT_PRICE = (float)stokcart.UNIT_PRICE;
                    opportunitdetails.UNIT = stokcart.UNIT.ToString();
                    opportunitdetails.STOK_ID = stokID;
                    opportunitdetails.STOK_CODE = stokcart.CODE;
                    opportunitdetails.QUANTITY = QUANTITY;
                    opportunitdetails.VERSION = "V1";
                    opportunitdetails.TOTAL = QUANTITY * (float)stokcart.UNIT_PRICE;
                    opportunitdetails.PRODUCT_NAME = stokcart.NAME_TR;
                    db.OPPORTUNITYDETAILs.Add(opportunitdetails);


                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var errs in ex.EntityValidationErrors)
                        {
                            foreach (var err in errs.ValidationErrors)
                            {
                                var propName = err.PropertyName;
                                var errMess = err.ErrorMessage;
                            }
                        }

                    }

                    ViewBag.kontrol = "" + stokcart.CUR_TYPE;
                }
                TotalHesapla(Convert.ToInt32(Session["Sepet"]));
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        [HttpPost]
        public bool setChartCompany(int id)
        {
            try
            {
                using (VdbSoftEntities db = new VdbSoftEntities())
                {
                    int oppCode = Convert.ToInt32(Session["Sepet"]);
                    OPPORTUNITYMASTER sepet = db.OPPORTUNITYMASTERs.Find(oppCode);
                    sepet.COMPANY_CODE = id;
                    db.Entry(sepet).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }


        [HttpGet]
        public ActionResult removeCartProduct(int oppCode, int row)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            OPPORTUNITYDETAIL item = (from d in db.OPPORTUNITYDETAILs
                                      where d.ROW_ORDER_NO == row && d.OPPORTUNITY_CODE == oppCode
                                      select d).SingleOrDefault();
            db.OPPORTUNITYDETAILs.Remove(item);
            db.SaveChanges();
            TotalHesapla(oppCode);
            return RedirectToAction("Chart");
        }

        [HttpPost]
        public ActionResult changeCartProduct(OPPORTUNITYDETAIL item)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            OPPORTUNITYDETAIL opp = (from d in db.OPPORTUNITYDETAILs
                                     where d.OPPORTUNITY_CODE == item.OPPORTUNITY_CODE && d.ROW_ORDER_NO == item.ROW_ORDER_NO
                                     select d).SingleOrDefault();
            opp.QUANTITY = item.QUANTITY;
            opp.TOTAL = item.QUANTITY * opp.UNIT_PRICE;
            db.OPPORTUNITYDETAILs.Attach(opp);
            db.Entry(opp).State = EntityState.Modified;
            db.SaveChanges();
            TotalHesapla(item.OPPORTUNITY_CODE);
            return RedirectToAction("Chart");
        }

        public ActionResult onayla(string OppCode)
        {
            int op = Convert.ToInt32(OppCode);
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                OPPORTUNITYMASTER master = db.OPPORTUNITYMASTERs.Find(op);
                List<int> rows = db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == op).Select(x=>x.ROW_ORDER_NO).ToList();
                if (sepetKontrol(op, rows))
                {
                    return RedirectToAction("Chart");
                }
                master.DOCUMENT_TYPE = 3;
                db.OPPORTUNITYMASTERs.Attach(master);
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
                Session.Remove("Sepet");
                return RedirectToAction("Index", "Home");
            }
        }

        private bool sepetKontrol(int oppMaster,List<int> rows)
        {
            bool returnValue = false;
            
            foreach (int  item in rows)
            {
                Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL> param = orderWareHouseCal(oppMaster, item);
                List<StokWareHouseViewModel> depolar = param.Item1;
                int totalStok = 0;
                foreach (StokWareHouseViewModel depo in depolar)
                {
                    totalStok += depo.QUANTITY ?? 0;
                }

                if (totalStok < param.Item2.QUANTITY)
                {
                    returnValue = true;
                    ViewBag.Hata = true;
                }
                
                
            }
            return returnValue;
        }
       
        public ActionResult Chart()
        {
            return View(getCartProduct());
        }
    }
}