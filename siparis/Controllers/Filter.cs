using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public IndexDataViewModel getFilterItem()
        {
            VdbSoftEntities db = new VdbSoftEntities(dbName);
            IndexDataViewModel data = new IndexDataViewModel();
            data.stokMainGroup = db.STOKMAINGROUPs.ToList();
            data.stokSubGroup = db.STOKSUBGROUPs.ToList();
            data.stokSubGroup2 = db.STOKSUBGROUP2.ToList();
            data.stokCategory = db.STOKCATEGORies.ToList();
            data.stokModel = db.STOKMODELs.ToList();
            data.stokRayon = db.STOKRAYONs.ToList();
            data.stokSector = db.STOKSECTORs.ToList();
            data.stokPacket = db.STOKPACKETs.ToList();
            data.stokcolor = db.STOKCOLORs.ToList();
            data.stokseason = db.STOKSEASONs.ToList();
            data.stokBody = db.STOKBODies.ToList();
            data.stokbrand = db.STOKBRANDs.ToList();
            return data;
        }
        public IndexDataViewModel getMainMenu()
        {
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                IndexDataViewModel data = new IndexDataViewModel();
                data.stokMainGroup = db.STOKMAINGROUPs.ToList();
                data.stokSubGroup = db.STOKSUBGROUPs.ToList();
                data.stokSubGroup2 = db.STOKSUBGROUP2.ToList();
                return data;
            };
        }

        public List<STOKCARDViewModel> getStok(int count = -1)
        {
            string top = "";
            if (count != -1)
            {
                top = String.Format("TOP({0})", count);
            }
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                string sorgu = String.Format("select {1} STOKCARD.ID, SUM(TOTAL_QUANTITIY) as UNIT,STOKCARD.UPPER_CODE,STOKCARD.DES_TR,STOKCARDUSERPRICE.PRICE as UNIT_PRICE,STOKCARDUSERPRICE.CUR_TYPE,NAME_TR,STOKCARDPICTURE.PATH as STOKCARDPICTUREs,SUB_GRUP1,MAIN_GRUP,STOKCARD.CODE,SUB_GRUP2,BRAND_CODE,BODY_CODE,CATEGORY_CODE,STOKCARD.COLOR_CODE,MODEL_CODE,PACK_CODE,RAYON_CODE,SEASON_CODE,SECTOR_CODE from STOKCARD left join STOKCARDPICTURE on STOKCARDPICTURE.STOK_ID = STOKCARD.ID left join STOKWAREHOUSEPRODUCT on STOKWAREHOUSEPRODUCT.STOK_ID=STOKCARD.ID left join STOKCARDUSERPRICE on STOKCARDUSERPRICE.STOK_ID=STOKCARD.ID and STOKCARDUSERPRICE.COMPANY_CODE={0} GROUP BY STOKCARD.UPPER_CODE,STOKCARD.DES_TR,STOKCARD.ID,STOKCARDUSERPRICE.PRICE,STOKCARDUSERPRICE.CUR_TYPE,NAME_TR,STOKCARDPICTURE.PATH,SUB_GRUP1,MAIN_GRUP,STOKCARD.CODE,SUB_GRUP2,BRAND_CODE,BODY_CODE,CATEGORY_CODE,STOKCARD.COLOR_CODE,MODEL_CODE,PACK_CODE,RAYON_CODE,SEASON_CODE,SECTOR_CODE;", 1, top);

                List<STOKCARDViewModel> stok = db.Database.SqlQuery<STOKCARDViewModel>(sorgu).ToList<STOKCARDViewModel>();
                return stok;
            }
        }


        public List<STOKCARDViewModel> getFilterStok(filterModel _filter)
        {
            List<STOKCARDViewModel> data = getStok();
            if (_filter.stokMainGroup != null)
                data = (from s in data
                        where s.MAIN_GRUP == _filter.stokMainGroup
                        select s).ToList() ?? data;
            if (_filter.stokSubGroup != null && _filter.stokSubGroup != 0)
                data = (from s in data
                        where s.SUB_GRUP1 == _filter.stokSubGroup
                        select s).ToList() ?? data;
            if (_filter.stokSubGroup2 != null && _filter.stokSubGroup2 != 0)
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
            return data;

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