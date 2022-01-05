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
    public class TinTucsController : Controller
    {
        private QLBanGiay db = new QLBanGiay();

        // GET: Admin/TinTucs
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "KhachHangs");
            }
            ViewBag.SapTheoTieuDe = String.IsNullOrEmpty(sortOrder) ? "title" : "";

            var tintucs = db.TinTucs.Select(p => p);

            if (!String.IsNullOrEmpty(searchString))
            {
                tintucs = tintucs.Where(p => p.TieuDe.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title":
                    tintucs = tintucs.OrderBy(p => p.TieuDe);
                    break;
                default:
                    tintucs = tintucs.OrderBy(p => p.TieuDe);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(tintucs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/TinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinTuc,TieuDe,NgayTao,NgaySua,link,NoiDung,AnhTieuDe,NguoiTao")] TinTuc tinTuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tinTuc.AnhTieuDe = "";
                    var f = Request.Files["ImageFile"];
                    ViewBag.f = f;
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);

                        string UploadPath = Server.MapPath("~/wwwroot/img/_blog/" + FileName);

                        f.SaveAs(UploadPath);

                        tinTuc.AnhTieuDe = FileName;
                        ViewBag.file = FileName;
                    }
                    db.TinTucs.Add(tinTuc);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(tinTuc);
            }
        }

        // GET: Admin/TinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinTuc,TieuDe,NgayTao,NgaySua,link,NoiDung,AnhTieuDe,NguoiTao")] TinTuc tinTuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(tinTuc.AnhTieuDe != null && tinTuc.AnhTieuDe.Length > 4)
                    {

                    }
                    else { 
                        tinTuc.AnhTieuDe = "";
                        var f = Request.Files["ImageFile"];
                        ViewBag.f = f;
                        if (f != null && f.ContentLength > 0)
                        {
                            string FileName = System.IO.Path.GetFileName(f.FileName);

                            string UploadPath = Server.MapPath("~/wwwroot/img/_blog/" + FileName);

                            f.SaveAs(UploadPath);

                            tinTuc.AnhTieuDe = FileName;
                            ViewBag.file = FileName;
                        }
                    }
                    db.Entry(tinTuc).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(tinTuc);
            }
        }

        // GET: Admin/TinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
