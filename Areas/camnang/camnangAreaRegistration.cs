using System.Web.Mvc;

namespace langfvn.Areas.camnang
{
    public class camnangAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "camnang";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "camnang_default",
                "camnang/{controller}/{action}/{id}",
                new { controller = "home", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}