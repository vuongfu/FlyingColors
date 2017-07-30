using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;
using TutorOnline.Common;
using TutorOnline.Web.Models;
using PagedList;
using System.Net;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class TutorManagementController : Controller
    {
        private TutorRepository Tres = new TutorRepository();
        public ActionResult Index(string btnSearch, string searchString, int? genderString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;
            ViewBag.btnSearch = btnSearch;
            ViewBag.GenderStr = genderString;
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Nam", Value = "1"},
                new SelectListItem {  Text = "Nữ", Value = "2"},
            }, "Value", "Text");

            var tutors = Tres.GetAllTutor();

            List<TutorInfoViewModels> result = new List<TutorInfoViewModels>();

            //Mapping Entity to ViewModel
            if (tutors.Count() > 0)
            {
                foreach (var item in tutors)
                {
                    TutorInfoViewModels model = new TutorInfoViewModels();
                    //Mapping model with entity
                    model.TutorId = item.TutorId;
                    model.FullName = item.FirstName + " " + item.LastName;
                    model.Photo = item.Photo;
                    model.Gender = (item.Gender == 1) ? "Nam" : "Nữ";
                    model.BirthDate = item.BirthDate;
                    model.Address = item.Address;
                    model.City = item.City;
                    model.PostalCode = item.PostalCode;
                    model.Country = item.Country;
                    model.Email = item.Email;
                    model.SkypeId = item.SkypeId;
                    model.PhoneNumber = item.PhoneNumber;
                    model.Salary = item.Salary;
                    model.BankId = item.BankId;
                    model.BankName = item.BankName;
                    model.BMemName = item.BMemName;
                    model.Description = item.Description;
                    model.isActived = (item.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
                    model.RegisterDate = item.RegisterDate;

                    result.Add(model);
                }
            }

            if (searchString == null && page == null)
            {
                result = result.Where(x => x.TutorId == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => Tres.SearchForString(x.FullName, searchString)).ToList();
            }

            if (genderString != null)
                result = result.Where(x => x.Gender == ((genderString == 1) ? "Nam" : "Nữ")).ToList();

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.FullName).ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DetailsTutor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tutor tutor = Tres.FindTutor(id);

            if (tutor == null)
            {
                return HttpNotFound();
            }

            TutorInfoViewModels model = new TutorInfoViewModels();

            //Mapping model with entity
            model.TutorId = tutor.TutorId;
            model.FullName = tutor.FirstName + " " + tutor.LastName;
            model.Photo = tutor.Photo;
            model.Gender = (tutor.Gender == 1) ? "Nam" : "Nữ";
            model.BirthDate = tutor.BirthDate;
            model.Address = tutor.Address;
            model.City = tutor.City;
            model.PostalCode = tutor.PostalCode;
            model.Country = tutor.Country;
            model.Email = tutor.Email;
            model.SkypeId = tutor.SkypeId;
            model.PhoneNumber = tutor.PhoneNumber;
            model.Salary = tutor.Salary;
            model.BankId = tutor.BankId;
            model.BankName = tutor.BankName;
            model.BMemName = tutor.BMemName;
            model.Description = tutor.Description;
            model.isActived = (tutor.isActived == true) ? "Đang hoạt động" : "Ngưng hoạt động";
            model.RegisterDate = tutor.RegisterDate;

            return View(model);
        }
    }
}