using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;
using TutorOnline.Web.Models;
using PagedList;
using System.Web.Security;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private UsersRepository URes = new UsersRepository();

        // GET: Users
        public ActionResult Index(string searchString, string roleString, int? genderString, string yearString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.Count = pageSize*(pageNumber-1) + 1;

            ViewBag.SearchStr = searchString;
            ViewBag.RoleStr = roleString;
            ViewBag.GenderStr = genderString;

            ViewBag.roleString = new SelectList(URes.GetAllRole(), "RoleName", "RoleName");
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            },"Value","Text");

            List<IndexUserViewModel> ListUsers = new List<IndexUserViewModel>();

            if ((searchString == null || roleString == null) && page == null)
            {
                //ListUsers = URes.GetAllBackEndUser().Where(s => s.Username == "-1");
                ViewBag.totalRecord = ListUsers.Count();
                return View(ListUsers.ToPagedList(pageNumber, pageSize));
            }

            var BackEndUser = URes.GetAllBackEndUser();
            var Tutor = URes.GetAllTutorUser();
            var Student = URes.GetAllStudentUser();
            var Parent = URes.GetAllParentUser();
            foreach(var record in BackEndUser)
            {
                IndexUserViewModel temp = new IndexUserViewModel();
                temp.Id = record.BackendUserId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.PhoneNumber = record.PhoneNumber;

                ListUsers.Add(temp);
            }
            foreach (var record in Tutor)
            {
                IndexUserViewModel temp = new IndexUserViewModel();
                temp.Id = record.TutorId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.Gender = record.Gender;
                temp.PhoneNumber = record.PhoneNumber;

                ListUsers.Add(temp);
            }
            foreach (var record in Parent)
            {
                IndexUserViewModel temp = new IndexUserViewModel();
                temp.Id = record.ParentId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.Gender = record.Gender;
                temp.PhoneNumber = record.PhoneNumber;

                ListUsers.Add(temp);
            }
            foreach (var record in Student)
            {
                IndexUserViewModel temp = new IndexUserViewModel();
                temp.Id = record.StudentId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.Gender = record.Gender;
                temp.PhoneNumber = record.PhoneNumber;

                ListUsers.Add(temp);
            }           
                             
            if (!String.IsNullOrEmpty(searchString))
            {               
                ListUsers = ListUsers.Where(s => URes.SearchForString(s.Username, searchString) || 
                                         URes.SearchForString(s.FirstName,searchString) || 
                                         URes.SearchForString(s.LastName,searchString)
                                    ).ToList();               
            }

            if (!String.IsNullOrEmpty(roleString))
            {
                ListUsers = ListUsers.Where(s => s.RoleName == roleString).ToList();
            }

            if(genderString != null)
                ListUsers = ListUsers.Where(s => s.Gender == genderString).ToList();


            ViewBag.totalRecord = ListUsers.Count();
            return View(ListUsers.OrderBy(x => x.Username).ToPagedList(pageNumber, pageSize));
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id, bool? info, string username)
        {
            int Rid = -1;

            if (info == true)
            {
                ViewBag.InfoClick = true;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rid = URes.GetRoleId(id, username);

            if (Rid == 5)
            {
                Parent user = URes.FindParentUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                DetailParentUserViewModels viewModel = new DetailParentUserViewModels(user);

                return View("DetailsParent",viewModel);
            }else if(Rid == 6)
            {
                Student user = URes.FindStudentUser(id);
                if(user == null)
                {
                    return HttpNotFound();
                }

                DetailStudentUserViewModels viewModel = new DetailStudentUserViewModels(user);

                return View("DetailsStudent", viewModel);              
            }else if(Rid == 7)
            {
                Tutor user = URes.FindTutorUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                DetailTutorUserViewModels viewModel = new DetailTutorUserViewModels(user);

                return View("DetailsTutor", viewModel);
            }
            else
            {
                BackendUser user = URes.FindBackEndUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                DetailBackEndUserViewModels viewModel = new DetailBackEndUserViewModels(user);

                return View("DetailsBackEnd", viewModel);
            }

            //User user = URes.FindBackEndUser(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}

            //DetailUserViewModels viewModel = new DetailUserViewModels();
            //viewModel.Address = user.Address;
            //viewModel.BankID = user.BankID;
            //viewModel.BankName = user.BankName;
            //viewModel.BirthDate = user.BirthDate;
            //viewModel.BMemName = user.BMemName;
            //viewModel.City = user.City;
            //viewModel.Country = user.Country;
            //viewModel.Description = user.Description;
            //viewModel.Email = user.Email;
            //viewModel.FirstName = user.FirstName;
            //viewModel.Gender = user.Gender;
            //viewModel.LastName = user.LastName;
            //viewModel.PhoneNumber = user.PhoneNumber;
            //viewModel.Photo = user.Photo;
            //viewModel.PostalCode = user.PostalCode;
            //viewModel.RoleName = user.Role.RoleName;
            //viewModel.Salary = user.Salary;
            //viewModel.SkypeID = user.SkypeID;
            //viewModel.Username = user.Username;
            //viewModel.Wallet = user.Wallet;
            //viewModel.Id = user.Id;

            //return View(viewModel);
        }

        //// GET: Users/Create
        //public ActionResult Create()
        //{
        //    ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "Id", "RoleName");
        //    ViewBag.Gender = new SelectList(new List<SelectListItem>
        //    {
        //        new SelectListItem {  Text = "Male", Value = "1"},
        //        new SelectListItem {  Text = "Female", Value = "2"},
        //    }, "Value", "Text");
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CreateUserViewModels userViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (URes.isExistsUsername(userViewModel.Username))
        //        {
        //            TempData["message"] = "Username is exists";
        //            return View(userViewModel);
        //        }

        //        User user = MapCreateViewToUser(userViewModel);
        //        URes.Add(user);
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "Id", "RoleName", userViewModel.RoleID);
        //    return View(userViewModel);
        //}

        //// GET: Users/Edit/5
        //public ActionResult Edit(int? id, bool? info)
        //{
        //    if(info == true)
        //    {
        //        ViewBag.InfoClick = true;
        //    }

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = URes.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4).Union(URes.GetAllRole().Where(x => x.RoleName == "Tutor")), "Id", "RoleName", user.RoleID);
        //    DetailUserViewModels model = new DetailUserViewModels();
        //    model.Id = user.Id;
        //    model.Address = user.Address;
        //    model.BankID = user.BankID;
        //    model.BankName = user.BankName;
        //    model.BirthDate = user.BirthDate;
        //    model.BMemName = user.BMemName;
        //    model.City = user.City;
        //    model.Country = user.Country;
        //    model.Description = user.Description;
        //    model.Email = user.Email;
        //    model.FirstName = user.FirstName;
        //    model.Gender = user.Gender;
        //    model.LastName = user.LastName;
        //    model.PhoneNumber = user.PhoneNumber;
        //    model.Photo = user.Photo;
        //    model.PostalCode = user.PostalCode;
        //    model.RoleID = user.RoleID;
        //    model.Salary = user.Salary;
        //    model.SkypeID = user.SkypeID;
        //    model.Username = user.Username;
        //    model.Wallet = user.Wallet;

        //    return View(model);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(DetailUserViewModels model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = URes.Find(model.Id);
        //        user.Id = model.Id;
        //        user.Address = model.Address;
        //        user.BankID = model.BankID;
        //        user.BankName = model.BankName;
        //        user.BirthDate = model.BirthDate;
        //        user.BMemName = model.BMemName;
        //        user.City = model.City;
        //        user.Country = model.Country;
        //        user.Description = model.Description;
        //        user.Email = model.Email;
        //        user.FirstName = model.FirstName;
        //        user.Gender = model.Gender;
        //        user.LastName = model.LastName;
        //        user.PhoneNumber = model.PhoneNumber;
        //        user.Photo = model.Photo;
        //        user.PostalCode = model.PostalCode;
        //        user.RoleID = model.RoleID;
        //        user.Salary = model.Salary;
        //        user.SkypeID = model.SkypeID;
        //        user.Username = model.Username;
        //        user.Wallet = model.Wallet;

        //        URes.Edit(user);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RoleID = new SelectList(URes.GetAllRole(), "Id", "RoleName", model.RoleID);
        //    return View(model);
        //}

        //// GET: Users/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = URes.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    URes.DeleteBackEndUser(id);
        //    return RedirectToAction("Index");
        //}

        //public ActionResult ChangePwd(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var user = URes.Find(id);
        //    if(user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ChangePasswordViewModel model = new ChangePasswordViewModel();
        //    model.Id = user.Id;
        //    model.Name = user.LastName + " " + user.FirstName;
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult ChangePwd(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var checkPass = URes.checkPassword(model.Id, model.Password);
        //        if (!checkPass)
        //        {
        //            TempData["message"] = "Mật khẩu cũ không chính xác";
        //            return View(model);
        //        }
        //        var user = URes.Find(model.Id);
        //        user.Password = model.NewPassword;
        //        URes.EditBackEndUser(user);
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                URes.Dispose();
            }
            base.Dispose(disposing);
        }

        //protected User MapCreateViewToUser(CreateUserViewModels model)
        //{
        //    User temp = new User();

        //    temp.Address = model.Address;
        //    temp.BankID = model.BankID;
        //    temp.BankName = model.BankName;
        //    temp.BirthDate = model.BirthDate;
        //    temp.BMemName = model.BMemName;
        //    temp.City = model.City;
        //    temp.Country = model.Country;
        //    temp.Description = model.Description;
        //    temp.Email = model.Email;
        //    temp.FirstName = model.FirstName;
        //    temp.Gender = model.Gender;
        //    temp.LastName = model.LastName;
        //    temp.Password = model.Password;
        //    temp.PhoneNumber = model.PhoneNumber;
        //    temp.Photo = model.Photo;
        //    temp.PostalCode = model.PostalCode;
        //    temp.RoleID = model.RoleID;
        //    temp.Salary = model.Salary;
        //    temp.SkypeID = model.SkypeID;
        //    temp.Username = model.Username;
        //    temp.Wallet = model.Wallet;

        //    return temp;
        //}

    }
}
