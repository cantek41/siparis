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
    
    public partial class USER
    {
        public int USER_CODE { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PASSWORD { get; set; }
        public string AUSER_NAME { get; set; }
        public string SURNAME { get; set; }
        public string USER_LANGUAGE { get; set; }
        public string MAIL { get; set; }
        public string USER_RIGHT { get; set; }
        public Nullable<int> ISACTIVE { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public Nullable<int> CONTACT_CODE { get; set; }
        public string LAST_UPDATE_USER { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public Nullable<int> TOTALWORKTIME { get; set; }
        public Nullable<System.DateTime> WORKSTARTHOUR { get; set; }
        public string MAIL_USER_NAME { get; set; }
        public string MAIL_PASSWORD { get; set; }
        public string INCOMING_MAIL_SERVER { get; set; }
        public string OUTGOING_MAIL_SERVER { get; set; }
        public string POP3_PORT { get; set; }
        public string SMTP_PORT { get; set; }
        public string SSL { get; set; }
        public Nullable<int> UPPER_USER_CODE { get; set; }
    }
}