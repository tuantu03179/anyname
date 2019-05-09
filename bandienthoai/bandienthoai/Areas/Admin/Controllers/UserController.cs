using bandienthoai.Areas.Admin.Models.DAO;
using bandienthoai.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using bandienthoai.Common;

namespace bandienthoai.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index( )
        {
            getallTypeUser();
               var dao = new UserDAO();
            var x= ((UserLogin)Session[CommonStants.USER_SESSION]).userID;
            var user = new UserDAO().ViewDetail(x);
            ViewBag.typeLoai = getTypeUserView(user);
            ViewBag.IDUser = x;
           //var model = dao.listuser(page, pageSize);
           var model =dao.GetLoaiTaiKhoan();
            return View(model);
        }
        public void getallTypeUser(long? selectedId = null)
        {
            var dao = new LoaiTaiKhoanDAO();
            ViewBag.IDLOAITAIKHOAN = new SelectList(dao.GetListLoaiTK(), "IDLOAITAIKHOAN", "TENLOAITAIKHOAN", selectedId);
        }
        [HttpGet]
        public ActionResult Create()
        {
            getTypeUser();
            return View();
        }
        [HttpPost]
        public  ActionResult Create(TAIKHOAN user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var tk = dao.GetByUserName(user.TENTAIKHOAN);

                if (tk != null)
                {
                     SetAlert("Tai khaon ton tai", "fail");
                    return RedirectToAction("Index", "User");
                }
                user.MATKHAU = Encryptor.MD5Hash(user.MATKHAU);
                user.CREATEBY = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
                user.CREATEDATE = DateTime.Now;
                decimal id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm Thành công","success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("","Thêm không thành công!");
                }
            }
            return View("Index");
            
        }
        public void getTypeUser(long? selectedId = null)
        {
            var dao = new UserDAO();
            var result = dao.getAllTypeUser();
          
            ViewBag.LOAITAIKHOAN_ID = new SelectList(result, "LOAITAIKHOAN_ID", "TENLOAITK", selectedId);
        }
        public int getTypeUserView(TAIKHOAN user)
        {
            var typeUser = new LoaiTaiKhoanDAO().GetTypeUserByID(user.LOAITAIKHOAN_ID);
            if (typeUser.TENLOAITK.ToLower() == "admin")
            {
               return 1;
            }
            else
               return 2;
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            getTypeUser();
            var user = new UserDAO().ViewDetail(id);
            user.MATKHAU= Encryptor.Decrypt(user.MATKHAU);
            ViewBag.typeLoai = getTypeUserView(user);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(TAIKHOAN user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var tmp = dao.GetByID(user.ID);
                if(tmp.MATKHAU!= Encryptor.MD5Hash(user.MATKHAU))
                {
                    SetAlert("Sai Mật khẩu", "warning");
                    return RedirectToAction("Index", "User");
                }
                var mk = Request.Form.Get("txtMatKhauMoi");
                if (!string.IsNullOrEmpty(mk))
                {
                    
                    user.MATKHAU = Encryptor.MD5Hash(mk);
                }
                else
                {
                    user.MATKHAU= Encryptor.MD5Hash(user.MATKHAU);
                }
               
                user.MODIFILEDBY = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
                var id = dao.Update(user);
                if (id)
                {
                    SetAlert("Sửa thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Sửa không thành công", "danger");
                    ModelState.AddModelError("", "Cập nhật không thành Công!");
                }
            }
            return View("Index");

        }   
        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    var dao = new UserDAO();
        //    var tk = dao.GetByID(id);
        //    if (tk.TENTAIKHOAN.ToLower() == "admin")
        //    {
        //        SetAlert("Tài khoản Admin không được xóa", "success");
        //    }
        //    else { 
        //   dao.Delete(id);
        //    SetAlert("Xóa thành công", "success");
        //    }
        //    return RedirectToAction("Index");
         
        //}
    
        public JsonResult Delete(int id)
        {
            var dao = new UserDAO();
            var tk = dao.GetByID(id);
            var result = false;
            if (tk.TENTAIKHOAN.ToLower() == "admin")
            {
                SetAlert("Tài khoản Admin không được xóa", "success");
            }
            else
            {
                dao.Delete(id);
                result = true;
                SetAlert("Xóa thành công", "success");
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpGet]
        public void getLoaiTK(long id)
        {
            var result = new UserDAO().GetLoaiTaiKhoanByID(id);
            ViewBag.LOAITAIKHOAN_ID = result.ToList();
        }
        //[HttpGet]
        //public ActionResult checkTaiKhoan(long id)
        //{
        //    return
        //}
        }

}