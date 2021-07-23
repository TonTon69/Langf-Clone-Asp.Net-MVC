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
    [AccessDeniedAuthorize(Roles = "Admin")]
    public class ReviewsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Reviews
        public ActionResult Index(int? page, string search)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            ViewBag.CurrentFilter = search;
            var reviews = from r in db.Reviews select r;
            if (!string.IsNullOrEmpty(search))
            {
                reviews = reviews.Where(s => s.Account.FullName.Contains(search) ||
                s.Content.Contains(search) || s.Store.StoreName.Contains(search));
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            reviews = reviews.OrderBy(s => s.ReviewID);
            return View(reviews.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/Reviews/Delete/id
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

        // POST: admin/Reviews/Delete/id
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
