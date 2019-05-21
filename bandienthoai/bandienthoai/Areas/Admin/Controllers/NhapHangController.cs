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
    public class NhapHangController : BaseController
    {
        // GET: Admin/NhapHang
        public ActionResult QLNhaCungCap()
        {
            var dao = new NhapHangDAO();
            var model = dao.listNhaCungCap();
            return View(model);
        }
        [HttpGet]
        public JsonResult QLPhieuNhapHang()
        {
            var dao = new NhapHangDAO();
            var model = dao.listPhieuNhapHang();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPhieuNhapHang(int id)
        {
            var dao = new NhapHangDAO();
            var pn = dao.listPhieuNhapHang().Find(x => x.PHIEUNHAPHANG_ID.Equals(x));
            return Json(pn, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreatePhieuNhap(PHIEUNHAPHANG entity)
        {
            var dao = new NhapHangDAO();
            var pn = dao.InsertPhieuNhapHang(entity);
            return Json(new { msg = pn }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateNCC()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreatePhieuNhapHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNCC(NHACUNGCAP ncc)
        {
            if (ModelState.IsValid)
            {

                var dao = new NhapHangDAO();
                decimal id = dao.InsertNCC(ncc);
                if (id > 0)
                {
                    SetAlert("Thêm Thành công", "success");
                    return RedirectToAction("QLNhaCungCap", "NhapHang");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công!");
                    SetAlert("Thêm Thành công", "warning");
                }
            }
            return View("QLNhaCungCap");

        }

        [HttpGet]
        public ActionResult EditNCC(int id)
        {
            var user = new NhapHangDAO().ViewDetail(id);

            return View(user);
        }
        [HttpGet]
        public ActionResult EditPhieuNhapHang(int id)
        {
            var user = new NhapHangDAO().ViewDetail_PhieuNhapHang(id);

            return View(user);
        }
        [HttpPost]
        public ActionResult EditNCC(NHACUNGCAP ncc)
        {
            if (ModelState.IsValid)
            {
                var dao = new NhapHangDAO();

                var id = dao.UpdateNCC(ncc);
                if (id)
                {
                    SetAlert("Sửa thành công", "success");
                    return RedirectToAction("QLNhaCungCap", "NhapHang");
                }
                else
                {
                    SetAlert("Sửa không thành công", "warning");
                    ModelState.AddModelError("", "Cập nhật không thành Công!");
                }
            }
            return View("QLNhaCungCap");
        }
        [HttpPost]
        public ActionResult EditPhieuNhapHang(PHIEUNHAPHANG pn)
        {
            if (ModelState.IsValid)
            {
                var dao = new NhapHangDAO();

                var id = dao.UpdatePhieuNhapHang(pn);
                if (id)
                {
                    SetAlert("Sửa thành công", "success");
                    return RedirectToAction("QLPhieuNhapHang", "NhapHang");
                }
                else
                {
                    SetAlert("Sửa không thành công", "warning");
                    ModelState.AddModelError("", "Cập nhật không thành Công!");
                }
            }
            return View("QLPhieuNhapHang");

        }
        [HttpDelete]
        public ActionResult DeleteNCC(int id)
        {
            new NhapHangDAO().DeleteNhaCungCap(id);
            SetAlert("Sửa không thành công", "warning");
            return RedirectToAction("QLNhaCungCap");
        }
        [HttpDelete]
        public ActionResult DeletePhieuNhapHang(int id)
        {
            new NhapHangDAO().DeletePhieuNhapHang(id);
            SetAlert("Sửa không thành công", "warning");
            return RedirectToAction("QLPhieuNhapHang");
        }
    }
}