using bandienthoai.Areas.Admin.Models.DAO;
using bandienthoai.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bandienthoai.Common;
using System.IO;
using System.Text;

namespace bandienthoai.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product

        public ActionResult Index(string searching)
        {
            getNCC();
            getTypeProduct();
            var dao = new SanPhamDAO();
            var model = dao.GetListProduct();

            return View(model);
        }
        [HttpGet]
        public JsonResult DSSanPham()
        {
            var dao = new SanPhamDAO();
            var model = dao.GetListProduct();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public void getTypeProduct(long? selectedId = null)
        {
            var dao = new SanPhamDAO();
            ViewBag.LOAISANPHAM_ID = new SelectList(dao.getAllTypeProduct(), "LOAISANPHAM_ID", "TEN_LOAISANPHAM", selectedId);
        }
        public void getProductbyID(long selectedId)
        {
            var dao = new SanPhamDAO();
            ViewBag.LOAISANPHAM_ID = dao.getProductbyID(selectedId);
        }
        public void getNSX(long? selectedId = null)
        {
            var dao = new NhaSanXuatDAO();
            ViewBag.ID_NSX = new SelectList(dao.getAllNSX(), "ID_NSX", "TEN_NSX", selectedId);
        }
        public void getNCC(long? selectedId = null)
        {
            var dao = new NhaCungCapDAO();
            ViewBag.ID_NCC = new SelectList(dao.getAllNCC(), "ID_NCC", "TEN_NCC", selectedId);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new SanPhamDAO();
            var line = dao.GetByID(id);
            string link = Server.MapPath("/Data/Product/pro" + line.SANPHAM_ID + ".cshtml");
            dao.DeleteFile(link);
            dao.Delete(id);

            SetAlert("Xóa thành công", "success");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            getTypeProduct();
            getNSX();
            getNCC();
            return View();

        }
        [HttpGet]
        public ActionResult Filter()
        {
            var result = new SanPhamDAO().GetListProduct();

            return Json(new
            {
                status = result
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new SanPhamDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SANPHAM sp)
        {
            if (ModelState.IsValid)
            {

                var dao = new SanPhamDAO();
                var tk = dao.GetByTitle(sp.TEN_SANPHAM);

                if (tk != null)
                {
                    SetAlert("Sản phẩm đã  tồn tại", "fail");
                    return RedirectToAction("Index", "Product");
                }

                //string txtContent = ViewBag.GhiChu;

                var txtContent = Request.Unvalidated.Form.Get("txtContent");

                // encode the data

                // decode the data


                sp.CREATEBY = ((UserLogin)Session[CommonStants.USER_SESSION]).userName;
                sp.CREATEDATE = DateTime.Now;
                sp.LUOTXEM = 0;
                sp.NOIDUNG = txtContent;

                decimal id = dao.Insert(sp);

                if (id > 0)
                {
                    //long LastId = dao.getIDLastChild();
                    //var namenew = dao.getIDLastChild();
                    ////string exePath = System.AppContext.BaseDirectory + "\\Data\\Content\\" + namenew + ".html";
                    //string exePath = Server.MapPath("\\Data\\Product\\" + "pro" + namenew + ".cshtml");
                    //FileStream fs = new FileStream(exePath, FileMode.Create);
                    //using (StreamWriter sw = new StreamWriter(fs))
                    //{

                    //    foreach (var s in txtContent)
                    //    {
                    //        sw.Write(s);
                    //    }
                    //    sw.Flush();

                    //}
                    //fs.Close();
                    //sp.GHICHU_SANPHAM = "/Data/Product/" + namenew + ".html";
                    //dao.updatenoidung(sp);
                    SetAlert("Thêm Thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công!");
                }
            }
            return View("Index");

        }
        public FileStreamResult SaveData(string example)
        {
            //todo: add some data from your database into that string:
            var string_with_your_data = example;

            //Build your stream
            var byteArray = Encoding.ASCII.GetBytes(string_with_your_data);
            var stream = new MemoryStream(byteArray);

            //Returns a file that will match your value passed in (ie TestID2.txt)
            return File(stream, "text/plain", String.Format("{0}.cshtml", example));
        }
    }
}