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

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        // GET: Subjects
        private SubjectsRepository SRes = new SubjectsRepository();
        private CategoriesRepository CRes = new CategoriesRepository();
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
                string photoUrl = FileUpload.UploadFile(file);
                Subject subject = new Subject();
                if (SRes.isExistsSubjectName(model.SubjectName, model.CategoryId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistSubjectName.ToString();
                    ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName");
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
            ViewBag.CategoryId = new SelectList(CRes.GetAllCategories(), "CategoryId", "CategoryName");
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
            string photoUrl = FileUpload.UploadFile(file);
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
                return RedirectToAction("Index");
            }
            if (SRes.isExistedLessonIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistLessonIn.ToString();
                return RedirectToAction("Index");
            }
            SRes.DeleteSubject(id);
            TempData["message"] = new ManagerStringCommon().deleteSubjectsSuccess.ToString();
            return RedirectToAction("Index");
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