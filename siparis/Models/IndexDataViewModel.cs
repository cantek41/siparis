using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siparis.Models
{
    public class IndexDataViewModel
    {
        public List<STOKCARD> stokcard { get; set; }
        public List<STOKGROUP> stokgroup { get; set; }
        public List<STOKBRAND> stokbrand { get; set; }
        public List<STOKSEASON> stokseason { get; set; }
        public List<STOKCOLOR> stokcolor { get; set; }
        public List<STOKBODY> stokBody { get; set; }
        
    }


}