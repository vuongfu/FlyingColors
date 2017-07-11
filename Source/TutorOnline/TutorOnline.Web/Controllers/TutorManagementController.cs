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
        // GET: TutorManagement
        public ActionResult Index()
        {
            return View();
        }
    }
}