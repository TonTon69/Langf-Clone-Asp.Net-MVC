using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Areas.admin.Controllers
{
    public class homeController : Controller
    {
        // GET: admin/home
        public ActionResult index()
        {
            return View();
        }
    }
}