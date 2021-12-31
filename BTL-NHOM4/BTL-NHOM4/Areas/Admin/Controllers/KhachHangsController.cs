using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_ASP.Models;
using PagedList;

namespace BTL_ASP.Areas.Admin.Controllers
{
    public class KhachHangsController : Controller
    {
        private QLBanGiay db = new QLBanGiay();

        // GET: Admin/KhachHangs
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "NguoiDungs");
            }
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "name" : "";

            var khachhangs = db.KhachHangs.Select(p => p);

            if (!String.IsNullOrEmpty(searchString))
            {
                khachhangs = khachhangs.Where(p => p.Ten.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    khachhangs = khachhangs.OrderBy(p => p.Ten);
                    break;
                default:
                    khachhangs = khachhangs.OrderBy(p => p.Ten);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(khachhangs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhachHang,Ho,Ten,Tuoi,DiaChi,Email,QuanTriVien,MatKhau,SDT")] KhachHang khachHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(khachHang);
            }
        }

        // GET: Admin/KhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhachHang,Ho,Ten,Tuoi,DiaChi,Email,QuanTriVien,MatKhau,SDT")] KhachHang khachHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(khachHang).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(khachHang);
            }
        }

        // GET: Admin/KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn)) { ViewData["Loi1"] = "<p stlye='color:red'>Phải nhập tên đăng nhập</p>"; }
            else if (String.IsNullOrEmpty(matkhau)) { ViewData["Loi2"] = "Phải nhập mật khẩu"; }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.Email == tendn &&
                                                                    n.MatKhau == matkhau);
                if (kh != null)
                {
                    //ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh.Ho + " " + kh.Ten;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không chính xác";
                }
            }
            return View();
        }
    }
}
