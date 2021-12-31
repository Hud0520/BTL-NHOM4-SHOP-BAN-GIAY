namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [StringLength(50)]
        public string Ho { get; set; }

        [StringLength(20)]
        public string Ten { get; set; }

        public int? Tuoi { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public bool? QuanTriVien { get; set; }

        [Required]
        [StringLength(100)]
        public string MatKhau { get; set; }

        [Key]
        public int MaKhachHang { get; set; }

        [StringLength(11)]
        public string SDT { get; set; }
    }
}
