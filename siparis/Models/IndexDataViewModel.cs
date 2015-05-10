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
        public List<STOKCARD> stokcard { get; set; }
        public List<STOKCARDViewModel> stokcardView { get; set; }
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

    public class filterModel
    {
        public int stokMainGroup { get; set; }
        public int stokSubGroup { get; set; }
        public int stokSubGroup2 { get; set; }
        public int[] stokCategory { get; set; }
        public int[] stokbrand { get; set; }
        public int[] stokseason { get; set; }
        public int[] stokcolor { get; set; }
        public int[] stokBody { get; set; }
        public int[] stokPacket { get; set; }
        public int[]  stokRayon { get; set; }
        public int[] stokModel { get; set; }
        public int[] stokSector { get; set; }
        
    }
    public partial class STOKCARDViewModel
    {       
        public int ID { get; set; }
        public string CODE { get; set; }
        public string UPPER_CODE { get; set; }
        public string NAME_TR { get; set; }       
        public Nullable<int> UNIT { get; set; }      
        public Nullable<decimal> UNIT_PRICE { get; set; }
        public string CUR_TYPE { get; set; }       
        public string DES_TR { get; set; }      
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
        public string STOKCARDPICTUREs { get; set; }
      
    }

}