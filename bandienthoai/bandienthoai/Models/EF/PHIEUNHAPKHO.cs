namespace bandienthoai.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUNHAPKHO")]
    public partial class PHIEUNHAPKHO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PHIEUNHAPKHO_ID { get; set; }

        public long PHIEUYCNHAPKHO_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string SOPHIEUNHAPKHO { get; set; }

        public DateTime? NGAYLAP_PHIEUNHAPKHO { get; set; }

        [StringLength(250)]
        public string GHICHU_PHIEUNHAPKHO { get; set; }

        public bool? IS_DELETE { get; set; }

        [StringLength(50)]
        public string CREATEBY { get; set; }

        public DateTime? CREATEDATE { get; set; }

        [StringLength(50)]
        public string MODIFILEDBY { get; set; }

        public DateTime? MODIFILEDDATE { get; set; }

        public virtual PHIEUYEUCAUNHAPKHO PHIEUYEUCAUNHAPKHO { get; set; }
    }
}
