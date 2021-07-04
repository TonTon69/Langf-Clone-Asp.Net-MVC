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
    public class KindOfNewsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/KindOfNews
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var kindOfNews = db.KindOfNews.OrderBy(k => k.KonID);
            return View(kindOfNews.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/KindOfNews/Details/id
        public ActionResult Details(int? id)
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
