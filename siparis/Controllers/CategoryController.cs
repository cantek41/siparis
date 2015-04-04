using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        public ActionResult getCategoryProduct(int Id)
        {
            TempData["Category"] = Id;
            return View(getDetailCategory(Id));
        }



        /// <summary>
        /// sayfa atlatma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getCategoryProduct([ModelBinder(typeof(DevExpressEditorsBinder))] IndexDataViewModel model,int Id)
        {
            int CategoryID = Id;
            int[] season = CheckBoxListExtension.GetSelectedValues<int>("SEASON");
            int[] color = CheckBoxListExtension.GetSelectedValues<int>("COLOR");
            int[] size = CheckBoxListExtension.GetSelectedValues<int>("Size");

            VdbSoftEntities db = new VdbSoftEntities();

            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = (from stk in db.STOKCARDs
                             where stk.CATEGORY_CODE == CategoryID
                             select stk).ToList();

            data.stokcard = (from s in data.stokcard
                             join c in color on s.COLOR_CODE equals c
                             select s).ToList();
            data.stokcard = (from s in data.stokcard
                             join c in season on s.SEASON_CODE equals c
                             select s).ToList();
            data.stokcard = (from s in data.stokcard
                             join c in size on s.BODY_CODE equals c
                             select s).ToList();
           
            data.stokcolor = (from brand in data.stokcolor
                              join x in data.stokcolor on brand.COLOR_CODE equals x.COLOR_CODE
                              select brand).Distinct().ToList();

            return View();

        }

        //public List<STOKCOLOR> getFilterColor(List<STOKCARD> stoklar,)
        //{

        //}


        public IndexDataViewModel getDetailCategory(int CategoryId)
        {
            VdbSoftEntities db = new VdbSoftEntities();
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = db.STOKCARDs.Where(x => x.CATEGORY_CODE == CategoryId).OrderBy(c => c.ID).ToList();
            data.stokgroup = db.STOKGROUPs.Where(x => x.STOK_GROUP_CODE == CategoryId).ToList();
            data.stokbrand = (from brand in db.STOKBRANDs
                              join x in db.STOKCARDs on brand.BRAND_CODE equals x.BRAND_CODE
                              where x.CATEGORY_CODE == CategoryId
                              select brand).Distinct().ToList();
            data.stokBody = (from brand in db.STOKBODies
                             join x in db.STOKCARDs on brand.BODY_CODE equals x.BODY_CODE
                             where x.CATEGORY_CODE == CategoryId
                             select brand).Distinct().ToList();

            data.stokcolor = (from brand in db.STOKCOLORs
                              join x in db.STOKCARDs on brand.COLOR_CODE equals x.COLOR_CODE
                              where x.CATEGORY_CODE == CategoryId
                              select brand).Distinct().ToList();
            data.stokseason = (from brand in db.STOKSEASONs
                               join x in db.STOKCARDs on brand.SEASON_CODE equals x.SEASON_CODE
                               where x.CATEGORY_CODE == CategoryId
                               select brand).Distinct().ToList();

            return data;

        }
    }
}