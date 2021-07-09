using langfvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/home
        public ActionResult Index()
        {
            return View();
        }

        // Đếm số lượng tin tức
        public PartialViewResult CountNews()
        {
            var countNews = db.News.Count();
            ViewBag.CountNews = countNews;
            return PartialView("_CountNews", countNews);
        }

        // Đếm số lượng người dùng
        public PartialViewResult CountUser()
        {
            var countUser = db.Accounts.Count();
            ViewBag.CountUser = countUser;
            return PartialView("_CountUser", countUser);
        }

        // Đếm số lượng cửa hàng
        public PartialViewResult CountStore()
        {
            var countStore = db.Stores.Count();
            ViewBag.CountStore = countStore;
            return PartialView("_CountStore", countStore);
        }

        // Đếm số lượng món ăn
        public PartialViewResult CountView()
        {
            var countView = 1;
            ViewBag.CountView = countView;
            return PartialView("_CountView", countView);
        }
    }
}