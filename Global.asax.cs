using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = @"data source=admin\hoangit;initial catalog=CamNangLangDaiHoc;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT ViewCount FROM AccessTime";
                Application["luottruycap"] = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application["SessionCount"] = Convert.ToInt32(Application["SessionCount"]) + 1;
            Application["luottruycap"] = Convert.ToInt32(Application["luottruycap"]) + 1;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = @"data source=admin\hoangit;initial catalog=CamNangLangDaiHoc;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "update AccessTime Set ViewCount =" + Application["luottruycap"];
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State.Equals(ConnectionState.Open)) con.Close();
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["SessionCount"] = Convert.ToInt32(Application["SessionCount"]) - 1;
            Application.UnLock();

        }
    }
}
