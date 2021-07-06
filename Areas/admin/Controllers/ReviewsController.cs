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
    public class ReviewsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Account).Include(r => r.Store);
            return View(reviews.ToList());
        }

        // GET: admin/Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: admin/Reviews/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Accounts, "UserID", "FullName");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName");
            return View();
        }

        // POST: admin/Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,UserID,StoreID,Content,Image,Star")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Accounts, "UserID", "FullName", review.UserID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", review.StoreID);
            return View(review);
        }

        // GET: admin/Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Accounts, "UserID", "FullName", review.UserID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", review.StoreID);
            return View(review);
        }

        // POST: admin/Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,UserID,StoreID,Content,Image,Star")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Accounts, "UserID", "FullName", review.UserID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", review.StoreID);
            return View(review);
        }

        // GET: admin/Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: admin/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
