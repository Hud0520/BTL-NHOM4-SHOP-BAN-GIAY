namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Giay")]
    public partial class Giay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Giay()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        [DisplayName("Mã giày")]
        public int MaGiay { get; set; }

        [DisplayName("Mã danh mục")]
        public int MaDanhMuc { get; set; }

        [StringLength(200)]
        [DisplayName("Tên giày")]
        public string TenGiay { get; set; }

        [StringLength(20)]
        [DisplayName("Màu")]
        public string Mau { get; set; }

        [StringLength(20)]
        [DisplayName("Kích thước")]
        public string KichThuoc { get; set; }

        [DisplayName("Giá")]
        public float? Gia { get; set; }

        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }

        [StringLength(100)]
        [DisplayName("Ảnh")]
        public string Anh { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime? NgayTao { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
