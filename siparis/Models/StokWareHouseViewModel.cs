using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siparis.Models
{
    public class StokWareHouseViewModel
    {
        public int  OPPORTUNITY_CODE { get; set; }
        public int ROW_ORDER_NO { get; set; }
        public int WAREHOUSE_ID { get; set; }
        public string WAREHOUSE_NAME { get; set; }
        public Nullable<int> STOK_ID { get; set; }
        public string STOK_CODE { get; set; }
        public Nullable<int> TOTAL_QUANTITIY { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<int> CHOSE { get; set; }
        public Nullable<bool> STATUS { get; set; }
    }
}