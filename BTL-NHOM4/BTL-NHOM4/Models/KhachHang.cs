namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        [StringLength(50)]
        [DisplayName("Họ")]
        public string Ho { get; set; }

        [StringLength(20)]
        [DisplayName("Tên")]
        public string Ten { get; set; }

        [DisplayName("Tuổi")]
        public int? Tuoi { get; set; }

        [StringLength(200)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Quyền quản trị viên")]
        public bool? QuanTriVien { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }

        [Key]
        [DisplayName("Mã khách hàng")]
        public int MaKhachHang { get; set; }

        [StringLength(11)]
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
