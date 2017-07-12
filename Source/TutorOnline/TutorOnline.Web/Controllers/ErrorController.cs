using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TutorOnline.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult notfound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
    }
}