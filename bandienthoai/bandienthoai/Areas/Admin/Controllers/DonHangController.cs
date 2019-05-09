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
    public class DonHangController : BaseController
    {
        // GET: Admin/DonHang
        public ActionResult Index()
        {
            var dao = new DonHangDAO();
            getStatus();
            var x = ((UserLogin)Session[CommonStants.USER_SESSION]).userID;
            var user = new UserDAO().ViewDetail(x);
            ViewBag.typeLoai = getTypeUserView(user);
            var model = dao.GetListDonHang();
            return View(model);
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
        public JsonResult GetListDonHang()
        {
            var dao = new DonHangDAO();
            var result = dao.GetListDonHang();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public void getStatus(long? selectedId = null)
        {
            var dao = new DonHangDAO();
            ViewBag.TRANGTHAI = new SelectList(dao.getAllHoaDon(), "TRANGTHAI", "TRANGTHAI", selectedId);
        }
        // set chờ duyệt
        public int changeChoDuyet(int id)
        {
            var user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
            var dao = new DonHangDAO();
            var kq = dao.ChangeStatus(id, user, "Chờ Duyệt");
            if (kq == 1)
                return 1;
            else
                return 0;
        }
        public int changeDaDuyet(int id)
        {
            var user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
            var dao = new DonHangDAO();
            var kq = dao.ChangeStatus(id, user, "Đã Liên Hệ");
            if (kq == 1)
                return 2;
            else
                return 0;
        }
        public int changeThanhToan(int id)
        {
            var user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
            var dao = new DonHangDAO();
            var kq = dao.ChangeStatus(id, user, "Đã Thanh Toán");
            if (kq == 1)
                return 3;
            else
                return 0;
          
        }
        public int changeGiaoHang(int id)
        {
            var user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
            var dao = new DonHangDAO();
            var kq = dao.ChangeStatus(id, user, "Đã Liên Hệ");
            if (kq == 1)
                return 4;
            else
                return 0;
        }
        public JsonResult Delete(int id)
        {
            var dao = new DonHangDAO();
        
            var result = false;


            result = dao.Delete(id);
            if(result)
            {
                SetAlert("Xóa thành công", "success");
            }
    else
            {
                ModelState.AddModelError("", "Xóa Thất Bại!");
            }
        
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}