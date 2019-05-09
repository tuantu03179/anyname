using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bandienthoai.Areas.Admin.Models
{
    public class LoaitaiKhoanModel
    {


        [Required]
        public int IDLOAITAIKHOAN { get; set; }

        [StringLength(50)]
        public string TENLOAITAIKHOAN { get; set; }

        [StringLength(250)]
        public string GHICHU { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        [StringLength(50)]
        public string CREATEBY { get; set; }

        public Nullable<DateTime> CREATEDATE { get; set; }
   
        public Nullable<DateTime> MODIFILEDDATE { get; set; }
        [StringLength(50)]
        public string MODIFILEDBY { get; set; }


    }
}