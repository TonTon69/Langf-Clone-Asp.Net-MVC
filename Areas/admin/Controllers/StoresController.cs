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
    public class StoresController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Stores
        public ActionResult Index()
        {
            var stores = db.Stores.Include(s => s.Place);
            return View(stores.ToList());
        }

        // GET: admin/Stores/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: admin/Stores/Create
        public ActionResult Create()
        {
            ViewBag.PlaceID = new SelectList(db.Places, "PlaceID", "PlaceName");
            return View();
        }

        // POST: admin/Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317id98.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StoreID,PlaceID,StoreName,Address,Image,NoteDiscount")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceID = new SelectList(db.Places, "PlaceID", "PlaceName", store.PlaceID);
            return View(store);
        }

        // GET: admin/Stores/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceID = new SelectList(db.Places, "PlaceID", "PlaceName", store.PlaceID);
            return View(store);
        }

        // POST: admin/Stores/Edit/id
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317id98.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoreID,PlaceID,StoreName,Address,Image,NoteDiscount")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceID = new SelectList(db.Places, "PlaceID", "PlaceName", store.PlaceID);
            return View(store);
        }

        // GET: admin/Stores/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: admin/Stores/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
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
