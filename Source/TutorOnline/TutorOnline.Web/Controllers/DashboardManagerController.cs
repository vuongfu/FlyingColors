using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class DashboardManagerController : Controller
    {
        CategoriesRepository CRes = new CategoriesRepository();
        SubjectsRepository SRes = new SubjectsRepository();
        LearningMaterialRepository LMRes = new LearningMaterialRepository();
        LessonRepository LRes = new LessonRepository();
        UsersRepository URes = new UsersRepository();
        // GET: DashboardManager
        public ActionResult Index()
        {
            //Categories statistic
            int numCategories = CRes.GetAllCategories().Count();
            ViewBag.numCategories = numCategories;
            //Subjects statistic
            int numSubjects = SRes.GetAllSubject().Count();
            int numSubJanpanese = SRes.GetSubCategory(1).Count();
            int numSubEnglish = SRes.GetSubCategory(2).Count();
            ViewBag.numSubjects = numSubjects;
            ViewBag.numSubJanpanese = numSubJanpanese;
            ViewBag.numSubEnglish = numSubEnglish;
            //Lesson statistic
            int numLessons = LRes.GetAllLessons().Count();
            int numLesJapanese = LRes.GetLesCategory(1).Count();
            int numLesEnglish = LRes.GetLesCategory(2).Count();
            ViewBag.numSubjects = numSubjects;
            ViewBag.numSubJanpanese = numSubJanpanese;
            ViewBag.numSubEnglish = numSubEnglish;
            //Material statistic
            int numMaterial = LMRes.GetAllMaterial().Count();
            int numMaJapanese = LMRes.GetMaCategory(1).Count();
            int numMaEnglish = LMRes.GetMaCategory(2).Count();
            ViewBag.numMaterial = numMaterial;
            ViewBag.numMaJapanese = numMaJapanese;
            ViewBag.numMaEnglish = numMaEnglish;

            return View();
        }
    }
}