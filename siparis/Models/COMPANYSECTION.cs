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
    
    public partial class COMPANYSECTION
    {
        public Nullable<int> SECTION_CODE { get; set; }
        public string SECTION_NAME { get; set; }
        public int COMPANY_CODE { get; set; }
        public int ISDEFAULT { get; set; }
        public string LAST_UPDATE_USER { get; set; }
        public System.DateTime LAST_UPDATE { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
    }
}