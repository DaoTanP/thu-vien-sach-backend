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

        [Key]
        [StringLength(50)]
        public string SoThe { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhauThe { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayHetHan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NguoiDung> NguoiDungs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ThongTinMuonSach> ThongTinMuonSaches { get; set; }
    }
}
