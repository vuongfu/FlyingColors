using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Common;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = UserCommonString.Manager)]
    public class DashboardManagerController : Controller
    {
        CategoriesRepository CRes = new CategoriesRepository();
        SubjectsRepository SRes = new SubjectsRepository();
        LearningMaterialRepository LMRes = new LearningMaterialRepository();
        LessonRepository LRes = new LessonRepository();
        UsersRepository URes = new UsersRepository();
        TutorRepository TRes = new TutorRepository();
        StudentRepository StuRes = new StudentRepository();
        // GET: DashboardManager
        public ActionResult Index()
        {
            //Categories statistic
            int numCategories = CRes.GetAllCategories().Count();
            ViewBag.numCategories = numCategories;
            //Subjects statistic
            int numSubjects = SRes.GetAllSubject().Count();
            int numSubJanpanese = SRes.GetSubByCategory(1).Count();
            int numSubEnglish = SRes.GetSubByCategory(2).Count();
            ViewBag.numSubjects = numSubjects;
            ViewBag.numSubJanpanese = numSubJanpanese;
            ViewBag.numSubEnglish = numSubEnglish;
            //Lesson statistic
            int numLessons = LRes.GetAllLessons().Count();
            int numLesJapanese = LRes.GetLesCategory(1).Count();
            int numLesEnglish = LRes.GetLesCategory(2).Count();
            ViewBag.numLessons = numLessons;
            ViewBag.numLesJapanese = numLesJapanese;
            ViewBag.numLesEnglish = numLesEnglish;
            //Material statistic
            int numMaterial = LMRes.GetAllMaterial().Count();
            int numMaJapanese = LMRes.GetMaCategory(1).Count();
            int numMaEnglish = LMRes.GetMaCategory(2).Count();
            ViewBag.numMaterial = numMaterial;
            ViewBag.numMaJapanese = numMaJapanese;
            ViewBag.numMaEnglish = numMaEnglish;
            //Tutor statistic
            int numTutor = TRes.GetAllTutor().Count();
            int numTuVN = TRes.GetTuByCountry(new ManagerStringCommon().vn.ToString()).Count();
            int numTuForeign = TRes.GetTuByCountry(new ManagerStringCommon().foreign.ToString()).Count();
            int numTuNotApproved = TRes.GetAllPretutor().Count();
            ViewBag.numTutor = numTutor;
            ViewBag.numTuVN = numTuVN;
            ViewBag.numTuForeign = numTuForeign;
            ViewBag.numTuNotApproved = numTuNotApproved;
            //Student statistic
            int numStudent = StuRes.GetAllStudent().Count();
            int numStuVN = StuRes.GetStuByCountry(new ManagerStringCommon().vn.ToString()).Count();
            int numStuForeign = StuRes.GetStuByCountry(new ManagerStringCommon().foreign.ToString()).Count();
            int numStuInMonth = StuRes.GetStuInMonth().Count();
            ViewBag.numStudent = numStudent;
            ViewBag.numStuVN = numStuVN;
            ViewBag.numStuForeign = numStuForeign;
            ViewBag.numStuInMonth = numStuInMonth;

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CRes.Dispose();
                SRes.Dispose();
                LMRes.Dispose();
                LRes.Dispose();
                URes.Dispose();
                TRes.Dispose();
                StuRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}