using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siparis.Models
{
    public class OppDetail
    {
        public string Picture { get; set; }
        public int OPPORTUNITY_CODE { get; set; }
        public int ROW_ORDER_NO { get; set; }
        public string VERSION { get; set; }
        public string STOK_CODE { get; set; }
        public Nullable<int> STOK_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public Nullable<float> QUANTITY { get; set; }
        public Nullable<float> UNIT_PRICE { get; set; }
        public Nullable<float> TAX_PERCENT { get; set; }
        public Nullable<float> TOTAL { get; set; }
        public Nullable<float> TOTAL_UNTAX { get; set; }
        public Nullable<float> TOTAL_UPBK { get; set; }
        public Nullable<float> TOTAL_UPBK_UNTAX { get; set; }
        public string CUR_TYPE { get; set; }
        public Nullable<float> CUR_VALUE { get; set; }
        public string UNIT { get; set; }
        public Nullable<float> DISCOUNT_PERCENT { get; set; }
        public Nullable<float> DISCOUNT_TOTAL { get; set; }
        public Nullable<float> EXPENSE_PERCENT { get; set; }
        public Nullable<float> EXPENSE_TOTAL { get; set; }
        public string EXPLANATION { get; set; }
        public string LAST_UPDATE_USER { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string CODE2 { get; set; }
        public Nullable<float> DISCOUNT_UNIT_PRICE { get; set; }
        public Nullable<float> BUYING_PRICE { get; set; }
        public Nullable<float> BUYING_DISCOUNT_PERCENT { get; set; }
        public string DETAILDESCRIPTION { get; set; }
    }
}