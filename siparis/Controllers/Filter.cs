using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using siparis.Models;

namespace siparis.Controllers
{
    public class Filter
    {
        private string dbName;
        public Filter(string dbName)
        {
            this.dbName = dbName;
        }
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, NoStore = true)]
        public IndexDataViewModel getFilterItem()
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokMainGroup = db.STOKMAINGROUPs.OrderByDescending(x => x.ID).ToList();
            data.stokSubGroup = db.STOKSUBGROUPs.OrderByDescending(x => x.ID).ToList();
            data.stokSubGroup2 = db.STOKSUBGROUP2.OrderByDescending(x => x.CODE).ToList();
            data.stokCategory = db.STOKCATEGORies.OrderByDescending(x => x.CODE).ToList();
            data.stokModel = db.STOKMODELs.OrderByDescending(x => x.CODE).ToList();
            data.stokRayon = db.STOKRAYONs.OrderByDescending(x => x.CODE).ToList();
            data.stokSector = db.STOKSECTORs.OrderByDescending(x => x.CODE).ToList();
            data.stokPacket = db.STOKPACKETs.OrderByDescending(x => x.CODE).ToList();
            data.stokcolor = db.STOKCOLORs.OrderByDescending(x => x.CODE).ToList();
            data.stokseason = db.STOKSEASONs.OrderByDescending(x => x.CODE).ToList();
            data.stokBody = db.STOKBODies.OrderByDescending(x => x.CODE).ToList();
            data.stokbrand = db.STOKBRANDs.OrderByDescending(x => x.CODE).ToList();
            return data;
        }
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, NoStore = true)]
        public IndexDataViewModel getMainMenu()
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                IndexDataViewModel data = new IndexDataViewModel();
                data.stokMainGroup = db.STOKMAINGROUPs.OrderByDescending(x => x.ID).ToList();
                data.stokSubGroup = db.STOKSUBGROUPs.OrderByDescending(x => x.ID).ToList();
                data.stokSubGroup2 = db.STOKSUBGROUP2.ToList();
                return data;
            };
        }

        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, NoStore = true, VaryByParam = "count")]
        public List<STOKCARDViewModel> getStok(int count)
        {
            string top = "";
            if (count != -1)
            {
                top = String.Format("TOP({0})", count);
            }
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                string sorgu = String.Format(" select * from(select {1} STOKCARD.ID, SUM(TOTAL_QUANTITIY) as UNIT,STOKCARD.UPPER_CODE,STOKCARD.DES_TR,STOKCARDUSERPRICE.PRICE as UNIT_PRICE,STOKCARDUSERPRICE.CUR_TYPE,NAME_TR,STOKCARDPICTURE.PATH as STOKCARDPICTUREs,SUB_GRUP1,MAIN_GRUP,STOKCARD.CODE,SUB_GRUP2,BRAND_CODE,BODY_CODE,CATEGORY_CODE,STOKCARD.COLOR_CODE,MODEL_CODE,PACK_CODE,RAYON_CODE,SEASON_CODE,SECTOR_CODE,  ROW_NUMBER()  OVER(PARTITION BY STOKCARD.ID ORDER BY STOKCARD.ID DESC ) rn from STOKCARD left join STOKCARDPICTURE on STOKCARDPICTURE.STOK_ID = STOKCARD.ID left join STOKWAREHOUSEPRODUCT on STOKWAREHOUSEPRODUCT.STOK_ID=STOKCARD.ID left join STOKCARDUSERPRICE on STOKCARDUSERPRICE.STOK_ID=STOKCARD.ID and STOKCARDUSERPRICE.COMPANY_CODE={0} GROUP BY STOKCARD.UPPER_CODE,STOKCARD.DES_TR,STOKCARD.ID,STOKCARDUSERPRICE.PRICE,STOKCARDUSERPRICE.CUR_TYPE,NAME_TR,STOKCARDPICTURE.PATH,SUB_GRUP1,MAIN_GRUP,STOKCARD.CODE,SUB_GRUP2,BRAND_CODE,BODY_CODE,CATEGORY_CODE,STOKCARD.COLOR_CODE,MODEL_CODE,PACK_CODE,RAYON_CODE,SEASON_CODE,SECTOR_CODE) a where rn=1;", 1, top);
                List<STOKCARDViewModel> stok = db.Database.SqlQuery<STOKCARDViewModel>(sorgu).ToList<STOKCARDViewModel>();
                return stok;
            }
        }

        public List<STOKCARDViewModel> getFilterStok(filterModel _filter)
        {
            List<STOKCARDViewModel> data = getStok(-1);
            if (_filter.stokMainGroup != 0)
                data = (from s in data
                        where s.MAIN_GRUP == _filter.stokMainGroup
                        select s).ToList() ?? data;
            if (_filter.stokSubGroup != 0)
                data = (from s in data
                        where s.SUB_GRUP1 == _filter.stokSubGroup
                        select s).ToList() ?? data;
            if (_filter.stokSubGroup2 != 0)
                data = (from s in data
                        where s.SUB_GRUP2 == _filter.stokSubGroup2
                        select s).ToList() ?? data;
            if (_filter.stokCategory != null && _filter.stokCategory.Count() > 0)
                data = (from s in data
                        join c in _filter.stokCategory on s.CATEGORY_CODE equals c
                        select s).ToList() ?? data;
            if (_filter.stokbrand != null && _filter.stokbrand.Count() > 0)
                data = (from s in data
                        join c in _filter.stokbrand on s.BRAND_CODE equals c
                        select s).ToList() ?? data;
            if (_filter.stokModel != null && _filter.stokModel.Count() > 0)
                data = (from s in data
                        join c in _filter.stokModel on s.MODEL_CODE equals c
                        select s).ToList() ?? data;
            if (_filter.stokPacket != null && _filter.stokPacket.Count() > 0)
                data = (from s in data
                        join c in _filter.stokPacket on s.PACK_CODE equals c
                        select s).ToList() ?? data;
            if (_filter.stokRayon != null && _filter.stokRayon.Count() > 0)
                data = (from s in data
                        join c in _filter.stokRayon on s.RAYON_CODE equals c
                        select s).ToList() ?? data;
            if (_filter.stokcolor != null && _filter.stokcolor.Count() > 0)
                data = (from s in data
                        join c in _filter.stokcolor on s.COLOR_CODE equals c
                        select s).ToList() ?? data;

            if (_filter.stokseason != null && _filter.stokseason.Count() > 0)
                data = (from s in data
                        join c in _filter.stokseason on s.SEASON_CODE equals c
                        select s).ToList() ?? data;
            if (_filter.stokSector != null && _filter.stokSector.Count() > 0)
                data = (from s in data
                        join c in _filter.stokSector on s.SECTOR_CODE equals c
                        select s).ToList() ?? data;
            if (_filter.stokBody != null && _filter.stokBody.Count() > 0)
                data = (from s in data
                        join c in _filter.stokBody on s.BODY_CODE equals c
                        select s).ToList() ?? data;
            foreach (var item in data)
            {
                if (String.IsNullOrEmpty(item.STOKCARDPICTUREs))
                {
                    item.STOKCARDPICTUREs = "../Content/images/404/No_Image_Available.png";
                }
            }
            return data;

        }
    }
}