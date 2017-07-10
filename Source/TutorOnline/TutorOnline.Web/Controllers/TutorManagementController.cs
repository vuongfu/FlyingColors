using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class TutorManagementController : Controller
    {
        [Authorize(Roles = "Manager")]
        // GET: TutorManagement
        public ActionResult Index()
        {
            return View();
        }
    }
}