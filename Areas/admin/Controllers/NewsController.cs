using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using langfvn.Models;
using PagedList;

namespace langfvn.Areas.admin.Controllers
{
    public class NewsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/News
        public ActionResult Index(int? page, string search, string loai_tin)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            var news = from n in db.News select n;
            ViewBag.CurrentFilter = search;
            ViewBag.KonID = new SelectList(db.KindOfNews, "KonID", "KonName");
            if (!string.IsNullOrEmpty(search))
            {
                news = news.Where(n => n.Title.Contains(search) || n.Content.Contains(search)
                || n.KindOfNew.KonName.Contains(search) || n.KindOfNew.CategoryNew.CNewsName.Contains(search));
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (loai_tin != null)
            {
                news = news.OrderBy(s => s.NewsID).Where(s => s.KindOfNew.KonName == loai_tin);
                return View(news.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                news = news.OrderBy(s => s.NewsID);
                return View(news.ToPagedList(pageNumber, pageSize));
            }
        }

        public PartialViewResult KindOfNews()
        {
            return PartialView("_KindOfNews", db.KindOfNews.ToList());
        }

        // GET: admin/News/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: admin/News/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Accounts.Where(x => x.Role.RoleName != "Member"), "UserID", "FullName");
            ViewBag.KonID = new SelectList(db.KindOfNews, "KonID", "KonName");
            return View();
        }

        // POST: admin/News/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,KonID,UserID,Title,Content,Image,TotalView,CreatedDate")] News news)
        {
            if (ModelState.IsValid)
            {
                news.TotalView = 1;
                news.CreatedDate = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                TempData["Success"] = "Thêm mới tin tức thành công";
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Accounts.Where(x => x.Role.RoleName != "Member"), "UserID", "FullName", news.UserID);
            ViewBag.KonID = new SelectList(db.KindOfNews, "KonID", "KonName", news.KonID);
            return View(news);
        }

        // GET: admin/News/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Accounts.Where(x => x.Role.RoleName != "Member"), "UserID", "FullName", news.UserID);
            ViewBag.KonID = new SelectList(db.KindOfNews, "KonID", "KonName", news.KonID);
            return View(news);
        }

        // POST: admin/News/Edit/id
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,KonID,UserID,Title,Content,Image,TotalView,CreatedDate")] News news)
        {
            if (ModelState.IsValid)
            {
                news.CreatedDate = DateTime.Now;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Cập nhật tin tức thành công";
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Accounts.Where(x => x.Role.RoleName != "Member"), "UserID", "FullName", news.UserID);
            ViewBag.KonID = new SelectList(db.KindOfNews, "KonID", "KonName", news.KonID);
            return View(news);
        }

        // GET: admin/News/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            TempData["Success"] = "Xóa tin tức thành công";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
