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
    
    public partial class STOKACTUALORDER
    {
        public int ID { get; set; }
        public Nullable<int> OPPORTUNITY_CODE { get; set; }
        public string STOK_CODE { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<int> WAREHOUSE { get; set; }
    
        public virtual OPPORTUNITYMASTER OPPORTUNITYMASTER { get; set; }
    }
}
