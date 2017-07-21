using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Common;
using TutorOnline.DataAccess;
using TutorOnline.Web.Models;
namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class QuestionController : Controller
    {
        // GET: Question
        private QuestionRepository QRes = new QuestionRepository();
        private LessonRepository LRes = new LessonRepository();

        public ActionResult Create(int? lesId, int? subId)
        {
            if (lesId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName", lesId);
            ViewBag.lesId = lesId;
            ViewBag.subId = subId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModels model, HttpPostedFileBase file)
        {
            int subId = Convert.ToInt32(TempData["subId"]);
            if (ModelState.IsValid)
            {
                string photoUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);
                Question question = new Question();
                if (QRes.isExistsQuestionName(model.Content, model.LessonId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistQuestionName.ToString();
                    ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName", model.LessonId);
                    ViewBag.lesId = model.LessonId;
                    ViewBag.subId = model.SubjectId;
                    return View(model);
                }
                //Mapping Entity to ViewModel
                question.QuestionId = model.QuestionId;
                question.Content = model.Content;
                question.Photo = (string.IsNullOrEmpty(photoUrl) ? model.Photo : photoUrl);
                question.LessonId = model.LessonId;
                question.SubjectId = model.SubjectId;

                QRes.AddQuestion(question);
                TempData["message"] = new ManagerStringCommon().addQuestionSuccess.ToString();
                ViewBag.lesId = model.LessonId;
                ViewBag.subId = model.SubjectId;
                return RedirectToAction("Details", "Lessons", new { id = model.LessonId });
            }
            ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName", model.LessonId);
            ViewBag.lesId = model.LessonId;
            ViewBag.subId = model.SubjectId;

            return View(model);
        }

        public ActionResult Details(int? id, int? subId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = QRes.FindQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            QuestionViewModels model = new QuestionViewModels();

            //Mapping Entity to ViewModel
            model.QuestionId = question.QuestionId;
            model.Content = question.Content;
            model.LessonId = question.LessonId;
            model.lessonName = question.Lesson.LessonName;
            model.SubjectId = question.SubjectId;
            model.Photo = question.Photo;

            ViewBag.subId = subId;
            ViewBag.lesId = model.LessonId;
            return View(model);
        }

        public ActionResult Edit(int? id, int? subId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = QRes.FindQuestion(id);
            QuestionViewModels model = new QuestionViewModels();
            if (question == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.QuestionId = question.QuestionId;
                model.Content = question.Content;
                model.Photo = question.Photo;
                model.lessonName = question.Lesson.LessonName;
                model.LessonId = question.LessonId;
                model.SubjectId = question.SubjectId;
                ViewBag.subId = subId;
                ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName", question.LessonId);
            }
            ViewBag.subId = subId;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModels model, HttpPostedFileBase file)
        {
            int subId = Convert.ToInt32(TempData["subId"]);
            string photoUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);
            if (QRes.isExistsQuestionName(model.Content, model.LessonId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistQuestionName.ToString();
                ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName");
                ViewBag.lesId = model.LessonId;
                ViewBag.subId = subId;
                return View(model);
            }

            Question question = new Question();

            //Mapping Entity to ViewModel
            question.QuestionId = model.QuestionId;
            question.Content = model.Content;
            question.Photo = (string.IsNullOrEmpty(photoUrl) ? model.Photo : photoUrl);
            question.SubjectId = model.SubjectId;
            question.LessonId = model.LessonId;

            if (ModelState.IsValid)
            {
                QRes.EditQuestion(question);
                TempData["message"] = new ManagerStringCommon().updateQuestionSuccess.ToString();
                ViewBag.lesId = model.LessonId;
                ViewBag.subId = subId;
                return RedirectToAction("Details", new { id = model.QuestionId });
            }
            ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName", question.LessonId);
            ViewBag.lesId = model.LessonId;
            ViewBag.subId = subId;
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = QRes.FindQuestion(id);
            QuestionViewModels model = new QuestionViewModels();
            if (question == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.QuestionId = question.QuestionId;
                model.Content = question.Content;
                model.lessonName = question.Lesson.LessonName;
                model.Photo = question.Photo;
                model.SubjectId = question.SubjectId;
                model.LessonId = question.LessonId;

                ViewBag.lesId = question.LessonId;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int? lesId)
        {
            if (QRes.isExistedAnswerIn(id))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistAnswerIn.ToString();
                return RedirectToAction("Delete");
            }

            QRes.DeleteQuestion(id);
            TempData["message"] = new ManagerStringCommon().deleteQuestionSuccess.ToString();
            return RedirectToAction("Details", "Lessons", new { id = lesId });
        }

        public ActionResult AnswerInQuestion(int? id)
        {
            return View();
        }
    }
}