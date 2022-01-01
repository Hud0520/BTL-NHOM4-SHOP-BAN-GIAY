using BTL_NHOM4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_NHOM4.Controllers
{
    public class HomeController : Controller
    {
        DBQLBanGiay db = new DBQLBanGiay();
        public ActionResult Index()
        {
            var pro = (from sp in db.Giay
                       orderby sp.MaGiay descending
                       select sp).Take(4).ToList();
            var blog = (from bl in db.TinTuc
                        orderby bl.MaTinTuc descending
                        select bl).Take(4).ToList();
            var slide = (from sl in db.Slide
                         orderby sl.Id descending
                         select sl).Take(3).ToList();
            ViewBag.Slide = slide;
            ViewBag.SanPham = pro;
            ViewBag.TinTuc = blog;
            return View(ViewBag);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}