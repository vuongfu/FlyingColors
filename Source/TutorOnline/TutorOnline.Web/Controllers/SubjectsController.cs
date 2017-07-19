using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;
using TutorOnline.Common;
using PagedList;
using TutorOnline.Web.Models;
using System.Net;
using System.IO;

namespace TutorOnline.Web.Controllers
{
    [Authorize (Roles = "Manager")]
    public class SubjectsController : Controller
    {
        // GET: Subjects
        private SubjectsRepository SRes = new SubjectsRepository();
        private CategoriesRepository CRes = new CategoriesRepository();
        private LessonRepository LRes = new LessonRepository();
        private LearningMaterialRepository LMRes = new LearningMaterialRepository();
        private QuestionRepository QRes = new QuestionRepository();

        public ActionResult Index(string btnSearch, string searchString, string cateString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;
            ViewBag.cateStr = cateString;
            ViewBag.cateString = new SelectList(CRes.GetAllCategories(), "CategoryName", "CategoryName");
            ViewBag.btnSearch = btnSearch;

            var subjects = SRes.GetAllSubject();
            List<SubjectsViewModels> result = new List<SubjectsViewModels>();

            //Mapping Entity to ViewModel
            if (subjects.Count() > 0)
            {
                foreach (var item in subjects)
                {
                    SubjectsViewModels model = new SubjectsViewModels();
                    model.SubjectId = item.SubjectId;
                    model.SubjectName = item.SubjectName;
                    model.CategoryId = item.CategoryId;
                    model.CategoryName = item.Category.CategoryName;
                    if (item.Purpose != null && item.Purpose.ToString().Length >= 30)
                        model.Purpose = item.Purpose.ToString().Substring(0, 30) + "...";
                    else
                        model.Purpose = item.Purpose;
                    if (item.Requirement != null && item.Requirement.ToString().Length >= 30)
                        model.Requirement = item.Requirement.ToString().Substring(0, 30) + "...";
                    else
                        model.Requirement = item.Requirement;
                    if (item.Description != null && item.Description.ToString().Length >= 30)
                        model.Description = item.Description.ToString().Substring(0, 30) + "...";
                    else
                        model.Description = item.Description;
                    model.Photo = item.Photo;
                    result.Add(model);
                }
            }

            if ((searchString == null || cateString == null) && page == null)
            {
                result = result.Where(x => x.SubjectId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToList().ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => SRes.SearchForString(x.SubjectName, searchString) ||
                                           SRes.SearchForString(x.Purpose, searchString) ||
                                           SRes.SearchForString(x.Requirement, searchString) ||
                                           SRes.SearchForString(x.Description, searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(cateString))
            {
                result = result.Where(x => x.CategoryName == cateString).ToList();
            }

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.SubjectName).ToList().ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectsViewModels model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string photoUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);
                Subject subject = new Subject();
                if (SRes.isExistsSubjectName(model.SubjectName, model.CategoryId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistSubjectName.ToString();
                    ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName", model.CategoryId);
                    return View(model);
                }
                //Mapping Entity to ViewModel
                subject.CategoryId = model.CategoryId;
                subject.SubjectName = model.SubjectName;
                subject.Purpose = model.Purpose;
                subject.Requirement = model.Requirement;
                subject.Photo = (string.IsNullOrEmpty(photoUrl) ? model.Photo : photoUrl);
                subject.Description = model.Description;

                SRes.AddSubject(subject);
                TempData["message"] = new ManagerStringCommon().addSubjectsSuccess.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get subject
            Subject subject = SRes.FindSubject(id);

            if (subject == null)
            {
                return HttpNotFound();
            }
            SubjectsViewModels model = new SubjectsViewModels();
            //Mapping Entity to ViewModel
            model.SubjectId = subject.SubjectId;
            model.CategoryId = subject.CategoryId;
            model.CategoryName = subject.Category.CategoryName;
            model.SubjectName = subject.SubjectName;
            model.Purpose = subject.Purpose;
            model.Requirement = subject.Requirement;
            model.Description = subject.Description;
            model.Photo = subject.Photo;

            //return
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Subject subject = SRes.FindSubject(id);
            SubjectsViewModels model = new SubjectsViewModels();
            if (subject == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.SubjectId = subject.SubjectId;
                model.CategoryId = subject.CategoryId;
                model.CategoryName = subject.Category.CategoryName;
                model.SubjectName = subject.SubjectName;
                model.Purpose = subject.Purpose;
                model.Requirement = subject.Requirement;
                model.Description = subject.Description;
                model.Photo = subject.Photo;
            }
            ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectsViewModels model, HttpPostedFileBase file)
        {
            string photoUrl = FileUpload.UploadFile(file,  FileUpload.TypeUpload.image);
            if (SRes.isExistsSubjectName(model.SubjectName, model.CategoryId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistSubjectName.ToString();
                ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName");
                return View(model);
            }
            Subject subject = new Subject();

            //Mapping Entity to ViewModel
            subject.SubjectId = model.SubjectId;
            subject.CategoryId = model.CategoryId;
            subject.SubjectName = model.SubjectName;
            subject.Purpose = model.Purpose;
            subject.Requirement = model.Requirement;
            subject.Description = model.Description;
            subject.Photo = (string.IsNullOrEmpty(photoUrl) ? model.Photo : photoUrl);

            if (ModelState.IsValid)
            {
                SRes.EditSubject(subject);
                TempData["message"] = new ManagerStringCommon().updateSubjectSuccess.ToString();
                return RedirectToAction("Details", new { id = model.SubjectId });
            }
            ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName");
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = SRes.FindSubject(id);
            SubjectsViewModels model = new SubjectsViewModels();
            if (subject == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.SubjectId = subject.SubjectId;
                model.CategoryId = subject.CategoryId;
                model.CategoryName = subject.Category.CategoryName;
                model.SubjectName = subject.SubjectName;
                model.Purpose = subject.Purpose;
                model.Requirement = subject.Requirement;
                model.Photo = subject.Photo;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (SRes.isExistedStudentIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistStudentIn.ToString();
                return RedirectToAction("Delete");
            }
            if (SRes.isExistedLessonIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistLessonIn.ToString();
                return RedirectToAction("Delete");
            }
            SRes.DeleteSubject(id);
            TempData["message"] = new ManagerStringCommon().deleteSubjectsSuccess.ToString();
            return RedirectToAction("Index");
        }

        public ActionResult LessonsInSubject(int? id)
        {
            ViewBag.subjectId = id;

            var lessons = LRes.GetAllLessons();
            List<LessonViewModels> result = new List<LessonViewModels>();

            //Mapping Entity to ViewModel
            if (lessons.Count() > 0)
            {
                foreach (var item in lessons)
                {
                    if (item.isActived == true)
                    {
                        LessonViewModels model = new LessonViewModels();
                        model.LessonId = item.LessonId;
                        model.LessonName = item.LessonName;
                        model.SubjectId = item.SubjectId;
                        model.SubjectName = item.Subject.SubjectName;
                        if (item.Content != null && item.Content.ToString().Length >= 80)
                            model.Content = item.Content.ToString().Substring(0, 80) + "...";
                        else
                            model.Content = item.Content;

                        result.Add(model);
                    }
                }
            }

            if (id == null)
            {
                result = result.Where(x => x.LessonId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToList());
            }

            if (id != null)
            {
                result = result.Where(x => x.SubjectId == id).ToList();
                ViewBag.totalRecord = result.Count();
            }

            return View(result.OrderBy(x => x.LessonName).ToList());
        }

        public ActionResult MaterialInSubject(int? id)
        {
            ViewBag.subjectId = id;

            var material = LMRes.GetAllMaterial();
            List<MaterialViewModels> result = new List<MaterialViewModels>();

            //Mapping Entity to ViewModel
            if (material.Count() > 0)
            {
                foreach (var item in material)
                {
                    if (item.isActived == true)
                    {
                        MaterialViewModels model = new MaterialViewModels();
                        model.MaterialId = item.MaterialId;
                        model.MaterialUrl = item.MaterialUrl;
                        model.MaterialTypeId = item.MaterialTypeId;
                        model.MaterialTypeName = item.MaterialType.MaterialTypeName;
                        model.LessonId = item.LessonId;
                        model.SubjectId = item.SubjectId;
                        if (item.Description != null && item.Description.ToString().Length >= 80)
                            model.Description = item.Description.ToString().Substring(0, 80) + "...";
                        else
                            model.Description = item.Description;

                        result.Add(model);
                    }
                }
            }

            if (id == null)
            {
                result = result.Where(x => x.MaterialId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToList());
            }

            if (id != null)
            {
                result = result.Where(x => x.SubjectId == id).ToList();
                ViewBag.totalRecord = result.Count();
            }

            return View(result.OrderBy(x => x.MaterialId).ToList());
        }
        public FileResult DownloadFile(string file)
        {

            var FileVirtualPath = "~/Content/Uploads/Documents/" + file;
            return File(FileVirtualPath, "application/pdf", Path.GetFileName(FileVirtualPath));
        }

        public ActionResult QuestionInSubject(int? id)
        {
            var question = QRes.GetAllSubQuestion(id);
            List<QuestionViewModels> result = new List<QuestionViewModels>();

            //Mapping Entity to ViewModel
            if (question.Count() > 0)
            {
                foreach (var item in question)
                {
                    QuestionViewModels model = new QuestionViewModels();
                    model.QuestionId = item.QuestionId;
                    model.LessonId = item.LessonId;
                    model.SubjectId = item.SubjectId;
                    model.Photo = item.Photo;
                    model.Content = item.Content;
                    result.Add(model);
                }
            }

            if (id == null)
            {
                result = result.Where(x => x.QuestionId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToList());
            }

            if (id != null)
            {
                result = result.ToList();
                ViewBag.totalRecord = result.Count();
            }

            return View(result.OrderBy(x => x.QuestionId).ToList()); //.Where(x => x.SubjectId == id).
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}