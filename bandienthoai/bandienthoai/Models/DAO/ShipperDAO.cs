using bandienthoai.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bandienthoai.Models.DAO
{
    public class ShipperDAO
    {
        QlBanHangDbContext db = null;
        public ShipperDAO()
        {
            db = new QlBanHangDbContext();
        }
       public List<ORDER> GetListOrder(int st ,string key=null)
        {
            return db.ORDERs.Where(x => x.STATUS == st).ToList();
        }

        public ORDER GetDetailOrderbyID(int id)
        {
            return db.ORDERs.Find(id);
        }
    }
}