using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Core.Entity;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    public class CourseController : Controller
    {
        private CourseRepository _courseRes = new CourseRepository();
        private SubjectRepository _subjectRes = new SubjectRepository();

        // GET: Course
        public ActionResult Index()
        {
            var temp = _courseRes.GetAll();
            IEnumerable<CourseViewModel> model;
            List<CourseViewModel> list = new List<CourseViewModel>();
            foreach(Course course in temp)
            {
                CourseViewModel tempModel = new CourseViewModel();
                tempModel.CourseID = course.CourseID;
                tempModel.CourseName = course.CourseName;
                tempModel.Description = course.Description;
                tempModel.EndDate = course.EndDate;
                tempModel.Photo = course.Photo;
                tempModel.Price = course.Price;
                tempModel.Purpose = course.Purpose;
                tempModel.Requirement = course.Requirement;
                tempModel.StartDate = course.StartDate;
                tempModel.SubjectID = course.SubjectID;
                tempModel.Title = course.Title;

                var subject = _subjectRes.GetSubject(course.SubjectID);
                if (subject != null)
                    tempModel.SubjectName = subject.SubjectName;

                list.Add(tempModel);
            }

            model = list;
            return View(model);
        }


        public ActionResult Create()
        {
            CourseViewModel model = new CourseViewModel();
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today;
            model.ListSub = GetListSubjectToDropDownList(0);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                Course _course = new Course();
                _course.CourseID = -1;
                _course.CourseName = course.CourseName;
                _course.Description = course.Description;
                _course.EndDate = course.EndDate;
                _course.Photo = course.Photo;
                _course.Price = course.Price;
                _course.Purpose = course.Purpose;
                _course.Requirement = course.Requirement;
                _course.StartDate = course.StartDate;
                _course.SubjectID = course.SubjectID;
                _course.Title = course.Title;

                _courseRes.AddOrUpdate(_course);
                TempData["message"] = string.Format("{0} has been saved.", _course.CourseName);
                return RedirectToAction("Index");
            }
            else
            {
                
                course.ListSub = GetListSubjectToDropDownList(course.SubjectID);

                //there is some thing was wrong with the data values
                return View(course);
            }
        }

        public ActionResult Edit(int courseID)
        {
            var EditCourse = _courseRes.GetCourse(courseID);
            CourseViewModel tempModel = new CourseViewModel();
            tempModel.CourseID = EditCourse.CourseID;
            tempModel.CourseName = EditCourse.CourseName;
            tempModel.Description = EditCourse.Description;
            tempModel.EndDate = EditCourse.EndDate;
            tempModel.Photo = EditCourse.Photo;
            tempModel.Price = EditCourse.Price;
            tempModel.Purpose = EditCourse.Purpose;
            tempModel.Requirement = EditCourse.Requirement;
            tempModel.StartDate = EditCourse.StartDate;
            tempModel.SubjectID = EditCourse.SubjectID;
            tempModel.Title = EditCourse.Title;
            tempModel.ListSub = GetListSubjectToDropDownList(EditCourse.SubjectID);

            return View(tempModel);
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                Course _course = new Course();
                _course.CourseID = course.CourseID;
                _course.CourseName = course.CourseName;
                _course.Description = course.Description;
                _course.EndDate = course.EndDate;
                _course.Photo = course.Photo;
                _course.Price = course.Price;
                _course.Purpose = course.Purpose;
                _course.Requirement = course.Requirement;
                _course.StartDate = course.StartDate;
                _course.SubjectID = course.SubjectID;
                _course.Title = course.Title;

                _courseRes.AddOrUpdate(_course);
                TempData["message"] = string.Format("{0} has been saved.", _course.CourseName);
                return RedirectToAction("Index");
            }
            else
            {

                course.ListSub = GetListSubjectToDropDownList(course.SubjectID);

                //there is some thing was wrong with the data values
                return View(course);
            }
        }

        public ActionResult Details(int courseId)
        {
            var course = _courseRes.GetCourse(courseId);
            CourseViewModel tempModel = new CourseViewModel();
            tempModel.CourseID = course.CourseID;
            tempModel.CourseName = course.CourseName;
            tempModel.Description = course.Description;
            tempModel.EndDate = course.EndDate;
            tempModel.Photo = course.Photo;
            tempModel.Price = course.Price;
            tempModel.Purpose = course.Purpose;
            tempModel.Requirement = course.Requirement;
            tempModel.StartDate = course.StartDate;
            tempModel.SubjectID = course.SubjectID;
            tempModel.Title = course.Title;

            var subject = _subjectRes.GetSubject(course.SubjectID);
            if (subject != null)
                tempModel.SubjectName = subject.SubjectName;

            return View(tempModel);
        }

        public ActionResult Delete(int courseID)
        {
            Course DeleteCourse = _courseRes.DeleteCourse(courseID);
            if(DeleteCourse != null)
            {
                TempData["message"] = string.Format("{0} was deleted.", DeleteCourse.CourseName);
            }
            
            return RedirectToAction("Index");
        }

        List<SelectListItem> GetListSubjectToDropDownList(int selected)
        {
            IEnumerable<Subject> ListSubs = _subjectRes.GetAll();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Subject sub in ListSubs)
            {
                bool isSelected = false;
                if (sub.SubjectID == selected)
                    isSelected = true;
                items.Add(new SelectListItem { Text = sub.SubjectName, Value = sub.SubjectID.ToString(), Selected = isSelected });
            }

            return items;
        }
    }
}