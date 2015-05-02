using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DevExpress.Web.Mvc;
using DevExpress.Xpo;
using siparis.Models;

namespace siparis.Controllers
{

    public class HomeController : BaseController
    {


        public ActionResult Index()
        {
            checkCart();
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            ///sayfalama bilgisi yukleniyor
            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "ID";
            info.SortDirection = "ascending";
            info.PageSize = 12;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(db.STOKCARDs.Where(x => x.UPPER_CODE == x.CODE).Count()
                           / info.PageSize)));
            info.CurrentPageIndex = 0;
            ViewBag.SortingPagingInfo = info;
            /// sayfalama bilgiswi bitti
            return View(stokViewList(info));


        }

        /// <summary>
        /// sayfa atlatma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(SortingPagingInfo info)
        {
            return View(stokViewList(info));
        }

        public IndexDataViewModel stokViewList(SortingPagingInfo info)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);

            IQueryable<STOKCARD> query = null;
            switch (info.SortField)
            {
                case "ID":
                    query = (info.SortDirection == "ascending" ?
                             db.STOKCARDs.Where(x => x.ID != 0).Where(x => x.UPPER_CODE == x.CODE).OrderBy(c => c.ID) :
                             db.STOKCARDs.OrderByDescending(c => c.ID));
                    break;
                //case "CompanyName":
                //    query = (info.SortDirection == "ascending" ?
                //             db.STOKCARDs.OrderBy(c => c.CompanyName) :
                //             db.STOKCARDs.OrderByDescending(c => c.CompanyName));
                //    break;
                //case "ContactName":
                //    query = (info.SortDirection == "ascending" ?
                //             db.STOKCARDs.OrderBy(c => c.ContactName) :
                //             db.STOKCARDs.OrderByDescending(c => c.ContactName));
                //    break;
                //case "Country":
                //    query = (info.SortDirection == "ascending" ?
                //             db.STOKCARDs.OrderBy(c => c.Country) :
                //             db.STOKCARDs.OrderByDescending(c => c.Country));
                //break;
            }

            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = query.ToList();
            data = getDetailFilter(data);
            //data.stokBody = db.STOKBODies.ToList();
            //data.stokbrand = db.STOKBRANDs.ToList();
            //data.stokCategory = db.STOKCATEGORies.ToList();
            //data.stokcolor = db.STOKCOLORs.ToList();
            //data.stokMainGroup = db.STOKMAINGROUPs.ToList();
            //data.stokModel = db.STOKMODELs.ToList();
            //data.stokPacket = db.STOKPACKETs.ToList();
            //data.stokRayon = db.STOKRAYONs.ToList();
            //data.stokseason = db.STOKSEASONs.ToList();
            //data.stokSector = db.STOKSECTORs.ToList();
            //data.stokSubGroup = db.STOKSUBGROUPs.ToList();
            //data.stokSubGroup2 = db.STOKSUBGROUP2.ToList();
            //query = query.Skip(info.CurrentPageIndex
            //    * info.PageSize).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;
          //  data.stokcard = query.ToList();
            return data;


        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            // ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        //public ActionResult CategorizedProducts(int Id)
        //{
        //    TempData["Category"] = Id;
        //    return View(getDetailCategory(Id));
        //}
        /// <summary>
        /// sayfa atlatma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CategorizedProducts([ModelBinder(typeof(DevExpressEditorsBinder))] IndexDataViewModel model)
        {
            //seçilen combobaxları al dizilere at
            int[] main = CheckBoxListExtension.GetSelectedValues<int>("MAIN_GRUOP");
            int[] sub1 = CheckBoxListExtension.GetSelectedValues<int>("SUB_GROUP");
            int[] sub2 = CheckBoxListExtension.GetSelectedValues<int>("SUB_GROUP2");
            int[] category = CheckBoxListExtension.GetSelectedValues<int>("CATEGORY");
            int[] brands = CheckBoxListExtension.GetSelectedValues<int>("BRAND");
            int[] modelP = CheckBoxListExtension.GetSelectedValues<int>("MODEL");
            int[] size = CheckBoxListExtension.GetSelectedValues<int>("SIZE");
            int[] color = CheckBoxListExtension.GetSelectedValues<int>("COLOR");
            int[] season = CheckBoxListExtension.GetSelectedValues<int>("SEASON");
            int[] packet = CheckBoxListExtension.GetSelectedValues<int>("PACKET");
            int[] rayon = CheckBoxListExtension.GetSelectedValues<int>("RAYON");
            int[] sector = CheckBoxListExtension.GetSelectedValues<int>("SECTOR");
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokcard = (from s in db.STOKCARDs
                             join c in main on s.MAIN_GRUP equals c
                             select s).ToList();

            if (sub1 != null && sub1.Count()>0)
                data.stokcard = (from s in data.stokcard
                                 join c in sub1 on s.SUB_GRUP1 equals c
                                 select s).ToList() ?? data.stokcard;
            if (sub2 != null && sub2.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in sub2 on s.SUB_GRUP2 equals c
                                 select s).ToList() ?? data.stokcard;
            if (category != null && category.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in category on s.CATEGORY_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (brands != null && sub1.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in brands on s.BRAND_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (modelP != null && modelP.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in modelP on s.MODEL_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (packet != null && packet.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in packet on s.PACK_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (rayon != null && rayon.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in rayon on s.RAYON_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (color != null && color.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in color on s.COLOR_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (season != null && season.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in season on s.SEASON_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (sector != null && sector.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in sector on s.SECTOR_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            if (size != null && size.Count() > 0)
                data.stokcard = (from s in data.stokcard
                                 join c in size on s.BODY_CODE equals c
                                 select s).ToList() ?? data.stokcard;
            //if (price > 0)
            //{
            //    data.stokcard = (from s in data.stokcard
            //                     where s.UNIT_PRICE < price
            //                     select s).ToList();
            //}

            //stok.brand ve stok.group data ya atanmalı
            data = getDetailFilter(data);
            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "ID";
            info.SortDirection = "ascending";
            info.PageSize = 6;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(data.stokcard.Where(x => x.UPPER_CODE == x.CODE).Count()
                           / info.PageSize)));           
            info.CurrentPageIndex = 0;
            ViewBag.SortingPagingInfo = info;
            return View(data);

        }



        public IndexDataViewModel getDetailFilter(IndexDataViewModel data)
        {
            data.stokMainGroup = MainFilter(data);
            data.stokSubGroup = SubGroupFilter(data);
            data.stokSubGroup2 = SubGroup2Filter(data);
            data.stokCategory = CategoryFilter(data);
            data.stokModel = ModelFilter(data);
            data.stokRayon = RayonFilter(data);
            data.stokSector = SectorFilter(data);
            data.stokPacket = PacketFilter(data);
            data.stokcolor = ColorFilter(data);
            data.stokseason = SeasonFilter(data);
            data.stokBody = BodyFilter(data);
            data.stokbrand = BrandFilter(data);
            return data;

        }
        public List<STOKMAINGROUP> MainFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokMainGroup = (from brand in data.stokcard
                                  join x in db.STOKMAINGROUPs on brand.MAIN_GRUP equals x.ID
                                  select x).Distinct().ToList();
            return data.stokMainGroup;
        }
        public List<STOKSUBGROUP> SubGroupFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokSubGroup = (from brand in data.stokcard
                                 join x in db.STOKSUBGROUPs on brand.SUB_GRUP1 equals x.ID
                                 select x).Distinct().ToList();
            return data.stokSubGroup;
        }
        public List<STOKSUBGROUP2> SubGroup2Filter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokSubGroup2 = (from brand in data.stokcard
                                  join x in db.STOKSUBGROUP2 on brand.SUB_GRUP2 equals x.ID
                                  select x).Distinct().ToList();
            return data.stokSubGroup2;
        }
        public List<STOKCATEGORY> CategoryFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokCategory = (from brand in data.stokcard
                                 join x in db.STOKCATEGORies on brand.CATEGORY_CODE equals x.STOK_GROUP_CODE
                                 select x).Distinct().ToList();
            return data.stokCategory;
        }
        public List<STOKMODEL> ModelFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokModel = (from brand in data.stokcard
                              join x in db.STOKMODELs on brand.MODEL_CODE equals x.MODEL_CODE
                              select x).Distinct().ToList();
            return data.stokModel;
        }
        public List<STOKRAYON> RayonFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokRayon = (from brand in data.stokcard
                              join x in db.STOKRAYONs on brand.RAYON_CODE equals x.RAYON_CODE
                              select x).Distinct().ToList();
            return data.stokRayon;
        }
        public List<STOKSECTOR> SectorFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokSector = (from brand in data.stokcard
                               join x in db.STOKSECTORs on brand.SECTOR_CODE equals x.SECTOR_CODE
                               select x).Distinct().ToList();
            return data.stokSector;
        }
        public List<STOKPACKET> PacketFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokPacket = (from brand in data.stokcard
                               join x in db.STOKPACKETs on brand.PACK_CODE equals x.PACKET_CODE
                               select x).Distinct().ToList();
            return data.stokPacket;
        }

        public List<STOKBRAND> BrandFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokbrand = (from brand in data.stokcard
                              join x in db.STOKBRANDs on brand.BRAND_CODE equals x.BRAND_CODE
                              select x).Distinct().ToList();
            return data.stokbrand;
        }
        public List<STOKCOLOR> ColorFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokcolor = (from brand in data.stokcard
                              join x in db.STOKCOLORs on brand.COLOR_CODE equals x.COLOR_CODE
                              select x).Distinct().ToList();
            return data.stokcolor;
        }
        public List<STOKSEASON> SeasonFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokseason = (from brand in data.stokcard
                               join x in db.STOKSEASONs on brand.SEASON_CODE equals x.SEASON_CODE
                               select x).Distinct().ToList();
            return data.stokseason;
        }
        public List<STOKBODY> BodyFilter(IndexDataViewModel data)
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            data.stokBody = (from brand in data.stokcard
                             join x in db.STOKBODies on brand.BODY_CODE equals x.BODY_CODE
                             select x).Distinct().ToList();
            return data.stokBody;
        }

    }

}