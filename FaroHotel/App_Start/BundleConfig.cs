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
                    "~/css/daterangepicker.css",
                    "~/css/custom.css",
                    "~/css/animate.min.css",
                    "~/css/jquery-ui.theme.css",
                    "~/css/jquery-ui.structure.css",
                    "~/css/font-awesome.min.css",					
                    "~/css/dataTables.bootstrap.css",
                    "~/css/dataTables.responsive.css",
                    "~/css/jquery.dataTables.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                            "~/Scripts/jquery.validate*",
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/select2.full.min.js",
                            "~/Scripts/respond.js",
                            "~/Scripts/moment.min.js",
                            "~/Scripts/jquery-ui.min.js",
                            "~/Scripts/custom.js",							
 		                    "~/Scripts/jquery.dataTables.js",
                            "~/Scripts/dataTables.responsive.min.js",                            
                            "~/Scripts/plugins/tinymce/tinymce.min.js",
                            "~/Scripts/dataTables.bootstrap.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
