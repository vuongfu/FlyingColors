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
        public ActionResult Index(string searchString, string cateString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;
            ViewBag.cateStr = cateString;
            ViewBag.cateString = new SelectList(CRes.GetAllCategories(), "CategoryName", "CategoryName");

            var subjects = SRes.GetAllSubject();
            List<SubjectsViewModels> result = new List<SubjectsViewModels>();

            //Mapping Entity to ViewModel
            if (subjects.Count() > 0)
            {
                foreach (var item in subjects)
                {
                    SubjectsViewModels model = new SubjectsViewModels();
                    model.Id = item.Id;
                    model.SubjectName = item.SubjectName;
                    model.CategoryID = item.CategoryID;
                    model.CategoryName = item.Category.CategoryName;
                    model.Purpose = item.Purpose;
                    model.Requirement = item.Requirement;
                    model.Description = item.Description;
                    model.Photo = item.Photo;
                    model.Duration = item.Duration;
                    model.Price = item.Price;

                    result.Add(model);
                }
            }

            if ((searchString == null || cateString == null) && page == null)
            {
                result = result.Where(x => x.Id == 0).ToList();
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
            ViewBag.CategoryID = new SelectList(CRes.GetAllCategories(), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectsViewModels model)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject();
                if (SRes.isExistsSubjectName(model.SubjectName))
                {
                    TempData["message"] = new StringCommon().isExitSubjectName.ToString();
                    return View(model);
                }
                //Mapping Entity to ViewModel
                subject.CategoryID = model.CategoryID;
                subject.SubjectName = model.SubjectName;
                subject.Purpose = model.Purpose;
                subject.Requirement = model.Requirement;
                subject.Photo = model.Photo;
                subject.Price = model.Price;
                subject.Duration = model.Duration;
                subject.Description = model.Description;

                SRes.AddSubject(subject);
                TempData["message"] = new StringCommon().addSubjectsSuccess.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(CRes.GetAllCategories(), "Id", "CategoryName");
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
            model.Id = subject.Id;
            model.CategoryID = subject.CategoryID;
            model.CategoryName = subject.Category.CategoryName;
            model.SubjectName = subject.SubjectName;
            model.Purpose = subject.Purpose;
            model.Requirement = subject.Requirement;
            model.Duration = subject.Duration;
            model.Description = subject.Description;
            model.Photo = subject.Photo;
            model.Price = subject.Price;
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
                model.Id = subject.Id;
                model.CategoryID = subject.CategoryID;
                model.CategoryName = subject.Category.CategoryName;
                model.SubjectName = subject.SubjectName;
                model.Purpose = subject.Purpose;
                model.Requirement = subject.Requirement;
                model.Duration = subject.Duration;
                model.Description = subject.Description;
                model.Photo = subject.Photo;
                model.Price = subject.Price;
            }
            ViewBag.CategoryID = new SelectList(CRes.GetAllCategories(), "Id", "CategoryName", model.CategoryID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectsViewModels model)
        {
            Subject subject = new Subject();

            //Mapping Entity to ViewModel
            subject.Id = model.Id;
            subject.CategoryID = model.CategoryID;
            subject.SubjectName = model.SubjectName;
            subject.Purpose = model.Purpose;
            subject.Requirement = model.Requirement;
            subject.Duration = model.Duration;
            subject.Description = model.Description;
            subject.Photo = model.Photo;
            subject.Price = model.Price;

            if (ModelState.IsValid)
            {
                SRes.EditSubject(subject);
                return RedirectToAction("Details", new { id = model.Id });
            }
            ViewBag.CategoryID = new SelectList(CRes.GetAllCategories(), "Id", "CategoryName");
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
                model.Id = subject.Id;
                model.CategoryID = subject.CategoryID;
                model.CategoryName = subject.Category.CategoryName;
                model.SubjectName = subject.SubjectName;
                model.Purpose = subject.Purpose;
                model.Requirement = subject.Requirement;
                model.Photo = subject.Photo;
                model.Price = subject.Price;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            SRes.DeleteSubject(id);
            TempData["message"] = new StringCommon().deleteSubjectsSuccess.ToString();
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