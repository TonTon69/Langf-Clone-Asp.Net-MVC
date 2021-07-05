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
    public class CategoryFoodsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/CategoryFoods
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var categoryFood = db.CategoryFoods.OrderBy(k => k.CFoodID);
            return View(categoryFood.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/CategoryFoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/CategoryFoods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CFoodID,CFoodName,Image,CreatedDate")] CategoryFood categoryFood)
        {
            if (ModelState.IsValid)
            {
                categoryFood.CreatedDate = DateTime.Now;
                db.CategoryFoods.Add(categoryFood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryFood);
        }

        // GET: admin/CategoryFoods/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryFood categoryFood = db.CategoryFoods.Find(id);
            if (categoryFood == null)
            {
                return HttpNotFound();
            }
            return View(categoryFood);
        }

        // POST: admin/CategoryFoods/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CFoodID,CFoodName,Image,CreatedDate")] CategoryFood categoryFood)
        {
            if (ModelState.IsValid)
            {
                categoryFood.CreatedDate = DateTime.Now;
                db.Entry(categoryFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryFood);
        }

        // GET: admin/CategoryFoods/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryFood categoryFood = db.CategoryFoods.Find(id);
            if (categoryFood == null)
            {
                return HttpNotFound();
            }
            return View(categoryFood);
        }

        // POST: admin/CategoryFoods/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryFood categoryFood = db.CategoryFoods.Find(id);
            db.CategoryFoods.Remove(categoryFood);
            db.SaveChanges();
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
