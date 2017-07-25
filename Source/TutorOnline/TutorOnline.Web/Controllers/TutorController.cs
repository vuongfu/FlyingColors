using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutorOnline.Web.Controllers
{
    public class TutorController : Controller
    {
        // GET: Tutor
        public ActionResult Index()
        {
            return View();
        }
    }
}