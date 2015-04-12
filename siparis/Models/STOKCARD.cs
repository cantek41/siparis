//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace siparis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class STOKCARD
    {
        public STOKCARD()
        {
            this.OPPORTUNITYDETAILs = new HashSet<OPPORTUNITYDETAIL>();
            this.STOKCARDPICTUREs = new HashSet<STOKCARDPICTURE>();
        }
    
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
        public string LAST_UPDATE_USER { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public string DES_TR { get; set; }
        public string DES_EN { get; set; }
        public string DES_GR { get; set; }
        public string NAME_SHORT { get; set; }
        public string ISHIDDEN { get; set; }
        public string SPECIAL_CODE1 { get; set; }
        public string SPECIAL_CODE2 { get; set; }
        public string SPECIAL_CODE3 { get; set; }
        public Nullable<int> TYPE { get; set; }
        public string MANUFACTURER_PRODUCT_CODE { get; set; }
        public string MANUFACTURER_COMPANY_CODE { get; set; }
        public string DETAILS_FOLLOW { get; set; }
        public Nullable<float> UNIT1_MULTIPLIER { get; set; }
        public Nullable<float> UNIT1_WEIGHT { get; set; }
        public Nullable<float> UNIT1_WIDTH { get; set; }
        public Nullable<float> UNIT1_LENGTH { get; set; }
        public Nullable<float> UNIT1_HEIGHT { get; set; }
        public Nullable<float> UNIT1_TARE { get; set; }
        public string UNIT2_NAME { get; set; }
        public Nullable<float> UNIT2_MULTIPLIER { get; set; }
        public Nullable<float> UNIT2_WEIGHT { get; set; }
        public Nullable<float> UNIT2_WIDTH { get; set; }
        public Nullable<float> UNIT2_LENGTH { get; set; }
        public Nullable<float> UNIT2_HEIGHT { get; set; }
        public Nullable<float> UNIT2_TARE { get; set; }
        public Nullable<int> MIN_QUANTITY { get; set; }
        public Nullable<int> MAX_QUANTITY { get; set; }
        public Nullable<float> ORDER_QUANTITY { get; set; }
        public Nullable<float> RETAIL_TAX { get; set; }
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
        public Nullable<float> ACTIVE_COST { get; set; }
        public string ACTIVE_COST_CURRENCY { get; set; }
        public Nullable<float> PURCHASE_PRICE { get; set; }
        public string PURCHASE_CURRENCY { get; set; }
        public string BARCODE_01 { get; set; }
        public string BARCODE_02 { get; set; }
        public string BARCODE_03 { get; set; }
        public string WEB_TRANSFER { get; set; }
        public string ISACTIVE { get; set; }
        public Nullable<int> WEB_AMOUNT { get; set; }
        public string SERIAL_NO1 { get; set; }
        public string SERIAL_NO2 { get; set; }
        public string SERIAL_NO3 { get; set; }
        public Nullable<int> PRODUCTION_TIME { get; set; }
        public Nullable<int> MAX_SEND_TIME { get; set; }
    
        public virtual ICollection<OPPORTUNITYDETAIL> OPPORTUNITYDETAILs { get; set; }
        public virtual STOKBODY STOKBODY { get; set; }
        public virtual STOKBRAND STOKBRAND { get; set; }
        public virtual STOKCOLOR STOKCOLOR { get; set; }
        public virtual STOKGROUP STOKGROUP { get; set; }
        public virtual STOKSEASON STOKSEASON { get; set; }
        public virtual ICollection<STOKCARDPICTURE> STOKCARDPICTUREs { get; set; }
    }
}
