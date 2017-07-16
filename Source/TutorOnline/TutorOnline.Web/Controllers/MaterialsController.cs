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
    public class MaterialsController : Controller
    {
        // GET: Materials
        private LearningMaterialRepository LMRes = new LearningMaterialRepository();
        // GET: Lessons
        private LessonRepository LRes = new LessonRepository();
        //GET: Subjects
        private SubjectsRepository SRes = new SubjectsRepository();
        
        public ActionResult Create(int? subId, int? lesId)
        {
            if (subId != null && lesId == null)
            {
                ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                ViewBag.subId = subId;
                
            }
            else
            {
                ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName");
                ViewBag.lesId = lesId;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, int? subId, int? lesId)
        {
            string docUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.document);

            MaterialViewModels model = new MaterialViewModels();
            model.MaterialUrl = docUrl;
            model.MaterialTypeId = FileUpload.GetFileTypeId(file);
            model.LessonId = lesId;
            model.SubjectId = subId;
            model.Description = "";

            if (ModelState.IsValid)
            {
                LearningMaterial material = new LearningMaterial();
                if (LMRes.isExistsMaterialNameInSub(model.MaterialUrl, model.SubjectId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistMaterialNameSub.ToString();
                    if (subId != null && lesId == null)
                    {
                        ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                        ViewBag.subId = subId;

                    }
                    else
                    {
                        ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName");
                        ViewBag.lesId = lesId;
                    }
                    return View(model);
                }
                //Mapping Entity to ViewModel
                material.MaterialUrl = model.MaterialUrl;
                material.LessonId = model.LessonId;
                material.SubjectId = model.SubjectId;
                material.MaterialTypeId = model.MaterialTypeId;

                LMRes.AddMaterial(material);
                TempData["message"] = new ManagerStringCommon().addMaterialSuccess.ToString();

                if (subId != null && lesId == null)
                    return RedirectToAction("Details", "Subjects", new { id = subId });
                else
                    return RedirectToAction("Details", "Lessons", new { id = lesId });
            }

            if (subId != null && lesId == null)
            {
                ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                ViewBag.subId = subId;

            }
            else
            {
                ViewBag.LessonId = new SelectList(LRes.GetLesInSub(subId), "LessonId", "LessonName");
                ViewBag.lesId = lesId;
            }
            return View(model);
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LearningMaterial material = LMRes.FindMaterial(id);

            if (material == null)
            {
                return HttpNotFound();
            }

            MaterialViewModels model = new MaterialViewModels();

            //Mapping Entity to ViewModel
            model.MaterialId = material.MaterialId;
            model.MaterialUrl = material.MaterialUrl;
            model.SubjectId = material.SubjectId;
            model.LessonId = material.LessonId;
            model.MaterialTypeId = material.MaterialTypeId;
            model.MaterialTypeName = material.MaterialType.MaterialTypeName;

            return View(model);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LearningMaterial material = LMRes.FindMaterial(id);
            MaterialViewModels model = new MaterialViewModels();
            if (material == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.MaterialId = material.MaterialId;
                model.MaterialUrl = material.MaterialUrl;
                model.SubjectId = material.Lesson.Subject.SubjectId;
                model.LessonId = material.LessonId;
                model.MaterialTypeId = material.MaterialTypeId;
                model.MaterialTypeName = material.MaterialType.MaterialTypeName;
            }

            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName", model.SubjectId);
            ViewBag.LessonId = new SelectList(LRes.GetAllLessons(), "LessonId", "LessonName", model.LessonId);
            //ViewBag.MaterialTypeId = new SelectList(LMRes.GetAllMaType(), "MaterialTypeId", "MaterialTypeName", model.MaterialTypeId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaterialViewModels model, HttpPostedFileBase file)
        {
            string docUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.document);

            if (LMRes.isExistsMaterialNameInLes(model.MaterialUrl, model.LessonId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistMaterialNameLes.ToString();
                ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                ViewBag.LessonId = new SelectList(LRes.GetAllLessons(), "LessonId", "LessonName");
                //ViewBag.MaterialTypeId = new SelectList(LMRes.GetAllMaType(), "MaterialTypeId", "MaterialTypeName");
                return View(model);
            }

            LearningMaterial material = new LearningMaterial();

            //Mapping Entity to ViewModel
            material.MaterialId = model.MaterialId;
            material.MaterialUrl = (string.IsNullOrEmpty(docUrl) ? model.MaterialUrl : docUrl);
            material.LessonId = model.LessonId;
            material.MaterialTypeId = model.MaterialTypeId;

            if (ModelState.IsValid)
            {
                LMRes.EditMaterial(material);
                TempData["message"] = new ManagerStringCommon().updateMaterialSuccess.ToString();
                return RedirectToAction("Details", new { id = model.MaterialId });
            }
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
            ViewBag.LessonId = new SelectList(LRes.GetAllLessons(), "LessonId", "LessonName");
            //ViewBag.MaterialTypeId = new SelectList(LMRes.GetAllMaType(), "MaterialTypeId", "MaterialTypeName");
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningMaterial material = LMRes.FindMaterial(id);
            MaterialViewModels model = new MaterialViewModels();
            if (material == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Mapping Entity to ViewModel
                model.MaterialId = material.MaterialId;
                model.MaterialUrl = material.MaterialUrl;
                model.SubjectId = material.SubjectId;
                model.LessonId = material.LessonId;
                model.MaterialTypeId = material.MaterialTypeId;
                model.MaterialTypeName = material.MaterialType.MaterialTypeName;

                ViewBag.subId = material.SubjectId;
                ViewBag.lesId = material.LessonId;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int? subId, int? lesId)
        {
            LMRes.DeleteMaterial(id);
            TempData["message"] = new ManagerStringCommon().deleteMaterialSuccess.ToString();
            if (subId != null && lesId == null)
                return RedirectToAction("Details", "Subjects", new { id = subId});
            else
                return RedirectToAction("Details", "Lessons", new { id = lesId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LMRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}