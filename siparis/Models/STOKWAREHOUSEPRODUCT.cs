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
    
    public partial class STOKWAREHOUSEPRODUCT
    {
        public int ID { get; set; }
        public Nullable<int> WAREHOUSE_ID { get; set; }
        public Nullable<int> STOK_ID { get; set; }
        public string STOK_CODE { get; set; }
        public Nullable<int> TOTAL_QUANTITIY { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public Nullable<int> CREATE_USER { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE { get; set; }
        public Nullable<int> LAST_UPDATE_USER { get; set; }
    }
}