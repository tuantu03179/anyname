using bandienthoai.Areas.Admin.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bandienthoai.Areas.Admin.Models.DAO
{
    public class DonHangDAO
    {
        QlBanHangDbContext db = null;
        public DonHangDAO()
        {
            db = new QlBanHangDbContext();
        }
        public List<HOADON> getAllHoaDon()
        {
            return db.HOADONs.ToList();
        }
        public List<DonHangModel> GetListDonHang()
        {
            var kq = (from t in db.HOADONs
                      join u in db.TAIKHOANs on t.IDTOAIKHOAN equals u.ID
                      join k in db.KHACHHANGs on t.IDKHACHHANG equals k.IDKHACHHANG
                      select new DonHangModel
                      {
                          HOADON_ID = t.HOADON_ID,
                          IDKHACHHANG = k.IDKHACHHANG,
                          HOTEN = k.HOTEN,
                          SDT = k.SDT,
                          GMAIL = k.GMAIL,
                          IDTOAIKHOAN = t.IDTOAIKHOAN,
                          TENTAIKHOAN = u.TENTAIKHOAN,
                          HINHTHUCTHANHTOAN = t.HINHTHUCTHANHTOAN,
                          NGAYBDGIAO = t.NGAYBDGIAO,
                          NGAYKTGIAO = t.NGAYKTGIAO,
                          NGAYXUATHD = t.NGAYXUATHD,
                          TRANGTHAI = t.TRANGTHAI,
                          IS_DELETE = t.IS_DELETE,
                          CREATEBY = t.CREATEBY,
                          MODIFILEDBY = t.MODIFILEDBY,
                          MODIFILEDDATE = t.MODIFILEDDATE,
                          CREATEDATE = t.CREATEDATE,


                      }).ToList();
            return kq;
        }


        public int ChangeStatus(int id, string name, string stt)
        {

            try
            {
                var kq = db.HOADONs.Find(id);
                kq.TRANGTHAI = stt;
                kq.MODIFILEDBY = name;
                kq.MODIFILEDDATE = DateTime.Now;

                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool Delete(decimal id)
        {
            try
            {
                var user = db.HOADONs.Find(id);
                db.HOADONs.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}