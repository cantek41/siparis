using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc.Html;

namespace siparis.Models
{
  
    public class ProfileInfo
    {
        public string FirmaAdi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int User_Code { get; set; }
        public  string Telefon { get; set; }
        public  string Mail { get; set; } 
        public  string Adres { get; set; }
        public  string Facebook { get; set; }
        public  string Twitter { get; set; }
        public  string Pinterest { get; set; }
        public  string GoogleMaps { get; set; }
        public  string Adres2 { get; set; }
        public  string Enlem { get; set; }
        public  string Boylam { get; set; }
    }
}