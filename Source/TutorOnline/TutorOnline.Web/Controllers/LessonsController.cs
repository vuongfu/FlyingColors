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
    [Authorize(Roles = "Manager")]
    public class LessonsController : Controller
    {
        // GET: Lesson
        private LessonRepository LRes = new LessonRepository();
        private LearningMaterialRepository LMRes = new LearningMaterialRepository();
        private QuestionRepository QRes = new QuestionRepository();
        private AnswersRepository ARes = new AnswersRepository();
        private SubjectsRepository SRes = new SubjectsRepository();
        
        //public ActionResult Index(string btnSearch, string searchString, string subString, int? page)
        //{
        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);

        //    ViewBag.searchString = searchString;
        //    ViewBag.subStr = subString;
        //    ViewBag.subString = new SelectList(SRes.GetAllSubject(), "SubjectName", "SubjectName");
        //    ViewBag.btnSearch = btnSearch;

        //    var lessons = LRes.GetAllLessons();
        //    List<LessonViewModels> result = new List<LessonViewModels>();

        //    //Mapping Entity to ViewModel
        //    if (lessons.Count() > 0)
        //    {
        //        foreach (var item in lessons)
        //        {
        //            LessonViewModels model = new LessonViewModels();
        //            model.LessonId = item.LessonId;
        //            model.LessonName = item.LessonName;
        //            model.SubjectId = item.SubjectId;
        //            model.SubjectName = item.Subject.SubjectName;
        //            if (item.Content != null && item.Content.ToString().Length >= 30)
        //                model.Content = item.Content.ToString().Substring(0, 30) + "...";
        //            else
        //                model.Content = item.Content;
                    
        //            result.Add(model);
        //        }
        //    }

        //    if ((searchString == null || subString == null) && page == null)
        //    {
        //        result = result.Where(x => x.LessonId == 0).ToList();
        //        ViewBag.totalRecord = result.Count();
        //        return View(result.ToList().ToPagedList(pageNumber, pageSize));
        //    }

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        result = result.Where(x => LRes.SearchForString(x.LessonName, searchString) ||
        //                                   LRes.SearchForString(x.Content, searchString)).ToList();
        //    }

        //    if (!String.IsNullOrEmpty(subString))
        //    {
        //        result = result.Where(x => x.SubjectName == subString).ToList();
        //    }

        //    ViewBag.totalRecord = result.Count();
        //    return View(result.OrderBy(x => x.LessonName).ToList().ToPagedList(pageNumber, pageSize));

        //}
        
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName", ViewBag.SubjectId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LessonViewModels model)
        {
            if (ModelState.IsValid)
            {
                Lesson lesson = new Lesson();
                if (LRes.isExistsLessonName(model.LessonName, model.SubjectId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistLessonName.ToString();
                    ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                    return View(model);
                }
                //Mapping Entity to ViewModel
                lesson.LessonId = model.LessonId;
                lesson.LessonName = model.LessonName;
                lesson.Content = model.Content;
                lesson.SubjectId = model.SubjectId;

                LRes.AddLesson(lesson);
                TempData["message"] = new ManagerStringCommon().addLessonSuccess.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName", model.SubjectId);

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
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName", model.SubjectId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LessonViewModels model)
        {
            if (LRes.isExistsLessonName(model.LessonName, model.SubjectId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistLessonName.ToString();
                ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                return View(model);
            }

            Lesson lesson = new Lesson();

            //Mapping Entity to ViewModel
            lesson.LessonId = model.LessonId;
            lesson.LessonName = model.LessonName;
            lesson.SubjectId = model.SubjectId;
            lesson.Content = model.Content;

            if (ModelState.IsValid)
            {
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
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (LRes.isExistedMaterialIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistMaterialIn.ToString();
                return RedirectToAction("Index");
            }
            if (LRes.isExistedQuestionIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistQuestionIn.ToString();
                return RedirectToAction("Index");
            }

            LRes.DeleteLesson(id);
            TempData["message"] = new ManagerStringCommon().deleteLessonSuccess.ToString();
            return RedirectToAction("Index");
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