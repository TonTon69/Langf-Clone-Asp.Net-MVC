using langfvn.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Areas.camnang.Controllers
{
    public class NewsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();
        // GET: camnang/News
        public ActionResult Index(int? page, string category, string kon)
        {
            //Tạo biến quy định số bài viết ở mỗi trang
            int pageSize = 9;
            //Biến số trang
            int pageNum = (page ?? 1);//Nếu page = null thì mặc định là 1 khác null thì sẽ lấy giá trị của page

            if (category != null && kon != null)
            {
                ViewBag.kindOfNews = kon;
                var kindOfNews = db.News.OrderByDescending(x => x.CreatedDate).Where(x => x.KindOfNew.KonName == kon);
                return View(kindOfNews.ToPagedList(pageNum, pageSize));
            }
            else if (category != null && kon == null)
            {
                ViewBag.categorynews = category;
                var categoryNews = db.News.OrderByDescending(x => x.CreatedDate).Where(x => x.KindOfNew.CategoryNew.CNewsName == category);
                return View(categoryNews.ToPagedList(pageNum, pageSize));
            }
            else
            {
                var categoryNews = db.News.OrderByDescending(x => x.CreatedDate).ToList();
                return View(categoryNews.ToPagedList(pageNum, pageSize));
            }
           
        }

      
        //Partial Slide
        public PartialViewResult SlideNews()
        {
            var list = db.News.OrderBy(x => Guid.NewGuid()).Take(3); //Randow chọn bài trong table news
            return PartialView("_SlideNews", list);
        }


        //Chi tiết 1 bài viết
        public ActionResult Details(int? id, string name)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            var count = db.News.SingleOrDefault(x => x.NewsID == id);
            count.TotalView++;
            db.News.AddOrUpdate(count);
            db.SaveChanges();
            return View(news);
        }

        //Bài viết liên quan
        public PartialViewResult RelatedPosts(int id)
        {
            var relatedPost = db.News.OrderByDescending(x => Guid.NewGuid()).Where(x => x.NewsID != id && x.KonID == id).Take(3).ToList();
            return PartialView("_RelatedPosts", relatedPost);
        }

        //Partial Hot Blog
        public PartialViewResult HotBlogNews()
        {
            var hotBlog = db.News.OrderByDescending(x => x.TotalView).Take(10).ToList();
            return PartialView("_HotBlogNews", hotBlog);
        }
    }
}