namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TacGia")]
    public partial class TacGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TacGia()
        {
            Saches = new HashSet<Sach>();
        }

        [StringLength(100)]
        public string Id { get; set; }

        [Display(Name = "Họ đệm")]
        [StringLength(50)]
        public string HoDem { get; set; }

        [Display(Name = "Tên")]
        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        [Display(Name = "Họ Tên")]
        public string HoTen { get => HoDem + " " + Ten; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
