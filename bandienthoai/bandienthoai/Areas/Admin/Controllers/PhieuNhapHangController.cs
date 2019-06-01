using bandienthoai.Areas.Admin.Models;
using bandienthoai.Areas.Admin.Models.DAO;
using bandienthoai.Areas.Admin.Models.EF;
using bandienthoai.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bandienthoai.Areas.Admin.Controllers
{
    public class PhieuNhapHangController : BaseController
    {
        // GET: Admin/PhieuNhapHang
        public ActionResult Index()
        {
            var x = ((UserLogin)Session[CommonStants.USER_SESSION]).userID;
            var user = new UserDAO().ViewDetail(x);
            ViewBag.typeLoai = getTypeUserView(user);
            return View();
        }
        //get type user
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
        //get list PhieuNhap
        public JsonResult GetListPhieuNhap()
        {
            var dao = new PhieuNhapHangDAO();
            var result = dao.getAllPhieuNhap();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //get list PhieuNhap by id
        public JsonResult GetListPhieuNhapByID(int id)
        {
            var dao = new PhieuNhapHangDAO();
            var result = dao.GetListPhieuNhapByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //delete
        public JsonResult Delete(int id)
        {
            var dao = new PhieuNhapHangDAO();
            bool value = dao.Delete(id);
            if (value)
                SetAlert("Xóa thành công", "success");
            else
                SetAlert("Xóa thất bại", "success");

            return Json(value, JsonRequestBehavior.AllowGet);
        }
        //Ẩn hiện table
        public int changeAn(int id)
        {
            var user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
            var dao = new PhieuNhapHangDAO();
            var kq = dao.ChangeStatus(id, user, true);
            return kq;
        }
        public int changeHien(int id)
        {
            var user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
            var dao = new PhieuNhapHangDAO();
            var kq = dao.ChangeStatus(id, user, false);
            return kq;
        }
        public int changeAnHien(int id)
        {
            var user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
            var dao = new PhieuNhapHangDAO();
            var type = dao.GetById(id);

            var kq = dao.ChangeStatus(id, user, !(bool)type.IS_DELETE);
            return kq;
        }
        //save data
        [HttpPost]
        public ActionResult SaveData(PhieuNhapHangModel sp)
        {
            int result = 0;
            bool kq = false;
            if (ModelState.IsValid)
            {
                var dao = new PhieuNhapHangDAO();
                string user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;

                result = dao.SaveData(sp, user);
                if (result == 1)
                {
                    SetAlert("Cập Nhật Thành Công!", "success");
                    kq = true;
                }
                else if (result == 2)
                {
                    SetAlert("Thêm Thành Công!", "success");
                    kq = true;
                }
                else
                    ModelState.AddModelError("", "Thất bại!");

            }
            return Json(kq, JsonRequestBehavior.AllowGet);

        }
    }
}