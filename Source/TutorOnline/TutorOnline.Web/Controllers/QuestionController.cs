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
    [Authorize(Roles = UserCommonString.Manager)]
    public class QuestionController : Controller
    {
        // GET: Question
        private QuestionRepository QRes = new QuestionRepository();
        private LessonRepository LRes = new LessonRepository();
        private AnswersRepository ARes = new AnswersRepository();
        private SubjectsRepository SRes = new SubjectsRepository();

        public ActionResult CreateLinkQuestion(int? subId)
        {
            if (subId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.subName = SRes.FindSubject(subId).SubjectName;
            ViewBag.subId = subId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkQuestion(QuestionLinkViewModels model)
        {
            int subId = Convert.ToInt32(TempData["subId"]);
            string subName = Convert.ToString(TempData["subName"]);

            if (ModelState.IsValid)
            {
                Question question = new Question();
                if (QRes.isExistsQuestionLink(model.Link, subId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistQuestionLink.ToString();
                    ViewBag.subId = subId;
                    ViewBag.subName = subName;
                    return View(model);
                }
                //Mapping Entity to ViewModel
                question.Content = model.Link.Trim();
                question.SubjectId = subId;

                QRes.AddQuestion(question);
                TempData["message"] = new ManagerStringCommon().addLinkQuestionSuccess.ToString();
                ViewBag.subId = subId;
                ViewBag.subName = subName;
                return RedirectToAction("Details", "Subjects", new { id = subId });
            }
            ViewBag.subId = subId;
            ViewBag.subName = subName;

            return View(model);
        }
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
                question.Content = model.Content.Trim();
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

            if (QRes.isExistsQuestionNameEdit(model.Content, model.QuestionId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistQuestionName.ToString();
                ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName", model.LessonId);
                ViewBag.lesId = model.LessonId;
                ViewBag.subId = subId;
                return View(model);
            }

            if (ModelState.IsValid)
            {
                Question question = QRes.FindQuestion(model.QuestionId);

                //Mapping Entity to ViewModel
                question.Content = model.Content.Trim();
                question.Photo = (string.IsNullOrEmpty(photoUrl) ? model.Photo : photoUrl);

                QRes.EditQuestion(question);
                TempData["message"] = new ManagerStringCommon().updateQuestionSuccess.ToString();
                ViewBag.lesId = model.LessonId;
                ViewBag.subId = subId;
                return RedirectToAction("Details", new { id = model.QuestionId, subId = ViewBag.subId });
            }
            ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName", model.LessonId);
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

        public ActionResult DeleleQuestionLink (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = QRes.FindQuestion(id);
            QuestionLinkViewModels model = new QuestionLinkViewModels();
            if (question == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.QuestionId = question.QuestionId;
                model.Link = question.Content;
                model.subjectName = question.Subject.SubjectName;

                ViewBag.subId = question.SubjectId;
            }
            return View(model);
        }

        [HttpPost, ActionName("DeleleQuestionLink")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleleQuestionLink(int id, int? subId)
        {
            QRes.DeleteQuestion(id);
            TempData["message"] = new ManagerStringCommon().deleteQuestionLinkSuccess.ToString();
            return RedirectToAction("Details", "Subjects", new { id = subId });
        }
        public ActionResult AnswerInQuestion(int? queId)
        {
            var answer = ARes.GetQuesAnswer(queId);
            List<AnswerViewModels> result = new List<AnswerViewModels>();

            //Mapping Entity to ViewModel
            if (answer.Count() > 0)
            {
                foreach (var item in answer)
                {
                    if (item.isActived == true)
                    {
                        AnswerViewModels model = new AnswerViewModels();
                        model.AnswerId = item.AnswerId;
                        model.QuestionId = item.QuestionId;
                        model.QuesContent = item.Question.Content;
                        model.isCorrectStr = (item.isCorrect == true) ? "Đúng" : "Sai";
                        model.isCorrect = item.isCorrect;
                        if (item.Content != null && item.Content.ToString().Length >= 50)
                            model.Content = item.Content.ToString().Substring(0, 50) + "...";
                        else
                            model.Content = item.Content;

                        result.Add(model);
                    }
                }
            }

            if (queId == null)
            {
                result = result.Where(x => x.AnswerId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                ViewBag.queId = queId;

                return View(result.ToList());
            }

            if (queId != null)
            {
                result = result.ToList();
                ViewBag.totalRecord = result.Count();
            }
            ViewBag.queId = queId;

            return View(result.Where(x => x.QuestionId == queId).OrderBy(x => x.AnswerId).ToList());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                QRes.Dispose();
                LRes.Dispose();
                ARes.Dispose();
                SRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}