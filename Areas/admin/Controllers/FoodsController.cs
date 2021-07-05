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
    public class FoodsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Foods
        public ActionResult Index()
        {
            var foods = db.Foods.Include(f => f.KindOfFood);
            return View(foods.ToList());
        }

        // GET: admin/Foods/Details/id
        public ActionResult Details(int? id)
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
