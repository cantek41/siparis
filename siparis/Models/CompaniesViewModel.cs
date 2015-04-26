using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace siparis.Models
{
    public class company
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public static class CompaniesViewModel
    {
        private static List<company> _companyies;
        private static List<company> doldur()
        {
            VdbSoftEntities db = new VdbSoftEntities();
            return _companyies = (from d in db.COMPANies
                                 select new company { Id=d.COMPANY_CODE,Name=d.COMPANY_NAME }).ToList();
        }

        public static IEnumerable<SelectListItem> Company { get { doldur(); return new SelectList(_companyies, "Id", "Name", "Select Area Id"); } }

    }
}