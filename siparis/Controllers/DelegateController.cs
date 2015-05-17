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
            new STOKCARDViewModel{ ID=16,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=17,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=18,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=19,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=20,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"}
            };

            ViewData["Company"] = getCompanyList();
            ViewData["WareHouse"] = getWareHouse();
            ViewData["DeliveryType"] = getWareDeliveryType();
            ViewData["Adress"] = null;
            return View(model);
        }

        public ActionResult MultiColumnComboBoxPartial()
        {
            ViewData["Company"] = getCompanyList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult deliveryAdress()
        {
            int company = (Request.Params["Company"] != null) ? int.Parse(Request.Params["Company"]) : -1;
            ViewData["Adress"] = getComapanyAdress(company);
            return PartialView();
        }
        [ValidateInput(false)]
        public ActionResult OrderMasterGridPartial()
        {
            List<STOKCARDViewModel> model = new List<STOKCARDViewModel> { 
           new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=16,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=17,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=18,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=19,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=20,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"}
         };
            return PartialView(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult addStok(STOKCARDViewModel item)
        {
            //var model = db.DOCUMENTS;
            //try
            //{
            //    item.PATH = filePath;
            //    item.PRIORITY = 0;
            //    item.ROW_ORDER_NO = 0;
            //    item.VISIBLE = true;
            //    item.CREATE_USER = getCurrentUserName();
            //    item.CREATE_DATE = DateTime.Now;
            //    item.LAST_UPDATE = DateTime.Now;
            //    item.LAST_UPDATE_USER = getCurrentUserName();
            //    filePath = null;
            //    /// DOCUMENT modelItem = model.FirstOrDefault(it => it.DOCUMENT_CODE == item.DOCUMENT_CODE);
            //    if (item != null)
            //    {
            //        db.DOCUMENTS.Attach(item);
            //        db.Entry(item).State = EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception e)
            //{
            //    ViewData["EditError"] = e.Message;
            //}
            ////}
            ////else
            ////    ViewData["EditError"] = "Please, correct all errors.";
            //var model1 = from d in db.DOCUMENTS
            //             join b in db.CHAPTERS on d.CHAPTER_CODE equals b.CHAPTER_CODE
            //             join cs in db.COURSES on d.COURSE_CODE equals cs.COURSE_CODE
            //             join lesson in db.LESSONS on d.LESSON_CODE equals lesson.LESSON_CODE
            //             select new { d.DOCUMENT_CODE, CHAPTER_CODE = b.CHAPTER_NAME, d.DOCUMENT_TYPE, d.DURATION, d.LINK_TYPE, d.PATH, LESSON_CODE = lesson.LESSON_NAME, COURSE_CODE = cs.COURSE_NAME, d.DOCUMENT_NAME };
          //  return PartialView("_GridView1Partial", model.ToList());
            List<STOKCARDViewModel> model = new List<STOKCARDViewModel> { 
            new STOKCARDViewModel{ ID=15,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=16,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=17,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=18,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=19,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"},
            new STOKCARDViewModel{ ID=20,NAME_TR="ISI DÜZENLEYİCİ",UNIT_PRICE=150,CUR_TYPE="TL",UNIT=15,DES_TR="Isı Düzenmele",CODE="15.05.2015"}
            };
            return PartialView("gridView", model);
        }

    }
}