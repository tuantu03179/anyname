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
    public class SanPhamDAO
    {
        QlBanHangDbContext db = null;
        public SanPhamDAO()
        {
            db = new QlBanHangDbContext();
        }
        public bool ChangeStatus(decimal id)
        {
            var user = db.SANPHAMs.Find(id);
            user.TRANGTHAI = !user.TRANGTHAI;
            db.SaveChanges();
            return user.TRANGTHAI;
        }
      
        public List<SanPhamModel> GetListProduct(long idncc, long idloaisp)
        {

            var res = (from t in db.SANPHAMs
                       join l in db.LOAISANPHAMs on t.LOAISANPHAM_ID equals l.LOAISANPHAM_ID
                       join nc in db.NHACUNGCAPs on t.ID_NCC equals nc.ID_NCC
                       join sx in db.NHASANXUATs on t.ID_NCC equals sx.ID_NSX
                       where t.ID_NCC==idncc && t.LOAISANPHAM_ID==idloaisp
                       select new
                       {
                           idnsx = sx.ID_NSX,
                           Loaisp = l.TEN_LOAISANPHAM,
                           Masp = t.MA_SANPHAM,
                           Id = t.SANPHAM_ID,
                           SLTon = t.SOLUONGTON,
                           Tontoithieu = t.TONTOITHIEU,
                           SPkemtheo = t.SPDIKEM,
                           Ncc = nc.TEN_NCC,
                           maloai = l.LOAISANPHAM_ID,
                           idncc = nc.ID_NCC,

                           Tensp = t.TEN_SANPHAM,
                           Mota = t.MOTA_SANPHAM,
                           Giasp = t.GIA_SANPHAM,
                           Luotxem = t.LUOTXEM,
                           Trangthai = t.TRANGTHAI,
                           Khuyenmai = t.KHUYENMAI,
                           Hinhanh = t.HINHANH_SANPHAM

                       }).ToList();
            List<SanPhamModel> tk = new List<SanPhamModel>();

            foreach (var item in res)
            {
                SanPhamModel m = new SanPhamModel();
                m.ID = item.Id;
                m.LOAISANPHAM = item.Loaisp;
                m.TENSANPHAM = item.Tensp;
                m.NCC = item.Ncc;
                m.IDLOAISP = item.maloai;
                m.IDNCC = item.idncc;
                m.MOTA = item.Mota;
                m.GIASANPHAM = item.Giasp;
                m.TRANGTHAI = item.Trangthai;
                m.HINHANH = item.Hinhanh;
                m.MASANPHAM = item.Masp;

                m.SANPHAMKEMTHEO = item.SPkemtheo;
                m.SOLUONGTON = item.SLTon;
                m.TONTOITHIEU = item.Tontoithieu;
                m.LUOTXEM = item.Luotxem;
                m.KHUYENMAI = item.Khuyenmai;
                tk.Add(m);
            }
            return tk;
        }
        public List<SanPhamModel> GetListProduct()
        {

            var res = (from t in db.SANPHAMs
                       join l in db.LOAISANPHAMs on t.LOAISANPHAM_ID equals l.LOAISANPHAM_ID
                       join nc in db.NHACUNGCAPs on t.ID_NCC equals nc.ID_NCC
                       join sx in db.NHASANXUATs on t.ID_NCC equals sx.ID_NSX
                       select new
                       {
                           idnsx = sx.ID_NSX,
                           Loaisp = l.TEN_LOAISANPHAM,
                           Masp = t.MA_SANPHAM,
                           Id = t.SANPHAM_ID,
                           SLTon = t.SOLUONGTON,
                           Tontoithieu = t.TONTOITHIEU,
                           SPkemtheo = t.SPDIKEM,
                           Ncc = nc.TEN_NCC,
                           maloai = l.LOAISANPHAM_ID,
                           idncc=nc.ID_NCC,
                         
                           Tensp = t.TEN_SANPHAM,
                          Mota = t.MOTA_SANPHAM,
                           Giasp = t.GIA_SANPHAM,
                           Luotxem=t.LUOTXEM,
                           Trangthai = t.TRANGTHAI,
                           Khuyenmai=t.KHUYENMAI,
                           Hinhanh = t.HINHANH_SANPHAM

                       }).ToList();
            List<SanPhamModel> tk = new List<SanPhamModel>();

            foreach (var item in res)
            {
                SanPhamModel m = new SanPhamModel();
                m.ID = item.Id;
                m.LOAISANPHAM = item.Loaisp;
                m.TENSANPHAM = item.Tensp;
                m.NCC = item.Ncc;
                m.IDLOAISP = item.maloai;
                m.IDNCC = item.idncc;
                m.MOTA = item.Mota;
                m.GIASANPHAM = item.Giasp;
                m.TRANGTHAI = item.Trangthai;
                m.HINHANH = item.Hinhanh;
                m.MASANPHAM = item.Masp;
             
                m.SANPHAMKEMTHEO = item.SPkemtheo;
                m.SOLUONGTON = item.SLTon;
                m.TONTOITHIEU = item.Tontoithieu;
                m.LUOTXEM = item.Luotxem;
                m.KHUYENMAI = item.Khuyenmai;
                tk.Add(m);
            }
            return tk;
        }
        public SANPHAM GetByID(int id)
        {
            return db.SANPHAMs.SingleOrDefault(x => x.SANPHAM_ID == id);
        }
        public SANPHAM GetByTitle(string tieude)
        {
            return db.SANPHAMs.SingleOrDefault(t => t.TEN_SANPHAM == tieude);
        }
        public List<LOAISANPHAM> getAllTypeProduct()
        {
            return db.LOAISANPHAMs.ToList();
        }
        public String getProductbyID(long id)
        {
            return db.LOAISANPHAMs.SingleOrDefault(x => x.LOAISANPHAM_ID == id).TEN_LOAISANPHAM;
        }
        public bool updatenoidung(SANPHAM tt)
        {
            try
            {
                var user = db.SANPHAMs.Find(tt.SANPHAM_ID);
                user.GHICHU_SANPHAM = tt.GHICHU_SANPHAM;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public long getIDLastChild()
        {
            long kq = -1;
            try
            {
                
               var i= db.SANPHAMs.Count();
                if (i > 0)
                {
                    List<SANPHAM> result = db.SANPHAMs.OrderBy(t => t.SANPHAM_ID).Skip(db.SANPHAMs.Count() - 1).Take(1).ToList();
                   kq =  result[0].SANPHAM_ID;
                }
            }

            catch (Exception ex)
            {
                kq = -1;
            }
            return kq;
        }
        public decimal Insert(SANPHAM tt)
        {

            db.SANPHAMs.Add(tt);
            db.SaveChanges();
            return tt.SANPHAM_ID;

        }
        public void DeleteFile(string fileName)
        {
            try
            {
                FileInfo fi;
                if (System.IO.File.Exists(fileName) == true)
                {
                    fi = new FileInfo(fileName);
                    fi.Delete();

                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }
        public bool Delete(int id)
        {
            try
            {
                var t = db.SANPHAMs.Find(id);
                db.SANPHAMs.Remove(t);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public decimal InsertSanPham(SANPHAM sp)
        {
            db.SANPHAMs.Add(sp);
            db.SaveChanges();
            return sp.SANPHAM_ID;
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
                phieunhap.NGUOIGIAO = pn.NGUOIGIAO;
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