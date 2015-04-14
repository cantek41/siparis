using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class WareHouseController : Controller
    {
        public ActionResult updateWareHouse(MVCxGridViewBatchUpdateValues<StokWareHouseViewModel, int> updateValues)
        {
            using (VdbSoftEntities db = new VdbSoftEntities())
            {
                foreach (var product in updateValues.Update)
                {
                    if (updateValues.IsValid(product))
                    {
                        STOKACTUALORDER stk = new STOKACTUALORDER();
                        stk.OPPORTUNITY_CODE = product.OPPORTUNITY_CODE;
                        stk.ROW_ORDER_NO = product.ROW_ORDER_NO;



                        db.STOKACTUALORDERs.Add(stk);
                        db.SaveChanges();
                    }
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult saveWareHouse(StokWareHouseViewModel item)
        {

            return null;
        }
    }
}