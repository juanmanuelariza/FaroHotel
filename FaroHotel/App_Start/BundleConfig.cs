using System.Web;
using System.Web.Optimization;

namespace FaroHotel
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                    "~/css/styles.css",
                    "~/css/bootstrap.css",
                    "~/css/select2.min.css",  
                    "~/css/select2-bootstrap.css",
                    "~/css/custom.css",
                    "~/css/animate.min.css",
                    "~/css/jquery-ui.theme.css",
                    "~/css/jquery-ui.structure.css",
                    "~/css/font-awesome.min.css",
                    "~/css/jquery.dataTables.min.css",
                    "~/css/buttons.dataTables.min.css",
                    "~/css/dataTables.responsive.css"));
            //"~/css/dataTables.bootstrap.css"

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                            "~/Scripts/jquery.validate*",
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/select2.full.min.js",
                            "~/Scripts/respond.js",
                            "~/Scripts/moment.min.js",
                            "~/Scripts/jquery-ui.min.js",
                            "~/Scripts/jquery.maskMoney.js",
                            "~/Scripts/custom.js",					
                            "~/Scripts/dataTables.responsive.min.js",
                            "~/Scripts/dataTables.buttons.min.js",
                            "~/Scripts/jszip.min.js",
                            "~/Scripts/buttons.html5.min.js",
                            "~/Scripts/buttons.print.min.js",                  
                            "~/Scripts/plugins/tinymce/tinymce.min.js"));
            //"~/Scripts/dataTables.bootstrap.js"));
#if (DEBUG == false)
            BundleTable.EnableOptimizations = true;
#else
            BundleTable.EnableOptimizations = false;
#endif
        }
    }
}
