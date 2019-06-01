using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bandienthoai.Areas.Admin.Models.EF;
using bandienthoai.Common;
using PagedList;

namespace bandienthoai.Areas.Admin.Models.DAO
{
    public class NhapHangDAO
    {
        QlBanHangDbContext db = null;
        //Khởi tạo tham số
        public NhapHangDAO()
        {
            db = new QlBanHangDbContext();
        }
      
        public decimal InsertNCC(NHACUNGCAP ncc)
        {
            db.NHACUNGCAPs.Add(ncc);
            db.SaveChanges();
            return ncc.ID_NCC;
        }
        public decimal InsertPhieuNhapHang(PHIEUNHAPHANG pn)
        {
            db.PHIEUNHAPHANGs.Add(pn);
            db.SaveChanges();
            return pn.PHIEUNHAPHANG_ID;
        }
        public bool UpdateNCC(NHACUNGCAP ncc)
        {
            try
            {
                var nhacc = db.NHACUNGCAPs.Find(ncc.ID_NCC);
                nhacc.TEN_NCC = ncc.TEN_NCC;
                nhacc.DIACHI_NCC = ncc.DIACHI_NCC;
                nhacc.EMAIL_NCC = ncc.EMAIL_NCC;
                nhacc.SDT_NCC = ncc.SDT_NCC;
                nhacc.IS_DELETE = ncc.IS_DELETE;
                nhacc.MODIFILEDBY = ncc.MODIFILEDBY;
                nhacc.MODIFILEDDATE = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdatePhieuNhapHang(PHIEUNHAPHANG pn)
        {
            try
            {
                var phieunhap = db.PHIEUNHAPHANGs.Find(pn.PHIEUNHAPHANG_ID);
                phieunhap.SOPHIEUNHAPHANG = pn.SOPHIEUNHAPHANG;
                phieunhap.MA_NCC = pn.MA_NCC;
                phieunhap.NGAYGIAO = pn.NGAYGIAO;
                phieunhap.TRANGTHAINHAPHANG = pn.TRANGTHAINHAPHANG;
                phieunhap.MODIFILEDBY = pn.MODIFILEDBY;
                phieunhap.MODIFILEDDATE = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteNhaCungCap(int id)
        {
            try
            {
                var ncc = db.NHACUNGCAPs.Find(id);
                db.NHACUNGCAPs.Remove(ncc);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool DeletePhieuNhapHang(int id)
        {
            try
            {
                var pn = db.PHIEUNHAPHANGs.Find(id);
                db.PHIEUNHAPHANGs.Remove(pn);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<NHACUNGCAP> listNhaCungCap()
        {
            return db.NHACUNGCAPs.ToList();
        }
        public List<PHIEUNHAPHANG> listPhieuNhapHang()
        {
            return db.PHIEUNHAPHANGs.ToList();
        }
        public NHACUNGCAP ViewDetail(int id)
        {
            return db.NHACUNGCAPs.Find(id);
        }
        public PHIEUNHAPHANG ViewDetail_PhieuNhapHang(int id)
        {
            return db.PHIEUNHAPHANGs.Find(id);
        }
    }
}