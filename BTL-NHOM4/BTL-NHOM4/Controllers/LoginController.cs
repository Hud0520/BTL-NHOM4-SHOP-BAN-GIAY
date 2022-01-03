using BTL_NHOM4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NHOM4.Controllers
{
    public class LoginController : Controller
    {
        DBQLBanGiay db = new DBQLBanGiay();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            var tendn = f["userName"];
            var matkhau = f["passWord"];
            ViewBag.tendn = tendn;
            if (String.IsNullOrEmpty(tendn)) { ViewData["Loi1"] = "Phải nhập tên đăng nhập"; }
            else if (String.IsNullOrEmpty(matkhau)) { ViewData["Loi2"] = "Phải nhập mật khẩu"; }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)
                KhachHang kh = db.KhachHang.SingleOrDefault(n => n.Email == tendn);
                if (kh.MatKhau == matkhau)
                {
                    //ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["userLogined"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Loi2"] = "Mật khẩu không đúng";
                }
            }
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection f, KhachHang dk)
        {
            ViewBag.type2 = "active";
            dk.Ho = f.Get("fname");
            dk.Ten = f.Get("lname");
            dk.Email = f.Get("dkemail");
            dk.MatKhau = f.Get("dkpass");

            String cfpass = f.Get("cfpass");
            bool flag = true;
            if (String.IsNullOrEmpty(dk.Ho))
            { 
                ViewData["LoiDK1"] = "Họ tên không được để trống";
                flag = false;
            }
            if (String.IsNullOrEmpty(dk.Ten))
            { ViewData["LoiDK2"] = "Họ tên không được để trống"; flag = false; }
            if (String.IsNullOrEmpty(dk.Email))
            { ViewData["LoiDK3"] = "Email không được để trống"; flag = false; }
            else
            {
                var iex = db.KhachHang.SingleOrDefault(i => i.Email == dk.Email);
                if (iex != null)
                {
                    ViewData["LoiDK3"] = "Email đã được sửa dụng"; flag = false;
                }
                else if(!System.Text.RegularExpressions.Regex.IsMatch(dk.Email, @"^\w+[-,.]*\w+@\w+\.\w+$"))
                {
                    ViewData["LoiDK3"] = "Không đúng cấu trúc hộp thư"; flag = false;
                }
            }
            if (String.IsNullOrEmpty(dk.MatKhau))
            { ViewData["LoiDK4"] = "Mật khẩu không được để trống"; flag = false; }
            if (!dk.MatKhau.Equals(cfpass))
            { ViewData["LoiDK5"] = "Mật khẩu xác nhận bị sai"; flag = false; }
            if(flag)
            {   //Gán giá trị cho đối tượng được tạo mới (kh)
                db.KhachHang.Add(dk);
                db.SaveChanges();
                Response.Write("<script>alert('" + "Đăng ký thành công" + "')</script>");
                return View("Index");
                //return RedirectToAction("Dangnhap");
            }
            else
            {
                ViewBag.fname = dk.Ho;
                ViewBag.lname = dk.Ten;
                ViewBag.dkemail = dk.Email;
                ViewBag.dkpass = dk.MatKhau;
                return View();
            }
            
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}