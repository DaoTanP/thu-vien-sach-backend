namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            DanhSachYeuThiches = new HashSet<DanhSachYeuThich>();
            ThongTinMuonSaches = new HashSet<ThongTinMuonSach>();
        }

        [StringLength(100)]
        public string Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ đệm")]
        [StringLength(100)]
        public string HoDem { get; set; }

        [Display(Name = "Tên")]
        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        [Display(Name = "Ngày sinh")]
        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public bool? GioiTinh { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(1000)]
        public string DiaChi { get; set; }

        [Display(Name = "Số điện thoại")]
        public decimal? SoDienThoai { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [StringLength(50)]
        public string AnhDaiDien { get; set; }

        [Display(Name = "Số thẻ")]
        [StringLength(50)]
        public string SoThe { get; set; }

        [Display(Name = "Mật khẩu thẻ")]
        [StringLength(50)]
        public string MatKhauThe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhSachYeuThich> DanhSachYeuThiches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinMuonSach> ThongTinMuonSaches { get; set; }
    }
}
