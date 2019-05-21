
using bandienthoai.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bandienthoai.Models.DAO
{
    public class BannerDAO
    {
        QlBanHangDbContext db = null;
        public BannerDAO()
        {
            db = new QlBanHangDbContext();
        }
        public List<SLIDE> GetListBanner(int index)
        {
            return db.SLIDEs.Where(x => x.STATUS == 1).OrderBy(y => y.DISPLAYORDER).ToList();
            //var kq = db.BANNERs.Where(t=>t.VITRI==index).Select(t => new BannerModel
            //{
            //    BANNER_ID = t.BANNER_ID,
            //    LINK = t.LINK,
            //    GHICHU_BANNER = t.GHICHU_BANNER,
            //    VITRI = t.VITRI,
            //    HINH = t.HINH,
            //    TIEUDE = t.TIEUDE,
            //    TENTK = t.TENTK,
            //    IS_DELETE = t.IS_DELETE,
            //    CREATEBY = t.CREATEBY,
            //    MODIFILEDBY = t.MODIFILEDBY,
            //    MODIFILEDDATE = t.MODIFILEDDATE,
            //    CREATEDATE = t.CREATEDATE,


            //}).ToList();
           
        }
    }
}