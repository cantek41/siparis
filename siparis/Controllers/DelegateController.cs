using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class DelegateController : BaseController
    {
        //
        // GET: /Delegate/
        public ActionResult Index()
        {
            List<STOKCARDViewModel> model = new List<STOKCARDViewModel> { 
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"}
            };
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult OrderMasterGridPartial()
        {
            List<STOKCARDViewModel> model = new List<STOKCARDViewModel> { 
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"}
            };
            return PartialView(model);
        }
    }
}