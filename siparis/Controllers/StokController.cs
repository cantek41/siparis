using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;
using System.Data.Entity;
using System.Collections;

namespace siparis.Controllers
{
    public class StokController : Controller
    {
        //
        // GET: /Stok/
        public ActionResult AddStok()
        {
            VdbSoftEntities db = new VdbSoftEntities();
            ViewDataDoldur();
            return View();
        }

        [HttpPost]
        public ActionResult AddStok(STOKCARD stok)
        {
            VdbSoftEntities db = new VdbSoftEntities();
            stok.ID = db.STOKCARDs.Where(x => x.CODE == stok.CODE).Select(x => x.ID).FirstOrDefault();
            if (stok.ID!=0)
            {
                return EditStok(stok);
            }          

            stok.CREATE_DATE = DateTime.Now;
            stok.LAST_UPDATE = DateTime.Now;
            stok.CREATE_USER = "1";// fix me
            stok.LAST_UPDATE_USER = "1";//fix me
            db.STOKCARDs.Add(stok);
            db.SaveChanges();
            ViewDataDoldur();
            return View(stok);
        }

        public void ViewDataDoldur()
        {
            VdbSoftEntities db = new VdbSoftEntities();
            ViewData["Category"] = (from d in db.STOKGROUPs
                                                  select new { Key = d.STOK_GROUP_CODE, Text = d.NAME_TR });
            ViewData["Brand"] = (from d in db.STOKBRANDs
                                               select new { Key = d.BRAND_CODE, Text = d.NAME_TR });
            ViewData["Color"] = (from d in db.STOKCOLORs
                                               select new { Key = d.COLOR_CODE, Text = d.NAME_TR });
            ViewData["Season"] = (from d in db.STOKSEASONs
                                                select new { Key = d.SEASON_CODE, Text = d.NAME_TR });
            ViewData["Body"] = (from d in db.STOKBODies
                                              select new { Key = d.BODY_CODE, Text = d.NAME_TR });
            ViewData["ishidden"] = (from d in db.GROUPS
                                    where d.GROUP_CODE == 62
                                         select d.EXP_TR );
            ViewData["CurType"] = (from d in db.CURTYPEs
                                select  d.CUR_SYMBOL );
            ViewData["Unit"] = (from d in db.GROUPS
                                    where d.GROUP_CODE == 59
                                    select d.EXP_TR);
           
        }

        public ActionResult EditStok(STOKCARD stok)
        {
            VdbSoftEntities db = new VdbSoftEntities();
            db.STOKCARDs.Attach(stok);
            db.Entry(stok).State = EntityState.Modified;
            db.SaveChanges();
            ViewDataDoldur();
            return View(stok);

        }

    }
}