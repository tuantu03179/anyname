namespace bandienthoai.Areas.Admin.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUNHAPHANG")]
    public partial class PHIEUNHAPHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUNHAPHANG()
        {
            CHITIETPHIEUNHAPHANGs = new HashSet<CHITIETPHIEUNHAPHANG>();
            PHIEUYEUCAUNHAPKHOes = new HashSet<PHIEUYEUCAUNHAPKHO>();
        }

        [Key]
        public long PHIEUNHAPHANG_ID { get; set; }

        public int MA_NCC { get; set; }

        [Required]
        [StringLength(50)]
        public string SOPHIEUNHAPHANG { get; set; }

        public DateTime? NGAYLAP_PHIEUNHAPHANG { get; set; }

        public DateTime? NGAYGIAO { get; set; }

        public DateTime? NGUOIGIAO { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAPHANG> CHITIETPHIEUNHAPHANGs { get; set; }

        public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUYEUCAUNHAPKHO> PHIEUYEUCAUNHAPKHOes { get; set; }
    }
}
