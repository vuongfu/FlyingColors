using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Web.Models;
using TutorOnline.DataAccess;
using System.Globalization;
using TutorOnline.Common;
using System.IO;
using Ionic.Zip;

namespace TutorOnline.Web.Controllers
{
    [System.Web.Mvc.Authorize(Roles = UserCommonString.Parent)]
    public class ParentController : Controller
    {
        SubjectsRepository SubRes = new SubjectsRepository();
        LessonRepository LesRes = new LessonRepository();
        StudentSubjectRepository StuSubRes = new StudentSubjectRepository();
        QuestionRepository QuesRes = new QuestionRepository();
        AnswersRepository AnsRes = new AnswersRepository();
        TutorRepository TurRes = new TutorRepository();
        ScheduleRespository ScheRes = new ScheduleRespository();
        StudentRepository StuRes = new StudentRepository();
        AccountantRepository AccRes = new AccountantRepository();
        UsersRepository URes = new UsersRepository();
        CategoriesRepository CateRes = new CategoriesRepository();
        FeedBackRepository FBRes = new FeedBackRepository();

        static int thisWeekNumber = DateAndWeekSelection.GetIso8601WeekOfYear(DateTime.Today);
        static DateTime firstDayOfWeek = DateAndWeekSelection.FirstDateOfWeek(DateTime.Today.Year, thisWeekNumber, CultureInfo.CurrentCulture);

        public ActionResult ViewSchedule()
        {
            DateTime firstDayWeek1 = firstDayOfWeek;
            ViewBag.CurrentWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.FirstWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.SecondWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            ViewBag.FirstDayOfWeek = firstDayOfWeek;

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            Parent Result = URes.GetAllParentUser().Where(s => s.ParentId == int.Parse(Uid)).FirstOrDefault();

            int StudentId = Result.Students.FirstOrDefault().StudentId;

            ViewBag.StudentId = StudentId;

            return View();
        }

