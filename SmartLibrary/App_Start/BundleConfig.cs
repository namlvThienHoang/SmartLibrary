using System.Web;
using System.Web.Optimization;

namespace SmartLibrary
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/popper").Include(
                         "~/Scripts/bootstrap.bundle.min.js"
                        ));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"
                      ));

            bundles.Add(new Bundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/css/select2.min.css",
                      "~/Content/site.css"));

            bundles.Add(new Bundle("~/bundles/select2").Include(
                        "~/Scripts/select2.min.js"));
        }
    }
}
