using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace langfvn
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["SessionCount"] = 0;
            Application["luottruycap"] = 0;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["SessionCount"] = Convert.ToInt32(Application["SessionCount"]) + 1;
            Application["luottruycap"] = Convert.ToInt32(Application["luottruycap"]) + 1;
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["SessionCount"] = Convert.ToInt32(Application["SessionCount"]) - 1;
            Application.UnLock();
        }
    }
}
