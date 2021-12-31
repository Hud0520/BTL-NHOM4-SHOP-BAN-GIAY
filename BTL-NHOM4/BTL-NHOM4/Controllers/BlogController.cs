using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_NHOM4.Models;
using PagedList;

namespace BTL_NHOM4.Controllers
{
    public class BlogController : Controller
    {
        DBQLBanGiay db = new DBQLBanGiay();
        // GET: Blog
        public ActionResult Index(string page)
        {
            int p = page == null || "".Equals(page) ? 1 : int.Parse(page);
            var list = (from bl in db.TinTuc
                        orderby bl.MaTinTuc descending
                        select bl
                        );
            return View(list.ToPagedList(p,1));
        }
        public ActionResult Detail(int id)
        {
            var tt = db.TinTuc.FirstOrDefault(t => t.MaTinTuc == id);
            var list = (from bl in db.TinTuc
                        orderby bl.MaTinTuc descending
                        select bl
                        ).Take(5).ToList();
            ViewData["ListTT"] = list;
            return View(tt);
        }
    }
}