using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class TutorManagementController : Controller
    {
        public ActionResult Index(string searchString, string roleString, int? status, int? page)
        {
            return View();
        }
    }
}