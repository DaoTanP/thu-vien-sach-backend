namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("NhaXuatBan")]
    public partial class NhaXuatBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaXuatBan()
        {
            Saches = new HashSet<Sach>();
        }

        [DataMember(Name = "id", Order = 0)]
        [StringLength(100)]
        public string Id { get; set; }

        [DataMember(Name = "name", Order = 1)]
        [Display(Name = "Tên nhà xuất bản")]
        [Column("NhaXuatBan")]
        [Required]
        [StringLength(4000)]
        public string TenNhaXuatBan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Sach> Saches { get; set; }
    }
}
