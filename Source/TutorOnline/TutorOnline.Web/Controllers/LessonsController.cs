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
    [Authorize(Roles = "Manager")]
    public class LessonsController : Controller
    {
        // GET: Lesson
        private LessonRepository LRes = new LessonRepository();
        private LearningMaterialRepository LMRes = new LearningMaterialRepository();
        private QuestionRepository QRes = new QuestionRepository();
        private AnswersRepository ARes = new AnswersRepository();
        private SubjectsRepository SRes = new SubjectsRepository();
        private CriteriaRepository CRes = new CriteriaRepository();
        
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName", id);
            ViewBag.subId = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LessonViewModels model)
        {
            int subId = Convert.ToInt32(TempData["subId"]);
            
            if (ModelState.IsValid)
            {
                Lesson lesson = new Lesson();
                if (LRes.isExistsLessonName(model.LessonName, model.SubjectId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistLessonName.ToString();
                    ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                    ViewBag.subId = model.SubjectId;
                    return View(model);
                }
                //get Max Order
                int maxOrder;
                if (LRes.GetLesInSub(subId).ToList().Count != 0)
                    maxOrder = LRes.GetLesInSub(subId).OrderByDescending(x => x.Order).First().Order;
                else
                    maxOrder = 0;

                //Mapping Entity to ViewModel
                lesson.LessonId = model.LessonId;
                lesson.LessonName = model.LessonName.Trim();
                lesson.Content = model.Content;
                lesson.SubjectId = model.SubjectId;
                lesson.Order = maxOrder + 1;


                LRes.AddLesson(lesson);
                TempData["message"] = new ManagerStringCommon().addLessonSuccess.ToString();
                ViewBag.subId = model.SubjectId;
                return RedirectToAction("Details", "Subjects", new { id = model.SubjectId});
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName", model.SubjectId);
            ViewBag.subId = model.SubjectId;

            return View(model);
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = LRes.FindLesson(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }

            LessonViewModels model = new LessonViewModels();

            //Mapping Entity to ViewModel
            model.LessonId = lesson.LessonId;
            model.LessonName = lesson.LessonName;
            model.Content = lesson.Content;
            model.SubjectId = lesson.SubjectId;
            model.SubjectName = lesson.Subject.SubjectName;
            model.Order = lesson.Order;

            return View(model);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lesson lesson = LRes.FindLesson(id);
            LessonViewModels model = new LessonViewModels();
            if (lesson == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.LessonId = lesson.LessonId;
                model.LessonName = lesson.LessonName;
                model.SubjectId = lesson.SubjectId;
                model.SubjectName = lesson.Subject.SubjectName;
                model.Content = lesson.Content;
                model.Order = lesson.Order;
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName", model.SubjectId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LessonViewModels model)
        {
            if (LRes.isExistsLessonNameEdit(model.LessonName, model.LessonId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistLessonName.ToString();
                ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                Lesson lesson = new Lesson();

                //Mapping Entity to ViewModel
                lesson.LessonId = model.LessonId;
                lesson.LessonName = model.LessonName.Trim();
                lesson.SubjectId = model.SubjectId;
                lesson.Content = model.Content;
                lesson.Order = model.Order;

                LRes.EditLesson(lesson);
                TempData["message"] = new ManagerStringCommon().updateLessonSuccess.ToString();
                return RedirectToAction("Details", new { id = model.LessonId });
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");

            return View(model);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = LRes.FindLesson(id);
            LessonViewModels model = new LessonViewModels();
            if (lesson == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.LessonId = lesson.LessonId;
                model.LessonName = lesson.LessonName;
                model.SubjectId = lesson.SubjectId;
                model.SubjectName = lesson.Subject.SubjectName;
                model.Content = lesson.Content;
                model.Order = lesson.Order;

                ViewBag.subId = lesson.SubjectId;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int? subId)
        {
            if (LRes.isExistedMaterialIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistMaterialIn.ToString();
                return RedirectToAction("Delete");
            }
            if (LRes.isExistedQuestionIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistQuestionIn.ToString();
                return RedirectToAction("Delete");
            }

            LRes.DeleteLesson(id);
            TempData["message"] = new ManagerStringCommon().deleteLessonSuccess.ToString();
            return RedirectToAction("Details", "Subjects", new { id = subId});
        }

        public ActionResult MaterialInLesson (int? lesId, int? subId)
        {
            ViewBag.LessonId = lesId;
            ViewBag.SubjectId = subId;

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

            if (lesId == null)
            {
                result = result.Where(x => x.MaterialId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToList());
            }

            if (lesId != null)
            {
                result = result.Where(x => x.LessonId == lesId).ToList();
                ViewBag.totalRecord = result.Count();
            }

            return View(result.OrderBy(x => x.MaterialId).ToList());
        }
        public FileResult DownloadFile(string file)
        {

            var FileVirtualPath = "~/Content/Uploads/Documents/" + file;
            return File(FileVirtualPath, "application/pdf", Path.GetFileName(FileVirtualPath));
        }
        public ActionResult QuestionInLesson (int? lesId, int? subId)
        {
            var question = QRes.GetAllLesQuestion(lesId);
            List<QuestionTestViewModels> result = new List<QuestionTestViewModels>();

            //Mapping Entity to ViewModel
            if (question.Count() > 0)
            {
                foreach (var item in question)
                {
                    if (item.isActived == true)
                    {
                        QuestionTestViewModels model = new QuestionTestViewModels();
                        model.QuestionId = item.QuestionId;
                        model.LessonId = item.LessonId;
                        model.SubjectId = item.SubjectId;
                        model.Photo = item.Photo;
                        if (item.Content != null && item.Content.ToString().Length >= 50)
                            model.Content = item.Content.ToString().Substring(0, 50) + "...";
                        else
                            model.Content = item.Content;
                        model.ListAnswer = item.Answers.Where(x => x.QuestionId == item.QuestionId).ToList();
                        result.Add(model);
                    }
                }
            }

            if (lesId == null)
            {
                result = result.Where(x => x.QuestionId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                ViewBag.lesId = lesId;
                ViewBag.subId = subId;
                return View(result.ToList());
            }

            if (lesId != null)
            {
                result = result.ToList();
                ViewBag.totalRecord = result.Count();
            }
            ViewBag.lesId = lesId;
            ViewBag.subId = subId;
            return View(result.Where(x => x.LessonId == lesId).OrderBy(x => x.QuestionId).ToList());
        }
        public ActionResult FeedbackInLesson (int? id)
        {
            var criteria = CRes.GetAllCriteriaInLes(id);
            List<CriteriaViewModel> result = new List<CriteriaViewModel>();

            //Mapping Entity to ViewModel
            if (criteria.Count() > 0)
            {
                foreach (var item in criteria)
                {
                    if (item.isActived == true)
                    {
                        CriteriaViewModel model = new CriteriaViewModel();
                        model.CriteriaId = item.CriteriaId;
                        model.LessonId = item.LessonId;
                        model.LessonName = item.Lesson.LessonName;
                        if (item.CriteriaName != null && item.CriteriaName.ToString().Length >= 50)
                            model.CriteriaName = item.CriteriaName.ToString().Substring(0, 50) + "...";
                        else
                            model.CriteriaName = item.CriteriaName;

                        result.Add(model);
                    }
                }
            }

            if (id == null)
            {
                result = result.Where(x => x.CriteriaId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                ViewBag.lesId = id;
                return View(result.ToList());
            }

            if (id != null)
            {
                result = result.ToList();
                ViewBag.totalRecord = result.Count();
            }
            ViewBag.lesId = id;
            return View(result.Where(x => x.LessonId == id).OrderBy(x => x.CriteriaId).ToList());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}