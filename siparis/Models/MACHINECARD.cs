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
    
    public partial class MACHINECARD
    {
        public string CODE { get; set; }
        public string UPPER_CODE { get; set; }
        public string NAME_TR { get; set; }
        public string NAME_EN { get; set; }
        public string NAME_GR { get; set; }
        public Nullable<int> UNIT { get; set; }
        public Nullable<decimal> TAX { get; set; }
        public Nullable<decimal> UNIT_PRICE { get; set; }
        public string CUR_TYPE { get; set; }
        public string LAST_UPDATE_USER { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
    }
}