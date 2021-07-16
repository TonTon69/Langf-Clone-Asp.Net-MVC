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
    public class StoresController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Stores
        public ActionResult Index(int? page, string search, string khu_vuc)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            ViewBag.CurrentFilter = search;
            var stores = from s in db.Stores select s;
            if (!string.IsNullOrEmpty(search))
            {
                stores = stores.Where(s => s.StoreName.Contains(search) || s.Address.Contains(search) || s.Place.PlaceName.Contains(search));
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (khu_vuc != null)
            {
                stores = stores.OrderBy(s => s.StoreID).Where(s => s.Place.PlaceName == khu_vuc);
                return View(stores.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                stores = stores.OrderBy(s => s.StoreID);
                return View(stores.ToPagedList(pageNumber, pageSize));
            }
        }

        public PartialViewResult Places()
        {
            return PartialView("_Places", db.Places.ToList());
        }

        // GET: admin/Stores/Create
        public ActionResult Create()
        {
            ViewBag.PlaceID = new SelectList(db.Places, "PlaceID", "PlaceName");
            return View();
        }

        // POST: admin/Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StoreID,PlaceID,StoreName,Address,Image,NoteDiscount")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                TempData["success"] = "Thêm mới cửa hàng thành công";
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoreID,PlaceID,StoreName,Address,Image,NoteDiscount")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật cửa hàng thành công";
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
            TempData["success"] = "Xóa cửa hàng thành công";
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
