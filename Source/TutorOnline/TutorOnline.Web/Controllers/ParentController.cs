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
                if (temp.Status != 11 && temp.Status != 5)
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

            Schedule = ScheRes.GetAllStudentSchedule(StudentId).Where(s => s.Status == 3).OrderBy(s => s.OrderDate).ToList();
            if (!String.IsNullOrEmpty(CategoryId) && String.IsNullOrEmpty(SubjectId))
            {
                Schedule.Where(s => s.Lesson.Subject.CategoryId == int.Parse(CategoryId));
            }

            if(!String.IsNullOrEmpty(SubjectId))
            {
                StudentSubject Stu = StuSub.Where(s => s.SubjectId == int.Parse(SubjectId)).FirstOrDefault();
                Schedule = Schedule.Where(s => s.Lesson.SubjectId == Stu.SubjectId).ToList();
            }

            foreach(var item in Schedule)
            {
                DateTime temp = new DateTime(item.OrderDate.Year, item.OrderDate.Month, item.OrderDate.Day, int.Parse(GetSlotTime(item.OrderSlot)), 0, 0);
                item.OrderDate = temp;
            }

            ViewBag.searchClick = true;
            ViewBag.totalRecord = Schedule.Count();

            return View(Schedule.ToPagedList(pageNumber, pageSize));
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

        public ActionResult ViewParentInfo()
        {
            int ParentId = 0;
            if (Request.Cookies["UserInfo"]["UserId"] != null)
            {
                ParentId = int.Parse(Request.Cookies["UserInfo"]["UserId"]);
            }


            Parent Parent = URes.GetAllParentUser().Where(s => s.ParentId == ParentId).FirstOrDefault();
            DetailTutorViewModel returnData = new DetailTutorViewModel();
            returnData.Address = Parent.Address;
            returnData.BirthDate = Parent.BirthDate;
            returnData.City = Parent.City;
            returnData.Country = Parent.Country;
            returnData.Description = Parent.Description;
            returnData.Email = Parent.Email;
            returnData.FullName = Parent.LastName + " " + Parent.FirstName;
            returnData.Gender = (Parent.Gender == 1 ? "Nam" : "Nữ");
            returnData.PhoneNumber = Parent.PhoneNumber;
            returnData.Photo = (Parent.Photo == null ? "DefaultIcon.png" : Parent.Photo);
            returnData.PostalCode = Parent.PostalCode;
            returnData.SkypeId = Parent.SkypeId;
            returnData.UserName = Parent.UserName;
            returnData.Balance = Parent.Balance;
            returnData.RoleName = Parent.Role.RoleName;

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

            int ParentId = int.Parse(Uid);

            var Parent = URes.FindParentUser(ParentId);

            string checkPass = Parent.Password;

            if (!checkPass.Equals(oldPass, StringComparison.CurrentCulture))
            {
                return Json(new { stt = false, mess = "Mật khẩu cũ không chính xác." }, JsonRequestBehavior.AllowGet);
            }

            Parent.Password = newPass;
            URes.EditParentUser(Parent);

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

            int ParentId = int.Parse(Uid);

            var Parent = URes.GetAllParentUser().Where(s => s.ParentId == ParentId).FirstOrDefault();

            EditTutorViewModel returnData = new EditTutorViewModel();

            returnData.Address = Parent.Address;
            returnData.BirthDate = Parent.BirthDate;
            returnData.City = Parent.City;
            returnData.Country = Parent.Country;
            returnData.Description = Parent.Description;
            returnData.FirstName = Parent.FirstName;
            returnData.Gender = Parent.Gender;
            returnData.Id = Parent.ParentId;
            returnData.LastName = Parent.LastName;
            returnData.PhoneNumber = Parent.PhoneNumber;
            returnData.Photo = Parent.Photo;
            returnData.potalCode = Parent.PostalCode;
            returnData.skypeId = Parent.SkypeId;

            ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key", Parent.Country);
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text", Parent.Gender);

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

                Parent data = URes.GetAllParentUser().Where(s => s.ParentId == model.Id).FirstOrDefault();
                data.Address = model.Address;
                data.BirthDate = model.BirthDate;
                data.City = model.City;
                data.Country = model.Country;
                data.Description = model.Description;
                data.FirstName = model.FirstName;
                data.Gender = model.Gender;
                data.LastName = model.LastName;
                data.PhoneNumber = model.PhoneNumber;
                data.Photo = (string.IsNullOrEmpty(photoUrl) ? model.Photo : photoUrl);
                data.PostalCode = model.potalCode;
                data.SkypeId = model.skypeId;

                HttpCookie cookie = Request.Cookies["Avata"];
                if (cookie != null)
                {
                    cookie.Values["AvaName"] = data.Photo;
                }
                else
                {
                    cookie = new HttpCookie("Avata");
                    cookie.Values["AvaName"] = data.Photo;
                }
                Response.Cookies.Add(cookie);

                URes.EditParentUser(data);
                TempData["message"] = "Đã cập nhặt thông tin của người dùng " + data.UserName + " thành công.";
                return RedirectToAction("ViewParentInfo", "Parent");
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