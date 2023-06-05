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

        [Display(Name = "Người mượn")]
        [Required]
        [StringLength(100)]
        public string NguoiMuon_Id { get; set; }

        [Display(Name = "Sách")]
        [Required]
        [StringLength(100)]
        public string Sach_Id { get; set; }

        [Display(Name = "Ngày mượn")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime NgayMuon { get; set; }

        [Display(Name = "Ngày trả")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime NgayTra { get; set; }

        public NguoiDung NguoiDung { get; set; }

        public Sach Sach { get; set; }
    }
}
