using bandienthoai.Models;
using bandienthoai.Models.DAO;
using bandienthoai.Models.EF;
using Commom;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace bandienthoai.Controllers
{
    public class CartController : Controller
    {
        private string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];

            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
                return View(list);
        }
        //thanh toán
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];

            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string email, string address)
        {
            decimal total = 0;
            var order = new ORDER();
            order.CREATEDATE = DateTime.Now;
            order.SHIPDDRESS = address;
            order.SHIPNAME = shipName;
            order.SHIPMOBILE = mobile;
            order.SHIPEMAIL = email;
            try
            {
                var id = new OrderDAO().Insert(order);
                var sessionCart = (List<CartItem>)Session[CartSession];
                var orderDetailDAO = new OrderDetailDAO();
                foreach (var item in sessionCart)
                {
                    var orderDetail = new ORDERDETAIL();
                    orderDetail.PRODUCTID = item.Product.SANPHAM_ID;
                    orderDetail.ORDERID = id;
                    orderDetail.ORDERID = id;
                    orderDetail.PRICE = (long)item.Product.GIA_SANPHAM;
                    orderDetail.QUANTITY = item.Quantity;
                    orderDetailDAO.Insert(orderDetail);
                    total += (item.Product.GIA_SANPHAM * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/teamplet/NewOrder.html"));
                content = content.Replace("{{customereName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);

                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));


                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"];
                new MailHelper().SenMail(toEmail, "Đơn hàng mới từ Shop", content);

                new MailHelper().SenMail(email,"Đơn hàng mới từ Shop", content);
            }
            catch (Exception e)
            {
                return Redirect("/loi-thanh-toan");

            }
            return Redirect("/hoan-thanh");
        }
        //hoan thành
        public ActionResult Success()
        {
            return View();
        }
        // xóa gio hang
        public JsonResult DeleteAll()
        {
           
            Session[CartSession] = null;
            
            return Json(new
            {
                status = true
            });
        }
        // xóa từng sp gio hang
        public JsonResult Delete(long id)
        {

            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.SANPHAM_ID == id);
            Session[CartSession]=sessionCart;
            return Json(new
            {
                status = true
            });
        }
        // update gio hang
        public JsonResult  Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.SANPHAM_ID == item.Product.SANPHAM_ID);
                if (jsonItem!=null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long ProductId, int quantity)
        {
            var cart = Session[CartSession];
            var product = new SanPhamDAO().ViewDetail(ProductId);
            if (cart!=null)
            {
                var list =  (List<CartItem>)cart;
                if (list.Exists(x=>x.Product.SANPHAM_ID==ProductId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.SANPHAM_ID == ProductId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tao doi tuong cartitem
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    //them vào list

                    list.Add(item);
                }
              
            }
            else
            {
             
                //tao doi tuong cartitem
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                // gans vao sesion
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}