using System.Web;
using System.Web.Optimization;

namespace siparis
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
      
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
          
            bundles.Add(new ScriptBundle("~/bundels/scripts").Include(
                      "~/Scripts/jquery.js", "~/Scripts/bootstrap.min.js", "~/Scripts/jquery.scrollUp.min.js", "~/Scripts/price-range.js","~/Scripts/jquery.prettyPhoto.js","~/Scripts/main.js"));
  
            bundles.Add(new StyleBundle("~/bundels/css").Include(
                      "~/Content/css/site.css", "~/Content/css/animate.css", "~/Content/css/bootstrap.css", "~/Content/css/font-awesome.min.css", "~/Content/css/main.css", "~/Content/css/prettyPhoto.css", "~/Content/css/price-range.css", "~/Content/css/responsive.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
