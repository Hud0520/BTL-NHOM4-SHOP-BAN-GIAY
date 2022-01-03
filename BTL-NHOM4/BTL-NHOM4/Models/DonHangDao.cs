using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_NHOM4.Models
{
    public class DonHangDao
    {
        public int MaDonHang { get; set; }

        public int MaKhachHang { get; set; }

        public string DiaChi { get; set; }

        public string GhiChu { get; set; }

        public string TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }

        public float TongTien { get; set; }

    }
}