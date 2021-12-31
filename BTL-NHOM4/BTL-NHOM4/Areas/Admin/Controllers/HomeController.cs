using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_ASP.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "KhachHangs");
            }
            return View();
        }
    }
}