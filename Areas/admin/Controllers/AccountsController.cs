using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using langfvn.Models;
using PagedList;

namespace langfvn.Areas.admin.Controllers
{
    public class AccountsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Accounts
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string search)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            ViewBag.CurrentFilter = search;
            var accounts = from a in db.Accounts select a;
            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts.Where(a => a.FullName.Contains(search) || a.Email.Contains(search) || a.Phone.Contains(search)
                || a.Address.Contains(search) || a.Unit.Contains(search) || a.Role.RoleName.Contains(search));
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            accounts = accounts.Where(s => s.Role.RoleName != "Admin").OrderBy(s => s.UserID);
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/Accounts/ProfileAdmin
        public ActionResult ProfileAdmin(int? id)
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account acc = db.Accounts.Find(id);
            if (acc == null)
            {
                return HttpNotFound();
            }
            return View(acc);
        }

        // GET: admin/Accounts/Edit/id
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", account.RoleID);
            return View(account);

        }

        // POST: admin/Accounts/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", account.RoleID);
            return View(account);
        }

        // GET: admin/Accounts/Delete/id
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: admin/Accounts/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            TempData["success"] = "Xóa người dùng thành công";
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

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        //login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var ad = db.Accounts.Where(x => x.Email.Equals(email) && x.Password.Equals(f_password) && x.Role.RoleName != "Member").FirstOrDefault();
                if (ad != null)
                {
                    FormsAuthentication.SetAuthCookie(ad.Email, false);
                    Session["AdId"] = ad.UserID;
                    Session["Email"] = ad.Email;
                    Session["FullName"] = ad.FullName;
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewBag.errorLogin = "Email đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", account.RoleID);
            return View(account);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Cập nhật thông tin thành công";
                return RedirectToAction("profileadmin", "accounts", new { id = Session["AdID"] });
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", account.RoleID);
            return View(account);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel account)
        {
            var oldPass = GetMD5(account.OldPassword);
            var checkPass = db.Accounts.Where(x => x.Password == oldPass && x.Email == account.Email).FirstOrDefault();
            if (checkPass != null)
            {
                checkPass.Password = GetMD5(account.NewPassword);
                db.SaveChanges();
                TempData["success"] = "Cập nhật mật khẩu mới thành công";
                return RedirectToAction("profileadmin", "accounts", new { id = Session["AdID"] });
            }
            else
            {
                ViewBag.Error = "Mật khẩu hoặc email đăng nhập không đúng";
            }
            return View(account);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("login", "accounts");
        }
    }
}
