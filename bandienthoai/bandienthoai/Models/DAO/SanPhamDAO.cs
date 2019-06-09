
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
        public List<SANPHAM> GetListAllProduct( ref int TotalRecord, int page = 1, int PageSize = 2)
        {
            TotalRecord = db.SANPHAMs.Count();
            var kq = db.SANPHAMs.OrderByDescending(x => x.CREATEDATE).Skip((PageSize - 1) * page).Take(PageSize).ToList();

            return kq;
        }
        public List<SANPHAM> GetListAllKhuyenMai(ref int TotalRecord, int page = 1, int PageSize = 2)
        {
            TotalRecord = db.SANPHAMs.Where(t=>t.KHUYENMAI>0).Count();
            var kq = db.SANPHAMs.Where(t => t.KHUYENMAI > 0).OrderByDescending(x => x.CREATEDATE).Skip((PageSize - 1) * page).Take(PageSize).ToList();

            return kq;
        }
        //san phảm mới
        public List<SANPHAM> GetListNewProduct(int top)
        {
            var kq = db.SANPHAMs.OrderByDescending(x=>x.CREATEDATE).Take(top).ToList();

            return kq;
        }
        //get list image
        public List<IMAGEPRODDUCT> GetListImagetId(decimal id)
        {
            try { 
            return db.IMAGEPRODDUCTs.Where(x => x.IDPRODUCT == id).ToList();
            }
            catch
            {
                return null;
            }



        }
        //public List<SANPHAM> GetListFeatureProduct()
        //{
        //    var kq = db.SANPHAMs.Where(x => x.TOP).Take(top).ToList();

        //    return kq;
        //}
        //get sp mới

        //public List<ProductViewModel> GetListPromotion()
        //{

        //    var kq = (from a in db.SANPHAMs
        //              join b in db.KHUYENMAIs
        //              on a.SANPHAM_ID equals b.IDSANPHAM
        //              where b.GIATRI > 0
        //              select new
        //              {

        //                  ID = a.SANPHAM_ID,
        //                  Images = a.HINHANH_SANPHAM,
        //                  MetaTitle = a.GHICHU_SANPHAM,
        //                  Price = a.GIA_SANPHAM,
        //                  Promotion = b.GIATRI,
        //                  Name = a.TEN_SANPHAM,
        //                  Luotxem = a.LUOTXEM

        //              }).AsEnumerable().Select(x => new ProductViewModel
        //              {

        //                  ID = x.ID,
        //                  Images = x.Images,
        //                  MetaTitle = x.MetaTitle,
        //                  Price = x.Price,
        //                  Promotion = x.Promotion,
        //                  Name = x.Name,
        //                  Luotxem = x.Luotxem,
        //              });

        //    return kq.ToList();
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
        // get list product new
        public List<SANPHAM> GetListNew(int  tmp)
        {
            var kq = db.SANPHAMs.OrderByDescending(t=>t.CREATEDATE).Take(tmp).ToList();

            return kq;
        }
        //search
        public List<ProductViewModel> Search(string Keyword, ref int TotalRecord, int page = 1, int PageSize = 2)
        {
            TotalRecord = db.SANPHAMs.Where(t => t.TEN_SANPHAM.Contains(Keyword)).Count();

            var model = (from a in db.SANPHAMs
                         join b in db.LOAISANPHAMs
                         on a.LOAISANPHAM_ID equals b.LOAISANPHAM_ID
                         where a.TEN_SANPHAM.Contains(Keyword)
                         select new
                         {
                             CateMetatitle = b.GHICHU_LOAISANPHAM,
                             CateName = b.TEN_LOAISANPHAM,
                             ID = a.SANPHAM_ID,
                             Images = a.HINHANH_SANPHAM,
                             MetaTitle = a.GHICHU_SANPHAM,
                             Price = a.GIA_SANPHAM,
                             DateKT=a.NGAYKTKM,
                             Promotion = a.KHUYENMAI,
                             Name = a.TEN_SANPHAM,
                             Luotxem = a.LUOTXEM

                         }).AsEnumerable().Select(x => new ProductViewModel
                         {
                             CateMetatitle = x.CateMetatitle,
                             CateName = x.CateName,
                             ID = x.ID,
                             NgayKTKM=x.DateKT,
                             Images = x.Images,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             Promotion = x.Promotion,
                             Name = x.Name,
                             Luotxem = x.Luotxem,
                         });
           var kq= model.OrderByDescending(x => x.CREATEDATE).Skip((PageSize - 1) * page).Take(PageSize).ToList();
            return kq.ToList();
        }
        public List<SANPHAM> GetListRelatedProducts(long id)
        {
            var product = db.SANPHAMs.Find(id);
            var kq = db.SANPHAMs.Where(t => t.SANPHAM_ID !=id && t.LOAISANPHAM_ID==product.LOAISANPHAM_ID).ToList();

            return kq;
        }
        public List<string> ListName(string key)
        {
          
            var kq = db.SANPHAMs.Where(t => t.TEN_SANPHAM.Contains(key)).Select(x=>x.TEN_SANPHAM).ToList();

            return kq;
        }
        public SANPHAM ViewDetail(long id)
        {
        return db.SANPHAMs.Find(id);

           
        }
    }
}