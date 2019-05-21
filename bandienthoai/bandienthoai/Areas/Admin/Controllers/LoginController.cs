using bandienthoai.Areas.Admin.Models;
using bandienthoai.Areas.Admin.Models.DAO;
using bandienthoai.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bandienthoai.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult logout(LoginModel model)
        {
            var userSession = new UserLogin();
            Session.Add(CommonStants.USER_SESSION, userSession);
            return View("index");
        }
        public ActionResult login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
        
                var dao = new UserDAO();
                var result = dao.Login(model.userName, Encryptor.MD5Hash(model.passWord));
                if (result==1)
                {
                    var user = dao.GetByUserName(model.userName);
                    var userSession = new UserLogin();
                    userSession.userName = user.TENTAIKHOAN;
                    userSession.userID = user.ID;
                    Session.Add(CommonStants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result==0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                    
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa!");

                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu!");

                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập không đúng");

                }

            }
            else {
                

            }
            return View("index");
        }

    }
}