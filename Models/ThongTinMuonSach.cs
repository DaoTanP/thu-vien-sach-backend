namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("ThongTinMuonSach")]
    public partial class ThongTinMuonSach
    {
        [DataMember(Name = "id", Order = 1)]
        [StringLength(50)]
        public string Id { get; set; }

        [DataMember(Name = "cardNumber", Order = 2)]
        [Display(Name = "Số thẻ")]
        [Required]
        [StringLength(50)]
        public string SoThe { get; set; }

        [DataMember(Name = "bookId", Order = 3)]
        [Display(Name = "Sách")]
        [Required]
        [StringLength(100)]
        public string Sach_Id { get; set; }

        [DataMember(Name = "borrowDate", Order = 4)]
        [Display(Name = "Ngày mượn")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime NgayMuon { get; set; }

        [DataMember(Name = "returnDate", Order = 5)]
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
