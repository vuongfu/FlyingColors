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
                TempData["message"] = "Bạn đã đổi mật khẩu thành công";
                return RedirectToAction("ChangePwd", new { id = user.Id });
            }
            return View(manager);
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