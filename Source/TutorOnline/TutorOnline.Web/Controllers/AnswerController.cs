using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Web.Models;
using TutorOnline.DataAccess;
using TutorOnline.Common;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = UserCommonString.Manager)]
    public class AnswerController : Controller
    {
        private QuestionRepository QRes = new QuestionRepository();
        private AnswersRepository ARes = new AnswersRepository();

        public ActionResult Create(int? queId)
        {
            if (queId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.isCorrect = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Đúng", Value = "true"},
                new SelectListItem {  Text = "Sai", Value = "false"},
            }, "Value", "Text");
            ViewBag.quesContent = QRes.FindQuestion(queId).Content.ToString();
            ViewBag.quesPhoto = QRes.FindQuestion(queId).Photo;
            ViewBag.queId = queId;
            ViewBag.subId = QRes.FindQuestion(queId).Lesson.SubjectId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerViewModels model)
        {
            int queId = Convert.ToInt32(TempData["queId"]);
            if (ModelState.IsValid)
            {
                Answer answer = new Answer();
                if (ARes.isExistsAnswerName(model.Content, queId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistAnswerContent.ToString();
                    ViewBag.isCorrect = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Đúng", Value = "true"},
                        new SelectListItem {  Text = "Sai", Value = "false"},
                    }, "Value", "Text");
                    ViewBag.quesContent = QRes.FindQuestion(queId).Content.ToString();
                    ViewBag.quesPhoto = QRes.FindQuestion(queId).Photo;
                    ViewBag.queId = queId;
                    ViewBag.subId = QRes.FindQuestion(queId).Lesson.SubjectId;
                    return View(model);
                }
                //Mapping Entity to ViewModel
                answer.QuestionId = queId;
                answer.AnswerId = model.AnswerId;
                answer.Content = model.Content.Trim();
                answer.isCorrect = model.isCorrect;

                ARes.AddAnswer(answer);
                TempData["message"] = new ManagerStringCommon().addAnswerSuccess.ToString();
                ViewBag.queId = queId;
                ViewBag.subId = QRes.FindQuestion(queId).Lesson.SubjectId;
                return RedirectToAction("Details", "Question", new { id = queId, subId = ViewBag.subId });
            }
            ViewBag.isCorrect = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Đúng", Value = "true"},
                new SelectListItem {  Text = "Sai", Value = "false"},
            }, "Value", "Text");
            ViewBag.quesContent = QRes.FindQuestion(queId).Content.ToString();
            ViewBag.quesPhoto = QRes.FindQuestion(queId).Photo;
            ViewBag.queId = queId;
            ViewBag.subId = QRes.FindQuestion(queId).Lesson.SubjectId;

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = ARes.FindAnswer(id);
            if (answer == null)
            {
                return HttpNotFound();
            }

            AnswerViewModels model = new AnswerViewModels();

            //Mapping Entity to ViewModel
            model.AnswerId = answer.AnswerId;
            model.QuestionId = answer.QuestionId;
            model.Content = answer.Content;
            model.isCorrect = answer.isCorrect;
            model.QuesContent = answer.Question.Content;
            model.QuesPhoto = answer.Question.Photo;
            model.isCorrectStr = (answer.isCorrect == true) ? "Đúng" : "Sai";

            ViewBag.queId = answer.QuestionId;
            ViewBag.subId = QRes.FindQuestion(answer.QuestionId).Lesson.SubjectId;
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = ARes.FindAnswer(id);
            AnswerViewModels model = new AnswerViewModels();
            if (answer == null)
            {
                return HttpNotFound();
            }
            else
            {
                model.AnswerId = answer.AnswerId;
                model.Content = answer.Content;
                model.QuestionId = answer.QuestionId;
                model.QuesContent = answer.Question.Content;
                model.QuesPhoto = answer.Question.Photo;
                model.isCorrect = answer.isCorrect;
                model.isCorrectStr = (answer.isCorrect == true) ? "Đúng" : "Sai";
                ViewBag.isCorrect = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {  Text = "Đúng", Value = "true"},
                    new SelectListItem {  Text = "Sai", Value = "false"},
                }, "Value", "Text", model.isCorrect);
            }
            ViewBag.subId = QRes.FindQuestion(model.QuestionId).Lesson.SubjectId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnswerViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (ARes.isExistsAnswerNameEdit(model.Content, model.AnswerId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistAnswerContent.ToString();
                    ViewBag.isCorrect = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Đúng", Value = "true"},
                        new SelectListItem {  Text = "Sai", Value = "false"},
                    }, "Value", "Text", model.isCorrect);
                    ViewBag.subId = QRes.FindQuestion(model.QuestionId).Lesson.SubjectId;
                    return View(model);
                }

                Answer answer = ARes.FindAnswer(model.AnswerId);
                answer.Content = model.Content.Trim();
                answer.isCorrect = model.isCorrect;


                ARes.EditAnswer(answer);

                TempData["message"] = new ManagerStringCommon().updateAnswerSuccess.ToString();
                ViewBag.isCorrect = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {  Text = "Đúng", Value = "true"},
                    new SelectListItem {  Text = "Sai", Value = "false"},
                }, "Value", "Text", model.isCorrect);
                ViewBag.subId = QRes.FindQuestion(model.QuestionId).Lesson.SubjectId;
                return RedirectToAction("Details", new { id = model.AnswerId });
            }

            ViewBag.isCorrect = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Đúng", Value = "true"},
                new SelectListItem {  Text = "Sai", Value = "false"},
            }, "Value", "Text", model.isCorrect);
            ViewBag.subId = QRes.FindQuestion(model.QuestionId).Lesson.SubjectId;
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Details(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int queId = ARes.FindAnswer(id).QuestionId;
            int subId = ARes.FindAnswer(id).Question.Lesson.SubjectId;

            ARes.DeleteAnswer(id);
            TempData["message"] = new ManagerStringCommon().deleteAnswerSuccess.ToString();
            return RedirectToAction("Details", "Question", new { id = queId, subId = subId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                QRes.Dispose();
                ARes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}