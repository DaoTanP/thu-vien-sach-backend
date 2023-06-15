namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinMuonSach")]
    public partial class ThongTinMuonSach
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Display(Name = "Số thẻ")]
        [Required]
        [StringLength(50)]
        public string SoThe { get; set; }

        [Display(Name = "Sách")]
        [Required]
        [StringLength(100)]
        public string Sach_Id { get; set; }

        [Display(Name = "Ngày mượn")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime NgayMuon { get; set; }

        [Display(Name = "Ngày trả")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime NgayTra { get; set; }

        [Display(Name = "Trạng thái")]
        [Required]
        [StringLength(50)]
        public string TrangThaiMuon_Id { get; set; }

        public Sach Sach { get; set; }

        public TheThuVien TheThuVien { get; set; }
        
        public TrangThaiMuonSach TrangThaiMuonSach { get; set; }
    }
}
