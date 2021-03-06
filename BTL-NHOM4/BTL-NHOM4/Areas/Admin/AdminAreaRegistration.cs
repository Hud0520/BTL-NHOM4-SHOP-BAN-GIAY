using System.Web.Mvc;

namespace BTL_NHOM4.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller="Home",action = "Index", id = UrlParameter.Optional },
                new[] { "BTL_ASP.Areas.Admin.Controllers" }
            );
        }
    }
}