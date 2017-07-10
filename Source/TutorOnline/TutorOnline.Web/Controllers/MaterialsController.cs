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
    [Authorize]
    public class MaterialsController : Controller
    {
        // GET: Materials
        private LearningMaterialRepository LMRes = new LearningMaterialRepository();
        // GET: Lessons
        private LessonRepository LRes = new LessonRepository();
        //GET: Subjects
        private SubjectsRepository SRes = new SubjectsRepository();

        public ActionResult Index(string btnSearch, string subString, string searchString, string lesString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.searchString = searchString;
            ViewBag.lesString = new SelectList(LRes.GetAllLessons(), "LessonName", "LessonName");
            ViewBag.lesStr = lesString;
            ViewBag.subString = new SelectList(SRes.GetAllSubject(), "SubjectName", "SubjectName");
            ViewBag.subStr = subString;
            ViewBag.btnSearch = btnSearch;

            var material = LMRes.GetAllMaterial();
            List<MaterialViewModels> result = new List<MaterialViewModels>();

            //Mapping Entity to ViewModel
            if (material.Count() > 0)
            {
                foreach (var item in material)
                {
                    MaterialViewModels model = new MaterialViewModels();
                    model.MaterialId = item.MaterialId;
                    if (item.MaterialUrl != null && item.MaterialUrl.ToString().Length >= 30)
                        model.MaterialUrl = item.MaterialUrl.ToString().Substring(0, 30) + "...";
                    else
                        model.MaterialUrl = item.MaterialUrl;
                    model.LessonId = item.LessonId;
                    if (item.Lesson.LessonName != null && item.Lesson.LessonName.ToString().Length >= 30)
                        model.LessonName = item.Lesson.LessonName.ToString().Substring(0, 30) + "...";
                    else
                        model.LessonName = item.Lesson.LessonName;
                    model.SubjectId = item.Lesson.Subject.SubjectId;
                    if (item.Lesson.Subject.SubjectName != null && item.Lesson.Subject.SubjectName.ToString().Length >= 30)
                        model.SubjectName = item.Lesson.Subject.SubjectName.ToString().Substring(0, 30) + "...";
                    else
                        model.SubjectName = item.Lesson.Subject.SubjectName;
                    model.MaterialTypeId = item.MaterialTypeId;
                    model.MaterialTypeName = item.MaterialType.MaterialTypeName;

                    result.Add(model);
                }
            }

            if ((searchString == null || lesString == null || subString == null) && page == null)
            {
                result = result.Where(x => x.MaterialId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToList().ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => LMRes.SearchForString(x.MaterialUrl, searchString)).ToList();
            }
            if (!String.IsNullOrEmpty(subString))
            {
                result = result.Where(x => x.SubjectName == subString).ToList();
            }
            if (!String.IsNullOrEmpty(lesString))
            {
                result = result.Where(x => x.LessonName == lesString).ToList();
            }

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.MaterialUrl).ToList().ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
            ViewBag.LessonId = new SelectList(LRes.GetAllLessons(), "LessonId", "LessonName");
            //ViewBag.MaterialTypeId = new SelectList(LMRes.GetAllMaType(), "MaterialTypeId", "MaterialTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaterialViewModels model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string docUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.document);
                LearningMaterial material = new LearningMaterial();
                if (LMRes.isExistsMaterialName(model.MaterialUrl, model.LessonId))
                {
                    TempData["messageWarning"] = new ManagerStringCommon().isExistMaterialName.ToString();
                    ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
                    ViewBag.LessonId = new SelectList(LRes.GetAllLessons(), "LessonId", "LessonName");
                    //ViewBag.MaterialTypeId = new SelectList(LMRes.GetAllMaType(), "MaterialTypeId", "MaterialTypeName");
                    return View(model);
                }
                //Mapping Entity to ViewModel
                material.MaterialId = model.MaterialId;
                material.MaterialUrl = (string.IsNullOrEmpty(docUrl) ? model.MaterialUrl : docUrl);
                material.LessonId = model.LessonId;
                material.MaterialTypeId = model.MaterialTypeId;

                LMRes.AddMaterial(material);
                TempData["message"] = new ManagerStringCommon().addMaterialSuccess.ToString();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(SRes.GetAllSubject(), "SubjectId", "SubjectName");
            ViewBag.LessonId = new SelectList(LRes.GetAllLessons(), "LessonId", "LessonName");
            //ViewBag.MaterialTypeId = new SelectList(LMRes.GetAllMaType(), "MaterialTypeId", "MaterialTypeName");
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
            model.SubjectId = material.Lesson.Subject.SubjectId;
            model.SubjectName = material.Lesson.Subject.SubjectName;
            model.LessonId = material.LessonId;
            model.LessonName = material.Lesson.LessonName;
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
                model.SubjectName = material.Lesson.Subject.SubjectName;
                model.LessonId = material.LessonId;
                model.LessonName = material.Lesson.LessonName;
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

            if (LMRes.isExistsMaterialName(model.LessonName, model.LessonId))
            {
                TempData["messageWarning"] = new ManagerStringCommon().isExistMaterialName.ToString();
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
                model.SubjectId = material.Lesson.Subject.SubjectId;
                model.SubjectName = material.Lesson.Subject.SubjectName;
                model.LessonId = material.LessonId;
                model.LessonName = material.Lesson.LessonName;
                model.MaterialTypeId = material.MaterialTypeId;
                model.MaterialTypeName = material.MaterialType.MaterialTypeName;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            LMRes.DeleteMaterial(id);
            TempData["message"] = new ManagerStringCommon().deleteMaterialSuccess.ToString();
            return RedirectToAction("Index");
        }

        public ActionResult Downloads()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Content/Uploads/Document/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }

            return View(items);
        }

        public FileResult Download(string file)
        {

            var FileVirtualPath = "~/Content/Uploads/Document/" + file;
            return File(FileVirtualPath, "application/pdf", Path.GetFileName(FileVirtualPath));
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