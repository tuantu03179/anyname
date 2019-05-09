using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using bandienthoai.Areas.Admin.Models.EF;
using bandienthoai.Common;

namespace bandienthoai.Areas.Admin.Models.DAO
{
  
    public class UserDAO
    {
       QlBanHangDbContext db=null;
       public UserDAO()
        {
            db = new QlBanHangDbContext();
        }
        public decimal Insert(TAIKHOAN tk)
        {
            db.TAIKHOANs.Add(tk);
            db.SaveChanges();
            return tk.ID;
            
        }
        public List<LOAITAIKHOAN> getAllTypeUser()
        {
            return db.LOAITAIKHOANs.Where(t=>t.IS_DELETE==false).ToList();
        }
        public bool Update(TAIKHOAN tk)
        {
            try
            {
                var user = db.TAIKHOANs.Find(tk.ID);
                user.HOTEN = tk.HOTEN;
                user.MATKHAU = tk.MATKHAU;
                user.SDT = tk.SDT;
                user.STATUS = tk.STATUS;
                user.DIACHI_TK = tk.DIACHI_TK;
                user.EMAIL_TK = tk.EMAIL_TK;
                user.GHICHU_TK = tk.GHICHU_TK;
                user.MODIFILEDBY = tk.MODIFILEDBY;
                user.MODIFILEDDATE = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        //public IEnumerable<TAIKHOAN> listuser(int page, int pagesize)
        //{
        //    return db.TAIKHOANs.OrderByDescending(x=>x.CREATEDATE).ToPagedList(page,pagesize);
        //}

        public decimal Login(string user, string pass)
        {
            var result = db.TAIKHOANs.SingleOrDefault(x => x.TENTAIKHOAN == user);
            if (result == null)
            {
                return 0;
            }
            
            else
            {
                if (result.STATUS==false)
                {
                    return -1;
                }
                else
                {
                    if (result.MATKHAU == pass)
                    {
                        return 1;
                    }
                    return -2;
                }
            }
        }
       
        public bool Delete(decimal id)
        {
            try
            {
                var user = db.TAIKHOANs.Find(id);
                db.TAIKHOANs.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
          
        }
        
            public TAIKHOAN GetByUserName(string userName)
        {
            return db.TAIKHOANs.SingleOrDefault(x => x.TENTAIKHOAN == userName);
        }
        public TAIKHOAN GetByID(int userName)
        {
            return db.TAIKHOANs.SingleOrDefault(x => x.ID == userName);
        }
        public TAIKHOAN ViewDetail(decimal id)
        {
            return db.TAIKHOANs.Find(id);
        }

        public bool ChangeStatus(decimal id)
        {
            var user = db.TAIKHOANs.Find(id);
            user.STATUS = !user.STATUS;
            db.SaveChanges();
            return user.STATUS;
        }
        public string GetLoaiTaiKhoanByID(long id)
        {
            var type = db.LOAITAIKHOANs.Find(id);
            return type.TENLOAITK;
        }
        public List<TaiKhoanModel> GetLoaiTaiKhoan()
        {
            
            var res = (from t in db.TAIKHOANs
                      join l in db.LOAITAIKHOANs
                           on t.LOAITAIKHOAN_ID equals l.LOAITAIKHOAN_ID into g
                      from d in g.DefaultIfEmpty()
                      select new
                      {
                          id = t.ID,
                          tentk=t.TENTAIKHOAN,
                          LoaiTk = d.TENLOAITK,
                          sdt = t.SDT,
                          Email=t.EMAIL_TK,
                          Diachi=t.DIACHI_TK,
                          TrangThai=t.STATUS,
                          hoten=t.HOTEN
                            
                      }).ToList();
            List<TaiKhoanModel> tk = new List<TaiKhoanModel>();
            
            foreach (var item in res)
            {
                TaiKhoanModel m = new TaiKhoanModel();
              m.ID = item.id;
                m.TENTAIKHOAN = item.tentk;
              m.LOAITAIKHOAN = item.LoaiTk;
              m.HOTEN = item.hoten;
              m.STATUS = item.TrangThai;
              m.DIACHI_TK = item.Diachi;
              m.SDT = item.sdt;
                m.EMAIL_TK = item.Email;
                tk.Add(m);
            }
            return tk;
        }
    }
   
}