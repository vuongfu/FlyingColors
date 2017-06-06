using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Entity;
using Tutor.Business.Repository;
using TutorOnline.DataAccess;

namespace TutorOnline.Web.Controllers
{
    public class AccountantController : Controller
    {
        // GET: Accountant
        public ActionResult Index()
        {
            return View();
        }
    }
}