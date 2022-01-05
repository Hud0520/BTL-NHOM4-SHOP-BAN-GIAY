using BTL_NHOM4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NHOM4.Controllers
{

    public class CartController : Controller
    {
        DBQLBanGiay db = new DBQLBanGiay();
        // GET: Card
        public ActionResult Index()
        {
            List<GioHang> lsgiohang = Laygiohang();
            if(lsgiohang.Count == 0)
            {
                return RedirectToAction("Index", "Shop");
            }
            
            return View(lsgiohang);
        }

        public ActionResult Checkout()
        {
            if(Session["userLogined"]== null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<GioHang> lsgiohang = Laygiohang();
            if (lsgiohang.Count == 0)
            {
                return RedirectToAction("Index", "Shop");
            }

            return View(lsgiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DonHang donhangmoi = new DonHang();
            KhachHang cu = (KhachHang)Session["userlogined"];
            List<GioHang> lsgiohang = Laygiohang();
            if(cu == null)
            {
                Response.Write("<script>alert('" + "Phiên làm việc hết hạn" + "')</script>");
                return View("Index", lsgiohang);
            }
            if(lsgiohang.Count == 0)
            {
                Response.Write("<script>alert('" + "Giỏ hàng trống" + "')</script>");
                return View("Index", lsgiohang);
            }
            donhangmoi.MaKhachHang = cu.MaKhachHang;
            donhangmoi.NgayTao = DateTime.Now;
            donhangmoi.DiaChi = f.Get("address")+ " - Số điện thoại người nhận : "+ f.Get("phone");
            donhangmoi.GhiChu = f.Get("ghichu");
            donhangmoi.TrangThai = ""+0;
            db.DonHang.Add(donhangmoi);
            db.SaveChanges();
            foreach (var item in lsgiohang)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDonHang = donhangmoi.MaDonHang;
                ctdh.MaGiay = item.iMaGiay;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.Gia = (float?)(decimal?)item.dDonGia;
                db.ChiTietDonHang.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            Session["TongSoLuong"] = null;
            Session["TongTien"] = null;
            return RedirectToAction("XacNhanDonHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
                Session["TongSoLuong"] = TongSanPham();
                Session["TongTien"] = TongTien();
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int iMaGiay, string url, string qtybutton)
        {
            Giay kho = db.Giay.SingleOrDefault(i => i.MaGiay == iMaGiay);
            List<GioHang> lsgiohang =Laygiohang();
            GioHang sanpham = lsgiohang.Find(i => i.iMaGiay == iMaGiay);
            int a;
            bool flag = int.TryParse(qtybutton, out a);
            
            if (sanpham == null)
            {
                if(kho.SoLuong < a)
                {
                    Response.Write("<script>alert('" + "Hết hàng" + "')</script>");
                }
                else
                {
                    sanpham = new GioHang(iMaGiay);
                    if (flag) { sanpham.iSoLuong = a; }
                    lsgiohang.Add(sanpham);
                }
                
            }
            else
            {
                if (kho.SoLuong < sanpham.iSoLuong + a)
                {
                    Response.Write("<script>alert('" +"Không đủ số lượng trong kho" + "')</script>");
                }
                else
                {
                    if (flag) { sanpham.iSoLuong += a; }
                    else
                    {
                        sanpham.iSoLuong++;
                    }
                }
                
            }
            Session["TongSoLuong"] = TongSanPham();
            Session["TongTien"] = TongTien();
            return Redirect(url);
        }

        public ActionResult XoaGioHang(int? iMaGiay)
        {
            List<GioHang> lsgiohang = Laygiohang();
            GioHang sp = lsgiohang.FirstOrDefault(i => i.iMaGiay == iMaGiay);

            if(sp != null)
            {
                lsgiohang.RemoveAll(i => i.iMaGiay == iMaGiay);
                Session["TongSoLuong"] = TongSanPham();
                Session["TongTien"] = TongTien();
            }
            if(lsgiohang.Count == 0)
            {
                return RedirectToAction("Index", "Shop");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Update(int? iMaGiay, FormCollection f)
        {
            List<GioHang> lsgiohang = Laygiohang();
            GioHang sp = lsgiohang.SingleOrDefault(i => i.iMaGiay == iMaGiay);
            Giay kho = db.Giay.SingleOrDefault(i => i.MaGiay == iMaGiay);
            string strsl = f.Get("sl");
            int sl;
            if (!int.TryParse(strsl,out sl))
            {
                Response.Write("<script>alert('" + "Số lượng bạn nhập không hợp lệ" + "')</script>");
                return View("Index", lsgiohang);
            }
            if(sp != null)
            {
                if (kho.SoLuong > sl)
                {
                    sp.iSoLuong = (int)sl;
                    Session["TongSoLuong"] = TongSanPham();
                    Session["TongTien"] = TongTien();
                    return RedirectToAction("Index");
                }
                else
                {
                    Response.Write("<script>alert('" + "Số lượng hàng không có đủ" + "')</script>");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Response.Write("<script>alert('"+"Có lỗi sảy ra không thể cập nhật"+ "')</script>");
                return RedirectToAction("Index");
            }
        }
        public double TongTien()
        {
            double tongTien = 0;
            List<GioHang> lsgiohang = Session["GioHang"] as List<GioHang>;
            if(lsgiohang != null)
            {
                tongTien = lsgiohang.Sum<GioHang>(i => i.dThanhTien);
            }
            return tongTien;
        }

        public int TongSanPham()
        {
            int tongSp = 0;
            List<GioHang> lsgiohang = Session["GioHang"] as List<GioHang>;
            if (lsgiohang != null)
            {
                tongSp = lsgiohang.Sum<GioHang>(i => i.iSoLuong);
            }
            return tongSp;
        }


    }

}