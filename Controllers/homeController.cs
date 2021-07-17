using langfvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Controllers
{
    public class HomeController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: home
        public ActionResult Index()
        {
            return View();
        }

    }
}