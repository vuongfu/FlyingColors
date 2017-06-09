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

namespace TutorOnline.Web.Controllers
{
     
    public class UsersController : Controller
    {
        private UsersRepository URes = new UsersRepository();

        // GET: Users
        public ActionResult Index(string searchString, string roleString, int? genderString, string yearString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.SearchStr = searchString;
            ViewBag.RoleStr = roleString;
            ViewBag.GenderStr = genderString;

            ViewBag.roleString = new SelectList(URes.GetAllRole(), "RoleName", "RoleName");
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            },"Value","Text");

            var users = URes.GetAllUser();
            if ((searchString == null || roleString == null) && page == null)
            {
                users = URes.GetAllUser().Where(s => s.Username == "-1");
                ViewBag.totalRecord = users.Count();
                return View(users.ToPagedList(pageNumber, pageSize));
            }    
                             
            if (!String.IsNullOrEmpty(searchString))
            {               
                users = users.Where(s => URes.SearchForString(s.Username, searchString) || 
                                         URes.SearchForString(s.FirstName,searchString) || 
                                         URes.SearchForString(s.LastName,searchString)
                                    );               
            }

            if (!String.IsNullOrEmpty(roleString))
            {
                users = users.Where(s => s.Role.RoleName == roleString);
            }

            if(genderString != null)
                users = users.Where(s => s.Gender == genderString);


            ViewBag.totalRecord = users.Count();
            return View(users.OrderBy(x => x.Username).ToPagedList(pageNumber, pageSize));
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = URes.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            DetailUserViewModels viewModel = new DetailUserViewModels();
            viewModel.Address = user.Address;
            viewModel.BankID = user.BankID;
            viewModel.BankName = user.BankName;
            viewModel.BirthDate = user.BirthDate;
            viewModel.BMemName = user.BMemName;
            viewModel.City = user.City;
            viewModel.Country = user.Country;
            viewModel.Description = user.Description;
            viewModel.Email = user.Email;
            viewModel.FirstName = user.FirstName;
            viewModel.Gender = user.Gender;
            viewModel.LastName = user.LastName;
            viewModel.PhoneNumber = user.PhoneNumber;
            viewModel.Photo = user.Photo;
            viewModel.PostalCode = user.PostalCode;
            viewModel.RoleName = user.Role.RoleName;
            viewModel.Salary = user.Salary;
            viewModel.SkypeID = user.SkypeID;
            viewModel.Username = user.Username;
            viewModel.Wallet = user.Wallet;
            viewModel.Id = user.Id;

            return View(viewModel);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "Id", "RoleName");
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            }, "Value", "Text");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserViewModels userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = MapCreateViewToUser(userViewModel);
                URes.Add(user);
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "Id", "RoleName", userViewModel.RoleID);
            return View(userViewModel);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = URes.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4).Union(URes.GetAllRole().Where(x => x.RoleName == "Tutor")), "Id", "RoleName", user.RoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleID,Username,Password,LastName,FirstName,BirthDate,Gender,Address,Email,SkypeID,City,PostalCode,Country,PhoneNumber,BankID,Salary,Wallet,Photo,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                URes.Edit(user);
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(URes.GetAllRole(), "Id", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = URes.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            URes.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                URes.Dispose();
            }
            base.Dispose(disposing);
        }

        protected User MapCreateViewToUser(CreateUserViewModels model)
        {
            User temp = new User();
     
            temp.Address = model.Address;
            temp.BankID = model.BankID;
            temp.BankName = model.BankName;
            temp.BirthDate = model.BirthDate;
            temp.BMemName = model.BMemName;
            temp.City = model.City;
            temp.Country = model.Country;
            temp.Description = model.Description;
            temp.Email = model.Email;
            temp.FirstName = model.FirstName;
            temp.Gender = model.Gender;
            temp.LastName = model.LastName;
            temp.Password = model.Password;
            temp.PhoneNumber = model.PhoneNumber;
            temp.Photo = model.Photo;
            temp.PostalCode = model.PostalCode;
            temp.RoleID = model.RoleID;
            temp.Salary = model.Salary;
            temp.SkypeID = model.SkypeID;
            temp.Username = model.Username;
            temp.Wallet = model.Wallet;

            return temp;
        }
               
    }
}
