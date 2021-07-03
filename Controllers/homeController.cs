using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult index()
        {
            return View();
        }
    }
}