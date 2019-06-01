using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bandienthoai.Areas.Admin.Models
{
    public class PhieuNhapHangModel
    {
        [Key]
        public long PHIEUNHAPHANG_ID { get; set; }

        public int MA_NCC { get; set; }

        [Required]
        [StringLength(50)]
        public string SOPHIEUNHAPHANG { get; set; }

        public DateTime? NGAYLAP_PHIEUNHAPHANG { get; set; }

        public string FNGAYLAP_PHIEUNHAPHANG { get { return ((DateTime)this.NGAYLAP_PHIEUNHAPHANG).ToString("dd/MM/yyyy"); } set { this.FNGAYLAP_PHIEUNHAPHANG = value; } }

        public DateTime? NGAYGIAO { get; set; }

        [Required]
        [StringLength(255)]
        public string TRANGTHAINHAPHANG { get; set; }

        [StringLength(250)]
        public string GHICHU_NHAPHANG { get; set; }

        public bool? IS_DELETE { get; set; }

        [StringLength(50)]
        public string CREATEBY { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(50)]
        public string MODIFILEDBY { get; set; }

        public DateTime? MODIFILEDDATE { get; set; }
    }
}