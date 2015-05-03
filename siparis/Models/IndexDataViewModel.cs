using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siparis.Models
{
    public class IndexDataViewModel
    {
        public List<STOKMAINGROUP> stokMainGroup { get; set; }
        public List<STOKSUBGROUP> stokSubGroup { get; set; }
        public List<STOKSUBGROUP2> stokSubGroup2 { get; set; }
        public List<stockVievModel> stokcard { get; set; }
        public List<stockVievModel> stokcardView { get; set; }
        public List<STOKCATEGORY> stokCategory { get; set; }
        public List<STOKBRAND> stokbrand { get; set; }
        public List<STOKSEASON> stokseason { get; set; }
        public List<STOKCOLOR> stokcolor { get; set; }
        public List<STOKBODY> stokBody { get; set; }
        public List<STOKPACKET> stokPacket{ get; set; }
        public List<STOKRAYON> stokRayon { get; set; }
        public List<STOKMODEL> stokModel{ get; set; }
        public List<STOKSECTOR> stokSector { get; set; }
        
    }

    public class stockVievModel
    {

        public int ID { get; set; }
        public string CODE { get; set; }
        public string UPPER_CODE { get; set; }
        public string NAME_TR { get; set; }
        public string NAME_EN { get; set; }
        public string NAME_GR { get; set; }
        public Nullable<int> UNIT { get; set; }
        public Nullable<decimal> TAX_PERCENT { get; set; }
        public Nullable<decimal> UNIT_PRICE { get; set; }
        public string CUR_TYPE { get; set; }       
        public string DES_TR { get; set; }
        public string DES_EN { get; set; }
        public string DES_GR { get; set; }
        public string NAME_SHORT { get; set; }       
        public Nullable<int> CATEGORY_CODE { get; set; }
        public Nullable<int> MAIN_GRUP { get; set; }
        public Nullable<int> SUB_GRUP1 { get; set; }
        public Nullable<int> SUB_GRUP2 { get; set; }
        public Nullable<int> SECTOR_CODE { get; set; }
        public Nullable<int> RAYON_CODE { get; set; }
        public Nullable<int> PACK_CODE { get; set; }
        public Nullable<int> BRAND_CODE { get; set; }
        public Nullable<int> BODY_CODE { get; set; }
        public Nullable<int> COLOR_CODE { get; set; }
        public Nullable<int> MODEL_CODE { get; set; }
        public Nullable<int> SEASON_CODE { get; set; }
        public virtual ICollection<STOKCARDPICTURE> STOKCARDPICTUREs { get; set; }
    }


}