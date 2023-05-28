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

        [Required]
        [StringLength(100)]
        public string NguoiMuon_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Sach_Id { get; set; }

        public DateTime NgayMuon { get; set; }

        public DateTime NgayTra { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
