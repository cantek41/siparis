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
    public class BaseController : Controller
    {
        public void checkCart()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
            int sepetID = (from d in db.OPPORTUNITYMASTERs
                           where d.OPEN_CLOSE == 0 && d.APPOINTED_USER_CODE == 1// user ID gelmeli Fix Me
                           select d.OPPORTUNITY_CODE).FirstOrDefault();
            Session.Add("Sepet", sepetID);

        }
        public IEnumerable<OPPORTUNITYDETAIL> getCartProduct()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
            IEnumerable<OPPORTUNITYDETAIL> model = from d in db.OPPORTUNITYDETAILs
                        join master in db.OPPORTUNITYMASTERs on d.OPPORTUNITY_CODE equals master.OPPORTUNITY_CODE
                        where master.OPEN_CLOSE == 0 && master.APPOINTED_USER_CODE == 1 //fix me 
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
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
            STOKCARD st = (from d in db.STOKCARDs
                           where d.ID == ID
                           select d).FirstOrDefault();
            return st;
        }

        public void addProductGroup(STOKGROUP stokgrup )
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();

            stokgrup.CREATE_DATE = DateTime.Now;
            stokgrup.LAST_UPDATE = DateTime.Now;
            db.STOKGROUPs.Add(stokgrup);
            db.SaveChanges();
            

        }
        public void EditProductGroup(STOKGROUP stokgrup)//ID almalı mı
        {
          
              
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
           
            stokgrup.CREATE_DATE = DateTime.Now;
            stokgrup.LAST_UPDATE = DateTime.Now;
          //  db.STOKGROUPs.(stokgrup);
            db.Entry(stokgrup).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        
        
        }

        public void AddProduct(STOKCARD stokcart)
        {

            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();

            stokcart.CREATE_DATE = DateTime.Now;
            stokcart.LAST_UPDATE = DateTime.Now;
            db.STOKCARDs.Add(stokcart);
            db.SaveChanges();

        }
      

        public static IEnumerable<STOKCARD> getProductAll()
        {
            siparis.Models.VdbSoftEntities db = new VdbSoftEntities();
            var model = from d in db.STOKCARDs
                        select d;
            return model;
        }
    }
}