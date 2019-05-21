
using bandienthoai.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bandienthoai.Models.DAO
{
    public class SanPhamDAO
    {
        QlBanHangDbContext db = null;
        public SanPhamDAO()
        {
            db = new QlBanHangDbContext();
        }
        //public List<SanPhamModel> GetListKhuyenMai()
        //{
        //    var res = (from t in db.SANPHAMs
        //               join l in db.LOAISANPHAMs on t.LOAISANPHAM_ID equals l.LOAISANPHAM_ID
        //               join nc in db.NHACUNGCAPs on t.ID_NCC equals nc.ID_NCC
        //               join sx in db.NHASANXUATs on t.ID_NCC equals sx.ID_NSX
        //    where t.KHUYENMAI>0
        //               select new
        //               {
        //                   idnsx = sx.ID_NSX,
        //                   Loaisp = l.TEN_LOAISANPHAM,
        //                   Masp = t.MA_SANPHAM,
        //                   Id = t.SANPHAM_ID,
        //                   SLTon = t.SOLUONGTON,
        //                   Tontoithieu = t.TONTOITHIEU,
        //                   SPkemtheo = t.SPDIKEM,
        //                   Ncc = nc.TEN_NCC,
        //                   maloai = l.LOAISANPHAM_ID,
        //                   idncc = nc.ID_NCC,

        //                   Tensp = t.TEN_SANPHAM,
        //                   Mota = t.MOTA_SANPHAM,
        //                   Giasp = t.GIA_SANPHAM,
        //                   Luotxem = t.LUOTXEM,
        //                   Trangthai = t.TRANGTHAI,
        //                   Khuyenmai = t.KHUYENMAI,
        //                   Hinhanh = t.HINHANH_SANPHAM

        //               }).ToList();
        //    List<SanPhamModel> tk = new List<SanPhamModel>();

        //    foreach (var item in res)
        //    {
        //        SanPhamModel m = new SanPhamModel();
        //        m.ID = item.Id;
        //        m.LOAISANPHAM = item.Loaisp;
        //        m.TENSANPHAM = item.Tensp;
        //        m.NCC = item.Ncc;
        //        m.IDLOAISP = item.maloai;
        //        m.IDNCC = item.idncc;
        //        m.MOTA = item.Mota;
        //        m.GIASANPHAM = item.Giasp;
        //        m.TRANGTHAI = item.Trangthai;
        //        m.HINHANH = item.Hinhanh;
        //        m.MASANPHAM = item.Masp;

        //        m.SANPHAMKEMTHEO = item.SPkemtheo;
        //        m.SOLUONGTON = item.SLTon;
        //        m.TONTOITHIEU = item.Tontoithieu;
        //        m.LUOTXEM = item.Luotxem;
        //        m.KHUYENMAI = item.Khuyenmai;
        //        tk.Add(m);
        //    }
        //    return tk;
        //}
        //san phảm mới
        public List<SANPHAM> GetListNewProduct(int top)
        {
            var kq = db.SANPHAMs.OrderByDescending(x=>x.CREATEDATE).Take(top).ToList();

            return kq;
        }
        //public List<SANPHAM> GetListFeatureProduct()
        //{
        //    var kq = db.SANPHAMs.Where(x => x.TOP).Take(top).ToList();

        //    return kq;
        //}
        public List<SANPHAM> GetListKhuyenMai()
        {
            var kq = db.SANPHAMs.Where(t => t.KHUYENMAI > 0).ToList();
            
            return kq;
        }
        // get list product
        public List<SANPHAM> GetListProductById(long CateId,ref int TotalRecord, int page=1,int PageSize=2)
        {
            TotalRecord = db.SANPHAMs.Where(t => t.LOAISANPHAM_ID == CateId).Count();
            var kq = db.SANPHAMs.Where(t => t.LOAISANPHAM_ID ==CateId).OrderByDescending(x=>x.CREATEDATE).Skip((PageSize-1) * page).Take(PageSize).ToList();

            return kq;
        }
        public List<SANPHAM> GetListRelatedProducts(long id)
        {
            var product = db.SANPHAMs.Find(id);
            var kq = db.SANPHAMs.Where(t => t.SANPHAM_ID !=id && t.LOAISANPHAM_ID==product.LOAISANPHAM_ID).ToList();

            return kq;
        }
        public SANPHAM ViewDetail(long id)
        {
        return db.SANPHAMs.Find(id);

           
        }
    }
}