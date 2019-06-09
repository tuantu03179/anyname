using bandienthoai.Areas.Admin.Models.DAO;
using bandienthoai.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using bandienthoai.Common;
using System.Web.Script.Serialization;

namespace bandienthoai.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            getallTypeUser();
            var dao = new UserDAO();
            var x = ((UserLogin)Session[CommonStants.USER_SESSION]).userID;
            var user = new UserDAO().ViewDetail(x);
            //ViewBag.typeLoai = getTypeUserView(user);
            ViewBag.IDUser = x;
            //var model = dao.listuser(page, pageSize);
            var model = dao.GetLoaiTaiKhoan();
            return View(model);
        }
        public void getallTypeUser(long? selectedId = null)
        {
            var dao = new LoaiTaiKhoanDAO();
            ViewBag.IDLOAITAIKHOAN = new SelectList(dao.GetListLoaiTK(), "IDLOAITAIKHOAN", "TENLOAITAIKHOAN", selectedId);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            getTypeUser();
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(TAIKHOAN user)
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
                    SetAlert("Thêm Thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công!");
                }
            }
            return View("Index");

        }
        //Phân Quyền
        [HttpPost]
        public JsonResult AddRole(string id, string list)
        {
            if (ModelState.IsValid)
            {
                var jsonList = new JavaScriptSerializer().Deserialize<List<String>>(list);
                var dao = new UserDAO();
             bool kq= dao.InsertRole(id, jsonList);
                var model = dao.getAllTypeUser();
            }

           return Json(new
            {
               
                status = true

            });
        }
        [HttpGet]
        public ActionResult PhanQuyen(long? selectedId = null)
        {
            var dao = new UserDAO();
            var model = dao.getAllTypeUser();
            ViewBag.ListTypeUser = new SelectList(model, "ID", "TENLOAITK", selectedId);
            ViewBag.ListRole = dao.GetListAllRole();
            return View();
        }
        public void GetListRole(string selectedId="")
        {
            var dao = new UserDAO();
            ViewBag.ListRole = dao.GetListRole(selectedId);
        }
        public void getTypeUser(long? selectedId = null)
        {
            var dao = new UserDAO();
            var result = dao.getAllTypeUser();

            ViewBag.LOAITAIKHOAN_ID = new SelectList(result, "LOAITAIKHOAN_ID", "TENLOAITK", selectedId);
        }
        //Get ROle theo id
        public JsonResult GetRole(string id)
        {
            var list = new UserDAO().GetRoleById(id);
           
            return Json(new
            {
                data = list,
                status = true

            });
        }
        //public int getTypeUserView(TAIKHOAN user)
        //{
        //   // var typeUser = new LoaiTaiKhoanDAO().GetTypeUserByID(user.LOAITAIKHOAN_ID);
        //    if (typeUser.TENLOAITK.ToLower() == "admin")
        //    {
        //        return 1;
        //    }
        //    else
        //        return 2;
        //}
        [HttpGet]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            getTypeUser();
            var user = new UserDAO().ViewDetail(id);
            user.MATKHAU = Encryptor.Decrypt(user.MATKHAU);
          //  ViewBag.typeLoai = getTypeUserView(user);
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(TAIKHOAN user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var tmp = dao.GetByID(user.ID);
                if (tmp.MATKHAU != Encryptor.MD5Hash(user.MATKHAU))
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
                    user.MATKHAU = Encryptor.MD5Hash(user.MATKHAU);
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
        [HasCredential(RoleID = "DELETE_USER")]
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
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpGet]
        [HasCredential(RoleID = "VIEW_USER")]
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