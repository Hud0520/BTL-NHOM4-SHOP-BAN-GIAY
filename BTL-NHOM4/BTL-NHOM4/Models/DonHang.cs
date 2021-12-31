namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [Key]
        public int MaDonHang { get; set; }

        public int MaKhachHang { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
