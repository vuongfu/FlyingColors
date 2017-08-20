using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Common;
using TutorOnline.DataAccess;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = UserCommonString.Tutor)]
    public class TutorController : Controller
    {
        private TutorRepository TuRes = new TutorRepository();
        private ScheduleRespository SchRes = new ScheduleRespository();
        private StudentSubjectRepository SSRes = new StudentSubjectRepository();
        private LessonRepository LRes = new LessonRepository();
        private AccountantRepository AccRes = new AccountantRepository();
        private SubjectsRepository SubRes = new SubjectsRepository();
        private TutorSubjectRepository TuSubRes = new TutorSubjectRepository();

        static int thisWeekNumber = DateAndWeekSelection.GetIso8601WeekOfYear(DateTime.Today);
        static DateTime firstDayOfWeek = DateAndWeekSelection.FirstDateOfWeek(DateTime.Today.Year, thisWeekNumber, CultureInfo.CurrentCulture);

        public ActionResult TutorBookSlot()
        {
            DateTime firstDayWeek1 = firstDayOfWeek;
            ViewBag.CurrentWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.FirstWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            firstDayWeek1 = firstDayWeek1.AddDays(7);
            ViewBag.SecondWeek = firstDayWeek1.ToShortDateString() + " - " + firstDayWeek1.AddDays(6).ToShortDateString();

            ViewBag.FirstDayOfWeek = firstDayOfWeek;
            ViewBag.Today = DateTime.Now.ToShortDateString();

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }
            int tutorId = int.Parse(Uid);
            var week0 = SchRes.GetAllSlotInTwoDates(firstDayOfWeek, firstDayOfWeek.AddDays(6), tutorId);
            var week1 = SchRes.GetAllSlotInTwoDates(firstDayOfWeek.AddDays(7), firstDayOfWeek.AddDays(13), tutorId);
            var week2 = SchRes.GetAllSlotInTwoDates(firstDayOfWeek.AddDays(14), firstDayOfWeek.AddDays(20), tutorId);
            var SlotBookedByStudent = SchRes.GetAllSlotBookedByStudentNotStart(firstDayOfWeek, firstDayOfWeek.AddDays(20), tutorId);

            List<string> SlotOfWeek0 = MapEntityToModel(week0, firstDayOfWeek);
            List<string> SlotOfWeek1 = MapEntityToModel(week1, firstDayOfWeek.AddDays(7));
            List<string> SlotOfWeek2 = MapEntityToModel(week2, firstDayOfWeek.AddDays(14));
            List<string> SlotBooked = new List<string>();

            string[] namesDays = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;

            foreach (var item in SlotBookedByStudent)
            {
                int week = ((item.OrderDate - firstDayOfWeek).Days) / 7;
                int numDay = ((item.OrderDate - firstDayOfWeek).Days) % 7;
                string day = namesDays[numDay];
                string temp = day + String.Format("{0:00}", item.OrderSlot);
                temp += week;
                SlotBooked.Add(temp);
            }

            ViewBag.SlotBooked0 = SlotOfWeek0;
            ViewBag.SlotBooked1 = SlotOfWeek1;
            ViewBag.SlotBooked2 = SlotOfWeek2;
            ViewBag.SlotBookedByStudent = SlotBooked;

            return View();
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

        [HttpPost]
        public ActionResult CancelSlot(string slot, int week, string reason)
        {
            DateTime temp = new DateTime();
            if (week == 0)
            {
                temp = firstDayOfWeek.AddDays(0);
            }
            else if (week == 1)
            {
                temp = firstDayOfWeek.AddDays(7);
            }
            else
            {
                temp = firstDayOfWeek.AddDays(14);
            }

            TutorBookSlotViewModel tempData = MapModelToEntity(slot, temp);

            SchRes.CancelSlot(tempData.TutorId, int.Parse(tempData.OrderSlot), tempData.OrderDate, reason);

            return Json("Hủy thành công", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveSlot(List<string> Week0, List<string> Week1, List<string> Week2)
        {
            List<TutorBookSlotViewModel> SlotBooked = new List<TutorBookSlotViewModel>();
            if (Week0 != null)
            {
                foreach (string item in Week0)
                {
                    TutorBookSlotViewModel tempData = MapModelToEntity(item, firstDayOfWeek);
                    SlotBooked.Add(tempData);
                }
            }

            if (Week1 != null)
            {
                foreach (string item in Week1)
                {
                    TutorBookSlotViewModel tempData = MapModelToEntity(item, firstDayOfWeek.AddDays(7));
                    SlotBooked.Add(tempData);
                }
            }

            if (Week2 != null)
            {
                foreach (string item in Week2)
                {
                    TutorBookSlotViewModel tempData = MapModelToEntity(item, firstDayOfWeek.AddDays(14));
                    SlotBooked.Add(tempData);
                }
            }


            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }
            int tutorId = int.Parse(Uid);
            var SlotBookedGetFromDB = SchRes.GetAllSlotInTwoDates(firstDayOfWeek, firstDayOfWeek.AddDays(20), tutorId);

            foreach (var item in SlotBooked)
            {
                var orderslot = int.Parse(item.OrderSlot);
                var Slot = SlotBookedGetFromDB.FirstOrDefault(x => x.OrderDate == item.OrderDate && x.OrderSlot == orderslot && x.TutorId == item.TutorId);
                if (Slot == null)
                {
                    Schedule data = new Schedule();
                    data.TutorId = item.TutorId;
                    data.OrderSlot = orderslot;
                    data.OrderDate = item.OrderDate;
                    data.Price = TuRes.GetPriceOfSlot(item.TutorId);
                    data.Status = 11;
                    SchRes.AddSlotBookedByTutor(data);
                }
            }

            List<int> DeleteList = new List<int>();
            foreach (var item in SlotBookedGetFromDB)
            {
                var orderslot = String.Format("{0:00}", item.OrderSlot);
                var check = SlotBooked.FirstOrDefault(x => x.OrderDate == item.OrderDate && x.OrderSlot == orderslot && x.TutorId == item.TutorId);
                if (check == null)
                {
                    DeleteList.Add(item.ScheduleId);
                }
            }

            foreach (int id in DeleteList)
            {
                SchRes.DeleteSlotBookedOfTutor(id);
            }

            return Json("Lưu thành công", JsonRequestBehavior.AllowGet);
        }

        public TutorBookSlotViewModel MapModelToEntity(string data, DateTime startDate)
        {
            TutorBookSlotViewModel temp = new TutorBookSlotViewModel();
            string Day = data.Substring(0, 3);
            string Slot = data.Substring(3);
            switch (Day)
            {
                case "Sun":
                    temp.OrderDate = startDate;
                    break;
                case "Mon":
                    temp.OrderDate = startDate.AddDays(1);
                    break;
                case "Tue":
                    temp.OrderDate = startDate.AddDays(2);
                    break;
                case "Wed":
                    temp.OrderDate = startDate.AddDays(3);
                    break;
                case "Thu":
                    temp.OrderDate = startDate.AddDays(4);
                    break;
                case "Fri":
                    temp.OrderDate = startDate.AddDays(5);
                    break;
                case "Sat":
                    temp.OrderDate = startDate.AddDays(6);
                    break;
                default:
                    break;
            }
            temp.OrderSlot = Slot;

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            temp.TutorId = int.Parse(Uid);
            return temp;
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

            int tutorId = int.Parse(Uid);

            ViewBag.tutorId = tutorId;

            return View();
        }

        [HttpPost]
        public ActionResult GetBookedSlotByStudent(DateTime startDate, DateTime endDate, int TutorId)
        {
            var week0 = SchRes.GetAllSlotBookedByStudent(startDate, endDate.AddHours(20), TutorId);
            List<BookedSlotByStudent> returnData = new List<BookedSlotByStudent>();
            List<string> SlotOfWeek0 = MapEntityToModel(week0, startDate);

            for (int i = 0; i < week0.Count(); i++)
            {
                BookedSlotByStudent temp = new BookedSlotByStudent();
                temp.Status = week0.ElementAt(i).Status;
                temp.tableSlotId = SlotOfWeek0[i];
                if (temp.Status != 11)
                {
                    temp.ScheduleId = week0.ElementAt(i).ScheduleId;
                    temp.StudentName = week0.ElementAt(i).Student.FirstName;
                    temp.LessonName = week0.ElementAt(i).Lesson.LessonName;
                }
                returnData.Add(temp);
            }

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSlotDetail(int id)
        {
            Schedule data = SchRes.getSlotById(id);
            DetailBookedSlotByStudent model = new DetailBookedSlotByStudent();
            var dataStudentSubject = data.Student.StudentSubjects;
            StudentSubject SSubject = dataStudentSubject.FirstOrDefault(x => x.SubjectId == data.Lesson.SubjectId);
            ViewBag.LessonId = new SelectList(LRes.GetAllLessons().Where(s => s.SubjectId == data.Lesson.SubjectId && s.Order <= (SSubject.StudiedLesson + 1)).
                OrderBy(x => x.Order).ToList(), "LessonId", "LessonName", data.LessonId);


            if (data != null)
            {
                model.Category = data.Lesson.Subject.Category.CategoryName;
                model.Date = data.OrderDate;
                model.Email = data.Student.Email;
                model.FullName = data.Student.LastName + " " + data.Student.FirstName;
                model.Gender = data.Student.Gender == 1 ? "Nam" : "Nữ";
                model.Lesson = data.Lesson.LessonName;
                string time = "";
                int slot = data.OrderSlot;
                if (slot <= 4)
                    time = String.Format("{0:00}", 7 + slot) + ":00 - " + String.Format("{0:00}", 7 + slot) + ":45";
                else if (slot > 4 && slot <= 8)
                    time = String.Format("{0:00}", 8 + slot) + ":00 - " + String.Format("{0:00}", 8 + slot) + ":45";
                else
                    time = String.Format("{0:00}", 10 + slot) + ":00 - " + String.Format("{0:00}", 10 + slot) + ":45";

                model.OrderTime = time;
                model.OrderSlot = data.OrderSlot;
                model.Phone = data.Student.PhoneNumber;
                model.Photo = data.Student.Photo;
                model.ScheduleId = data.ScheduleId;
                model.Skype = data.Student.SkypeId;
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

                var criteria = TuRes.getCriteriaForLesson((int)data.LessonId);
                List<int> dataForCriteId = new List<int>();
                List<string> dataForCriteContent = new List<string>();
                for (int i = 0; i < criteria.Count(); i++)
                {
                    var cri = criteria.ElementAt(i);
                    dataForCriteId.Add(cri.CriteriaId);
                    dataForCriteContent.Add(cri.CriteriaName);
                }
                model.CriteriaId = dataForCriteId;
                model.CriteriaContent = dataForCriteContent;
                model.ScheduleDate = data.OrderDate;
                var feedback = TuRes.FindFeedbackForStudent(id);
                model.Comment = "";
                if (feedback != null)
                    model.Comment = feedback.Comment;
                var now = DateTime.Now;

            }


            return View(model);
        }

        public ActionResult CheckFeedBack(int ScheduleId, int lessonId)
        {
            bool isfeedback = false;
            var slot = SchRes.getSlotById(ScheduleId);
            if (lessonId > 0)
                if (slot.LessonId != lessonId)
                {
                    slot.LessonId = lessonId;
                    SchRes.UpdateSlot(slot);
                }
            var temp = TuRes.FindFeedbackForStudent(ScheduleId);


            List<ViewFeedbackViewModel> listData = new List<ViewFeedbackViewModel>();
            if (temp == null)
            {
                var criteria = TuRes.getCriteriaForLesson((lessonId));
                List<int> dataForCriteId = new List<int>();
                List<string> dataForCriteContent = new List<string>();
                for (int i = 0; i < criteria.Count(); i++)
                {
                    var cri = criteria.ElementAt(i);
                    dataForCriteId.Add(cri.CriteriaId);
                    dataForCriteContent.Add(cri.CriteriaName);
                }
                return Json(new { isfeedback, dataForCriteId, dataForCriteContent }, JsonRequestBehavior.AllowGet);
            }


            foreach (var item in temp.TutorFeedbackDetails)
            {
                ViewFeedbackViewModel data = new ViewFeedbackViewModel();
                data.criteriaId = item.CriteriaId;
                data.value = item.CriteriaValue;

                listData.Add(data);
            }

            return Json(listData, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TuRes.Dispose();
                SchRes.Dispose();
                LRes.Dispose();
                AccRes.Dispose();
                SubRes.Dispose();
                TuSubRes.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult SaveFeedback(List<int> listCreId, List<int> listValue, int scheduleId, string comment)
        {

            var slot = SchRes.getSlotById(scheduleId);

            int check = TuRes.getTutorFeedbackId(slot.TutorId, slot.ScheduleId, (int)slot.StudentId);
            if (check != -1)
                return Json(false, JsonRequestBehavior.AllowGet);

            TutorFeedback feedback = new TutorFeedback();
            feedback.TutorId = slot.TutorId;
            feedback.StudentId = (int)slot.StudentId;
            feedback.LessonId = (int)slot.LessonId;
            feedback.ScheduleId = scheduleId;
            feedback.FeedbackDate = DateTime.Now;
            feedback.Comment = comment;
            TuRes.AddTutorFeedback(feedback);

            int feedbackId = TuRes.getTutorFeedbackId(feedback.TutorId, feedback.ScheduleId, feedback.StudentId);

            for (int i = 0; i < listCreId.Count; i++)
            {
                TutorFeedbackDetail data = new TutorFeedbackDetail();
                data.TutorFeedbackId = feedbackId;
                data.CriteriaId = listCreId[i];
                data.CriteriaValue = listValue[i];
                TuRes.AddTutorFeedbackDetail(data);
            }

            slot.Status = 3;
            SchRes.UpdateSlot(slot);
            var subject = SSRes.GetSubById(slot.Lesson.SubjectId, slot.StudentId);
            var dataStuSubject = subject.FirstOrDefault();
            dataStuSubject.StudiedLesson++;
            SSRes.EditSubject(dataStuSubject);
            var currentTutor = TuRes.FindTutor(slot.TutorId);
            currentTutor.Balance += (slot.Price / 2);
            TuRes.UpdateTutor(currentTutor);

            Transaction dataTrans = new Transaction();
            dataTrans.Amount = slot.Price / 2;
            dataTrans.Content = "Tiền lương cho buổi dạy vào slot " + slot.OrderSlot + ", ngày " + slot.OrderDate.ToShortDateString();
            dataTrans.TranDate = DateTime.Now;
            dataTrans.UserID = slot.TutorId;
            dataTrans.UserType = 2;
            AccRes.Add(dataTrans);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegistTutorSubject()
        {
            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            int tutorId = int.Parse(Uid);

            Tutor user = TuRes.FindTutor(tutorId);
            ViewBag.CurrentTutor = user;

            var tempDataId = TuSubRes.GetListSubjectIdOfTutor(tutorId);
            List<RegistTutorSubjectViewModel> DropDownSubject = new List<Models.RegistTutorSubjectViewModel>();
            List<RegistTutorSubjectViewModel> returnList = new List<Models.RegistTutorSubjectViewModel>();

            var AllSubject = SubRes.GetAllSubject();
            foreach (var item in AllSubject)
            {
                RegistTutorSubjectViewModel temp = new Models.RegistTutorSubjectViewModel();
                temp.SubjectId = item.SubjectId;
                temp.SubjectName = item.SubjectName;
                DropDownSubject.Add(temp);
            }

            foreach (var item in DropDownSubject)
            {
                if (!tempDataId.Exists(y => y == item.SubjectId))
                {
                    returnList.Add(item);
                }
            }


            ViewBag.TutorSubjectId = new SelectList(returnList, "SubjectId", "SubjectName");
            ViewBag.ListSubject = returnList;

            return View();
        }

        [HttpPost]
        public ActionResult SaveSubject(List<int> subjectId, List<string> exp)
        {
            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            int tutorId = int.Parse(Uid);

            for (int i = 0; i < subjectId.Count; i++)
            {
                TutorSubject data = new TutorSubject();
                data.Experience = exp[i];
                data.Status = 7;
                data.SubjectId = subjectId[i];
                data.TutorId = tutorId;

                TuSubRes.AddTutorSubject(data);
            }

            return Json("Đã đăng ký thành công.", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewTransaction(int? page, string searchString, string StartDate, string EndDate)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Count = pageSize * (pageNumber - 1) + 1;

            ViewBag.SearchStr = searchString;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.searchClick = true;

            List<TransactionListViewModels> ListTrans = new List<TransactionListViewModels>();

            if ((searchString == null || StartDate == null || EndDate == null) && page == null)
            {
                ViewBag.searchClick = false;
                ViewBag.totalRecord = ListTrans.Count();
                return View(ListTrans.ToPagedList(pageNumber, pageSize));
            }

            

            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            int tutorId = int.Parse(Uid);

            var dataTran = AccRes.GetAllTrans();
            dataTran = dataTran.Where(x => x.UserID == tutorId && x.UserType == 2);


            foreach (var record in dataTran)
            {
                TransactionListViewModels temp = new TransactionListViewModels();
                temp.TransactionId = record.TransactionId;
                temp.Content = record.Content;
                temp.Amount = (int)record.Amount;
                temp.TranDate = record.TranDate;
                temp.UserID = record.UserID;
                temp.UserType = record.UserType;
                if (record.UserType == 1)
                {
                    temp.UserTypeName = "Học sinh";
                }
                else
                {
                    temp.UserTypeName = "Gia sư";
                }

                ListTrans.Add(temp);
            }


            if (!string.IsNullOrEmpty(searchString))
            {
                ListTrans = ListTrans.Where(s => AccRes.SearchForString(s.Content, searchString)).ToList();
            }

            if (!string.IsNullOrEmpty(StartDate))
            {
                DateTime SDate = DateTime.ParseExact(StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                new LogWriter("SDate = " + SDate.ToString());
                ListTrans = ListTrans.Where(s => s.TranDate.Date >= SDate.Date).ToList();
            }
                

            if (!string.IsNullOrEmpty(EndDate))
            {
                DateTime EDate = DateTime.ParseExact(EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                new LogWriter("EDate = " + EDate.ToString());
                ListTrans = ListTrans.Where(s => s.TranDate.Date <= EDate.Date).ToList();
            }

            ViewBag.totalRecord = ListTrans.Count();

            return View(ListTrans.OrderBy(x => x.TransactionId).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ViewInfo(int id)
        {
            var tutor = TuRes.FindTutor(id);
            DetailTutorViewModel returnData = new DetailTutorViewModel();
            returnData.Address = tutor.Address;
            returnData.BankId = tutor.BankId;
            returnData.BankName = tutor.BankName;
            returnData.BirthDate = tutor.BirthDate;
            returnData.BMemName = tutor.BMemName;
            returnData.City = tutor.City;
            returnData.Country = tutor.Country;
            returnData.Description = tutor.Description;
            returnData.Email = tutor.Email;
            returnData.FullName = tutor.LastName + " " + tutor.FirstName;
            returnData.Gender = (tutor.Gender == 1 ? "Nam" : "Nữ");
            returnData.PhoneNumber = tutor.PhoneNumber;
            returnData.Photo = (tutor.Photo == null?"DefaultIcon.png":tutor.Photo);
            returnData.PostalCode = tutor.PostalCode;
            returnData.SkypeId = tutor.SkypeId;
            returnData.UserName = tutor.UserName;
            returnData.Balance = tutor.Balance;
            returnData.RoleName = tutor.Role.RoleName;

            List<TutorSubjectViewModels> tutorSubData = new List<TutorSubjectViewModels>();
            foreach(var item in tutor.TutorSubjects)
            {
                TutorSubjectViewModels temp = new TutorSubjectViewModels();
                if (item.Status == 6)
                {
                    temp.experiences = item.Experience;
                    temp.subjectName = item.Subject.SubjectName;
                    temp.TutorSubjectId = item.SubjectId;

                    tutorSubData.Add(temp);
                }
            }

            returnData.tutorSub = tutorSubData;
            return View(returnData);
        }

        [HttpPost]
        public ActionResult ChangePass(string oldPass, string newPass)
        {
            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            int tutorId = int.Parse(Uid);

            var tutor = TuRes.FindTutor(tutorId);

            string checkPass = tutor.Password;

            if (!checkPass.Equals(oldPass, StringComparison.CurrentCulture))
            {
                return Json(new { stt = false, mess = "Mật khẩu cũ không chính xác." }, JsonRequestBehavior.AllowGet);
            }

            tutor.Password = newPass;
            TuRes.UpdateTutor(tutor);

            return Json(new { stt = true, mess = "Đã thay đổi mật khẩu thành công." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit()
        {
            string Uid = "";
            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["UserId"] != null)
                {
                    Uid = Request.Cookies["UserInfo"]["UserId"];
                }
            }

            int tutorId = int.Parse(Uid);

            var tutor = TuRes.FindTutor(tutorId);

            EditTutorViewModel returnData = new EditTutorViewModel();

            returnData.Address = tutor.Address;
            returnData.BankId = tutor.BankId;
            returnData.BankName = tutor.BankName;
            returnData.BirthDate = tutor.BirthDate;
            returnData.BMemName = tutor.BMemName;
            returnData.City = tutor.City;
            returnData.Country = tutor.Country;
            returnData.Description = tutor.Description;
            returnData.FirstName = tutor.FirstName;
            returnData.Gender = tutor.Gender;
            returnData.Id = tutor.TutorId;
            returnData.LastName = tutor.LastName;
            returnData.PhoneNumber = tutor.PhoneNumber;
            returnData.Photo = tutor.Photo;
            returnData.potalCode = tutor.PostalCode;
            returnData.skypeId = tutor.SkypeId;

            ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key", tutor.Country);
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text", tutor.Gender);

            return View(returnData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditTutorViewModel model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (!IsImage(file))
                {
                    ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text");

                    TempData["messageWarning"] = "Bạn chỉ được chọn 1 trong các loại file sau: png, jpg, jpeg, gif.";

                    return View(model);
                }
            }
            if (ModelState.IsValid)
            {
                string photoUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);

                Tutor data = TuRes.FindTutor(model.Id);
                data.Address = model.Address;
                data.BankId = model.BankId;
                data.BankName = model.BankName;
                data.BirthDate = model.BirthDate;
                data.BMemName = model.BMemName;
                data.City = model.City;
                data.Country = model.Country;
                data.Description = model.Description;
                data.FirstName = model.FirstName;
                data.Gender = model.Gender;
                data.LastName = model.LastName;
                data.PhoneNumber = model.PhoneNumber;
                data.Photo = (string.IsNullOrEmpty(photoUrl)?model.Photo:photoUrl);
                data.PostalCode = model.potalCode;
                data.SkypeId = model.skypeId;

                HttpCookie cookie = Request.Cookies["Avata"];
                if (cookie != null)
                {
                    cookie.Values["AvaName"] = data.Photo;
                }else
                {
                    cookie= new HttpCookie("Avata");
                    cookie.Values["AvaName"] = data.Photo;
                }
                Response.Cookies.Add(cookie);

                TuRes.UpdateTutor(data);
                TempData["message"] = "Đã cập nhặt thông tin của người dùng " + data.UserName + " thành công.";
                return RedirectToAction("ViewInfo", "Tutor", new { id = model.Id });
            }
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text");
        
            TempData["messageWarning"] = "Đã có lỗi xảy ra khi cập nhật thông tin của người dùng.";
            ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key", model.Country);
            return View(model);
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if u like...

            foreach (var item in formats)
            {
                if (file.FileName.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public KeyValuePair<string, string>[] GetAllCountries()
        {
            var objDict = new Dictionary<string, string>();
            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var regionInfo = new RegionInfo(cultureInfo.Name);
                if (!objDict.ContainsKey(regionInfo.EnglishName))
                {
                    objDict.Add(regionInfo.EnglishName, regionInfo.TwoLetterISORegionName.ToLower());
                }
            }
            var obj = objDict.OrderBy(p => p.Key).ToArray();


            return obj;
        }
    }
}