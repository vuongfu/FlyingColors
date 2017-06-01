using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutor.Business.Repository;

namespace Tutor.Web.UI.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectRepository SR = new SubjectRepository();
        // GET: Subject
        public ActionResult Index()
        {
            return View(SR.GetAll());
        }
    }
}