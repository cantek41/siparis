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


}