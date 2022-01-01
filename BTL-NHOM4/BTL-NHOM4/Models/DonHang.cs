namespace BTL_NHOM4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [Key]
        [DisplayName("Mã đơn hàng")]
        public int MaDonHang { get; set; }

        [DisplayName("Mã khách hàng")]
        public int MaKhachHang { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }

        [StringLength(50)]
        [DisplayName("Trạng thái")]
        public string TrangThai { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime? NgayTao { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
