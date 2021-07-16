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
    [Authorize(Roles = "Admin")]
    public class AdvertisementsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Advertisements
        public ActionResult Index()
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            return View(db.Advertisements.ToList());
        }

        // GET: admin/Advertisements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Advertisements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdID,AdTitle,AdUrl,Image,Visible")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
                TempData["success"] = "Thêm mới quảng cáo thành công";
                return RedirectToAction("Index");
            }

            return View(advertisement);
        }

        // GET: admin/Advertisements/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: admin/Advertisements/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdID,AdTitle,AdUrl,Image,Visible")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật quảng cáo thành công";
                return RedirectToAction("Index");
            }
            return View(advertisement);
        }

        // GET: admin/Advertisements/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: admin/Advertisements/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            db.Advertisements.Remove(advertisement);
            db.SaveChanges();
            TempData["success"] = "Xóa quảng cáo thành công";
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
