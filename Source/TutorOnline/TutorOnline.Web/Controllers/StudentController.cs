using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Web.Models;
using TutorOnline.DataAccess;

namespace TutorOnline.Web.Controllers
{
    public class StudentController : Controller
    {
        SubjectsRepository SubRes = new SubjectsRepository();
        LessonRepository LesRes = new LessonRepository();
        StudentSubjectRepository StuSubRes = new StudentSubjectRepository();

        // GET: Student
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult ViewCategory( int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var Subject = SubRes.GetAllSubject().Where(s => s.CategoryId == 1);
            List<SubjectsViewModels> ListSub = new List<SubjectsViewModels>();
            foreach(var item in Subject)
            {
                SubjectsViewModels temp = new SubjectsViewModels();
                temp.CategoryId = item.CategoryId;
                temp.Description = item.Description;
                temp.Photo = item.Photo;
                temp.Purpose = item.Purpose;
                temp.SubjectId = item.SubjectId;
                temp.SubjectName = item.SubjectName;
                temp.Requirement = item.Requirement;
                ListSub.Add(temp);
            }
            ViewBag.totalRecord = ListSub.Count();
            return View(ListSub.OrderBy(x => x.SubjectId).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SubjectDetail(int SubjectId)
        {
            int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            StudentSubject StuSubject = StuSubRes.GetSubById(SubjectId, StudentId).FirstOrDefault();
            if(StuSubject != null)
            {
                ViewBag.isRegistered = 1;
            }
            Subject Subject = SubRes.GetAllSubject().Where(s => s.SubjectId == SubjectId).FirstOrDefault();
            return View(Subject);
        }
    }
}