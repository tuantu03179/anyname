using bandienthoai.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bandienthoai.Models.DAO
{
    public class OrderDAO
    {
        QlBanHangDbContext db = null;
        public OrderDAO()
        {
            db = new QlBanHangDbContext();
        }
        public long Insert(ORDER order)
        {
            db.ORDERs.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}