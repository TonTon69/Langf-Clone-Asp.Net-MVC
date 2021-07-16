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
    [Authorize(Roles = "Admin, AdminBranch2")]
    public class FoodsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Foods
        public ActionResult Index(int? page, string search)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            ViewBag.CurrentFilter = search;
            var foods = from f in db.Foods select f;
            if (!string.IsNullOrEmpty(search))
            {
                foods = foods.Where(f => f.KindOfFood.KofName.Contains(search) || f.FoodName.Contains(search));
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            foods = foods.OrderBy(k => k.FoodID);
            return View(foods.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/Foods/Create
        public ActionResult Create()
        {
            ViewBag.KofID = new SelectList(db.KindOfFoods, "KofID", "KofName");
            return View();
        }

        // POST: admin/Foods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodID,KofID,FoodName,FoodPrice,Description")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Foods.Add(food);
                db.SaveChanges();
                TempData["success"] = "Thêm mới món ăn thành công";
                return RedirectToAction("Index");
            }

            ViewBag.KofID = new SelectList(db.KindOfFoods, "KofID", "KofName", food.KofID);
            return View(food);
        }

        // GET: admin/Foods/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.KofID = new SelectList(db.KindOfFoods, "KofID", "KofName", food.KofID);
            return View(food);
        }

        // POST: admin/Foods/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodID,KofID,FoodName,FoodPrice,Description")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật món ăn thành công";
                return RedirectToAction("Index");
            }
            ViewBag.KofID = new SelectList(db.KindOfFoods, "KofID", "KofName", food.KofID);
            return View(food);
        }

        // GET: admin/Foods/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: admin/Foods/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            TempData["success"] = "Xóa món ăn thành công";
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
