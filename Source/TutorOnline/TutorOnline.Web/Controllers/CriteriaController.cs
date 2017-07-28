using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Common;
using TutorOnline.Web.Models;
using TutorOnline.DataAccess;
using System.Net;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CriteriaController : Controller
    {
        // GET: Criteria
        private CriteriaRepository CTRes = new CriteriaRepository();
        private LessonRepository LRes = new LessonRepository();

        public ActionResult Create(int? lesId)
        {
            ViewBag.lesName = LRes.FindLesson(lesId).LessonName;
            ViewBag.lesId = lesId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriteriaViewModel model)
        {
            int lesId = Convert.ToInt32(TempData["lesId"]);
            string lesName = Convert.ToString(TempData["lesName"]);
            if (ModelState.IsValid)
            {
                if (CTRes.isExistsCriteriaName(model.CriteriaName, lesId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistCriteriaName.ToString();
                    ViewBag.lesName = lesName;
                    ViewBag.lesId = lesId;
                    return View(model);
                }
                Criterion criteria = new Criterion();
                //Mapping Entity to ViewModel
                criteria.CriteriaId = model.CriteriaId;
                criteria.LessonId = lesId;
                criteria.RoleId = 6;
                criteria.CriteriaName = model.CriteriaName.Trim();

                CTRes.AddCriteria(criteria);

                TempData["message"] = new ManagerStringCommon().addCriteriaSuccess.ToString();
                ViewBag.lesName = lesName;
                ViewBag.lesId = lesId;
                return RedirectToAction("Details", "Lessons", new { id = lesId });
            }

            ViewBag.lesName = lesName;
            ViewBag.lesId = lesId;
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterion criteria = CTRes.FindCriteria(id);
            if (criteria == null)
            {
                return HttpNotFound();
            }

            CriteriaViewModel model = new CriteriaViewModel();

            //Mapping Entity to ViewModel
            model.CriteriaId = criteria.CriteriaId;
            model.LessonId = criteria.LessonId;
            model.CriteriaName = criteria.CriteriaName;
            model.RoleId = criteria.RoleId;
            model.LessonName = criteria.Lesson.LessonName;

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterion criteria = CTRes.FindCriteria(id);
            CriteriaViewModel model = new CriteriaViewModel();

            if (criteria == null)
            {
                return HttpNotFound();
            }
            else
            {
                model.CriteriaId = criteria.CriteriaId;
                model.LessonId = criteria.LessonId;
                model.CriteriaName = criteria.CriteriaName;
                model.RoleId = criteria.RoleId;
                model.LessonName = criteria.Lesson.LessonName;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CriteriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Criterion criteria = new Criterion();
                criteria.CriteriaId = model.CriteriaId;
                criteria.LessonId = model.LessonId;
                criteria.RoleId = model.RoleId;
                criteria.CriteriaName = model.CriteriaName.Trim();

                if (CTRes.isExistsCriteriaNameEdit(model.CriteriaName, model.CriteriaId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistCriteriaName.ToString();
                    return View(model);
                }

                CTRes.EditCriteria(criteria);
                TempData["message"] = new ManagerStringCommon().updateCriteriaSuccess.ToString();
                return RedirectToAction("Details", new { id = model.CriteriaId });
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterion criteria = CTRes.FindCriteria(id);
            CriteriaViewModel model = new CriteriaViewModel();

            if (criteria == null)
            {
                return HttpNotFound();
            }
            else
            {
                model.CriteriaId = criteria.CriteriaId;
                model.LessonId = criteria.LessonId;
                model.CriteriaName = criteria.CriteriaName;
                model.RoleId = criteria.RoleId;
                model.LessonName = criteria.Lesson.LessonName;
            }
            ViewBag.criteriaId = model.CriteriaId;
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int lesId = CTRes.FindCriteria(id).LessonId;
            if (CTRes.isUsedCriteriaInTuFeDetail(id, lesId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isUsedCriteriaInTuFeDe.ToString();
                ViewBag.criteriaId = id;
                return View();
            }
            else
            {
                CTRes.DeleteCriteria(id);
                TempData["message"] = new ManagerStringCommon().deleteCriteriaSuccess.ToString();
                return RedirectToAction("Details", "Lessons", new { id = lesId });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CTRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}