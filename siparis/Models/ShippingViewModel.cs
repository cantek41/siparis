using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siparis.Models
{
    public class ShippingViewModel
    {
        public int ID { get; set; }
        public int OPPORTUNITY_CODE { get; set; }
        public int ROW_ORDER_NO { get; set; }
        public int WAREHOUSE_ID { get; set; }
        public string WAREHOUSE_NAME { get; set; }
        public Nullable<int> STOK_ID { get; set; }
        public string STOK_CODE { get; set; }
        public Nullable<int> TOTAL_QUANTITIY { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<int> CHOSE { get; set; }
        public Nullable<bool> STATUS { get; set; }

        public string Picture { get; set; }
        public string PRODUCT_NAME { get; set; }


    }

   public class shippingPictureEquals : IEqualityComparer<ShippingViewModel>
    {
        public bool Equals(ShippingViewModel x, ShippingViewModel y)
        {
            return x.ID == y.ID;

        }

        public int GetHashCode(ShippingViewModel obj)
        {
            throw new NotImplementedException();
        }
    }

}