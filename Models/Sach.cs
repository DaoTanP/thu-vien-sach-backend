namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            DanhSachYeuThiches = new HashSet<DanhSachYeuThich>();
            ThongTinMuonSaches = new HashSet<ThongTinMuonSach>();
        }

        [DataMember(Name = "id", Order = 1)]
        [StringLength(100)]
        public string Id { get; set; }

        [DataMember(Name = "title", Order = 2)]
        [Display(Name = "Tiêu đề")]
        [Required]
        [StringLength(1000)]
        public string TieuDe { get; set; }

        [DataMember(Name = "genreId")]
        [Display(Name = "Thể loại")]
        [Required]
        [StringLength(100)]
        public string TheLoai_Id { get; set; }

        [DataMember(Name = "authorId")]
        [Display(Name = "Tác giả")]
        [Required]
        [StringLength(100)]
        public string TacGia_Id { get; set; }

        [DataMember(Name = "publisherId")]
        [Display(Name = "Nhà xuất bản")]
        [Required]
        [StringLength(100)]
        public string NhaXuatBan_Id { get; set; }

        [DataMember(Name = "publishDate", Order = 3)]
        [Display(Name = "Ngày xuất bản")]
        public short? NgayXuatBan { get; set; }

        [DataMember(Name = "numberOfPage", Order = 4)]
        [Display(Name = "Số trang")]
        public short? SoTrang { get; set; }

        [DataMember(Name = "overview", Order = 5)]
        [Display(Name = "Giới thiệu")]
        [StringLength(4000)]
        public string GioiThieu { get; set; }

        [DataMember(Name = "image", Order = 6)]
        [Display(Name = "Ảnh bìa")]
        [StringLength(50)]
        public string AnhBia { get; set; }

        [DataMember]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<DanhSachYeuThich> DanhSachYeuThiches { get; set; }

        public NhaXuatBan NhaXuatBan { get; set; }

        public TacGia TacGia { get; set; }

        public TheLoai TheLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ThongTinMuonSach> ThongTinMuonSaches { get; set; }
    }
}
