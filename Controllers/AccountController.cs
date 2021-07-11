using langfvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Controllers
{
    public class AccountController : Controller
    {
        private LangfvnContext db = new LangfvnContext();

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                Account user = db.Accounts.Where(x => x.Email.Equals(email) && x.Password.Equals(f_password)).FirstOrDefault();

                if (user != null)
                {
                    Session["UserId"] = user.UserID;
                    Session["Account"] = user;
                    Session["FullName"] = user.FullName;
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewBag.errorLogin = "Email đăng nhập hoặc mật khẩu không đúng!";
                    return RedirectToAction("login", "account");
                }
            }
            return View();
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
    }
}