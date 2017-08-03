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
    [System.Web.Mvc.Authorize(Roles = "Student")]
    public class StudentController : Controller
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

        // GET: Student
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult ViewCategory( int? page, int Category)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var Subject = SubRes.GetAllSubject().Where(s => s.CategoryId == Category);
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
            if(StuSubject != null && StuSubject.Status != 7)
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

                if (StuSubject != null) { SubjectDetail.StudiedLesson = StuSubject.StudiedLesson; }
                else { SubjectDetail.StudiedLesson = 0; }
                
                SubjectDetail.SubjectId = Subject.SubjectId;
                SubjectDetail.SubjectName = Subject.SubjectName;
                SubjectDetail.Description = Subject.Description;
                SubjectDetail.CategoryId = Subject.CategoryId;
                SubjectDetail.ListLesson = LesRes.GetAllLessons().Where(s => s.SubjectId == Subject.SubjectId).ToList();
            }
            return View(SubjectDetail);
        }

        //install Microsoft.AspNet.WebApi.Core
        [System.Web.Mvc.HttpPost]
        public ActionResult RegisterSubject(StudentSubject Subject)
        {
            int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            if (Subject != null)
            {
                StudentSubject StuSub;
                StuSub = StuSubRes.GetSubById(Subject.SubjectId, StudentId).FirstOrDefault();
                if (StuSub != null)
                {
                    //Thêm check tiến độ.
                    if(StuSub.StudiedLesson >= LesRes.GetAllLessons().Where(s => s.SubjectId == Subject.SubjectId).Count())
                    {
                        StuSub.Status = 10;
                    } else { StuSub.Status = 8; }
                    StuSubRes.EditSubject(StuSub);
                }
                else
                {
                    StuSub = new StudentSubject();
                    StuSub.StudentId = StudentId;
                    StuSub.SubjectId = Subject.SubjectId;
                    StuSub.Status = 8;
                    StuSubRes.AddSubject(StuSub);
                }
                return Json(new { registeredSubject = "Đăng ký môn học thành công" });
            }

            return Json(new { registeredSubject = "Đăng ký môn học thất bại" });
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult UnRegisterSubject(StudentSubject Subject)
        {

            if (Subject != null)
            {
                StudentSubject StuSub;
                StuSub = StuSubRes.GetSubById(Subject.SubjectId, int.Parse(Request.Cookies["UserInfo"]["UserId"])).FirstOrDefault();
                if (StuSub != null)
                {
                    StuSub.Status = 9;
                    StuSubRes.EditSubject(StuSub);
                }
                return Json(new { registeredSubject = "Hủy đăng ký môn học thành công" });
            }

            return Json(new { registeredSubject = " Hủy đăng ký môn học thất bại" });
        }

        public ActionResult Test(int? LessonId)
        {
            List<QuestionTestViewModels> listQuest = new List<QuestionTestViewModels>();
            if(LessonId != null)
            {
                var list = QuesRes.GetAllLesQuestion(LessonId).ToList();
                foreach (var item in list)
                {
                    QuestionTestViewModels temp = new QuestionTestViewModels();
                    temp.Content = item.Content;
                    temp.LessonId = item.LessonId;
                    temp.Photo = item.Photo;
                    temp.QuestionId = item.QuestionId;
                    temp.ListAnswer = AnsRes.GetAllAnswers(item.QuestionId).ToList();
                    listQuest.Add(temp);
                }
            }
            return View(listQuest);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CheckScore(StudentTestAnswerViewModels Result)
        {
            int score = 0;
            List<QuestionTestViewModels> listQuest = new List<QuestionTestViewModels>();

            var list = QuesRes.GetAllLesQuestion(Result.LessonId).ToList();
            foreach (var item in list)
            {
                QuestionTestViewModels temp = new QuestionTestViewModels();
                temp.Content = item.Content;
                temp.LessonId = item.LessonId;
                temp.Photo = item.Photo;
                temp.QuestionId = item.QuestionId;
                temp.ListAnswer = AnsRes.GetAllAnswers(item.QuestionId).ToList();
                listQuest.Add(temp);
            }

            for (int i = 0; i < Result.ListAnswer.Count(); i++)
            {
                if (listQuest[i].ListAnswer[Result.ListAnswer[i]].isCorrect == true)
                {
                    score++;
                }
            }

            return Json(new { registeredSubject = "Bạn đã trả lời đúng "+score+" trên " + Result.ListAnswer.Count + " câu."});
        }

        public ActionResult FeedBack(int SlotId)
        {
            return View();
        }

        //public ActionResult BookSlot()
        //{
        //    int SubjectId = 1;
        //    //change to today
        //    DateTime Date = DateTime.Parse("07/8/2017");
        //    ViewBag.LessonId = 1;
        //    ViewBag.Date = Date;
        //    ViewBag.SelectedDate = Date;
        //    ViewBag.SubjectId = SubjectId;
        //    ViewBag.StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
        //    List<IEnumerable<Schedule>> TutorSchedule = ScheRes.GetTutorSchedule(Date, SubjectId);
        //    return View(TutorSchedule);
        //}

        public ActionResult BookSlot(String SelectedDate, int Week, int LessonId, int SubjectId)
        {
            int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            StudentSubject StudentSubject = StuSubRes.GetSubById(SubjectId, StudentId).FirstOrDefault();
            //change to today
            DateTime Date = DateTime.Parse("07/8/2017");
            Date = Date.AddDays(Week * 7);
            DateTime ChoosedDate = DateTime.Parse(SelectedDate);
            ViewBag.LessonId = LessonId;
            ViewBag.Date = Date;
            ViewBag.SelectedDate = ChoosedDate;
            ViewBag.StudentId = StudentId;
            ViewBag.SubjectId = SubjectId;
            ViewBag.Week = Week;
            List<TutorSubject> TutorSubject = ScheRes.GetTutorSubject(SubjectId);
            List<TutorScheduleViewModels> ListSchedule = new List<TutorScheduleViewModels>();
            List<Tutor> ListTutor = TurRes.GetAllTutor().ToList();
            foreach (var item in TutorSubject)
            {
                Tutor Tutor = ListTutor.Where(x => x.TutorId == item.TutorId).FirstOrDefault();
                List<Schedule> TutorSchedule = ScheRes.GetTutorSchedule(ChoosedDate, item.TutorId);
                TutorScheduleViewModels temp = new TutorScheduleViewModels();
                temp.ListSchedule = TutorSchedule;
                if(Tutor != null)
                {
                    temp.Photo = Tutor.Photo;
                    temp.Description = Tutor.Description;
                    temp.Name = Tutor.FirstName + " " + Tutor.LastName;
                }


                ListSchedule.Add(temp);
            }
            
            return View(ListSchedule);
        }


        
        public ActionResult Book(int ScheduleId, int LessonId, int type)
        {
            Schedule Slot = ScheRes.FindSchedule(ScheduleId);
            Slot.LessonId = LessonId;
            Slot.Status = 4;
            Slot.Type = type;
            try {
                Slot.StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            } catch
            {
                return Json(new { BookSlot = false, Message ="Xin mời đăng nhập lại để thực hiện thao tác." });
            }
            

            if(type == 1)
            {
                Student Student = URes.FindStudentUser(Slot.StudentId);
                if(Student.Balance - Slot.Price < 0)
                {
                    return Json(new { BookSlot = false, Message = "Tài khoản của bạn không đủ để thực hiện thao tác này." });
                } else { 
                    Transaction Tran = new Transaction();
                    Tran.Amount = Slot.Price * -1;
                    Tran.Content = "Trừ tiền buổi học " + Slot.ScheduleId + " của học viên " + Student.UserName;
                    Tran.UserID = Student.StudentId;
                    Tran.UserType = 6;
                    Tran.TranDate = DateTime.Today;
                    AccRes.Add(Tran);
                    if(Slot.Price != 0)
                    {
                        Student.Balance = Student.Balance - Slot.Price;
                        URes.EditStudentUser(Student);
                    }                    
                } 
            }

            ScheRes.EditSchedule(Slot);
            return Json(new { BookSlot = true, Message = "Đặt lịch học thành công." });
        }


        [HttpPost]
        public ActionResult Cancel(int ScheduleId)
        {
            Schedule Slot = ScheRes.FindSchedule(ScheduleId);
            DateTime SlotTime = new DateTime(Slot.OrderDate.Year, Slot.OrderDate.Month, Slot.OrderDate.Day,int.Parse(GetSlotTime(Slot.OrderSlot)),0,0);
            if(SlotTime <= DateTime.Now.AddDays(4))
            {
                return Json(new { BookSlot = false, Message = "Bạn phải hủy môn học trước ít nhất 24 tiếng." });
            }
            Slot.Status = 11;
            Slot.LessonId = null;
            ScheRes.EditSchedule(Slot);
            String SlotTimeNew = GetSlotTime(Slot.OrderSlot) + ":00";

            if(Slot.Type == 1)
            {
                Student Student = URes.FindStudentUser(Slot.StudentId);
                Transaction Tran = new Transaction();
                Tran.Amount = Slot.Price;
                Tran.Content = "Hoàn tiền buổi học " + Slot.ScheduleId + " của học viên " + Student.UserName;
                Tran.UserID = Student.StudentId;
                Tran.UserType = 6;
                Tran.TranDate = DateTime.Today;
                AccRes.Add(Tran);
                Student.Balance = Student.Balance + Slot.Price;
                URes.EditStudentUser(Student);
            }
            return Json(new { BookSlot = true,  Time = SlotTimeNew});
        }

        public string GetSlotTime(int SlotOrder)
        {
            String time = "";
            switch (SlotOrder)
            {
                case 1:
                    time = "8";
                    break;
                case 2:
                    time = "9";
                    break;
                case 3:
                    time = "10";
                    break;
                case 4:
                    time = "11";
                    break;
                case 5:
                    time = "13";
                    break;
                case 6:
                    time = "14";
                    break;
                case 7:
                    time = "15";
                    break;
                case 8:
                    time = "16";
                    break;
                case 9:
                    time = "19";
                    break;
                case 10:
                    time = "20";
                    break;
                case 11:
                    time = "21";
                    break;
                default:
                    time = null;
                    break;
            }
            return time;
        }
    }
}
