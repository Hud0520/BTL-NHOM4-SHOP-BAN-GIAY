using BTL_NHOM4.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NHOM4.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        DBQLBanGiay db = new DBQLBanGiay();
        public ActionResult Index(string page,string theloai,string keyword,string sgia, string egia)
        {
            int pageSize = 2;
            int p = page == null || "".Equals(page) ? 1 : int.Parse(page);
            var lsdm = (from dm in db.TheLoai
                        select dm).ToList();
            ViewData["LsDM"] = lsdm;
            int TheLoai = theloai == null || "".Equals(theloai) ? 0 : int.Parse(theloai);
            ViewBag.TheLoai = TheLoai;
            if(keyword != null)
            {
                ViewBag.Keyword = keyword;
                float isgia = sgia == null || "".Equals(sgia) ? 0 : float.Parse(sgia);
                float iegia = egia == null || "".Equals(egia) ? 99999999 : float.Parse(egia);
                ViewBag.sgia = sgia;
                ViewBag.egia = egia;
                return View((from sp in db.Giay
                       where (sp.TenGiay.ToLower().Contains(keyword.ToLower()) || keyword == "")
                       &&(sp.MaDanhMuc == TheLoai || TheLoai == 0)
                       && (sp.Gia>= isgia) && (sp.Gia <=iegia)
                       orderby sp.MaGiay descending
                       select sp).ToPagedList(p, pageSize ));
            }
            else
            {
                var lssp = (from sp in db.Giay
                            where sp.MaDanhMuc == TheLoai || TheLoai == 0
                            orderby sp.MaGiay descending
                            select sp).ToPagedList(p, pageSize);
                return View(lssp);
            }
        }

        public ActionResult ProductDetail(int? id)
        {
            Giay dt = db.Giay.FirstOrDefault(g => g.MaGiay == id);
            if(dt != null)
            {
                return View(dt);
            }
            else
            {
                return RedirectToAction("Index","Shop");
            }
            
        }
    }
}