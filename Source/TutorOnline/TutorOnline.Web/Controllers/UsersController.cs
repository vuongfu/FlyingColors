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
using System.Web;
using TutorOnline.Common;
using System.Globalization;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private UsersRepository URes = new UsersRepository();
        // GET: Users

        [Authorize(Roles = UserCommonString.SysAdmin)]
        public ActionResult Index(string searchString, string roleString, int? genderString, string yearString, int? page)
        {
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            ViewBag.Count = pageSize*(pageNumber-1) + 1;

            ViewBag.SearchStr = searchString;
            ViewBag.RoleStr = roleString;
            ViewBag.GenderStr = genderString;

            ViewBag.roleString = new SelectList(URes.GetAllRole(), "RoleName", "RoleName");
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Nam", Value = "1"},
                new SelectListItem {  Text = "Nữ", Value = "2"},
            },"Value","Text");

            List<IndexUserViewModel> ListUsers = new List<IndexUserViewModel>();

            if ((searchString == null || roleString == null) && page == null)
            {
                //ListUsers = URes.GetAllBackEndUser().Where(s => s.Username == "-1");
                ViewBag.totalRecord = ListUsers.Count();
                ViewBag.searchClick = false;
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
                temp.Gender = record.Gender;

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

            ViewBag.searchClick = true;
            ViewBag.totalRecord = ListUsers.Count();
            return View(ListUsers.OrderBy(x => x.Username).ToPagedList(pageNumber, pageSize));
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id, bool? info, string username)
        {
            string RName = null;

            if (info == true)
            {
                ViewBag.InfoClick = true;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RName = URes.GetRoleName(username);

            if (RName == UserCommonString.Parent)
            {
                Parent user = URes.FindParentUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RName = RName;
                if (user.Photo == null) user.Photo = "";
                DetailUserViewModels viewModel = new DetailUserViewModels(user);

                return View(viewModel);
            }else if(RName == UserCommonString.Student)
            {
                Student user = URes.FindStudentUser(id);
                if(user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RName = RName;
                if (user.Photo == null) user.Photo = "";
                DetailUserViewModels viewModel = new DetailUserViewModels(user);

                return View(viewModel);              
            }else if(RName == UserCommonString.Tutor)
            {
                Tutor user = URes.FindTutorUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RName = RName;
                if (user.Photo == null) user.Photo = "";
                DetailUserViewModels viewModel = new DetailUserViewModels(user);

                return View(viewModel);
            }
            else
            {
                BackendUser user = URes.FindBackEndUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.RName = RName;
                if (user.Photo == null) user.Photo = "";
                DetailUserViewModels viewModel = new DetailUserViewModels(user);

                return View(viewModel);
            }
          
        }


        // GET: Users/Create
        [Authorize(Roles = UserCommonString.SysAdmin)]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(URes.GetAllRole().Take(4), "RoleId", "RoleName");
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Nam", Value = "1"},
                new SelectListItem {  Text = "Nữ", Value = "2"},
            }, "Value", "Text");
            ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key"); 
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, CreateUserViewModels userViewModel)
        {
            if (ModelState.IsValid)
            {
                if (URes.isExistsUsername(userViewModel.Username) || URes.checkEmailLogin(userViewModel.Email) != null)
                {
                    ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text");
                    TempData["message"] = "Username is exists";
                    ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "RoleId", "RoleName", userViewModel.RoleId);
                    return View(userViewModel);
                }

                BackendUser user = new BackendUser();
                user.Address = userViewModel.Address;
                user.BirthDate = userViewModel.BirthDate;
                user.City = userViewModel.City;
                user.Country = userViewModel.Country;
                user.Description = userViewModel.Description;
                user.Email = userViewModel.Email;
                user.FirstName = userViewModel.FirstName;
                user.Gender = userViewModel.Gender;
                user.LastName = userViewModel.LastName;
                user.Password = userViewModel.Password;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.Photo = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);
                user.RoleId = userViewModel.RoleId;
                user.UserName = userViewModel.Username;

                URes.AddBackEndUser(user);
                TempData["message"] = "Đã thêm thành công người dùng " + user.UserName;
                return RedirectToAction("Index");
            }
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text");
            ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "RoleId", "RoleName", userViewModel.RoleId);
            TempData["messageWarning"] = "Đã có lỗi xảy ra khi thêm người dùng " + userViewModel.Username;
            return View(userViewModel);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = UserCommonString.SysAdmin + "," + UserCommonString.Accountant + "," + UserCommonString.Supporter + "," + UserCommonString.Manager)]
        public ActionResult Edit(int? id, bool? info)
        {
            if (info == true)
            {
                ViewBag.InfoClick = true;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackendUser user = URes.FindBackEndUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text");
            ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "RoleId", "RoleName", user.RoleId);
            DetailBackEndUserViewModels model = new DetailBackEndUserViewModels(user);
            ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key", user.Country);

            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DetailBackEndUserViewModels model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string photoUrl = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);

                BackendUser user = URes.FindBackEndUser(model.Id);
                user.Address = model.Address;
                user.City = model.City;
                user.Country = model.Country;
                user.Description = model.Description;
                user.FirstName = model.FirstName;
                user.Gender = model.Gender;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Photo = (string.IsNullOrEmpty(photoUrl)? model.Photo:photoUrl);
                user.RoleId = model.RoleID;

                URes.EditBackEndUser(user);
                TempData["message"] = "Đã cập nhặt thông tin của người dùng " + user.UserName + " thành công.";
                return RedirectToAction("Details","Users", new { id = model.Id, info = true });
            }
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Nam", Value = "1"},
                        new SelectListItem {  Text = "Nữ", Value = "2"},
                    }, "Value", "Text");
            ViewBag.RoleID = new SelectList(URes.GetAllRole(), "RoleId", "RoleName", model.RoleID);
            TempData["messageWarning"] = "Đã có lỗi xảy ra khi cập nhật thông tin của người dùng " + model.Username;
            return View(model);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = UserCommonString.SysAdmin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackendUser user = URes.FindBackEndUser(id);
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
            URes.DeleteBackEndUser(id);
            TempData["message"] = "Đã xóa người dùng thành công";
            return RedirectToAction("Index");
        }

        public ActionResult ChangePwd(int? id, string role)
        {
            if (id == null || role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            if(role == UserCommonString.Parent)
            {
                var user = URes.FindParentUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ChangePasswordViewModel model = new ChangePasswordViewModel();
                model.Id = user.ParentId;
                model.Name = user.LastName + " " + user.FirstName;
                model.UserRole = user.RoleId;
                model.Rolename = role;
                return View(model);
            }
            else if(role == UserCommonString.Student)
            {
                var user = URes.FindStudentUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ChangePasswordViewModel model = new ChangePasswordViewModel();
                model.Id = user.StudentId;
                model.Name = user.LastName + " " + user.FirstName;
                model.UserRole = user.RoleId;
                model.Rolename = role;
                return View(model);
            }else if(role == UserCommonString.Tutor || role == UserCommonString.PreTutor)
            {
                var user = URes.FindTutorUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ChangePasswordViewModel model = new ChangePasswordViewModel();
                model.Id = user.TutorId;
                model.Name = user.LastName + " " + user.FirstName;
                model.UserRole = user.RoleId;
                model.Rolename = role;
                return View(model);
            }else
            {
                var user = URes.FindBackEndUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ChangePasswordViewModel model = new ChangePasswordViewModel();
                model.Id = user.BackendUserId;
                model.Name = user.LastName + " " + user.FirstName;
                model.UserRole = user.RoleId;
                model.Rolename = role;
                return View(model);
            }
            
        }

        [HttpPost]
        public ActionResult ChangePwd(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkPass = URes.checkPassword(model.Id, model.Password, model.UserRole);
                if (!checkPass)
                {
                    TempData["messageWarning"] = "Mật khẩu cũ không chính xác";
                    return View(model);
                }

                if(model.Rolename == UserCommonString.Parent)
                {
                    var user = URes.FindParentUser(model.Id);
                    user.Password = model.NewPassword;
                    URes.EditParentUser(user);
                }else if(model.Rolename == UserCommonString.Student)
                {
                    var user = URes.FindStudentUser(model.Id);
                    user.Password = model.NewPassword;
                    URes.EditStudentUser(user);
                }else if(model.Rolename == UserCommonString.Tutor || model.Rolename == UserCommonString.PreTutor)
                {
                    var user = URes.FindTutorUser(model.Id);
                    user.Password = model.NewPassword;
                    URes.EditTutorUser(user);
                }else
                {
                    var user = URes.FindBackEndUser(model.Id);
                    user.Password = model.NewPassword;
                    URes.EditBackEndUser(user);
                }

                TempData["message"] = "Đã thay đổi mật khẩu thành công.";
                return RedirectToAction("ChangePwd","Users",new {id = model.Id, role = model.Rolename });
            }

            TempData["messageWarning"] = "Đã có lỗi xảy ra trong quá trình cập nhật mật khẩu mới.";
            return View(model);
        }

        public KeyValuePair<string,string>[] GetAllCountries()
        {
            var objDict = new Dictionary<string, string>();
            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var regionInfo = new RegionInfo(cultureInfo.Name);
                if (!objDict.ContainsKey(regionInfo.EnglishName))
                {
                    objDict.Add(regionInfo.EnglishName, regionInfo.TwoLetterISORegionName.ToLower());
                }
            }
            var obj = objDict.OrderBy(p => p.Key).ToArray();
            

            return obj;
        }

        //[HttpPost]
        //public ActionResult GetAllCityByCountry(string country)
        //{
        //    var objDict = new Dictionary<string, string>();
            
        //    foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        //    {
        //        var regionInfo = new RegionInfo(cultureInfo.Name);
        //        if (!objDict.ContainsKey(regionInfo.EnglishName))
        //        {
        //            objDict.Add(regionInfo.EnglishName, regionInfo.TwoLetterISORegionName.ToLower());
        //        }
        //    }
        //    var obj = objDict.OrderBy(p => p.Key).ToArray();

        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                URes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
