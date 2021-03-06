﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Globalization;
using System.Threading;
using DevExpress.Xpo;
namespace siparis.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {


        public static string dbName = "VdbSoft";
        public ActionResult search(string param)
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                var model = (from d in db.STOKCARDs
                             where d.CODE.Contains(param) || d.NAME_TR.Contains(param)
                             select d);
            }
            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "ID";
            info.SortDirection = "ascending";
            info.PageSize = 12;
            info.PageCount = -1;
            info.CurrentPageIndex = 0;
            ViewBag.SortingPagingInfo = info;
            return null;
        }

        public void checkCart(int companyCode=-1)
        {
            if (companyCode==-1)
            {
                ProfileInfo profil = (ProfileInfo)Session["profilim"];
                companyCode = profil.FirmaKodu;                
            }                            
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);
            int sepetID = (from d in db.OPPORTUNITYMASTERs
                           where d.OPEN_CLOSE == 0 && d.DOCUMENT_TYPE == 15 && (int)d.COMPANY_CODE == companyCode
                           select d.OPPORTUNITY_CODE).FirstOrDefault();
            if (sepetID != 0)
            {
                Session.Add("Sepet", sepetID);
            }
            else if (Session["Sepet"]!=null)
            {
                Session.Remove("Sepet");
            }
                

        }

        public static Dictionary<int, string> getWareHouse()
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                return (from d in db.STOKWAREHOUSEs
                        select new { Key = d.ID, Value = d.NAME }).ToDictionary(t => t.Key, t => t.Value);
            }
        }
        public static Dictionary<int, string> getComapanyAdress(int companyCode)
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                return (from d in db.ADDRESSes
                        where d.COMPANY_CODE==companyCode
                        select new { Key = d.ADDRESS_CODE, Value = d.ADDRESS1 }).ToDictionary(t => t.Key, t => t.Value);
            }
        }
        public static Dictionary<int, string> getWareDeliveryType()
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                return (from d in db.GROUPS
                        where d.GROUP_CODE==69
                        select new { Key = d.ROW_ORDER_NO, Value = d.EXP_TR }).ToDictionary(t => t.Key, t => t.Value);
            }
        }
        public int getUserCode()
        {
            int userCode = 0;
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                userCode = (int)db.aspnet_Users.Where(x => x.UserName == User.Identity.Name).Select(x => x.UserCode).FirstOrDefault();
                return userCode;
                // fix me
            }

        }

        public int getUserCode(string name)
        {
            int userCode = 0;
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                userCode = (int)db.aspnet_Users.Where(x => x.UserName == name).Select(x => x.UserCode).FirstOrDefault();
                return userCode;
                // fix me
            }

        }
        public string getCompany()
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                int userContactCode = (int)db.USERS.Where(x => x.USER_NAME == User.Identity.Name).Select(x => x.CONTACT_CODE).FirstOrDefault();
                COMPANY company = db.COMPANies.Find(db.CONTACTs.Where(x => x.CONTACT_CODE == userContactCode).Select(x => x.COMPANY_CODE).FirstOrDefault());
                return company.COMPANY_NAME;
            }
        }

        public List<COMPANY> getCompanyList()
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                return db.COMPANies.ToList();
            }
        }

        [AllowAnonymous]
        public ActionResult changeLanguage(string lang)
        {
            if (Session["lang"] == null)
            {
                Session.Add("lang", lang);
            }
            else
            {
                Session["lang"] = lang;
            }
            return RedirectToAction("Index", "Home");
        }

        public IEnumerable<OPPORTUNITYDETAIL> getCartProduct()
        {
            int userCode = getUserCode();
            int oppCode = Convert.ToInt32(Session["Sepet"]);
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);
            IEnumerable<OPPORTUNITYDETAIL> model = from d in db.OPPORTUNITYDETAILs
                                                   where d.OPPORTUNITY_CODE == oppCode//fix me 
                                                   select d;
            List<OPPORTUNITYDETAIL> sepet = new List<OPPORTUNITYDETAIL>();
            ViewBag.Hata = false;
            foreach (var item in model)
            {
                item.STOKCARD = db.STOKCARDs.Where(x => x.ID == item.STOK_ID).FirstOrDefault();
                //   item.STOKCARD.STOKCARDPICTUREs =  db.STOKCARDPICTUREs.Where(x => x.STOK_ID == item.STOK_ID).ToList();
                //fix me
                ////
                Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL> param = orderWareHouseCal(item.OPPORTUNITY_CODE, item.ROW_ORDER_NO);
                List<StokWareHouseViewModel> depolar = param.Item1;
                int totalStok = 0;
                foreach (StokWareHouseViewModel depo in depolar)
                {
                    totalStok += depo.QUANTITY ?? 0;
                }

                if (totalStok < item.QUANTITY)
                {
                    ViewBag.Hata = true;

                    item.EXPLANATION = siparis.Resorces.Language.WarningStok + totalStok;
                }
                else
                {
                    item.EXPLANATION = siparis.Resorces.Language.StokMessage + totalStok;
                }
                item.TAX_PERCENT = totalStok;

                sepet.Add(item);

            }
            return sepet;
        }
        public STOKCARD getProduct(int ID = 1)
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);
            STOKCARD st = (from d in db.STOKCARDs
                           where d.ID == ID
                           select d
                           ).FirstOrDefault();
            ProfileInfo profil = (ProfileInfo)Session["profilim"];
            st.UNIT = db.STOKWAREHOUSEPRODUCTs.Where(x => x.STOK_ID == ID).Sum(x => x.TOTAL_QUANTITIY);
            var price = db.STOKCARDUSERPRICEs.Where(x => x.COMPANY_CODE == profil.FirmaKodu).Where(x => x.STOK_ID == ID).FirstOrDefault();
            if (price != null)
            {
                st.UNIT_PRICE = price.PRICE;
                st.CUR_TYPE = price.CUR_TYPE;
            }

            return st;
        }

        public void addProductGroup(STOKCATEGORY stokgrup)
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);

            stokgrup.CREATE_DATE = DateTime.Now;
            stokgrup.LAST_UPDATE = DateTime.Now;
            db.STOKCATEGORies.Add(stokgrup);
            db.SaveChanges();


        }
        public void EditProductGroup(STOKCATEGORY stokgrup)//ID almalı mı
        {


            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);

            stokgrup.CREATE_DATE = DateTime.Now;
            stokgrup.LAST_UPDATE = DateTime.Now;
            //  db.STOKCATEGORies.(stokgrup);
            db.Entry(stokgrup).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


        }

        public void AddProduct(STOKCARD stokcart)
        {

            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);

            stokcart.CREATE_DATE = DateTime.Now;
            stokcart.LAST_UPDATE = DateTime.Now;
            db.STOKCARDs.Add(stokcart);
            db.SaveChanges();

        }


        public static IEnumerable<STOKCARD> getProductAll()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);
            var model = from d in db.STOKCARDs
                        select d;
            return model;
        }

        public void TotalHesapla(int oppMasterID)
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                float toplam = (float)db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == oppMasterID).Sum(x => x.TOTAL);
                OPPORTUNITYMASTER oppMaster = db.OPPORTUNITYMASTERs.Find(oppMasterID);
                oppMaster.TOTAL = toplam;

                db.OPPORTUNITYMASTERs.Attach(oppMaster);
                var entry = db.Entry(oppMaster);
                entry.Property(e => e.TOTAL).IsModified = true;
                db.SaveChanges();

            }
        }


        public Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL> orderWareHouseCal(int oppCode, int rowCode)
        {
            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            OPPORTUNITYDETAIL model = db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == oppCode).Where(x => x.ROW_ORDER_NO == rowCode).FirstOrDefault();

            List<StokWareHouseViewModel> depolar = (from product in db.STOKACTUALs
                                                    join warehouse in db.STOKWAREHOUSEs on product.DEPOT_ID equals warehouse.ID
                                                    where product.STOK_CODE == model.STOK_CODE
                                                    select new StokWareHouseViewModel
                                                    {
                                                        WAREHOUSE_ID = warehouse.ID,
                                                        WAREHOUSE_NAME = warehouse.NAME,
                                                        STOK_CODE = product.STOK_CODE,
                                                        TOTAL_QUANTITIY = product.QUANTITY
                                                    }).ToList();
            int i = 0;
            bool stokTamam = false;
            foreach (StokWareHouseViewModel item in depolar)
            {
                int miktar = 0;
                try
                {
                    miktar = (int)(from d in db.STOKACTUALORDERs
                                   where d.OPPORTUNITY_CODE == model.OPPORTUNITY_CODE && d.ROW_ORDER_NO == model.ROW_ORDER_NO && d.STOK_CODE == model.STOK_CODE && d.WAREHOUSE == item.WAREHOUSE_ID
                                   select d.QUANTITY).FirstOrDefault();
                }
                catch (Exception)
                {

                }


                depolar.ElementAt(i).CHOSE = miktar;


                int depoMiktar = db.STOKACTUALORDERs.Where(x => x.STOK_CODE == item.STOK_CODE).Where(x => item.WAREHOUSE_ID == x.WAREHOUSE).Select(x => x.QUANTITY).FirstOrDefault() ?? 0;

                item.QUANTITY = item.TOTAL_QUANTITIY - depoMiktar;

                item.OPPORTUNITY_CODE = model.OPPORTUNITY_CODE;
                item.ROW_ORDER_NO = model.ROW_ORDER_NO;
                depolar.ElementAt(i).QUANTITY = item.QUANTITY;
                i++;
            }


            return new Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL>(depolar, model);
        }

        public void ProfilCreate(string name)
        {

            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                int userCode = getUserCode(name);
                int userCompanyCode = (int)db.USERS.Where(x => x.USER_CODE == userCode).Select(x => x.CONTACT_CODE).FirstOrDefault();
                COMPANY company = db.COMPANies.Find(userCompanyCode);
                USER kisi = db.USERS.Where(x => x.USER_CODE == userCode).FirstOrDefault();

                var model = (from c in db.COMPANies
                             join d in db.ADDRESSes on c.COMPANY_CODE equals d.COMPANY_CODE
                             join p in db.PHONEs on c.COMPANY_CODE equals p.COMPANY_CODE
                             where d.COMPANY_CODE == company.COMPANY_CODE
                             select new
                             {
                                 Enlem = d.GPS_LATITUDE,
                                 Boylam = d.GPS_LONGITUDE,
                                 GoogleMaps = d.ADDRESS3,
                                 Adres = d.ADDRESS1,
                                 Mail = company.MAIL,
                                 Telefon = p.PHONE_NUMBER,
                                 Adres2 = d.ADDRESS2
                             }).FirstOrDefault();

                ProfileInfo profilim = new ProfileInfo();
                profilim.User_Code = userCode;
                profilim.FirmaAdi = company.COMPANY_NAME;
                profilim.FirmaKodu = company.COMPANY_CODE;
                profilim.Adi = kisi.AUSER_NAME;
                profilim.Soyadi = kisi.SURNAME;
                if (model != null)
                {
                    profilim.Telefon = model.Telefon;
                    profilim.Adres = model.Adres;
                    profilim.Mail = model.Mail;
                    profilim.Adres2 = model.Adres2;
                    profilim.Enlem = model.Enlem;
                    profilim.Boylam = model.Boylam;

                }

                Session.Add("profilim", profilim);

            }

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}