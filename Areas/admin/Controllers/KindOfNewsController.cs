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
    [AccessDeniedAuthorize(Roles = "Admin, AdminBranch1")]
    public class KindOfNewsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/KindOfNews
        public ActionResult Index(int? page, string search)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            ViewBag.CurrentFilter = search;
            var kindOfNews = from k in db.KindOfNews select k;
            if (!string.IsNullOrEmpty(search))
            {
                kindOfNews = kindOfNews.Where(k => k.CategoryNew.CNewsName.Contains(search) || k.KonName.Contains(search));
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            kindOfNews = kindOfNews.OrderBy(k => k.KonID);
            return View(kindOfNews.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/KindOfNews/Create
        public ActionResult Create()
        {
            ViewBag.CNewsID = new SelectList(db.CategoryNews, "CNewsID", "CNewsName");
            return View();
        }

        // POST: admin/KindOfNews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KonID,CNewsID,KonName,CreatedDate")] KindOfNew kindOfNew)
        {
            if (ModelState.IsValid)
            {
                kindOfNew.CreatedDate = DateTime.Now;
                db.KindOfNews.Add(kindOfNew);
                db.SaveChanges();
                TempData["success"] = "Thêm mới loại tin thành công";
                return RedirectToAction("Index");
            }

            ViewBag.CNewsID = new SelectList(db.CategoryNews, "CNewsID", "CNewsName", kindOfNew.CNewsID);
            return View(kindOfNew);
        }

        // GET: admin/KindOfNews/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfNew kindOfNew = db.KindOfNews.Find(id);
            if (kindOfNew == null)
            {
                return HttpNotFound();
            }
            ViewBag.CNewsID = new SelectList(db.CategoryNews, "CNewsID", "CNewsName", kindOfNew.CNewsID);
            return View(kindOfNew);
        }

        // POST: admin/KindOfNews/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KonID,CNewsID,KonName,CreatedDate")] KindOfNew kindOfNew)
        {
            if (ModelState.IsValid)
            {
                kindOfNew.CreatedDate = DateTime.Now;
                db.Entry(kindOfNew).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Chỉnh sửa loại tin thành công";
                return RedirectToAction("Index");
            }
            ViewBag.CNewsID = new SelectList(db.CategoryNews, "CNewsID", "CNewsName", kindOfNew.CNewsID);
            return View(kindOfNew);
        }

        // GET: admin/KindOfNews/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfNew kindOfNew = db.KindOfNews.Find(id);
            if (kindOfNew == null)
            {
                return HttpNotFound();
            }
            return View(kindOfNew);
        }

        // POST: admin/KindOfNews/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KindOfNew kindOfNew = db.KindOfNews.Find(id);
            db.KindOfNews.Remove(kindOfNew);
            db.SaveChanges();
            TempData["success"] = "Xóa loại tin thành công";
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
