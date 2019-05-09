using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bandienthoai.Areas.Admin.Models
{
    public class DonHangModel
    {
        [Key]
        public long HOADON_ID { get; set; }

        public int IDTOAIKHOAN { get; set; }
        public int IDKHACHHANG { get; set; }
        public bool HINHTHUCTHANHTOAN { get; set; }
        [StringLength(50)]
        public string HOTEN { get; set; }
        [StringLength(50)]
        public string SDT { get; set; }
        [StringLength(50)]
        public string GMAIL { get; set; }
        [StringLength(50)]
        public string DIACHI { get; set; }
        public DateTime? NGAYXUATHD { get; set; }

        public DateTime? NGAYBDGIAO { get; set; }

        public DateTime? NGAYKTGIAO { get; set; }

        public double? THANHTIEN_HOADON { get; set; }
        [StringLength(50)]
        public string TENTAIKHOAN { get; set; }
        [StringLength(255)]
        public string TRANGTHAI { get; set; }

        public bool IS_DELETE { get; set; }

        [StringLength(50)]
        public string CREATEBY { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(50)]
        public string MODIFILEDBY { get; set; }

        public DateTime? MODIFILEDDATE { get; set; }

        [StringLength(250)]
        public string GHICHU_HOADON { get; set; }
    }
}