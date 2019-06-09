﻿
using bandienthoai.Common;
using bandienthoai.Models;
using bandienthoai.Models.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bandienthoai.Controllers
{
    public class TrangChuController : Controller
    {

        // GET: TrangChu
        public ActionResult Index()
        {
            var dao = new SanPhamDAO();
       
            ViewBag.KhuyenMais = dao.GetListKhuyenMai();
            ViewBag.NewProducts = dao.GetListNew(20);
            GetListBannerGiua();
            return View();
        }
       
        public ActionResult TopBar()
        {
            
            return PartialView();
        }
        [ChildActionOnly]
        
        public ActionResult MainMenu()
        {
           var logo = new BannerDAO().GetLogo();
            if(logo==null)
                ViewBag.logo = "\\Data\\slides\\logo-mobile.png";
            else
                ViewBag.logo = logo.HINH;

            var dao = new MenuDAO().ListByGroupId(1);
            return PartialView(dao);
        }
        
            public PartialViewResult HeaderCart()
        {

            var cart = Session[CommonStants.CartSession];

            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
      public JsonResult GetCart()
        {
            var cart = Session[CommonStants.CartSession];

            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return Json(new
            {
               data=list,
                status=true
            });
        }
        public ActionResult Footer()
        {
            var dao = new FooterDAO().GetFooter();
            
            return PartialView(dao);
        }
        public void GetListBannerGiua()
        {

            var dao = new BannerDAO();
            ViewBag.Banner = dao.GetListBanner(1);
        }
        // đọc ghi dữ liệu in foder
        //public string Edit()
        //{
        //    //  var txtContent = Request.Unvalidated.Form.Get("txtContent");

        //    var kq = "";
        //    string exePath = Server.MapPath("\\Data\\Footer\\" + "ft" + 2 + ".cshtml");
        //    FileStream fs = new FileStream(exePath, FileMode.Open);
        //    using (StreamReader sw = new StreamReader(fs))
        //    {

        //        kq= sw.ReadToEnd();

        //    }
        //    fs.Close();
        //    return kq;
        //}
        //public JsonResult Footer()
        //{
        //    string value = Edit();
        //    return Json(value, JsonRequestBehavior.AllowGet);
        //}
        //
        public ActionResult KhuyenMai()
        {
            var dao = new SanPhamDAO();
            var kq = dao.GetListKhuyenMai();
            return View(kq);
        }
    }
}