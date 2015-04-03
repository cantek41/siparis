using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult getCategoryProduct(SortingPagingInfo info, string CategoryId)
        {
            VdbSoftEntities db = new VdbSoftEntities();
            int Id = Convert.ToInt32(CategoryId);
            IQueryable<STOKCARD> query = null;
            switch (info.SortField)
            {
                case "ID":
                    query = (info.SortDirection == "ascending" ?
                             db.STOKCARDs.Where(x => x.CATEGORY_CODE == Id).OrderBy(c => c.ID) :
                             db.STOKCARDs.Where(x => x.CATEGORY_CODE == Id).OrderByDescending(c => c.ID));
                    break;
            }
            //query = query.Skip(info.CurrentPageIndex
            //      * info.PageSize).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;
            ViewBag.Category = Id;
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = query.ToList();
            data.stokgroup = db.STOKGROUPs.ToList();
            data.stokbrand = (from brand in db.STOKBRANDs
                              join x in db.STOKCARDs on brand.BRAND_CODE equals x.BRAND_CODE
                              where x.CATEGORY_CODE == Id
                              select brand).Distinct().ToList();
            return View(data);

        }

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
            return data;

        }
    }
}