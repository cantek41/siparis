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
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                {
                    int i;
                    // UpdateProduct(product, updateValues);
                }
            }
            return null;
        }
	}
}