using langfvn.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin, AdminBranch1, AdminBranch2")]
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

        // Đếm số lượng tổng lượt xem các bài viết
        public PartialViewResult CountView()
        {
            var countView = db.News.Select(x => x.TotalView).Sum();
            ViewBag.CountView = countView;
            return PartialView("_CountView", countView);
        }

        //Partial Hot Blog
        public PartialViewResult HotBlogNews()
        {
            var hotBlog = db.News.OrderByDescending(x => x.TotalView).Take(3).ToList();
            return PartialView("_HotBlogNews", hotBlog);
        }

        //Partial Promotion Store
        public PartialViewResult PromotionStore()
        {
            var promotionStore = db.Stores.Where(x => x.NoteDiscount != null).OrderByDescending(x => x.StoreID).Take(3).ToList();
            return PartialView("_PromotionStore", promotionStore);
        }

    }
}