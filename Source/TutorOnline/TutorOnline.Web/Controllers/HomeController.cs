using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Common;

namespace TutorOnline.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["Role"] != null && User.Identity.IsAuthenticated)
            {
                string Rname = null;
                string Rid = null;
                if (Request.Cookies["Role"]["RoleId"] != null)
                {
                    Rid = Request.Cookies["Role"]["RoleId"];
                    Rname = HttpUtility.UrlDecode(Request.Cookies["Role"]["RoleName"]);
                }
                if (Rname == UserCommonString.Manager)
                {
                    string url = (Url.Action("Index", "DashboardManager"));
                    return Redirect(url);
                }
                else if (Rname == UserCommonString.SysAdmin)
                {
                    string url = (Url.Action("Index", "Users"));
                    return Redirect(url);
                }
                else if (Rname == UserCommonString.Accountant)
                {
                    string url = (Url.Action("Index", "Accountant"));
                    return Redirect(url);
                }else if(Rname == UserCommonString.Tutor)
                {
                    string url = (Url.Action("ViewSchedule", "Tutor"));
                    return Redirect(url);
                }else if(Rname == UserCommonString.Student)
                {
                    string url = (Url.Action("ViewSchedule", "Student"));
                    return Redirect(url);
                }            
            }
            return View();
        }
    }
}