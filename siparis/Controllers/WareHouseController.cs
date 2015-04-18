using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using siparis.Models;

namespace siparis.Controllers
{
    public class WareHouseController : BaseController
    {
        public ActionResult updateWareHouse(StokWareHouseViewModel updateValues)
        {

            string[] keys = Request.Params["WAREHOUSE_ID;OPPORTUNITY_CODE;ROW_ORDER_NO;STOK_CODE"].Split('|');
            using (VdbSoftEntities db = new VdbSoftEntities(dbName))
            {
                STOKACTUALORDER orderForDepo = new STOKACTUALORDER();
                orderForDepo.OPPORTUNITY_CODE = Convert.ToInt32(keys[1]);
                orderForDepo.ROW_ORDER_NO = Convert.ToInt32(keys[2]);
                orderForDepo.STOK_CODE = keys[3];
                orderForDepo.WAREHOUSE = Convert.ToInt32(keys[0]);
                orderForDepo.QUANTITY = updateValues.CHOSE;
                STOKACTUALORDER checkOrder = (from d in db.STOKACTUALORDERs
                                              where d.OPPORTUNITY_CODE == orderForDepo.OPPORTUNITY_CODE && d.ROW_ORDER_NO == orderForDepo.ROW_ORDER_NO && d.WAREHOUSE == orderForDepo.WAREHOUSE && d.STOK_CODE == orderForDepo.STOK_CODE
                                              select d).FirstOrDefault();
                if (checkOrder != null)
                {
                    checkOrder.QUANTITY = updateValues.CHOSE;
                    db.STOKACTUALORDERs.Attach(checkOrder);
                    db.Entry(checkOrder).State = EntityState.Modified;
                }
                else
                {
                    db.STOKACTUALORDERs.Add(orderForDepo);
                }

                db.SaveChanges();
            }
            return saveWareHouse(String.Format("{0}|{1}", keys[1], keys[2]));
        }


        public ActionResult saveWareHouse(string ID)
        {
            string[] keys = ID.Split('|');
            int rowCode = Convert.ToInt32(keys[1]);
            int oppCode = Convert.ToInt32(keys[0]);

            Tuple<List<StokWareHouseViewModel>, OPPORTUNITYDETAIL> param = orderWareHouseCal(oppCode, rowCode);
            List<StokWareHouseViewModel> depolar = param.Item1;

            return PartialView("../OrderFollow/_wareHousePartial", depolar);
        }





    }
}