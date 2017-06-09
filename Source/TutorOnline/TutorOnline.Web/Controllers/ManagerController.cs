using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TutorOnline.Web.Models;
using TutorOnline.Business.Repository;
using System.Collections.Generic;
using TutorOnline.DataAccess;
using System.Net;
using TutorOnline.Common;
using PagedList;

namespace TutorOnline.Web.Controllers
{
    public class ManagerController : Controller
    {
        private ManagerRepository MRes = new ManagerRepository();
        public ManagerController(){}

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = MRes.Find(id);
            PersonalViewModel manager = new PersonalViewModel();
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                manager.Id = user.Id;
                manager.FullName = user.FirstName + " " + user.LastName;
                manager.FirstName = user.FirstName;
                manager.LastName = user.LastName;
                manager.Username = user.Username;
                manager.RoleName = user.Role.RoleName;
                manager.BirthDate = user.BirthDate;
                manager.Gender = user.Gender;
                manager.Address = user.Address;
                manager.Email = user.Email;
                manager.SkypeID = user.SkypeID;
                manager.City = user.City;
                manager.PostalCode = user.PostalCode;
                manager.Country = user.Country;
                manager.PhoneNumber = user.PhoneNumber;
                manager.Photo = user.Photo;
                manager.Description = user.Description;
            }
            return View(manager);
        }

        public ActionResult UpdatePerInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = MRes.Find(id);
            PersonalViewModel manager = new PersonalViewModel();
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                manager.Id = user.Id;
                manager.RoleID = user.RoleID;
                manager.FirstName = user.FirstName;
                manager.LastName = user.LastName;
                manager.Username = user.Username;
                manager.Password = user.Password;
                manager.OldPassword = user.Password;
                manager.NewPassword = user.Password;
                manager.ConfirmPassword = user.Password;
                manager.BirthDate = user.BirthDate;
                manager.Gender = user.Gender;
                manager.Address = user.Address;
                manager.Email = user.Email;
                manager.SkypeID = user.SkypeID;
                manager.City = user.City;
                manager.PostalCode = user.PostalCode;
                manager.Country = user.Country;
                manager.PhoneNumber = user.PhoneNumber;
                manager.Photo = user.Photo;
                manager.Description = user.Description;
            }
            return View(manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePerInfo(PersonalViewModel manager)
        {
            User user = new User();
            user.Id = manager.Id;
            user.RoleID = manager.RoleID;
            user.FirstName = manager.FirstName;
            user.LastName = manager.LastName;
            user.Username = manager.Username;
            user.Password = manager.Password;
            user.BirthDate = manager.BirthDate;
            user.Gender = manager.Gender;
            user.Address = manager.Address;
            user.Email = manager.Email;
            user.SkypeID = manager.SkypeID;
            user.City = manager.City;
            user.PostalCode = manager.PostalCode;
            user.Country = manager.Country;
            user.PhoneNumber = manager.PhoneNumber;
            user.Photo = manager.Photo;
            user.Description = manager.Description;

            if (ModelState.IsValid)
            {
                MRes.Edit(user);
                return RedirectToAction("Index", new { id = user.Id});
            }
            return View(manager);
        }

        public ActionResult ChangePwd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = MRes.Find(id);
            PersonalViewModel manager = new PersonalViewModel();
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                manager.Id = user.Id;
                manager.RoleID = user.RoleID;
                manager.FirstName = user.FirstName;
                manager.LastName = user.LastName;
                manager.Username = user.Username;
                manager.FullName = user.FirstName + " " + user.LastName;
                manager.BirthDate = user.BirthDate;
                manager.Gender = user.Gender;
                manager.Address = user.Address;
                manager.Email = user.Email;
                manager.SkypeID = user.SkypeID;
                manager.City = user.City;
                manager.PostalCode = user.PostalCode;
                manager.Country = user.Country;
                manager.PhoneNumber = user.PhoneNumber;
                manager.Photo = user.Photo;
                manager.Description = user.Description;
                manager.Password = user.Password;
                manager.OldPassword = "";
                manager.NewPassword = "";
                manager.ConfirmPassword = "";
            }
            return View(manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePwd(PersonalViewModel manager)
        {
            User user = new User();
            user.Id = manager.Id;
            user.FirstName = manager.FirstName;
            user.LastName = manager.LastName;
            user.Username = manager.Username;
            user.Password = manager.NewPassword;
            user.RoleID = manager.RoleID;
            user.BirthDate = manager.BirthDate;
            user.Gender = manager.Gender;
            user.Address = manager.Address;
            user.Email = manager.Email;
            user.SkypeID = manager.SkypeID;
            user.City = manager.City;
            user.PostalCode = manager.PostalCode;
            user.Country = manager.Country;
            user.PhoneNumber = manager.PhoneNumber;
            user.Photo = manager.Photo;
            user.Description = manager.Description;

            if (ModelState.IsValid)
            {
                MRes.Edit(user);
                TempData["message"] = new StringCommon().changePwdSuccess.ToString();
                return RedirectToAction("ChangePwd", new { id = user.Id });
            }
            return View(manager);
        }

        public ActionResult ManageCategories(string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;

            var Categories = MRes.GetAllCategories();
            List<CategoriesViewModel> result = new List<CategoriesViewModel>();

            //Mapping Entity to ViewModel
            if(Categories.Count() > 0)
            {
                foreach (var item in Categories)
                {
                    CategoriesViewModel model = new CategoriesViewModel();
                    model.Id = item.Id;
                    model.CategoryName = item.CategoryName;
                    model.Description = item.Description;
                    result.Add(model);
                }
            }

            if (searchString == null && page == null)
            {
                result = result.Where(x => x.Id == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => MRes.SearchForString(x.CategoryName, searchString) ||
                                           MRes.SearchForString(x.Description, searchString)).ToList();
            }

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.CategoryName).ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ManageSubjects(string searchString, string cateString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;
            ViewBag.cateStr = new SelectList(MRes.GetAllCategories(), "CategoryName", "CategoryName");

            var result = MRes.GetAllSubject();
            int totalRecord = result.Count();

            if ((searchString == null || cateString == null) && page == null)
            {
                result = result.Where(x => x.Id == 0);
                totalRecord = result.Count();
                return View(result.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => MRes.SearchForString(x.SubjectName, searchString) ||
                                           MRes.SearchForString(x.Description, searchString) ||
                                           MRes.SearchForString(x.Category.CategoryName, searchString));
            }

            if (!String.IsNullOrEmpty(cateString))
            {
                result = result.Where(x => x.Category.CategoryName == cateString);
            }

            ViewBag.totalRecord = totalRecord;
            return View(result.OrderBy(x => x.SubjectName).ToPagedList(pageNumber, pageSize));

        }

        public ActionResult ListPreTutor()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}