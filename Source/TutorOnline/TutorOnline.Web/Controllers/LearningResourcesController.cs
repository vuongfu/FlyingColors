﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class LearningResourcesController : Controller
    {
        // GET: LearningResources
        public ActionResult Index()
        {
            return View();
        }
    }
}