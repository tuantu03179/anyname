using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bandienthoai
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
              name: "Product Category",
              url: "san-pham/{GHICHU_LOAISANPHAM}-{id}",
              defaults: new { controller = "SanPham", action = "Category", id = UrlParameter.Optional }

          );
            routes.MapRoute(
            name: "Product Detail",
            url: "chi-tiet/{GHICHU_SANPHAM}-{id}",
            defaults: new { controller = "SanPham", action = "Detail", id = UrlParameter.Optional }

        );
            routes.MapRoute(
           name: "About",
           url: "gioi-thieu",
           defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }

       );
            routes.MapRoute(
        name: "Add Cart",
        url: "them-gio-hang",
        defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }

    );
            routes.MapRoute(
      name: "Cart",
      url: "gio-hang",
      defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }

  );
            routes.MapRoute(
name: "Payment",
url: "thanh-toan",
defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }

);
            routes.MapRoute(
name: "Hoàn Thành",
url: "hoan-thanh",
defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }

);
            routes.MapRoute(
name: "Liên hệ",
url: "lien-he",
defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }

);
            routes.MapRoute(
name: "Registers",
url: "dang-ky-thanh-vien",
defaults: new { controller = "UserClient", action = "Register", id = UrlParameter.Optional }

);
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional }

           );

        }
    }
}
