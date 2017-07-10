using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;
using TutorOnline.Common;
using PagedList;
using TutorOnline.Web.Models;
using System.Net;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class MaterialsController : Controller
    {
        private LearningMaterialRepository LMRes = new LearningMaterialRepository();
        // GET: Materials
        public ActionResult Index()
        {
            return View();
        }
    }
}