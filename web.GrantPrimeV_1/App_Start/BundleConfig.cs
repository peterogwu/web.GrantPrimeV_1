using System.Web;
using System.Web.Optimization;

namespace web.GrantPrimeV_1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));



            bundles.Add(new ScriptBundle("~/Content/EkitiJs").Include(
     "~/Content/consula/DataTables/DataTables-1.10.24/js/jquery.dataTables.min.js",
      "~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.semanticui.min.js",
      "~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.jqueryui.min.js",
       "~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.foundation.min.js",
       "~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.bootstrap4.min.js",
        "~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.bootstrap.min.js",
        //"~/Content/consula/consula/js/SweetAlert.js",
"~/Content/consula/consula/js/SweetAlert2.js"
//"~/Content/consula/consula/js/SweetAlert3.js"
//"~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.bootstrap.js",

//"~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.bootstrap4.js",

//"~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.foundation.js",

//"~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.jqueryui.js",

//"~/Content/consula/DataTables/DataTables-1.10.24/js/dataTables.semanticui.js",

//"~/Content/consula/DataTables/DataTables-1.10.24/js/jquery.dataTables.js",
));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/EkitiCss").Include(
       "~/Content/consula/DataTables/DataTables-1.10.24/css/jquery.dataTables.min.css",
           "~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.semanticui.min.css",
               "~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.jqueryui.min.css",
                   "~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.foundation.min.css",
                       "~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.bootstrap4.min.css",
                          "~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.bootstrap.min.css"
//"~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.bootstrap.css",

//"~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.bootstrap4.css",

//"~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.foundation.css",

//"~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.jqueryui.css",

//"~/Content/consula/DataTables/DataTables-1.10.24/css/dataTables.semanticui.css",

//"~/Content/consula/DataTables/DataTables-1.10.24/css/jquery.dataTables.css",
));
        }
    }
}
