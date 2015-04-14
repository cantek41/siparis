using System;
using System.Collections.Generic;
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
            //using (VdbSoftEntities db = new VdbSoftEntities())
            //{
            //     foreach (var product in updateValues.Update)
            //    {
            //        if (updateValues.IsValid(product))
            //        {
            //            STOKACTUALORDER stk = new STOKACTUALORDER();
            //            stk.OPPORTUNITY_CODE = product.OPPORTUNITY_CODE;
            //            stk.ROW_ORDER_NO = product.ROW_ORDER_NO;



            //            db.STOKACTUALORDERs.Add(stk);
            //            db.SaveChanges();
            //        }
            //    }
            //}
            return null;
        }

       
        public ActionResult saveWareHouse()
        {
            int oppCode = 1, rowCode = 1;

            siparis.Models.VdbSoftEntities db = new Models.VdbSoftEntities(dbName);
            OPPORTUNITYDETAIL model = db.OPPORTUNITYDETAILs.Where(x => x.OPPORTUNITY_CODE == oppCode).Where(x => x.ROW_ORDER_NO == rowCode).FirstOrDefault();

            List<StokWareHouseViewModel> depolar = (from product in db.STOKACTUALs
                                                    join warehouse in db.STOKWAREHOUSEs on product.DEPOT_ID equals warehouse.ID
                                                    where product.STOK_CODE == model.STOK_CODE
                                                    select new StokWareHouseViewModel
                                                    {
                                                        WAREHOUSE_ID = warehouse.ID,
                                                        WAREHOUSE_NAME = warehouse.NAME,
                                                        STOK_CODE = product.STOK_CODE,
                                                        TOTAL_QUANTITIY = product.QUANTITY
                                                    }).ToList();
            int i = 0;
            bool stokTamam = false;
            foreach (StokWareHouseViewModel item in depolar)
            {
                int miktar = db.STOKACTUALORDERs.Where(x => x.STOK_CODE == item.STOK_CODE).Select(x => x.QUANTITY).FirstOrDefault() ?? 0;
                item.QUANTITY = item.TOTAL_QUANTITIY - miktar;
                if (!stokTamam)
                    if (item.QUANTITY >= model.QUANTITY)
                    {
                        depolar.ElementAt(i).CHOSE = (int)model.QUANTITY;
                        stokTamam = true;
                    }
                item.OPPORTUNITY_CODE = model.OPPORTUNITY_CODE;
                item.ROW_ORDER_NO = model.ROW_ORDER_NO;
                depolar.ElementAt(i).QUANTITY = item.QUANTITY;
                i++;
            }


            return PartialView("../OrderFollow/_wareHousePartial", depolar);
        }
    }
}