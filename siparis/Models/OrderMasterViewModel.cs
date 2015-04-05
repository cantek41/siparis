using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siparis.Models
{
    public class OrderMasterViewModel
    {
        public int DOCUMENT_TYPE { get; set; }
        public int OPPORTUNITY_CODE { get; set; }
        public DateTime DOCUMENT_DATE { get; set; }
        public string COMPANY_CODE { get; set; }
        public string CONTACT_CODE { get; set; }
        public string APPOINTED_USER_CODE { get; set; }
        public float TOTAL { get; set; }
        public string CREATE_USER { get; set; }
    }
}