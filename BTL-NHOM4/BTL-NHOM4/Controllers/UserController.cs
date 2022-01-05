using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM4.Models;
namespace BTL_NHOM4.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DBQLBanGiay db = new DBQLBanGiay();
        public ActionResult Index(string page)
        {
            int pageSize = 10;
            int p = page == null || "".Equals(page) ? 1 : int.Parse(page);
            var rs = from dh in db.DonHang
                     join ctdh in db.ChiTietDonHang on dh.MaDonHang equals ctdh.MaDonHang
                     group ctdh by dh.MaDonHang into gr
                     select new DonHangDao { MaDonHang = .MaDonHang, TrangThai = dh.TrangThai, TongTien = (float)gr.Sum(x => x.SoLuong * x.Gia) };
        }

    }
}