namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheLoai")]
    public partial class TheLoai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public TheLoai()
        {
            Giays = new HashSet<Giay>();
        }

        [Key]
        [DisplayName("Mã danh mục")]
        public int MaDanhMuc { get; set; }

        [StringLength(200)]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Ngày sửa")]
        public DateTime? NgaySua { get; set; }

        [StringLength(50)]
        [DisplayName("Tên thể loại")]
        public string TenTheLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Giay> Giays { get; set; }
    }
}
