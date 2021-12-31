using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_NHOM4.Models
{
    public class GioHang
    {
        DBQLBanGiay db = new DBQLBanGiay();
        public int iMaGiay { get; set; }
        public string sTenGiay { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        public GioHang(int MaGiay)
        {
            iMaGiay = MaGiay;
            Giay giay = db.Giay.SingleOrDefault(i => i.MaGiay == MaGiay);
            sTenGiay = giay.TenGiay;
            sAnhBia = giay.Anh;
            dDonGia = (double)giay.Gia;
            iSoLuong = 1;
        }
    }

}