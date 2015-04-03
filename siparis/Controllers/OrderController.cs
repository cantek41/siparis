using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost]
        public bool AddCart(int stokID)
        {
            try
            {
                using (VdbSoftEntities db = new VdbSoftEntities())
                {
                    #region Sepet Var mı
                    if (Session["Sepet"] == null)
                    {
                        OPPORTUNITYMASTER sepet = new OPPORTUNITYMASTER();
                        int sepetID = db.OPPORTUNITYMASTERs.Max(x => x.OPPORTUNITY_CODE);
                        sepetID++;
                        sepet.OPPORTUNITY_CODE = sepetID;
                        sepet.VERSION = "V1";
                        sepet.COMPANY_CODE = 0;
                        sepet.CONTACT_CODE = 0;
                        sepet.APPOINTED_USER_CODE = 1;
                        db.OPPORTUNITYMASTERs.Add(sepet);
                        db.SaveChanges();
                        Session.Add("Sepet", sepetID);
                    }
                    #endregion

                    STOKCARD stokcart = db.STOKCARDs.Find(stokID);
                    OPPORTUNITYDETAIL opportunitdetails = new OPPORTUNITYDETAIL();

                    opportunitdetails.OPPORTUNITY_CODE = Convert.ToInt32(Session["Sepet"]);
                    int row;
                    if (db.OPPORTUNITYDETAILs.Count(x => x.OPPORTUNITY_CODE == opportunitdetails.OPPORTUNITY_CODE) == 0)
                    {
                        row = 0;
                    }
                    else row = db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == opportunitdetails.OPPORTUNITY_CODE).Max(x => x.ROW_ORDER_NO);

                    opportunitdetails.ROW_ORDER_NO = row + 1;
                    opportunitdetails.CUR_TYPE = stokcart.CUR_TYPE;
                    opportunitdetails.UNIT_PRICE = (float)stokcart.UNIT_PRICE;
                    opportunitdetails.UNIT = stokcart.UNIT.ToString();
                    opportunitdetails.STOK_ID = stokID;
                    opportunitdetails.QUANTITY = 1;
                    opportunitdetails.VERSION = "V1";

                    db.OPPORTUNITYDETAILs.Add(opportunitdetails);
                    db.SaveChanges();
                    ViewBag.kontrol = "" + stokcart.CUR_TYPE;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }




        }



        public ActionResult Chart()
        {
            return View(getCartProduct());
        }
    }
}