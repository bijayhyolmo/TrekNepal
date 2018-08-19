using System.Web;
using System.Web.Optimization;

namespace TrekNepal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                "~/Content/vendor/jquery/jquery.js",
                "~/Content/vendor/bootstrap/js/bootstrap.bundle.js",
                "~/Content/vendor/@coreui/coreui/js/coreui.min.js",
                "~/Content/admin/js/main.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/admincss").Include(
                      "~/Content/vendor/@coreui/cssicons/css/coreui-icons.min.css",
                      "~/Content/vendor/font-awesome/css/font-awesome.min.css",
                      "~/Content/vendor/bootstrap/css/bootstrap.css",
                      "~/Content/admin/css/style.css",
                      "~/Content/admin/css/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/publicjs").Include(
                "~/Content/vendor/jquery/jquery.slim.js",
                "~/Content/vendor/bootstrap/js/bootstrap.bundle.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/publiccss").Include(
                    "~/Content/vendor/bootstrap/css/bootstrap.css",
                      "~/Content/public/css/public.css"));
        }
    }
}
