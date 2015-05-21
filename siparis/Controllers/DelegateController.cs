using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using siparis.Models;
using DevExpress.Xpo;
using System.Data.Entity.Validation;

namespace siparis.Controllers
{
    public class DelegateController : BaseController
    {
        //
        // GET: /Delegate/
        public ActionResult Index()
        {
            List<STOKCARDViewModel> model = new List<STOKCARDViewModel>();
            ViewData["Company"] = getCompanyList();
            ViewData["WareHouse"] = getWareHouse();
            ViewData["DeliveryType"] = getWareDeliveryType();
            ViewData["Adress"] = null;
            return View(model);
        }

        public ActionResult MultiColumnComboBoxPartial()
        {
            ViewData["Company"] = getCompanyList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult deliveryAdress()
        {

            int company = (Request.Params["Company"] != null) ? int.Parse(Request.Params["Company"]) : -1;
            int drpDeliveryAddress = (!String.IsNullOrEmpty(Request.Params["drpDeliveryAddress"])) ? int.Parse(Request.Params["drpDeliveryAddress"]) : -1;
            TempData["drpDeliveryAddress"] = drpDeliveryAddress;
            TempData["Company"] = company;
            ViewData["Adress"] = getComapanyAdress(company);
            return PartialView();
        }
        [ValidateInput(false)]
        public ActionResult gridView()
        {
            int company = (int)TempData["Company"];
            TempData["Company"] = company;
            int drpDeliveryAddress = (int)TempData["drpDeliveryAddress"];
            TempData["drpDeliveryAddress"] = drpDeliveryAddress;
            List<STOKCARDViewModel> model = getStokCard(company);
            return PartialView(model);
        }
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, NoStore = true, VaryByParam = "company")]
        public List<STOKCARDViewModel> getStokCard(int company)
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                string sorgu = String.Format(" select * from(select STOKCARD.ID, SUM(TOTAL_QUANTITIY) as UNIT,STOKCARD.UPPER_CODE,STOKCARD.DES_TR,STOKCARDUSERPRICE.PRICE as UNIT_PRICE,STOKCARDUSERPRICE.CUR_TYPE,NAME_TR,STOKCARDPICTURE.PATH as STOKCARDPICTUREs,STOKSUBGROUP.EXP_TR as SUB_GRUP1s,STOKMAINGROUP.EXP_TR as MAIN_GRUPs,STOKCARD.CODE,SUB_GRUP2,BRAND_CODE,BODY_CODE,CATEGORY_CODE,STOKCARD.COLOR_CODE,MODEL_CODE,PACK_CODE,RAYON_CODE,SEASON_CODE,SECTOR_CODE,  ROW_NUMBER()  OVER(PARTITION BY STOKCARD.ID ORDER BY STOKCARD.ID DESC ) rn from STOKCARD left join STOKCARDPICTURE on STOKCARDPICTURE.STOK_ID = STOKCARD.ID left join STOKWAREHOUSEPRODUCT on STOKWAREHOUSEPRODUCT.STOK_ID=STOKCARD.ID left join STOKCARDUSERPRICE on STOKCARDUSERPRICE.STOK_ID=STOKCARD.ID and STOKCARDUSERPRICE.COMPANY_CODE={0} left join STOKMAINGROUP on STOKMAINGROUP.ID = STOKCARD.MAIN_GRUP left join STOKSUBGROUP on STOKSUBGROUP.ID = STOKCARD.SUB_GRUP1 GROUP BY STOKCARD.UPPER_CODE,STOKCARD.DES_TR,STOKCARD.ID,STOKCARDUSERPRICE.PRICE,STOKCARDUSERPRICE.CUR_TYPE,NAME_TR,STOKCARDPICTURE.PATH,STOKSUBGROUP.EXP_TR,STOKMAINGROUP.EXP_TR,STOKCARD.CODE,SUB_GRUP2,BRAND_CODE,BODY_CODE,CATEGORY_CODE,STOKCARD.COLOR_CODE,MODEL_CODE,PACK_CODE,RAYON_CODE,SEASON_CODE,SECTOR_CODE) a where rn=1;", company);
                List<STOKCARDViewModel> stok = db.Database.SqlQuery<STOKCARDViewModel>(sorgu).ToList<STOKCARDViewModel>();
                return stok;
            }

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult addStok(STOKCARDViewModel item)
        {
            //var model = db.DOCUMENTS;
            //try
            //{
            //    item.PATH = filePath;
            //    item.PRIORITY = 0;
            //    item.ROW_ORDER_NO = 0;
            //    item.VISIBLE = true;
            //    item.CREATE_USER = getCurrentUserName();
            //    item.CREATE_DATE = DateTime.Now;
            //    item.LAST_UPDATE = DateTime.Now;
            //    item.LAST_UPDATE_USER = getCurrentUserName();
            //    filePath = null;
            //    /// DOCUMENT modelItem = model.FirstOrDefault(it => it.DOCUMENT_CODE == item.DOCUMENT_CODE);
            //    if (item != null)
            //    {
            //        db.DOCUMENTS.Attach(item);
            //        db.Entry(item).State = EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ViewData["EditError"] = e.Message;
            //}
            ////}
            ////else
            ////    ViewData["EditError"] = "Please, correct all errors.";
            //var model1 = from d in db.DOCUMENTS
            //             join b in db.CHAPTERS on d.CHAPTER_CODE equals b.CHAPTER_CODE
            //             join cs in db.COURSES on d.COURSE_CODE equals cs.COURSE_CODE
            //             join lesson in db.LESSONS on d.LESSON_CODE equals lesson.LESSON_CODE
            //             select new { d.DOCUMENT_CODE, CHAPTER_CODE = b.CHAPTER_NAME, d.DOCUMENT_TYPE, d.DURATION, d.LINK_TYPE, d.PATH, LESSON_CODE = lesson.LESSON_NAME, COURSE_CODE = cs.COURSE_NAME, d.DOCUMENT_NAME };
            //  return PartialView("_GridView1Partial", model.ToList());
            int company = (int)TempData["Company"];
            TempData["Company"] = company;
            int drpDeliveryAddress = (int)TempData["drpDeliveryAddress"];
            TempData["drpDeliveryAddress"] = drpDeliveryAddress;
            AddCart(company, item);
            List<STOKCARDViewModel> model = getStokCard(company);
            return PartialView("gridView", model);
        }

        public bool AddCart(int companyCode, STOKCARDViewModel item)
        {
            try
            {
                using (VdbSoftEntities db = new VdbSoftEntities(dbName))
                {
                    #region Sepet Var mı
                    checkCart(companyCode);
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
                        sepet.COMPANY_CODE = companyCode;
                        sepet.CONTACT_CODE = 0;
                        sepet.DOCUMENT_DATE = DateTime.Now;
                        sepet.CREATE_DATE = DateTime.Now;
                        sepet.CREATE_USER = User.Identity.Name;
                        sepet.APPOINTED_USER_CODE = getUserCode();
                        sepet.EXT01 = (int)TempData["drpDeliveryAddress"];
                        db.OPPORTUNITYMASTERs.Add(sepet);
                        db.SaveChanges();
                        Session.Add("Sepet", sepetID);
                    }
                    #endregion


                    OPPORTUNITYDETAIL opportunitdetails = new OPPORTUNITYDETAIL();

                    opportunitdetails.OPPORTUNITY_CODE = Convert.ToInt32(Session["Sepet"]);
                    int row;
                    if (db.OPPORTUNITYDETAILs.Count(x => x.OPPORTUNITY_CODE == opportunitdetails.OPPORTUNITY_CODE) == 0)
                    {
                        row = 0;
                    }
                    else row = db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == opportunitdetails.OPPORTUNITY_CODE).Max(x => x.ROW_ORDER_NO);

                    ProfileInfo profil = (ProfileInfo)Session["profilim"];
                    opportunitdetails.ROW_ORDER_NO = row + 1;
                    opportunitdetails.CUR_TYPE = item.CUR_TYPE.Replace("\"","").Trim();
                    opportunitdetails.UNIT_PRICE = (float)item.UNIT_PRICE;
                   // opportunitdetails.UNIT = item.UNIT.ToString();
                    opportunitdetails.STOK_ID = item.ID;
                    opportunitdetails.STOK_CODE = item.CODE.Replace("\"","");
                    opportunitdetails.QUANTITY = item.QUNATITIY;
                    opportunitdetails.VERSION = "V1";
                    opportunitdetails.TOTAL = item.QUNATITIY * (float)item.UNIT_PRICE;
                    opportunitdetails.PRODUCT_NAME = item.NAME_TR.Replace("\"", ""); ;
                    opportunitdetails.EXPLANATION = item.DES_TR.Replace("\"", ""); ;
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

                    //  ViewBag.kontrol = "" + stokcart.CUR_TYPE;
                }
                TotalHesapla(Convert.ToInt32(Session["Sepet"]));
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}