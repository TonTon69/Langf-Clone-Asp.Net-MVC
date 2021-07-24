using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using langfvn.Models;


namespace langfvn.Areas.camnang.Controllers
{
    public class HomeController : Controller
    {
        private LangfvnContext db = new LangfvnContext();
        // GET: camnang/home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Introduce()
        {
            return View();
        }
        // Partial banner
        public PartialViewResult Banner()
        {
            return PartialView("_Banner", db.Banners.ToList());
        }


        //Partial Partner Logo
        public PartialViewResult PartnerLogo()
        {
            return PartialView("_PartnerLogo", db.Stores.ToList());
        }

        //Partial Hot Blog
        public PartialViewResult HotBlog()
        {
            var hotBlog = db.News.OrderByDescending(x => x.TotalView).Take(8).ToList();
            return PartialView("_HotBlog", hotBlog);
        }


        //Partial Question_KTX
        public PartialViewResult Question_KTX()
        {
            var ListNews = db.News.Where(x => x.Title.Contains("Ký túc xá") || x.Title.Contains("ktx")).Take(4).ToList();
            return PartialView("_Question_KTX", ListNews);
        }

        //Partial Question_Food
        public PartialViewResult Question_Food()
        {
            var ListNews = db.News.Where(x => x.KonID == 7).Take(4).ToList();
            return PartialView("_Question_Food", ListNews);
        }

        //
        //Partial Question_Need
        public PartialViewResult Question_Need()
        {
            var ListNews = db.News.Where(x => x.KindOfNew.KonID == 5 || x.KindOfNew.KonID == 6 || x.Title.Contains("dành cho sinh viên") || x.Title.Contains("sinh viên cần có")).Take(4).ToList();
            return PartialView("_Question_Need", ListNews);
        }


        //Partial Question_Fun
        public PartialViewResult Question_Fun()
        {
            var ListNews = db.News.Where(x => x.Title.Contains("có gì vui?") || x.Title.Contains("?") || x.Title.Contains("giải thích") || x.Title.Contains("giải mã bí ấn")).Take(4).ToList();
            return PartialView("_Question_Fun", ListNews);
        }

        //Partial Question_Lake
        public PartialViewResult Question_Lake()
        {
            var ListNews = db.News.Where(x => x.Title.Contains("hồ đá")).Take(4).ToList();
            return PartialView("_Question_Lake", ListNews);
        }

        //Partial Question_Fun
        public PartialViewResult Question_Discount()
        {
            var ListNews = db.News.Where(x => x.Title.Contains("giảm giá") || x.Title.Contains("khuyến mãi") || x.Title.Contains("học bổng")).Take(4).ToList();
            return PartialView("_Question_Discount", ListNews);
        }

        //Partial Header
        public PartialViewResult MenuHeader()
        {
            return PartialView("_MenuHeader", db.CategoryNews.ToList());
        }

        //Partial Header
        public PartialViewResult MenuHeaderCon(int? id)
        {
            var MenuHeaderCons = db.KindOfNews.Where(m => m.CNewsID == id).ToList();
            return PartialView("_MenuHeaderCon", MenuHeaderCons);
        }

        //Partial Cẩm nang sinh viên
        public PartialViewResult HandBook()
        {
            var lists = db.News.Where(m => m.KindOfNew.CNewsID == 2).Take(8).ToList();
            return PartialView("_HandBook", lists);
        }

        //Partial Chuyện ma
        public PartialViewResult GhostStory()
        {
            var lists = db.News.Where(m => m.KonID == 12).Take(6).ToList();
            return PartialView("_GhostStory", lists);
        }

        //Partial Làng Review
        public PartialViewResult LangReview()
        {
            var lists = db.News.Where(m => m.KindOfNew.CategoryNew.CNewsID == 3).Take(8).ToList();
            return PartialView("_LangReview", lists);
        }
        //Partial search
        public PartialViewResult Search(string search)
        {
            var result = db.News.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
            ViewBag.search = search;
            return PartialView("_Search", result);
        }
    }
}