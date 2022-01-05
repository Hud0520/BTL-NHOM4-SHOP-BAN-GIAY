using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM4.Models;
using PagedList;

namespace BTL_NHOM4.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DBQLBanGiay db = new DBQLBanGiay();
        public ActionResult Index(string page, int? mdh)
        {
            KhachHang cu = (KhachHang)Session["userLogined"];
            if(cu == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int pageSize = 10;
            int p = page == null || "".Equals(page) ? 1 : int.Parse(page);
            var rs = from dh in db.DonHang
                     join ctdh in db.ChiTietDonHang
                     on dh.MaDonHang equals ctdh.MaDonHang
                     into gr
                     orderby dh.MaDonHang descending
                     where dh.MaKhachHang == cu.MaKhachHang
                     select new DonHangDao
                     {
                        MaDonHang = dh.MaDonHang,
                        NgayTao = dh.NgayTao,
                        GhiChu = dh.GhiChu,
                        DiaChi = dh.DiaChi,
                        TrangThai = dh.TrangThai,
                        TongTien = (float)gr.Sum(i => i.SoLuong * i.Gia)
                     };
            if(mdh != null)
            {
                DonHang dhx = db.DonHang.SingleOrDefault(i => i.MaDonHang == mdh);
                if (dhx != null)
                {
                    if (dhx.TrangThai == "0")
                    {
                        dhx.TrangThai = "4";
                        dhx.GhiChu = "Người dùng hủy đơn hàng";
                        db.SaveChanges();
                        Response.Write("<script>alert('" + "Đơn hàng đã được hủy" + "')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('" + "Đơn hàng không được hủy" + "')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('" + "Không tìm thấy đơn hàng" + "')</script>");
                }
            }
            ViewBag.active = 1;
            ViewBag.Count = rs.Count();
            return View(rs.ToPagedList(p,pageSize));
        }

        public ActionResult ThongTinCaNhan(FormCollection f)
        {
            KhachHang cu = (KhachHang)Session["userLogined"];
            if (cu == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (f.Count > 0)
            {
                
                KhachHang edit = db.KhachHang.SingleOrDefault(i => i.MaKhachHang == cu.MaKhachHang);

                edit.Ho = f.Get("fname");
                edit.Ten = f.Get("lname");
                int tuoi;
                edit.Tuoi = int.TryParse(f.Get("tuoi"), out tuoi) ? tuoi : 0;
                edit.SDT = f.Get("phone");
                edit.DiaChi = f.Get("address");
                try
                {
                    db.SaveChanges();
                    Session["userLogined"] = edit;
                    Response.Write("<script>alert('" + "Cập nhật thông tin thành công" + "')</script>");
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('" + "Có lỗi xảy ra" + "')</script>");
                    throw;
                }
            }
            ViewBag.Count = db.DonHang.Where(i => i.MaKhachHang == cu.MaKhachHang).Count();
            ViewBag.active = 2;
            return View();
        }
   
        public ActionResult DoiMatKhau(FormCollection f)
        {
            KhachHang cu = (KhachHang)Session["userLogined"];
            if (cu == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            if (f.Count> 0)
            {
                string mkcu = f.Get("MatKhauCu");
                string mkmoi = f.Get("MatKhauMoi");
                string cfmk = f.Get("CFMatKhau");
                KhachHang edit = db.KhachHang.SingleOrDefault(i => i.MaKhachHang == cu.MaKhachHang);
                if (mkcu != edit.MatKhau)
                {
                    ViewData["Loi1"] = "* Mật khẩu không đùng";
                }
                else
                {
                    if(mkmoi == null)
                    {
                        ViewData["Loi2"] = "* Mật khẩu không được để trống";
                    }
                    else
                    {
                        if(mkmoi != cfmk)
                        {
                            ViewData["Loi2"] = "* Mật khẩu xác nhận không khớp";
                        }
                        else
                        {
                            edit.MatKhau = mkmoi;
                            db.SaveChanges();
                            Response.Write("<script>alert('" + "Thay đổi mật khẩu thành công" + "')</script>");

                           
                        }
                        
                    }
                }
                ViewBag.mkcu = mkcu;
                ViewBag.mkmoi = mkmoi;
            }
            ViewBag.Count = db.DonHang.Where(i => i.MaKhachHang == cu.MaKhachHang).Count();
            ViewBag.active = 3;
            return View();
        }
        public PartialViewResult menu()
        {
            
            return PartialView();
        }
    }
}