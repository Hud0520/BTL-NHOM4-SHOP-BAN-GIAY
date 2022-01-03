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
    public class GiaysController : Controller
    {
        private QLBanGiay db = new QLBanGiay();

        // GET: Admin/Giays
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "KhachHangs");
            }
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "name" : "";

            var giays = db.Giays.Select(p => p);

            if(!String.IsNullOrEmpty(searchString))
            {
                giays = giays.Where(p => p.TenGiay.Contains(searchString));
            }    

            switch (sortOrder)
            {
                case "name":
                    giays = giays.OrderBy(p => p.TenGiay);
                    break;
                default:
                    giays = giays.OrderBy(p => p.TenGiay);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(giays.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Giays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }

        // GET: Admin/Giays/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.TheLoais, "MaDanhMuc", "TenTheLoai");
            return View();
        }

        // POST: Admin/Giays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGiay,MaDanhMuc,TenGiay,Mau,KichThuoc,Gia,SoLuong,Anh,NgayTao,MoTa")] Giay giay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    giay.Anh = "";
                    var f = Request.Files["ImageFile"];
                    ViewBag.f = f;
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);

                        string UploadPath = Server.MapPath("~/wwwroot/img/_product/" + FileName);

                        f.SaveAs(UploadPath);

                        giay.Anh = FileName;
                        ViewBag.file = FileName;
                    }
                    ViewBag.MaDanhMuc = new SelectList(db.TheLoais, "MaDanhMuc", "TenTheLoai", giay.MaDanhMuc);
                    db.Giays.Add(giay);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(giay);
            }
        }

        // GET: Admin/Giays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.TheLoais, "MaDanhMuc", "TenTheLoai", giay.MaDanhMuc);
            return View(giay);
        }

        // POST: Admin/Giays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGiay,MaDanhMuc,TenGiay,Mau,KichThuoc,Gia,SoLuong,Anh,NgayTao,MoTa")] Giay giay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    giay.Anh = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);

                        string UploadPath = Server.MapPath("~/wwwroot/img/_product/" + FileName);

                        f.SaveAs(UploadPath);

                        giay.Anh = FileName;
                    }
                    ViewBag.MaDanhMuc = new SelectList(db.TheLoais, "MaDanhMuc", "TenTheLoai", giay.MaDanhMuc);
                    db.Entry(giay).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(giay);
            }
        }

        // GET: Admin/Giays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }

        // POST: Admin/Giays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Giay giay = db.Giays.Find(id);
            db.Giays.Remove(giay);
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
    }
}
