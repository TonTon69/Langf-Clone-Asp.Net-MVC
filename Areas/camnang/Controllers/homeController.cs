using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Areas.camnang.Controllers
{
    public class homeController : Controller
    {
        // GET: camnang/home
        public ActionResult index()
        {
            return View();
        }
    }
}