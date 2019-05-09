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
    public class LoaiTaiKhoanController : BaseController
    {
        // GET: Admin/LoaiTaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListLoaiTaikhoan()
        {
            var dao = new LoaiTaiKhoanDAO();
            var result = dao.GetListLoaiTK();
           
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListLoaiTaikhoanByID(long ID)
        {
            var value = new LoaiTaiKhoanDAO().GetListLoaiTaikhoanByID(ID);
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Delete(int id)
        {
            var dao = new LoaiTaiKhoanDAO();
            bool value = dao.Delete(id);
          
            if(value)
            SetAlert("Xóa thành công", "success");
            else
            SetAlert("Xóa thất bại", "success");
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveData(LoaitaiKhoanModel sp)
        {
            int result = 0;
            bool kq = false;
            if (ModelState.IsValid)
            {
                var dao = new LoaiTaiKhoanDAO();
                string user = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;

                result = dao.SaveData(sp, user);
                if (result == 1)
                {
                 
                    kq = true;
                }
                else if (result == 2)
                {
                  
                    kq = true;
                }
               
            }
            return Json(kq, JsonRequestBehavior.AllowGet);

        }
    }
}