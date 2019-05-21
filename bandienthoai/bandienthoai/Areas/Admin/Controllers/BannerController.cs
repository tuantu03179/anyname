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
    public class BannerController : BaseController
    {
        // GET: Admin/Banner
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListBanner()
        {
            var dao = new BannerDAO();
            var result = dao.GetListBanner();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveData(BannerModel sp)
        {
            int result = 0;
            bool kq = false;
            if (ModelState.IsValid)
            {
                var dao = new BannerDAO();
                var user = ((UserLogin)Session[CommonStants.USER_SESSION]);
                sp.TENTK = user.userID;
                result = dao.SaveData(sp, user.userName);
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
                    SetAlert("Thất Công!", "warning");

            }
            return Json(kq, JsonRequestBehavior.AllowGet);

        }
    }
}