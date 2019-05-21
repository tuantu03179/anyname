using bandienthoai.Models.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bandienthoai.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var dao = new SanPhamDAO();
            var kq = dao.GetListKhuyenMai();
          
            return View(kq);
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new CategoryDAO().ListAll();
            return PartialView(model);
        }
        public ActionResult Category(long id, int page=1, int PageSize=1)
        {
            var dao = new CategoryDAO().ViewDetail(id);
            ViewBag.Category = dao;
            int TotalRecord = 0;
            var model = new SanPhamDAO().GetListProductById(id,ref TotalRecord, page, PageSize);
     
            ViewBag.page = page;
            ViewBag.Total = TotalRecord;
            int MaxPage = 5;
            int TotalPage = 0;
            TotalPage =(int)Math.Ceiling((double)(TotalRecord / PageSize));
            ViewBag.TotalPage = TotalPage;
            ViewBag.MaxPage = MaxPage;
            ViewBag.First = 1;
            ViewBag.Last = TotalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev= page - 1;
            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var dao = new SanPhamDAO().ViewDetail(id);
            ViewBag.Category = new CategoryDAO().ViewDetail(dao.LOAISANPHAM_ID);
            ViewBag.RelatedProduct = new SanPhamDAO().GetListRelatedProducts(id);
            return View(dao);
        }
    }
}