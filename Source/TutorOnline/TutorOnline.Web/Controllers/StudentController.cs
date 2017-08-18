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
    [System.Web.Mvc.Authorize(Roles = UserCommonString.Student)]
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
        CategoriesRepository CateRes = new CategoriesRepository();
        FeedBackRepository FBRes = new FeedBackRepository();

        static int thisWeekNumber = DateAndWeekSelection.GetIso8601WeekOfYear(DateTime.Today);
        static DateTime firstDayOfWeek = DateAndWeekSelection.FirstDateOfWeek(DateTime.Today.Year, thisWeekNumber, CultureInfo.CurrentCulture);

        // GET: Student
        public ActionResult Index()
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

            int StudentId = int.Parse(Uid);

            ViewBag.StudentId = StudentId;
            ViewBag.Username = URes.GetAllStudentUser().Where(s => s.StudentId == StudentId).FirstOrDefault().UserName;
            return View();
        }
        public ActionResult ViewCategory(int? CategoryId, int? page )
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            List<SubjectsViewModels> ListSub = new List<SubjectsViewModels>();
            if(CategoryId != null)
            {
                var Subject = SubRes.GetAllSubject().Where(s => s.CategoryId == CategoryId);
                foreach (var item in Subject)
                {
                    SubjectsViewModels temp = new SubjectsViewModels();
                    temp.CategoryId = item.CategoryId;
                    temp.CategoryName = item.Category.CategoryName;
                    temp.Description = item.Description;
                    temp.Photo = item.Photo;
                    temp.Purpose = item.Purpose;
                    temp.SubjectId = item.SubjectId;
                    temp.SubjectName = item.SubjectName;
                    temp.Requirement = item.Requirement;
                    ListSub.Add(temp);
                }
            }
            ViewBag.totalRecord = ListSub.Count();
            return View(ListSub.OrderBy(x => x.SubjectId).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ViewAllCategory()
        {
            List<SubjectsViewModels> ListSub = new List<SubjectsViewModels>();
                var Subject = SubRes.GetAllSubject().OrderBy(x => x.SubjectId);
                foreach (var item in Subject)
                {
                    SubjectsViewModels temp = new SubjectsViewModels();
                    temp.CategoryName = item.Category.CategoryName;
                    temp.CategoryId = item.CategoryId;
                    temp.Description = item.Description;
                    temp.Photo = item.Photo;
                    temp.Purpose = item.Purpose;
                    temp.SubjectId = item.SubjectId;
                    temp.SubjectName = item.SubjectName;
                    temp.Requirement = item.Requirement;
                    ListSub.Add(temp);
                }
            List<List<SubjectsViewModels>> Result = new List<List<SubjectsViewModels>>();
            List<Category> ListCate = CateRes.GetAllCategories().ToList();

            foreach(var item in ListCate)
            {
                List<SubjectsViewModels> temp = new List<SubjectsViewModels>();
                temp = ListSub.Where(x => x.CategoryId == item.CategoryId).ToList();

                Result.Add(temp);
            }
            
            return View(Result);
        }

        public ActionResult ViewStudentSubject()
        {
            List<SubjectsViewModels> ListSub = new List<SubjectsViewModels>();
            if (Request.Cookies["UserInfo"]["UserId"] != null)
            {
                int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
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
                if(temp.Count != 0)
                {
                    Result.Add(temp);
                }               
            }

            return View(Result);
        }

        public ActionResult SubjectDetail(int SubjectId)
        {
            int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            StudentSubject StuSubject = StuSubRes.GetSubById(SubjectId, StudentId).FirstOrDefault();
            if (StuSubject != null && StuSubject.Status == 6)
            {
                ViewBag.isRegistered = 1;
            }
            Subject Subject = SubRes.GetAllSubject().Where(s => s.SubjectId == SubjectId).FirstOrDefault();
            SubjectDetailViewModels SubjectDetail = new SubjectDetailViewModels();
            if (Subject != null)
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

        //Install DotNetZip Nuget
        public FileResult Download (int LessonId)
        {
            Lesson Lesson = LesRes.FindLesson(LessonId);
            var outputStream = new MemoryStream();

            using (var zip = new ZipFile())
            {
                foreach (var item in Lesson.LearningMaterials)
                {
                    String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\Uploads\Documents\" + item.MaterialUrl);
                    zip.AddItem(path, "");
                }
                zip.Save(outputStream);
            }

            outputStream.Position = 0;
            return File(outputStream, "application/zip", Lesson.LessonName + ".zip");
            
        }

        public ActionResult GetFeedBack(int? LessonId, int? ScheduleId)
        {
            TutorFeedback returnData = new TutorFeedback();
            if (LessonId != null)
            {
                int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
                returnData = FBRes.GetTutorFeedBackByLesson(LessonId.Value, StudentId).FirstOrDefault();
            }
            if (ScheduleId != null)
            {
                returnData = FBRes.GetTutorFeedBackBySchedule(ScheduleId.Value).FirstOrDefault();
            }

            List<FeedBackDetail> Result = new List<FeedBackDetail>();

            
            if(returnData != null)
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
            } else
            {
                return Json(new { Exist = false }, JsonRequestBehavior.AllowGet);
            }

            
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
                    if (StuSub.StudiedLesson >= LesRes.GetAllLessons().Where(s => s.SubjectId == Subject.SubjectId).Count())
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
            if (LessonId != null)
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

            return Json(new { registeredSubject = "Bạn đã trả lời đúng " + score + " trên " + Result.ListAnswer.Count + " câu." });
        }

        public ActionResult FeedBack(int SlotId)
        {
            return View();
        }

        public ActionResult GetSlotDetail(int id)
        {
            Schedule data = ScheRes.getSlotById(id);
            DetailBookedSlotByStudent model = new DetailBookedSlotByStudent();

            if (data != null)
            {
                model.Category = data.Lesson.Subject.Category.CategoryName;
                model.Date = data.OrderDate;
                model.Email = data.Tutor.Email;
                model.FullName = data.Tutor.LastName + " " + data.Tutor.FirstName;
                model.Gender = data.Tutor.Gender == 1 ? "Nam" : "Nữ";
                model.Lesson = data.Lesson.LessonName;
                string time = "";
                int slot = data.OrderSlot;
                if (slot <= 4)
                    time = String.Format("{0:00}", 7 + slot) + ":00 - " + String.Format("{0:00}", 7 + slot) + ":45";
                else if (slot > 4 && slot <= 8)
                    time = String.Format("{0:00}", 12 + slot) + ":00 - " + String.Format("{0:00}", 12 + slot) + ":45";
                else
                    time = String.Format("{0:00}", 18 + slot) + ":00 - " + String.Format("{0:00}", 18 + slot) + ":45";

                model.OrderTime = time;
                model.OrderSlot = data.OrderSlot;
                model.Phone = data.Tutor.PhoneNumber;
                model.Photo = data.Tutor.Photo;
                model.ScheduleId = data.ScheduleId;
                model.Skype = data.Tutor.SkypeId;
                model.Subject = data.Lesson.Subject.SubjectName;
                model.Status = data.Status;
                if (model.Status == 3)
                    model.StatusName = StatusString.ScheduleFinished;
                else if (model.Status == 4)
                    model.StatusName = StatusString.ScheduleBooked;
                else if (model.Status == 5)
                    model.StatusName = StatusString.SchudulueCanceled;
                else
                    model.StatusName = StatusString.SchudulueAvailabled;

                List<int> dataForCriteId = new List<int>();
                List<string> dataForCriteContent = new List<string>();

                dataForCriteId.Add(0);
                dataForCriteContent.Add("Đánh giá");

                model.CriteriaId = dataForCriteId;
                model.CriteriaContent = dataForCriteContent;
                model.ScheduleDate = data.OrderDate;
                var feedback = TurRes.FindFeedbackForStudent(id);
                model.Comment = "";
                if (feedback != null)
                    model.Comment = feedback.Comment;
                var now = DateTime.Now;

            }


            return View(model);
        }

        [HttpPost]
        public ActionResult SaveFeedback(List<int> listCreId, List<int> listValue, int scheduleId, string comment)
        {

            var slot = ScheRes.getSlotById(scheduleId);

            int check = TurRes.getTutorFeedbackId(slot.TutorId, slot.ScheduleId, (int)slot.StudentId);
            if (check != -1)
                return Json(false, JsonRequestBehavior.AllowGet);

            StudentFeedback feedback = new StudentFeedback();
            feedback.TutorId = slot.TutorId;
            feedback.StudentId = (int)slot.StudentId;
            feedback.LessonId = (int)slot.LessonId;
            feedback.ScheduleId = scheduleId;
            feedback.FeedbackDate = DateTime.Now;
            feedback.Comment = comment;
            feedback.Rate = listValue[0];
            FBRes.AddStudentFeedback(feedback);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckFeedBack(int ScheduleId)
        {
            var temp = FBRes.FindFeedbackForTutor(ScheduleId);
            List<ViewFeedbackViewModel> listData = new List<ViewFeedbackViewModel>();
            if (temp == null)
                return Json(false, JsonRequestBehavior.AllowGet);

                ViewFeedbackViewModel data = new ViewFeedbackViewModel();
                data.criteriaId = 0;
                data.value = temp.Rate;

                listData.Add(data);

            return Json(listData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookSlot(String SelectedDate, int Week, int LessonId, int SubjectId)
        {
            int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            StudentSubject StudentSubject = StuSubRes.GetSubById(SubjectId, StudentId).FirstOrDefault();
            //change to today
            DateTime Date = DateTime.Today;
            Date = Date.AddDays(Week * 7);
            DateTime ChoosedDate = DateTime.ParseExact(SelectedDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            new LogWriter("ChoosedDate = " + ChoosedDate.ToString());
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
                
                if (Tutor != null)
                {
                    temp.Id = Tutor.TutorId;
                    temp.Photo = Tutor.Photo;
                    temp.Description = Tutor.Description;
                    temp.Name = Tutor.LastName + " " + Tutor.FirstName;
                    temp.ListSchedule = TutorSchedule;
                    ListSchedule.Add(temp);
                }

            }
            return View(ListSchedule);
        }



        public ActionResult Book(int ScheduleId, int LessonId, int type, String BookTime)
        {
            Schedule Slot = ScheRes.FindSchedule(ScheduleId);
            Slot.LessonId = LessonId;
            Slot.Status = 4;
            Slot.Type = type;
            try {
                Slot.StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            } catch
            {
                return Json(new { BookSlot = false, Message = "Xin mời đăng nhập lại để thực hiện thao tác." });
            }

            var BookedSlot = StuRes.GetAllSlotBookedByStudent(DateTime.Today, DateTime.Today.AddDays(14), Slot.StudentId.Value);

            DateTime SlotTime = Slot.OrderDate.AddHours(int.Parse(GetSlotTime(Slot.OrderSlot)));
            if( SlotTime < DateTime.Parse(BookTime))
            {
                return Json(new { BookSlot = false, Message = "Đã qua thời gian để có thể đặt tiết học này." });
            }

            var CheckSlot = BookedSlot.Where(x => x.LessonId == LessonId).FirstOrDefault();
            if (CheckSlot != null && CheckSlot.Status == 4)
            {
                return Json(new { BookSlot = false, Message = "Bạn đã đặt bài học cho tiết học này rồi." });
            }

            Lesson CheckLesson = LesRes.FindLesson(LessonId);
            if(CheckLesson == null)
            {
                return Json(new { BookSlot = false, Message = "Không tồn tại tiết học này." });
            }

            StudentSubject CheckSubject = StuSubRes.GetSubById(CheckLesson.SubjectId, Slot.StudentId).FirstOrDefault();
            if(CheckSubject == null)
            {
                return Json(new { BookSlot = false, Message = "Bạn phải đăng ký môn học trước khi có thể đặt tiết học này." });
            }
            if (CheckLesson.Order > CheckSubject.StudiedLesson + 1)
            {
                return Json(new { BookSlot = false, Message = "Bạn phải hoàn thành một số tiết học khác trước để có thể đặt tiết học này." });
            }

            if (type == 1)
            {
                Student Student = URes.FindStudentUser(Slot.StudentId);
                if (Student.Balance - Slot.Price < 0)
                {
                    return Json(new { BookSlot = false, Message = "Tài khoản của bạn không đủ để thực hiện thao tác này." });
                } else {
                    Transaction Tran = new Transaction();
                    Tran.Amount = Slot.Price * -1;
                    Tran.Content = "Trừ tiền tiết học " + Slot.ScheduleId + " của học viên " + Student.UserName;
                    Tran.UserID = Student.StudentId;
                    Tran.UserType = 1;
                    Tran.TranDate = DateTime.Now;
                    AccRes.Add(Tran);
                    if (Slot.Price != 0)
                    {
                        Student.Balance = Student.Balance - Slot.Price;
                        URes.EditStudentUser(Student);
                    }
                }
            }

            ScheRes.EditSchedule(Slot);
            return Json(new { BookSlot = true, Message = "Đặt tiết học thành công." });
        }


        [HttpPost]
        public ActionResult Cancel(int ScheduleId)
        {
            Schedule Slot = ScheRes.FindSchedule(ScheduleId);
            DateTime SlotTime = new DateTime(Slot.OrderDate.Year, Slot.OrderDate.Month, Slot.OrderDate.Day, int.Parse(GetSlotTime(Slot.OrderSlot)), 0, 0);
            if (SlotTime <= DateTime.Now.AddDays(1))
            {
                return Json(new { BookSlot = false, Message = "Bạn phải hủy tiết học trước ít nhất 24 tiếng." });
            }
            Slot.Status = 11;
            Slot.LessonId = null;
            ScheRes.EditSchedule(Slot);
            String SlotTimeNew = GetSlotTime(Slot.OrderSlot) + ":00";

            if (Slot.Type == 1)
            {
                Student Student = URes.FindStudentUser(Slot.StudentId);
                Transaction Tran = new Transaction();
                Tran.Amount = Slot.Price;
                Tran.Content = "Hoàn tiền buổi học " + Slot.ScheduleId + " của học viên " + Student.UserName;
                Tran.UserID = Student.StudentId;
                Tran.UserType = 1;
                Tran.TranDate = DateTime.Now;
                AccRes.Add(Tran);
                Student.Balance = Student.Balance + Slot.Price;
                URes.EditStudentUser(Student);
            }
            return Json(new { BookSlot = true, Time = SlotTimeNew });
        }

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

            int StudentId = int.Parse(Uid);

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
        
        private string GetSlotTime(int SlotOrder)
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

        public PartialViewResult ViewTransaction(String Content, String StartDate, String EndDate, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Count = pageSize * (pageNumber - 1) + 1;

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            int StudentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]); ;
            List<Transaction> ListTrans = new List<Transaction>();
            if (StudentId != 0)
            {
                
                ListTrans = AccRes.GetAllTrans().Where(x => x.UserID == StudentId && x.UserType == 1).ToList();

                try
                {
                    DateTime SDate = DateTime.ParseExact(StartDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                    new LogWriter("SDate = " + SDate.ToString());
                    ListTrans = ListTrans.Where(s => s.TranDate.Date >= SDate.Date).ToList();
                }
                catch (Exception e) { new LogWriter(e.ToString()); }

                try
                {
                    DateTime EDate = DateTime.ParseExact(EndDate, "d/m/yyyy", CultureInfo.InvariantCulture);
                    new LogWriter("EDate = " + EDate.ToString());
                    ListTrans = ListTrans.Where(s => s.TranDate.Date <= EDate.Date).ToList();
                }
                catch (Exception e) { new LogWriter(e.ToString()); }

                if (!String.IsNullOrEmpty(Content))
                {

                    ListTrans = ListTrans.Where(s => AccRes.SearchForString(s.Content, Content)).ToList();
                }

                ViewBag.searchClick = true;
                ViewBag.totalRecord = "Số kết quả tìm được: " + ListTrans.Count();

                return PartialView(ListTrans.OrderBy(x => x.TranDate).ToPagedList(pageNumber, pageSize));
            }
            return PartialView();
        }

        public ActionResult MoveViewTutorInfo(int id, int SubjectId, String Link)
        {
            TempData["Link"] = Link;
            return RedirectToAction("Search", new { Id = id , SubjectId = SubjectId});
        }

        public ActionResult ViewTutorInfo(int id, int SubjectId)
        {
            Tutor Tur = TurRes.FindTutor(id);
            TutorSubject TutorSub = Tur.TutorSubjects.Where(s => s.SubjectId ==  SubjectId).FirstOrDefault();

            String Link = "";
            TutorDetail ViewTutor = new TutorDetail();
            try
            {
                Link = TempData["Link"].ToString();
                TempData["Link"] = Link;
            }
            catch { }
            if(Link == "")
            {
                Link = Url.Action("Index", "Student");
            }
            
            TempData.Keep();
            ViewTutor.TutorSubject = TutorSub;
            ViewTutor.Tutor = Tur;
            ViewBag.Link = Link;
            return View(ViewTutor);
        }


    }
}
