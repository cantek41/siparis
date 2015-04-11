using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
namespace siparis.Controllers
{
    //[Authorize (Roles="Admin,Bayi")]
    public class BaseController : Controller
    {
        public static string dbName = "VdbSoft";

        public void checkCart()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);
            int sepetID = (from d in db.OPPORTUNITYMASTERs
                           where d.OPEN_CLOSE == 0 && d.DOCUMENT_TYPE == 15 && d.APPOINTED_USER_CODE == 1// user ID gelmeli Fix Me
                           select d.OPPORTUNITY_CODE).FirstOrDefault();
            if (sepetID != 0)
            {
                Session.Add("Sepet", sepetID);
            }

        }

        public int getUserCode()
        {
            int userCode = 0;
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                userCode = (int)db.aspnet_Users.Where(x => x.UserName == User.Identity.Name).Select(x => x.UserCode).FirstOrDefault();
                return userCode;
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


        

        public ActionResult changeLanguage(string lang)
        {
            Res.languege = lang;
            return RedirectToAction("Index", "Home");
        }
        public IEnumerable<OPPORTUNITYDETAIL> getCartProduct()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);
            IEnumerable<OPPORTUNITYDETAIL> model = from d in db.OPPORTUNITYDETAILs
                                                   join master in db.OPPORTUNITYMASTERs on d.OPPORTUNITY_CODE equals master.OPPORTUNITY_CODE
                                                   where master.OPEN_CLOSE == 0 && master.APPOINTED_USER_CODE == 1 && master.DOCUMENT_TYPE == 15//fix me 
                                                   select d;
            List<OPPORTUNITYDETAIL> sepet = new List<OPPORTUNITYDETAIL>();
            foreach (var item in model)
            {
                item.STOKCARD = db.STOKCARDs.Where(x => x.ID == item.STOK_ID).FirstOrDefault();
                item.STOKCARD.STOKCARDPICTUREs = db.STOKCARDPICTUREs.Where(x => x.STOK_ID == item.STOK_ID).ToList();
                sepet.Add(item);
            }

            return sepet;
        }
        public static STOKCARD getProduct(int ID = 1)
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);
            STOKCARD st = (from d in db.STOKCARDs
                           where d.ID == ID
                           select d).FirstOrDefault();
            return st;
        }

        public void addProductGroup(STOKGROUP stokgrup)
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);

            stokgrup.CREATE_DATE = DateTime.Now;
            stokgrup.LAST_UPDATE = DateTime.Now;
            db.STOKGROUPs.Add(stokgrup);
            db.SaveChanges();


        }
        public void EditProductGroup(STOKGROUP stokgrup)//ID almalı mı
        {


            siparis.Models.VdbSoftEntities db = new VdbSoftEntities(dbName);

            stokgrup.CREATE_DATE = DateTime.Now;
            stokgrup.LAST_UPDATE = DateTime.Now;
            //  db.STOKGROUPs.(stokgrup);
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
    }
}