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
    
    public partial class STOKACTUAL
    {
        public int ID { get; set; }
        public int DEPOT_ID { get; set; }
        public int STOK_ID { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public string UNIT { get; set; }
    
        public virtual STOKCARD STOKCARD { get; set; }
        public virtual STOKWAREHOUSE STOKWAREHOUSE { get; set; }
    }
}
