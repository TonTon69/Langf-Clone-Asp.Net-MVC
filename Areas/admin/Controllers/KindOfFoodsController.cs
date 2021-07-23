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
    [AccessDeniedAuthorize(Roles = "Admin, AdminBranch2")]
    public class KindOfFoodsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/KindOfFoods
        public ActionResult Index(int? page, string search)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            ViewBag.CurrentFilter = search;
            var kindOfFoods = from k in db.KindOfFoods select k;
            if (!string.IsNullOrEmpty(search))
            {
                kindOfFoods = kindOfFoods.Where(k => k.CategoryFood.CFoodName.Contains(search) || k.Store.StoreName.Contains(search) || k.KofName.Contains(search));
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            kindOfFoods = kindOfFoods.OrderBy(k => k.KofID);
            return View(kindOfFoods.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/KindOfFoods/Create
        public ActionResult Create()
        {
            ViewBag.CFoodID = new SelectList(db.CategoryFoods, "CFoodID", "CFoodName");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName");
            return View();
        }

        // POST: admin/KindOfFoods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KofID,StoreID,CFoodID,KofName,CreatedDate")] KindOfFood kindOfFood)
        {
            if (ModelState.IsValid)
            {
                kindOfFood.CreatedDate = DateTime.Now;
                db.KindOfFoods.Add(kindOfFood);
                db.SaveChanges();
                TempData["success"] = "Thêm mới loại món ăn thành công";
                return RedirectToAction("Index");
            }

            ViewBag.CFoodID = new SelectList(db.CategoryFoods, "CFoodID", "CFoodName", kindOfFood.CFoodID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", kindOfFood.StoreID);
            return View(kindOfFood);
        }

        // GET: admin/KindOfFoods/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfFood kindOfFood = db.KindOfFoods.Find(id);
            if (kindOfFood == null)
            {
                return HttpNotFound();
            }
            ViewBag.CFoodID = new SelectList(db.CategoryFoods, "CFoodID", "CFoodName", kindOfFood.CFoodID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", kindOfFood.StoreID);
            return View(kindOfFood);
        }

        // POST: admin/KindOfFoods/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KofID,StoreID,CFoodID,KofName,CreatedDate")] KindOfFood kindOfFood)
        {
            if (ModelState.IsValid)
            {
                kindOfFood.CreatedDate = DateTime.Now;
                db.Entry(kindOfFood).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Câp nhật loại món ăn thành công";
                return RedirectToAction("Index");
            }
            ViewBag.CFoodID = new SelectList(db.CategoryFoods, "CFoodID", "CFoodName", kindOfFood.CFoodID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", kindOfFood.StoreID);
            return View(kindOfFood);
        }

        // GET: admin/KindOfFoods/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfFood kindOfFood = db.KindOfFoods.Find(id);
            if (kindOfFood == null)
            {
                return HttpNotFound();
            }
            return View(kindOfFood);
        }

        // POST: admin/KindOfFoods/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KindOfFood kindOfFood = db.KindOfFoods.Find(id);
            db.KindOfFoods.Remove(kindOfFood);
            db.SaveChanges();
            TempData["success"] = "Xóa loại món ăn thành công";
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
