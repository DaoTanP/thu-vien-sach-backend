namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheThuVien")]
    public partial class TheThuVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheThuVien()
        {
            NguoiDungs = new HashSet<NguoiDung>();
            ThongTinMuonSaches = new HashSet<ThongTinMuonSach>();
        }

        [Display(Name = "Số thẻ")]
        [Key]
        [StringLength(50)]
        public string SoThe { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required]
        [StringLength(50)]
        public string MatKhauThe { get; set; }

        [Display(Name = "Ngày cấp")]
        [Column(TypeName = "date")]
        public DateTime NgayCap { get; set; }

        [Display(Name = "Ngày hết hạn")]
        [Column(TypeName = "date")]
        public DateTime NgayHetHan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NguoiDung> NguoiDungs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ThongTinMuonSach> ThongTinMuonSaches { get; set; }
    }
}
