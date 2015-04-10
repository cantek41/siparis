using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using siparis.Models;

namespace siparis.Models
{
    [Serializable]
    public class Res
    {
        public static string languege {get;set;}
        public static string resGetir(int resId)
        {

            string str = null;
            using (VdbSoftEntities db = new VdbSoftEntities(siparis.Controllers.BaseController.dbName))
            {

                switch (languege)
                {
                    case "TR":
                        str = (from icerik in db.RES
                               where icerik.R_ID == resId
                               select icerik.TR).FirstOrDefault().ToString();
                        break;
                    case "EN":
                        str = (from icerik in db.RES
                               where icerik.R_ID == resId
                               select icerik.EN).FirstOrDefault().ToString();
                        break;
                    default:
                        str = (from icerik in db.RES
                               where icerik.R_ID == resId
                               select icerik.TR).FirstOrDefault().ToString();
                        break;
                }
            }
            return str;
        }
    }
}