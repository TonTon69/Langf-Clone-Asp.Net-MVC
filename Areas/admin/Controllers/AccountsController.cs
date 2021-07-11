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
using langfvn.Models;
using PagedList;

namespace langfvn.Areas.admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        // GET: admin/Accounts
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
        public ActionResult Edit([Bind(Include = "UserID,FullName,Email,Password,Phone,Address,Unit,RoleID")] Account account)
        {
            if (ModelState.IsValid)
            {
                var checkPhone = db.Accounts.FirstOrDefault(x => x.Phone == account.Phone);
                if (checkPhone != null)
                {
                    ViewBag.ErrorPhone = "Số điện thoại này đã tồn tại";
                }
                else
                {
                    account.Password = GetMD5(account.Password);
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Cập nhật người dùng thành công";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", account.RoleID);
            return View(account);
        }

        // GET: admin/Accounts/Delete/id
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
                Account ad = db.Accounts.Where(x => x.Email.Equals(email) && x.Password.Equals(f_password)).FirstOrDefault();
                var checkRole = db.Accounts.FirstOrDefault(c => c.Role.RoleName != "Member");

                if (ad != null)
                {
                    if (checkRole != null)
                    {
                        Session["AdId"] = ad.UserID;
                        Session["FullName"] = ad.FullName;
                        return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    ViewBag.errorLogin = "Email đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("login", "accounts");
        }
    }
}
