using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using langfvn.Models;
using langfvn.Common;

namespace langfvn.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckEmail(registerModel.Email) == false)
                {
                    Account account = new Account();
                    account.Email = registerModel.Email;
                    account.Password = Encryptor.ToMD5(registerModel.Password);
                    account.FullName = registerModel.FullName;
                    account.Phone = registerModel.Phone;
                    account.RoleID = 2;
                    bool result = dao.Insert(account);
                    if (result)
                    {
                        TempData["success"] = "Tạo tài khoản thành công";
                        return RedirectToAction("login", "account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email này đã tồn tại");
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();//gọi lớp userDao
                var result = dao.Login(model.UserName, Encryptor.ToMD5(model.Password));// kiểm tra tk ,mk
                if (result)// nếu true
                {
                    var user = dao.GetAccountByEmail(model.UserName);// tìm kiếm user đã đăng nhập
                    var userSesion = new UserLogin();// tạo biến userlogin
                    userSesion.UserName = user.FullName;// lưu tên user vừa tìm được vào biến userlogin
                    userSesion.UserID = user.UserID;
                    Session.Add(CommonConstaints.USER_SESSION, userSesion);//Session.Add("Tên_Biến","Giá trị khởi tạo");
                    return RedirectToAction("Index", "Home");
                    /* Lý thuyết về session
                     * cú pháp tạo biến Session:  Session.Add("Tên_Biến","Giá trị khởi tạo"); hoặc Session["Tên Biến"] = Giá trị;
                     * Cú pháp để đọc giá trị của một biến sesstion : Session.Contents[“Tên_Biến”] hoặc dùng chỉ số: Session.Contents[i]; hoặc  <Biến> = Session["Tên Biến"];
                     */
                }
                else
                {
                    ViewBag.LoginFail = "Đăng nhập thất bại vui lòng thử lại";
                }
            }
            else
            {

            }
            return View("Index");
        }
        public ActionResult PersonalInformation
()
        {
            int id = (Session.Contents["USER_SESSION"] as UserLogin).UserID;
            var dao = new UserDao();//gọi lớp userDao
            Account account = dao.GetAccountById(id);
            return View(account);
        }
        public ActionResult UpdateInformation()
        {
            int id = (Session.Contents["USER_SESSION"] as UserLogin).UserID;
            var dao = new UserDao();//gọi lớp userDao
            Account account = dao.GetAccountById(id);
            if (TempData["result"] != null)
            {
                ViewBag.result = TempData["result"];
            }
            return View(account);
        }


        [HttpPost]
        public ActionResult UpdateInformation(Account account)
        {
            UserDao dao = new UserDao();
            dao.UpdateInformation(account);
            TempData["result"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("updateinformation", "account");
        }
    }
}