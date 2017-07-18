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
        QuestionRepository QuesRes = new QuestionRepository();
        AnswersRepository AnsRes = new AnswersRepository();

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
            SubjectDetailViewModels SubjectDetail = new SubjectDetailViewModels();
            if(Subject != null)
            {
                SubjectDetail.Photo = Subject.Photo;
                SubjectDetail.Purpose = Subject.Purpose;
                SubjectDetail.Requirement = Subject.Requirement;
                //SubjectDetail.Student = Subject;
                SubjectDetail.SubjectId = Subject.SubjectId;
                SubjectDetail.SubjectName = Subject.SubjectName;
                SubjectDetail.Description = Subject.Description;
                SubjectDetail.CategoryId = Subject.CategoryId;
                SubjectDetail.ListLesson = LesRes.GetAllLessons().Where(s => s.SubjectId == Subject.SubjectId).ToList();
            }
            return View(SubjectDetail);
        }

        public ActionResult RegisterSubject()
        {
            ViewBag.Sucess = "Fail";
            return Json(new { registeredSubject = "ok" });

        }
        [HttpPost]
        //public ActionResult RegisterSubject([FromBody] SubjectDetailViewModels Subject)
        //{
        //    if (Subject != null)
        //    {
        //        StudentSubject StuSub;
        //        StuSub = StuSubRes.GetSubById(Subject.SubjectId, int.Parse(Request.Cookies["UserInfo"]["UserId"])).FirstOrDefault();
        //        if (StuSub != null)
        //        {
        //            Thêm check tiến độ.
        //            StuSub.Status = 8;
        //            StuSubRes.EditSubject(StuSub);
        //        }
        //        else
        //        {
        //            StuSub = new StudentSubject();
        //            StuSub.StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
        //            StuSub.SubjectId = Subject.SubjectId;
        //            StuSub.Status = 8;
        //            StuSubRes.AddSubject(StuSub);
        //        }
        //        return Json(new { registeredSubject = "Đã đăng ký" });
        //    }

        //    return Json(new { registeredSubject = "Đăng ký môn học thất bại" });
        //}

        public ActionResult Test(int? LessonId)
        {
            List<QuestionTestViewModels> listQuest = new List<QuestionTestViewModels>();
            if(LessonId != null)
            {
                var list = QuesRes.GetAllLesQuestion(LessonId).ToList();
                foreach (var item in list)
                {

                }
            }
            return View();
        }
    }
}