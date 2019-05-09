namespace bandienthoai.Areas.Admin.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOAN")]
    public partial class TAIKHOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAIKHOAN()
        {
            BANNERs = new HashSet<BANNER>();
            BINHLUANs = new HashSet<BINHLUAN>();
            DANHGIAs = new HashSet<DANHGIA>();
            HOADONs = new HashSet<HOADON>();
            TINTUCs = new HashSet<TINTUC>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string TENTAIKHOAN { get; set; }

        public int LOAITAIKHOAN_ID { get; set; }

        [StringLength(11)]
        public string SDT { get; set; }

        [Required]
        [StringLength(50)]
        public string HOTEN { get; set; }

        [StringLength(250)]
        public string EMAIL_TK { get; set; }

        [StringLength(300)]
        public string DIACHI_TK { get; set; }

        [StringLength(50)]
        public string MATKHAU { get; set; }

        [StringLength(250)]
        public string GHICHU_TK { get; set; }

        public bool IS_DELETE { get; set; }

        [StringLength(50)]
        public string CREATEBY { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(50)]
        public string MODIFILEDBY { get; set; }

        public DateTime? MODIFILEDDATE { get; set; }

        public bool STATUS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANNER> BANNERs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUAN> BINHLUANs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA> DANHGIAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }

        public virtual LOAITAIKHOAN LOAITAIKHOAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TINTUC> TINTUCs { get; set; }
    }
}
