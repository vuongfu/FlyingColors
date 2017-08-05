using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;
using TutorOnline.Common;
using TutorOnline.Web.Models;
using PagedList;
using System.Net;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = UserCommonString.Manager)]
    public class TutorManagementController : Controller
    {
        private TutorRepository Tres = new TutorRepository();
        private TutorRepository TuRes = new TutorRepository();
        private CategoriesRepository CRes = new CategoriesRepository();
        public ActionResult Index(string btnSearch, string searchString, string cateString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            int tutorIsActived = Tres.GetAllTutor().Count();
            ViewBag.tutorIsActived = tutorIsActived;

            int allPretutor = Tres.GetAllPretutor().Count();
            ViewBag.allPretutor = allPretutor;

            int tutorSignNewSub = Tres.GetTutorIdWhoSignMoreSub().Count();
            ViewBag.tutorSignNewSub = tutorSignNewSub;

            ViewBag.searchStr = searchString;
            ViewBag.btnSearch = btnSearch;
            ViewBag.cateStr = cateString;
            ViewBag.cateString = new SelectList(CRes.GetAllCategories().OrderBy(x => x.CategoryName), "CategoryName", "CategoryName");
            
            var tutors = Tres.GetAllTutor();

            List<TutorInfoViewModels> result = new List<TutorInfoViewModels>();

            //Mapping Entity to ViewModel
            if (tutors.Count() > 0)
            {
                foreach (var item in tutors)
                {
                    TutorInfoViewModels model = new TutorInfoViewModels();
                    //Mapping model with entity
                    model.TutorId = item.TutorId;
                    model.FullName = item.LastName + " " + item.FirstName;
                    model.Photo = item.Photo;
                    model.Gender = (item.Gender == 1) ? "Nam" : "Nữ";
                    model.BirthDate = item.BirthDate;
                    model.Address = item.Address;
                    model.City = item.City;
                    model.PostalCode = item.PostalCode;
                    model.Country = item.Country;
                    model.Email = item.Email;
                    model.SkypeId = item.SkypeId;
                    model.PhoneNumber = item.PhoneNumber;
                    model.Salary = item.Salary;
                    model.BankId = item.BankId;
                    model.BankName = item.BankName;
                    model.BMemName = item.BMemName;
                    model.Description = item.Description;
                    model.isActived = (item.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
                    model.RegisterDate = item.RegisterDate;
                    model.cateTeaching = Tres.GetCateNameOfTutor(item.TutorId);
                    //TutorSubject
                    List<TutorSubject> tutorSubEntity = Tres.GetTutorSubjects(item.TutorId).ToList();
                    List<TutorSubjectViewModels> tutorSubModel = new List<TutorSubjectViewModels>();
                    for(int i = 0; i < tutorSubEntity.Count(); i ++)
                    {
                        TutorSubject entity = new TutorSubject();
                        entity = tutorSubEntity[i];
                        if(entity != null)
                        {
                            TutorSubjectViewModels t = new TutorSubjectViewModels();

                            t.TutorSubjectId = entity.TutorSubjectId;
                            t.subjectName = entity.Subject.SubjectName;
                            t.experiences = entity.Experience;

                            tutorSubModel.Add(t);
                        }
                    }

                    model.tutorSub = tutorSubModel;

                    result.Add(model);
                }
            }

            if (searchString == null && page == null)
            {
                result = result.Where(x => x.TutorId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => Tres.SearchForString(x.FullName, searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(cateString))
            {
                result = result.Where(x => x.cateTeaching.Contains(cateString)).ToList();
            }

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.FullName).ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DetailsTutor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tutor tutor = Tres.FindTutor(id);

            if (tutor == null)
            {
                return HttpNotFound();
            }

            TutorInfoViewModels model = new TutorInfoViewModels();

            //Mapping model with entity
            model.TutorId = tutor.TutorId;
            model.FullName = tutor.LastName + " " + tutor.FirstName;
            model.Photo = tutor.Photo;
            model.Gender = (tutor.Gender == 1) ? "Nam" : "Nữ";
            model.BirthDate = tutor.BirthDate;
            model.Address = tutor.Address;
            model.City = tutor.City;
            model.PostalCode = tutor.PostalCode;
            model.Country = tutor.Country;
            model.Email = tutor.Email;
            model.SkypeId = tutor.SkypeId;
            model.PhoneNumber = tutor.PhoneNumber;
            model.Salary = tutor.Salary;
            model.BankId = tutor.BankId;
            model.BankName = tutor.BankName;
            model.BMemName = tutor.BMemName;
            model.Description = tutor.Description;
            model.isActived = (tutor.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
            model.RegisterDate = tutor.RegisterDate;
            //TutorSubject
            List<TutorSubject> tutorSubEntity = Tres.GetTutorSubjects(tutor.TutorId).ToList();
            List<TutorSubjectViewModels> tutorSubModel = new List<TutorSubjectViewModels>();
            for (int i = 0; i < tutorSubEntity.Count(); i++)
            {
                TutorSubject entity = new TutorSubject();
                entity = tutorSubEntity[i];
                if (entity != null)
                {
                    TutorSubjectViewModels t = new TutorSubjectViewModels();

                    t.TutorSubjectId = entity.TutorSubjectId;
                    t.subjectName = entity.Subject.SubjectName;
                    t.experiences = entity.Experience;

                    tutorSubModel.Add(t);
                }
            }
            model.tutorSub = tutorSubModel.OrderBy(x => x.subjectName).ToList();

            //NewTutorSubject
            List<TutorSubject> newTutorSubEntity = Tres.GetTutorNewSubjects(tutor.TutorId).ToList();
            List<TutorSubjectViewModels> newTutorSubModel = new List<TutorSubjectViewModels>();
            for (int i = 0; i < newTutorSubEntity.Count(); i++)
            {
                TutorSubject entity = new TutorSubject();
                entity = newTutorSubEntity[i];
                if (entity != null)
                {
                    TutorSubjectViewModels t = new TutorSubjectViewModels();

                    t.TutorSubjectId = entity.TutorSubjectId;
                    t.subjectName = entity.Subject.SubjectName;
                    t.experiences = entity.Experience;

                    newTutorSubModel.Add(t);
                }
            }

            model.newTutorSub = newTutorSubModel.OrderBy(x => x.subjectName).ToList();

            return View(model);
        }
        public ActionResult PretutorIndex(string btnSearch, string searchString, string cateString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;
            ViewBag.btnSearch = btnSearch;
            ViewBag.cateStr = cateString;
            ViewBag.cateString = new SelectList(CRes.GetAllCategories().OrderBy(x => x.CategoryName), "CategoryName", "CategoryName");

            var tutors = Tres.GetAllPretutor();

            List<TutorInfoViewModels> result = new List<TutorInfoViewModels>();

            //Mapping Entity to ViewModel
            if (tutors.Count() > 0)
            {
                foreach (var item in tutors)
                {
                    TutorInfoViewModels model = new TutorInfoViewModels();
                    //Mapping model with entity
                    model.TutorId = item.TutorId;
                    model.FullName = item.LastName + " " + item.FirstName;
                    model.Photo = item.Photo;
                    model.Gender = (item.Gender == 1) ? "Nam" : "Nữ";
                    model.BirthDate = item.BirthDate;
                    model.Address = item.Address;
                    model.City = item.City;
                    model.PostalCode = item.PostalCode;
                    model.Country = item.Country;
                    model.Email = item.Email;
                    model.SkypeId = item.SkypeId;
                    model.PhoneNumber = item.PhoneNumber;
                    model.Salary = item.Salary;
                    model.BankId = item.BankId;
                    model.BankName = item.BankName;
                    model.BMemName = item.BMemName;
                    model.Description = item.Description;
                    model.isActived = (item.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
                    model.RegisterDate = item.RegisterDate;
                    model.cateTeaching = Tres.GetNewCateNameOfTutor(item.TutorId);

                    //NewTutorSubject
                    List<TutorSubject> newTutorSubEntity = Tres.GetTutorNewSubjects(item.TutorId).ToList();
                    List<TutorSubjectViewModels> newTutorSubModel = new List<TutorSubjectViewModels>();
                    for (int i = 0; i < newTutorSubEntity.Count(); i++)
                    {
                        TutorSubject entity = new TutorSubject();
                        entity = newTutorSubEntity[i];
                        if (entity != null)
                        {
                            TutorSubjectViewModels t = new TutorSubjectViewModels();

                            t.TutorSubjectId = entity.TutorSubjectId;
                            t.subjectName = entity.Subject.SubjectName;
                            t.experiences = entity.Experience;

                            newTutorSubModel.Add(t);
                        }
                    }

                    model.newTutorSub = newTutorSubModel;

                    result.Add(model);
                }
            }

            if (searchString == null && page == null)
            {
                result = result.Where(x => x.TutorId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => Tres.SearchForString(x.FullName, searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(cateString))
            {
                result = result.Where(x => x.cateTeaching.Contains(cateString)).ToList();
            }

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.FullName).ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult TutorSignMoreSubIndex(string btnSearch, string searchString, string cateString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;
            ViewBag.btnSearch = btnSearch;
            ViewBag.cateStr = cateString;
            ViewBag.cateString = new SelectList(CRes.GetAllCategories().OrderBy(x => x.CategoryName), "CategoryName", "CategoryName");

            List<int> tutorId = Tres.GetTutorIdWhoSignMoreSub();

            List<Tutor> lstTutor = new List<Tutor>();

            for(int i = 0; i < tutorId.Count; i ++)
            {
                if (tutorId[i] != 0)
                {
                    Tutor entity = new Tutor();
                    entity = Tres.FindTutor(tutorId[i]);
                    if (entity != null)
                        lstTutor.Add(entity);
                }
            }

            List<TutorInfoViewModels> result = new List<TutorInfoViewModels>();

            //Mapping Entity to ViewModel
            if (lstTutor.Count() > 0)
            {
                foreach (var item in lstTutor)
                {
                    TutorInfoViewModels model = new TutorInfoViewModels();
                    //Mapping model with entity
                    model.TutorId = item.TutorId;
                    model.FullName = item.LastName + " " + item.FirstName;
                    model.Photo = item.Photo;
                    model.Gender = (item.Gender == 1) ? "Nam" : "Nữ";
                    model.BirthDate = item.BirthDate;
                    model.Address = item.Address;
                    model.City = item.City;
                    model.PostalCode = item.PostalCode;
                    model.Country = item.Country;
                    model.Email = item.Email;
                    model.SkypeId = item.SkypeId;
                    model.PhoneNumber = item.PhoneNumber;
                    model.Salary = item.Salary;
                    model.BankId = item.BankId;
                    model.BankName = item.BankName;
                    model.BMemName = item.BMemName;
                    model.Description = item.Description;
                    model.isActived = (item.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
                    model.RegisterDate = item.RegisterDate;
                    model.cateTeaching = Tres.GetNewCateNameOfTutor(item.TutorId);
                    //TutorSubject
                    List<TutorSubject> tutorSubEntity = Tres.GetTutorSubjects(item.TutorId).ToList();
                    List<TutorSubjectViewModels> tutorSubModel = new List<TutorSubjectViewModels>();
                    for (int i = 0; i < tutorSubEntity.Count(); i++)
                    {
                        TutorSubject entity = new TutorSubject();
                        entity = tutorSubEntity[i];
                        if (entity != null)
                        {
                            TutorSubjectViewModels t = new TutorSubjectViewModels();

                            t.TutorSubjectId = entity.TutorSubjectId;
                            t.subjectName = entity.Subject.SubjectName;
                            t.experiences = entity.Experience;

                            tutorSubModel.Add(t);
                        }
                    }

                    model.tutorSub = tutorSubModel;

                    //NewTutorSubject
                    List<TutorSubject> newTutorSubEntity = Tres.GetTutorNewSubjects(item.TutorId).ToList();
                    List<TutorSubjectViewModels> newTutorSubModel = new List<TutorSubjectViewModels>();
                    for (int i = 0; i < newTutorSubEntity.Count(); i++)
                    {
                        TutorSubject entity = new TutorSubject();
                        entity = newTutorSubEntity[i];
                        if (entity != null)
                        {
                            TutorSubjectViewModels t = new TutorSubjectViewModels();

                            t.TutorSubjectId = entity.TutorSubjectId;
                            t.subjectName = entity.Subject.SubjectName;
                            t.experiences = entity.Experience;

                            newTutorSubModel.Add(t);
                        }
                    }

                    model.newTutorSub = newTutorSubModel;

                    result.Add(model);
                }
            }

            if (searchString == null && page == null)
            {
                result = result.Where(x => x.TutorId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => Tres.SearchForString(x.FullName, searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(cateString))
            {
                result = result.Where(x => x.cateTeaching.Contains(cateString)).ToList();
            }

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.FullName).ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DetailsPretutor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tutor tutor = Tres.FindPreTutor(id);

            if (tutor == null)
            {
                return HttpNotFound();
            }

            TutorInfoViewModels model = new TutorInfoViewModels();

            //Mapping model with entity
            model.TutorId = tutor.TutorId;
            model.FullName = tutor.LastName + " " + tutor.FirstName;
            model.Photo = tutor.Photo;
            model.Gender = (tutor.Gender == 1) ? "Nam" : "Nữ";
            model.BirthDate = tutor.BirthDate;
            model.Address = tutor.Address;
            model.City = tutor.City;
            model.PostalCode = tutor.PostalCode;
            model.Country = tutor.Country;
            model.Email = tutor.Email;
            model.SkypeId = tutor.SkypeId;
            model.PhoneNumber = tutor.PhoneNumber;
            model.Salary = tutor.Salary;
            model.BankId = tutor.BankId;
            model.BankName = tutor.BankName;
            model.BMemName = tutor.BMemName;
            model.Description = tutor.Description;
            model.isActived = (tutor.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
            model.RegisterDate = tutor.RegisterDate;

            //NewTutorSubject
            List<TutorSubject> newTutorSubEntity = Tres.GetTutorNewSubjects(tutor.TutorId).ToList();
            List<TutorSubjectViewModels> newTutorSubModel = new List<TutorSubjectViewModels>();
            List<int> lstNewSubId = new List<int>();
            for (int i = 0; i < newTutorSubEntity.Count(); i++)
            {
                TutorSubject entity = new TutorSubject();
                entity = newTutorSubEntity[i];
                if (entity != null)
                {
                    TutorSubjectViewModels t = new TutorSubjectViewModels();

                    t.TutorSubjectId = entity.TutorSubjectId;
                    t.subjectName = entity.Subject.SubjectName;
                    t.experiences = entity.Experience;

                    newTutorSubModel.Add(t);
                    lstNewSubId.Add(t.TutorSubjectId);
                }
            }

            model.newTutorSub = newTutorSubModel.OrderBy(x => x.subjectName).ToList();
            model.newTusubId = lstNewSubId.ToList();
            return View(model);
        }
        public ActionResult DetailsTutorSignMoreSub(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tutor tutor = Tres.FindTutor(id);

            if (tutor == null)
            {
                return HttpNotFound();
            }

            TutorInfoViewModels model = new TutorInfoViewModels();

            //Mapping model with entity
            model.TutorId = tutor.TutorId;
            model.FullName = tutor.LastName + " " + tutor.FirstName;
            model.Photo = tutor.Photo;
            model.Gender = (tutor.Gender == 1) ? "Nam" : "Nữ";
            model.BirthDate = tutor.BirthDate;
            model.Address = tutor.Address;
            model.City = tutor.City;
            model.PostalCode = tutor.PostalCode;
            model.Country = tutor.Country;
            model.Email = tutor.Email;
            model.SkypeId = tutor.SkypeId;
            model.PhoneNumber = tutor.PhoneNumber;
            model.Salary = tutor.Salary;
            model.BankId = tutor.BankId;
            model.BankName = tutor.BankName;
            model.BMemName = tutor.BMemName;
            model.Description = tutor.Description;
            model.isActived = (tutor.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
            model.RegisterDate = tutor.RegisterDate;
            //TutorSubject
            List<TutorSubject> tutorSubEntity = Tres.GetTutorSubjects(tutor.TutorId).ToList();
            List<TutorSubjectViewModels> tutorSubModel = new List<TutorSubjectViewModels>();
            for (int i = 0; i < tutorSubEntity.Count(); i++)
            {
                TutorSubject entity = new TutorSubject();
                entity = tutorSubEntity[i];
                if (entity != null)
                {
                    TutorSubjectViewModels t = new TutorSubjectViewModels();

                    t.TutorSubjectId = entity.TutorSubjectId;
                    t.subjectName = entity.Subject.SubjectName;
                    t.experiences = entity.Experience;

                    tutorSubModel.Add(t);
                }
            }
            model.tutorSub = tutorSubModel.OrderBy(x => x.subjectName).ToList();

            //NewTutorSubject
            List<TutorSubject> newTutorSubEntity = Tres.GetTutorNewSubjects(tutor.TutorId).ToList();
            List<TutorSubjectViewModels> newTutorSubModel = new List<TutorSubjectViewModels>();
            for (int i = 0; i < newTutorSubEntity.Count(); i++)
            {
                TutorSubject entity = new TutorSubject();
                entity = newTutorSubEntity[i];
                if (entity != null)
                {
                    TutorSubjectViewModels t = new TutorSubjectViewModels();

                    t.TutorSubjectId = entity.TutorSubjectId;
                    t.subjectName = entity.Subject.SubjectName;
                    t.experiences = entity.Experience;

                    newTutorSubModel.Add(t);
                }
            }

            model.newTutorSub = newTutorSubModel.OrderBy(x => x.subjectName).ToList();

            return View(model);
        }
        public ActionResult ApprovedSubject(int? subId, int? tuId)
        {
            if(subId != null)
            {
                Tres.ApprovedTutorSubject(subId);
                TempData["message"] = new ManagerStringCommon().approvedTutorSubjectSuccess.ToString();
            }
            return RedirectToAction("DetailsTutorSignMoreSub", "TutorManagement", new { id = tuId }); 
        }
        public ActionResult RejectedSubject(int? tusubId, int? tuId)
        {
            if (tusubId != null)
            {
                Tres.RejectedTutorSubject(tusubId);
                TempData["message"] = new ManagerStringCommon().rejectedTutorSubjectMoreSuccess.ToString();
            }
            return RedirectToAction("DetailsTutorSignMoreSub", "TutorManagement", new { id = tuId });
        }
        [HttpPost]
        public ActionResult ApprovedPreTutor(List<int> tusubId, int? tuId)
        {
            if (tusubId.Count != 0 && tuId != null)
            {
                Tres.ApprovedPreTutor(tusubId, tuId);
                TempData["message"] = new ManagerStringCommon().approvedPreTutorSuccess.ToString();

                return Json(new { Approved = true });
            }

            return Json(new { Approved = false });
        }
        [HttpPost]
        public ActionResult RejectedPreTutor(List<int> tusubId, int? tuId)
        {
            if (tusubId.Count != 0 && tuId != null)
            {
                Tres.RejectedPreTutor(tusubId, tuId);
                TempData["message"] = new ManagerStringCommon().rejectedPreTutorSuccess.ToString();

                return Json(new { Rejected = true });
            }

            return Json(new { Rejected = false });
        }
        [HttpPost]
        public ActionResult EditTuSalary(decimal? salary, int? tuId)
        {
            Tutor tutor = TuRes.FindTutor(tuId);
            TutorInfoViewModels model = new TutorInfoViewModels();
            if(tutor != null)
            {
                //Mapping model with entity
                model.TutorId = tutor.TutorId;
                model.FullName = tutor.LastName + " " + tutor.FirstName;
                model.Photo = tutor.Photo;
                model.Gender = (tutor.Gender == 1) ? "Nam" : "Nữ";
                model.BirthDate = tutor.BirthDate;
                model.Address = tutor.Address;
                model.City = tutor.City;
                model.PostalCode = tutor.PostalCode;
                model.Country = tutor.Country;
                model.Email = tutor.Email;
                model.SkypeId = tutor.SkypeId;
                model.PhoneNumber = tutor.PhoneNumber;
                model.Salary = tutor.Salary;
                model.BankId = tutor.BankId;
                model.BankName = tutor.BankName;
                model.BMemName = tutor.BMemName;
                model.Description = tutor.Description;
                model.isActived = (tutor.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
                model.RegisterDate = tutor.RegisterDate;
                //TutorSubject
                List<TutorSubject> tutorSubEntity = Tres.GetTutorSubjects(tutor.TutorId).ToList();
                List<TutorSubjectViewModels> tutorSubModel = new List<TutorSubjectViewModels>();
                for (int i = 0; i < tutorSubEntity.Count(); i++)
                {
                    TutorSubject entity = new TutorSubject();
                    entity = tutorSubEntity[i];
                    if (entity != null)
                    {
                        TutorSubjectViewModels t = new TutorSubjectViewModels();

                        t.TutorSubjectId = entity.TutorSubjectId;
                        t.subjectName = entity.Subject.SubjectName;
                        t.experiences = entity.Experience;

                        tutorSubModel.Add(t);
                    }
                }
                model.tutorSub = tutorSubModel.OrderBy(x => x.subjectName).ToList();

                //NewTutorSubject
                List<TutorSubject> newTutorSubEntity = Tres.GetTutorNewSubjects(tutor.TutorId).ToList();
                List<TutorSubjectViewModels> newTutorSubModel = new List<TutorSubjectViewModels>();
                for (int i = 0; i < newTutorSubEntity.Count(); i++)
                {
                    TutorSubject entity = new TutorSubject();
                    entity = newTutorSubEntity[i];
                    if (entity != null)
                    {
                        TutorSubjectViewModels t = new TutorSubjectViewModels();

                        t.TutorSubjectId = entity.TutorSubjectId;
                        t.subjectName = entity.Subject.SubjectName;
                        t.experiences = entity.Experience;

                        newTutorSubModel.Add(t);
                    }
                }

                model.newTutorSub = newTutorSubModel.OrderBy(x => x.subjectName).ToList();
            }
            if (salary != null && tuId != null)
            {
                Tres.EditTuSalary(salary, tuId);
                TempData["salaryEditMsg"] = "Tiền lương đã được cập nhật.";
            }
            else
                TempData["salaryEditMsg"] = "Tiền lương chưa được cập nhật.";

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Tres.Dispose();
                TuRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}