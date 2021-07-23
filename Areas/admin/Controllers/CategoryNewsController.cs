using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using langfvn.Models;

namespace langfvn.Areas.admin.Controllers
{
    [AccessDeniedAuthorize(Roles = "Admin, AdminBranch1")]
    public class CategoryNewsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/categorynews
        public ActionResult Index()
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            return View(db.CategoryNews.ToList());
        }

        // GET: admin/categorynews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/categorynews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CNewsID,CNewsName,CreatedDate")] CategoryNew categoryNew)
        {
            if (ModelState.IsValid)
            {
                categoryNew.CreatedDate = DateTime.Now;
                db.CategoryNews.Add(categoryNew);
                db.SaveChanges();
                TempData["success"] = "Thêm mới thể loại tin thành công";
                return RedirectToAction("Index");
            }

            return View(categoryNew);
        }

        // GET: admin/categorynews/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryNew categoryNew = db.CategoryNews.Find(id);
            if (categoryNew == null)
            {
                return HttpNotFound();
            }
            return View(categoryNew);
        }

        // POST: admin/categorynews/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CNewsID,CNewsName,CreatedDate")] CategoryNew categoryNew)
        {
            if (ModelState.IsValid)
            {
                categoryNew.CreatedDate = DateTime.Now;
                db.Entry(categoryNew).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật thể loại tin thành công";
                return RedirectToAction("Index");
            }
            return View(categoryNew);
        }

        // GET: admin/categorynews/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryNew categoryNew = db.CategoryNews.Find(id);
            if (categoryNew == null)
            {
                return HttpNotFound();
            }
            return View(categoryNew);
        }

        // POST: admin/categorynews/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryNew categoryNew = db.CategoryNews.Find(id);
            db.CategoryNews.Remove(categoryNew);
            db.SaveChanges();
            TempData["success"] = "Xóa thể loại tin thành công";
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
