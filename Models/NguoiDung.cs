namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            DanhSachYeuThiches = new HashSet<DanhSachYeuThich>();
            ThongTinMuonSaches = new HashSet<ThongTinMuonSach>();
        }

        [DataMember(Name = "id", Order = 0)]
        [StringLength(100)]
        public string Id { get; set; }

        [DataMember(Name = "username", Order = 1)]
        [Display(Name = "Tên đăng nhập")]
        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [DataMember(Name = "password", Order = 2)]
        [Display(Name = "Mật khẩu")]
        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [DataMember(Name = "lastName", Order = 3)]
        [Display(Name = "Họ đệm")]
        [StringLength(100)]
        public string HoDem { get; set; }

        [DataMember(Name = "firstName", Order = 4)]
        [Display(Name = "Tên")]
        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        [Display(Name = "Họ tên độc giả")]
        public string HoTen { get => HoDem + " " + Ten; }

        [DataMember(Name = "dateOfBirth", Order = 6)]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? NgaySinh { get; set; }

        [DataMember(Name = "gender", Order = 7)]
        [Display(Name = "Giới tính")]
        public bool? GioiTinh { get; set; }

        [DataMember(Name = "address", Order = 8)]
        [Display(Name = "Địa chỉ")]
        [StringLength(1000)]
        public string DiaChi { get; set; }

        [DataMember(Name = "phoneNumber", Order = 9)]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        
        [DataMember(Name = "email", Order = 9)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "avatarImage", Order = 10)]
        [Display(Name = "Ảnh đại diện")]
        [StringLength(50)]
        public string AnhDaiDien { get; set; }

        [DataMember(Name = "cardNumber", Order = 11)]
        [Display(Name = "Số thẻ")]
        [StringLength(50)]
        public string SoThe { get; set; }

        [DataMember(Name = "cardPassword", Order = 12)]
        [Display(Name = "Mật khẩu thẻ")]
        [StringLength(50)]
        public string MatKhauThe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<DanhSachYeuThich> DanhSachYeuThiches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ThongTinMuonSach> ThongTinMuonSaches { get; set; }
    }
}
