using bandienthoai.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bandienthoai.Areas.Admin.Models.DAO
{
    public class BannerDAO
    {
        QlBanHangDbContext db = null;
        public BannerDAO()
        {
            db = new QlBanHangDbContext();
        }
        public List<BannerModel> GetListBanner()
        {
        
            var kq = db.BANNERs.Select(t => new BannerModel
            {
                BANNER_ID = t.BANNER_ID,
                LINK = t.LINK,
                GHICHU_BANNER = t.GHICHU_BANNER,
                VITRI = t.VITRI,
                HINH = t.HINH,
                TIEUDE = t.TIEUDE,
                TENTK = t.TENTK,
                IS_DELETE = t.IS_DELETE,
                CREATEBY = t.CREATEBY,
                MODIFILEDBY = t.MODIFILEDBY,
                MODIFILEDDATE = t.MODIFILEDDATE,
                CREATEDATE = t.CREATEDATE,


            }).ToList();
            return kq;
        }
        //lưu dữ liệu
        public int SaveData(BannerModel x, string user)
        {
            try
            {
                if (x.BANNER_ID > 0)
                {

                    BANNER dao = db.BANNERs.SingleOrDefault(m => m.BANNER_ID == x.BANNER_ID);
                    dao.HINH = x.HINH;
                    dao.IS_DELETE = false;
                    dao.MODIFILEDDATE = DateTime.Now.Date;
                    dao.MODIFILEDBY = user;
                    dao.LINK = x.LINK;
                    dao.GHICHU_BANNER = x.GHICHU_BANNER;
                    dao.TENTK = x.TENTK;
                    dao.TIEUDE = x.TIEUDE;
                    dao.VITRI = x.VITRI;
                    db.SaveChanges();

                    return 1;

                }
                else
                {
                    BANNER dao = new BANNER();

                    dao.HINH = x.HINH;
                    dao.IS_DELETE = false;
                    dao.MODIFILEDDATE = DateTime.Now.Date;
                    dao.MODIFILEDBY = user;
                    dao.TENTK = x.TENTK;
                    dao.TIEUDE = x.TIEUDE;
                    dao.VITRI = x.VITRI;
                    dao.LINK = x.LINK;
                    dao.GHICHU_BANNER = x.GHICHU_BANNER;

                    dao.CREATEDATE = DateTime.Now.Date;
                    dao.CREATEBY = user;

                    db.BANNERs.Add(dao);
                    db.SaveChanges();

                    return 2;
                }

            }
            catch
            {
                return 0;
            }

        }

    }
}