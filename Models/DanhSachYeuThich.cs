namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("DanhSachYeuThich")]
    public partial class DanhSachYeuThich
    {
        [StringLength(100)]
        public string Id { get; set; }

        [Display(Name = "Người dùng")]
        [Required]
        [StringLength(100)]
        public string NguoiDung_Id { get; set; }

        [Display(Name = "Sách")]
        [Required]
        [StringLength(100)]
        public string Sach_Id { get; set; }

        public NguoiDung NguoiDung { get; set; }

    [DataMember(Name = "book")]
        public Sach Sach { get; set; }

    }
}
