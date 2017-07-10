using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class LearningResourcesController : Controller
    {
        [Authorize(Roles = "Manager")]
        // GET: LearningResources
        public ActionResult Index()
        {
            return View();
        }
    }
}