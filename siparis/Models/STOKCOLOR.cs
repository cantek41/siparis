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
    
    public partial class STOKCOLOR
    {
        public STOKCOLOR()
        {
            this.STOKCARDs = new HashSet<STOKCARD>();
        }
    
        public int COLOR_CODE { get; set; }
        public string NAME_TR { get; set; }
        public string NAME_EN { get; set; }
        public string NAME_GR { get; set; }
        public Nullable<bool> VISIBLE { get; set; }
    
        public virtual ICollection<STOKCARD> STOKCARDs { get; set; }
    }
}
