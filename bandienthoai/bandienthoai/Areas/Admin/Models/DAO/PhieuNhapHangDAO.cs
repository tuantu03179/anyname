using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using bandienthoai.Areas.Admin.Models.EF;
using bandienthoai.Common;
using Newtonsoft.Json;
using PagedList;


namespace bandienthoai.Areas.Admin.Models.DAO
{
    public class PhieuNhapHangDAO
    {
        QlBanHangDbContext db = null;
        public PhieuNhapHangDAO()
        {
            db = new QlBanHangDbContext();
        }
        public List<PhieuNhapHangModel> getAllPhieuNhap()
        {
            var kq = db.PHIEUNHAPHANGs.Select(t => new PhieuNhapHangModel
            {
                PHIEUNHAPHANG_ID = t.PHIEUNHAPHANG_ID,
                MA_NCC = t.MA_NCC,
                SOPHIEUNHAPHANG = t.SOPHIEUNHAPHANG,
                NGAYLAP_PHIEUNHAPHANG = t.NGAYLAP_PHIEUNHAPHANG,
                GHICHU_NHAPHANG = t.GHICHU_NHAPHANG,
                NGAYGIAO = t.NGAYGIAO,
                TRANGTHAINHAPHANG = t.TRANGTHAINHAPHANG,
                IS_DELETE = t.IS_DELETE,
                CREATEBY = t.CREATEBY,
                MODIFILEDBY = t.MODIFILEDBY,
                MODIFILEDDATE = t.MODIFILEDDATE,
                CREATEDATE = t.CREATEDATE,


            }).ToList();
            foreach (PhieuNhapHangModel phieu in kq.ToList())
            {
                if ((bool)phieu.IS_DELETE)
                    kq.Remove(phieu);
            }
            return kq;
        }
        public string GetListPhieuNhapByID(long ID)
        {
            var model = db.PHIEUNHAPHANGs.Where(x => x.PHIEUNHAPHANG_ID == ID).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return value;
        }
        //delete
        public bool Delete(int id)
        {
            try
            {
                var t = db.PHIEUNHAPHANGs.Find(id);
                t.IS_DELETE = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //get  ncc by id
        public PHIEUNHAPHANG GetById(int id)
        {


            try
            {
                var kq = db.PHIEUNHAPHANGs.SingleOrDefault(t => t.PHIEUNHAPHANG_ID == id);
                return kq;
            }
            catch
            {
                return null;
            }
        }
        // ẩn hiện
        public int ChangeStatus(int id, string name, bool kq)
        {
            int i = 0;
            try
            {
                var user = db.PHIEUNHAPHANGs.Find(id);
                if (kq)
                {
                    user.IS_DELETE = true;
                    i = 1;
                }
                else
                { user.IS_DELETE = false; i = 2; }
                user.MODIFILEDBY = name;
                user.MODIFILEDDATE = DateTime.Now;

                db.SaveChanges();
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //lưu dữ liệu
        public int SaveData(PhieuNhapHangModel x, string user)
        {
            try
            {
                if (x.PHIEUNHAPHANG_ID > 0)
                {


                    PHIEUNHAPHANG dao = db.PHIEUNHAPHANGs.SingleOrDefault(m => m.PHIEUNHAPHANG_ID == x.PHIEUNHAPHANG_ID);
                    dao.MA_NCC = x.MA_NCC;
                    dao.IS_DELETE = false;
                    dao.MODIFILEDDATE = DateTime.Now.Date;
                    dao.MODIFILEDBY = user;
                    dao.SOPHIEUNHAPHANG = x.SOPHIEUNHAPHANG;
                    dao.NGAYLAP_PHIEUNHAPHANG = x.NGAYGIAO;
                    dao.TRANGTHAINHAPHANG = x.TRANGTHAINHAPHANG;
                    dao.GHICHU_NHAPHANG = x.GHICHU_NHAPHANG;

                    db.SaveChanges();

                    return 1;

                }
                else
                {
                    PHIEUNHAPHANG dao = new PHIEUNHAPHANG();

                    dao.MA_NCC = x.MA_NCC;
                    dao.IS_DELETE = false;
                    dao.MODIFILEDDATE = DateTime.Now.Date;
                    dao.MODIFILEDBY = user;
                    dao.SOPHIEUNHAPHANG = x.SOPHIEUNHAPHANG;
                    dao.NGAYLAP_PHIEUNHAPHANG = x.NGAYGIAO;
                    dao.TRANGTHAINHAPHANG = x.TRANGTHAINHAPHANG;
                    dao.GHICHU_NHAPHANG = x.GHICHU_NHAPHANG;


                    dao.CREATEDATE = DateTime.Now.Date;
                    dao.CREATEBY = user;

                    db.PHIEUNHAPHANGs.Add(dao);
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