using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using bandienthoai.Areas.Admin.Models.EF;
using bandienthoai.Common;
using PagedList;

namespace bandienthoai.Areas.Admin.Models.DAO
{
    public class NhaSXDAO
    {
        QlBanHangDbContext db = null;
        public NhaSXDAO()
        {
            db = new QlBanHangDbContext();
        }
        public List<NHASANXUAT> getAllNSX()
        {
            return db.NHASANXUATs.ToList();
        }
    }
}