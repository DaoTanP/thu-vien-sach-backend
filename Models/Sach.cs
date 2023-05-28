namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            DanhSachYeuThiches = new HashSet<DanhSachYeuThich>();
            ThongTinMuonSaches = new HashSet<ThongTinMuonSach>();
        }

        [StringLength(100)]
        public string Id { get; set; }

        [Display(Name = "Tiêu đề")]
        [Required]
        [StringLength(1000)]
        public string TieuDe { get; set; }

        [Display(Name = "Thể loại")]
        [Required]
        [StringLength(100)]
        public string TheLoai_Id { get; set; }

        [Display(Name = "Tác giả")]
        [Required]
        [StringLength(100)]
        public string TacGia_Id { get; set; }

        [Display(Name = "Nhà xuất bản")]
        [Required]
        [StringLength(100)]
        public string NhaXuatBan_Id { get; set; }

        [Display(Name = "Ngày xuất bản")]
        public short? NgayXuatBan { get; set; }

        [Display(Name = "Số trang")]
        public short? SoTrang { get; set; }

        [Display(Name = "Giới thiệu")]
        [StringLength(4000)]
        public string GioiThieu { get; set; }

        [Display(Name = "Ảnh bìa")]
        [StringLength(50)]
        public string AnhBia { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhSachYeuThich> DanhSachYeuThiches { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }

        public virtual TacGia TacGia { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinMuonSach> ThongTinMuonSaches { get; set; }
    }
}
