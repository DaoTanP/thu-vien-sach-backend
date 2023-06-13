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

        [DataMember(Name = "userId")]
        [Display(Name = "Người dùng")]
        [Required]
        [StringLength(100)]
        public string NguoiDung_Id { get; set; }

        [DataMember(Name = "bookId")]
        [Display(Name = "Sách")]
        [Required]
        [StringLength(100)]
        public string Sach_Id { get; set; }

        public NguoiDung NguoiDung { get; set; }

        public Sach Sach { get; set; }

    }
}
