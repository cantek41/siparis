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
            ViewBag.Category = Id;
            return View(getDetailCategory(Id));
        }



        /// <summary>
        /// sayfa atlatma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getCategoryProduct([ModelBinder(typeof(DevExpressEditorsBinder))] IndexDataViewModel model, int Id)
        {

            ViewBag.Category = Id;
            int[] season = CheckBoxListExtension.GetSelectedValues<int>("SEASON");
            int[] color = CheckBoxListExtension.GetSelectedValues<int>("COLOR");
            int[] size = CheckBoxListExtension.GetSelectedValues<int>("Size");
          
            return View();

        }
   
        //[HttpPost]
        //public ActionResult Index([ModelBinder(typeof(DevExpressEditorsBinder))] MyModel model)
        //{
        //    //Manually Bound Field - CheckBoxList (multi select)
        //    model.ProgLanguages = CheckBoxListExtension.GetSelectedValues<int>("ProgLanguagesUnbound");

        //    TempData["PostedModel"] = model;
        //    return RedirectToAction("Success");
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