        [HttpPost]
        public ActionResult GetBookedSlot(DateTime startDate, DateTime endDate, int StudentId)
        {
            var week0 = StuRes.GetAllSlotBookedByStudent(startDate, endDate, StudentId);
            List<BookedSlot> returnData = new List<BookedSlot>();
            List<string> SlotOfWeek0 = MapEntityToModel(week0, startDate);

            for (int i = 0; i < week0.Count(); i++)
            {
                BookedSlot temp = new BookedSlot();
                temp.Status = week0.ElementAt(i).Status;
                temp.tableSlotId = SlotOfWeek0[i];
                if (temp.Status != 11)
                {
                    temp.ScheduleId = week0.ElementAt(i).ScheduleId;
                    temp.TutorName = week0.ElementAt(i).Tutor.FirstName;
                    temp.LessonName = week0.ElementAt(i).Lesson.LessonName;
                }
                returnData.Add(temp);
            }

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        private List<string> MapEntityToModel(IEnumerable<Schedule> ListData, DateTime StartDate)
        {
            List<string> returnList = new List<string>();
            string[] namesDays = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;

            foreach (var item in ListData)
            {
                string day = namesDays[(item.OrderDate - StartDate).Days];
                string temp = day + String.Format("{0:00}", item.OrderSlot);
                returnList.Add(temp);
            }
            return returnList;
        }

        public ActionResult ViewHistory(String CategoryId, String SubjectId, int? page)
        {

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Count = pageSize * (pageNumber - 1) + 1;

            ViewBag.Category = CategoryId;
            ViewBag.Subject = SubjectId;

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            Parent Result = URes.GetAllParentUser().Where(s => s.ParentId == int.Parse(Uid)).FirstOrDefault();
            int StudentId = Result.Students.FirstOrDefault().StudentId;

            List<Category> ListCate = new List<Category>();

            var StuSub = StuSubRes.GetAllSubject();
            foreach(var item in StuSub)
            {
                int count = 0;
                Category Cate = item.Subject.Category;
                foreach(var temp in ListCate)
                {
                    if (temp.CategoryId == Cate.CategoryId)
                    {
                        count++;
                    }
                }

                if(count == 0)
                {
                    ListCate.Add(Cate);
                }
            }

            ViewBag.CategoryId = new SelectList(ListCate, "CategoryId", "CategoryName");
            List<Subject> ListSub = new List<Subject>();
            if (!String.IsNullOrEmpty(CategoryId))
            {
                var ListStuSub = StuSubRes.GetAllSubject().Where(s => s.StudentId == StudentId && s.Subject.CategoryId == int.Parse(CategoryId)).ToList();
                foreach (var item in ListStuSub)
                {
                    ListSub.Add(item.Subject);
                }
            }
            ViewBag.SubjectId = new SelectList(ListSub, "SubjectId", "SubjectName");
            List<Schedule> Schedule = new List<Schedule>();

            if ((CategoryId == null || SubjectId == null) && page == null)
            {
                ViewBag.searchClick = false;
                ViewBag.totalRecord = Schedule.Count();
                return View(Schedule.ToPagedList(pageNumber, pageSize));
            }

            Schedule = ScheRes.GetAllStudentSchedule(StudentId).OrderBy(s => s.OrderDate).ToList();
            if (!String.IsNullOrEmpty(CategoryId) && String.IsNullOrEmpty(SubjectId))
            {
                Schedule.Where(s => s.Lesson.Subject.CategoryId == int.Parse(CategoryId));
            }

            if(!String.IsNullOrEmpty(SubjectId))
            {
                StudentSubject Stu = StuSub.Where(s => s.SubjectId == int.Parse(SubjectId)).FirstOrDefault();
                Schedule.Where(s => s.Lesson.SubjectId == Stu.SubjectId);
            }

            ViewBag.searchClick = true;
            ViewBag.totalRecord = Schedule.Count();

            return View(Schedule.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult GetSubjectList(int CategoryId)
        {
            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            Parent Par = URes.GetAllParentUser().Where(s => s.ParentId == int.Parse(Uid)).FirstOrDefault();
            int StudentId = Par.Students.FirstOrDefault().StudentId;

            var Result = StuSubRes.GetAllSubject().Where(s => s.Subject.CategoryId == CategoryId).Select(a => "<option value='" + a.SubjectId + "'>" + a.Subject.SubjectName + "</option>'");
            List<String> ListSub = new List<string>();
            ListSub.Add("<option value=0>Tất cả</option>");
            foreach (var item in Result)
            {
                ListSub.Add(item);
            }
            return Content(String.Join("", ListSub));
        }

        public ActionResult GetFeedBack(int? LessonId, int? ScheduleId)
        {
            TutorFeedback returnData = new TutorFeedback();
            if (ScheduleId != null)
            {
                returnData = FBRes.GetTutorFeedBackBySchedule(ScheduleId.Value).FirstOrDefault();
            }

            List<FeedBackDetail> Result = new List<FeedBackDetail>();


            if (returnData != null)
            {
                foreach (var item in returnData.TutorFeedbackDetails)
                {
                    FeedBackDetail temp = new FeedBackDetail();
                    temp.CriterionName = item.Criterion.CriteriaName;
                    temp.CriterionValue = item.CriteriaValue.ToString();
                    Result.Add(temp);
                }
                FeedBackDetail Test = new FeedBackDetail();
                Test.CriterionName = "Kết quả kiểm tra";
                if (returnData.TestResult == null)
                {
                    Test.CriterionValue = "Chưa làm bài kiểm tra";
                }
                else
                {
                    Test.CriterionValue = returnData.TestResult.ToString();
                }

                Result.Add(Test);

                FeedBackDetail Comment = new FeedBackDetail();
                if (returnData.Comment != null)
                {
                    Comment.CriterionValue = returnData.Comment;
                }
                else
                {
                    Comment.CriterionValue = "Không có phản hồi thêm cho bài học này";
                }

                Comment.CriterionName = "Phản hồi khác";

                Result.Add(Comment);
                return Json(new { Result = Result, Count = returnData.TutorFeedbackDetails.Count, Exist = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Exist = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ViewStudentSubject()
        {
            List<SubjectsViewModels> ListSub = new List<SubjectsViewModels>();
            if (Request.Cookies["UserInfo"]["UserId"] != null)
            {
                int Uid = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
                Parent Par = URes.GetAllParentUser().Where(s => s.ParentId == Uid).FirstOrDefault();
                int StudentId = Par.Students.FirstOrDefault().StudentId;
                
                var StudentSubject = StuSubRes.GetAllSubject().Where(x => x.StudentId == StudentId && x.Status == 8).OrderBy(x => x.SubjectId);
                foreach (var item in StudentSubject)
                {
                    SubjectsViewModels temp = new SubjectsViewModels();
                    temp.CategoryName = item.Subject.Category.CategoryName;
                    temp.CategoryId = item.Subject.CategoryId;
                    temp.Description = item.Subject.Description;
                    temp.Photo = item.Subject.Photo;
                    temp.Purpose = item.Subject.Purpose;
                    temp.SubjectId = item.Subject.SubjectId;
                    temp.SubjectName = item.Subject.SubjectName;
                    temp.Requirement = item.Subject.Requirement;
                    ListSub.Add(temp);
                }
            }
            List<List<SubjectsViewModels>> Result = new List<List<SubjectsViewModels>>();
            List<Category> ListCate = CateRes.GetAllCategories().ToList();

            foreach (var item in ListCate)
            {
                List<SubjectsViewModels> temp = new List<SubjectsViewModels>();
                temp = ListSub.Where(x => x.CategoryId == item.CategoryId).ToList();
                if (temp.Count != 0)
                {
                    Result.Add(temp);
                }
            }

            return View(Result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                URes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}