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
    
    public partial class CUR
    {
        public System.DateTime CURDATE { get; set; }
        public string CURSYMBOL { get; set; }
        public string CUR_NAME { get; set; }
        public Nullable<decimal> FOREX_BUYING { get; set; }
        public Nullable<decimal> FOREX_SELING { get; set; }
        public Nullable<decimal> BANKNOTE_BUYING { get; set; }
        public Nullable<decimal> BANKNOTE_SELING { get; set; }
        public string LAST_UPDATE_USER { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public Nullable<decimal> FIX_BUYING { get; set; }
        public Nullable<decimal> FIX_SELING { get; set; }
    }
}