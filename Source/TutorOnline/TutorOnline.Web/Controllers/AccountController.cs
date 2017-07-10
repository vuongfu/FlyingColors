using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TutorOnline.Business.Repository;
using TutorOnline.Common;
using TutorOnline.DataAccess;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AccountRepository AccRes = new AccountRepository();
        private UsersRepository URes = new UsersRepository();
        private UserStringCommon StrCmm = new UserStringCommon();

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // if they came to the page directly, ReturnUrl will be null.
                if (String.IsNullOrEmpty(Request["ReturnUrl"]))
                {
                    if (Request.Cookies["Role"] != null)
                    {
                        string Rname = null;
                        string Rid = null;
                        if (Request.Cookies["Role"]["RoleId"] != null)
                        {
                            Rid = Request.Cookies["Role"]["RoleId"];
                            Rname = Request.Cookies["Role"]["RoleName"];
                        }
                        if (Rname == StrCmm.Parent || Rname == StrCmm.Student || Rname == StrCmm.Tutor || Rname == StrCmm.PreTutor)
                            return RedirectToAction("Index", "Home");
                        else
                            return RedirectToAction("Details", "Users", new { id = Rid, info = true });
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied");
                }
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var RoleName = AccRes.Authenticate(model.Username, model.Password);
                if (!string.IsNullOrEmpty(RoleName))
                {
                    int tempId;
                    HttpCookie Role = new HttpCookie("Role");
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    if (RoleName == StrCmm.Parent)
                    {
                        var user = AccRes.getCurrentUserTypeParent(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = user.Role.RoleName;
                        tempId = user.ParentId;
                    }
                    else if (RoleName == StrCmm.Student)
                    {
                        var user = AccRes.getCurrentUserTypeStudent(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = user.Role.RoleName;
                        tempId = user.StudentId;
                    }
                    else if (RoleName == StrCmm.Tutor)
                    {
                        var user = AccRes.getCurrentUserTypeTutor(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = user.Role.RoleName;
                        tempId = user.TutorId;
                    }
                    else
                    {
                        var user = AccRes.getCurrentUserTypeBackEnd(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = user.Role.RoleName;
                        tempId = user.BackendUserId;
                    }


                    Role.Expires.Add(new TimeSpan(0, 15, 0));
                    Response.Cookies.Add(Role);

                    HttpCookie UserInfo = new HttpCookie("UserInfo");
                    UserInfo["UserId"] = tempId.ToString();
                    UserInfo["UserName"] = model.Username;
                    UserInfo.Expires.Add(new TimeSpan(0, 15, 0));
                    Response.Cookies.Add(UserInfo);

                    if(RoleName == StrCmm.Manager)
                    {
                        string url = (String.IsNullOrEmpty(ReturnUrl) ? Url.Action("Index", "DashboardManager") : ReturnUrl);
                        return Redirect(url);
                    }else if(RoleName == StrCmm.SysAdmin)
                    {
                        string url = (String.IsNullOrEmpty(ReturnUrl) ? Url.Action("Index", "Users") : ReturnUrl);
                        return Redirect(url);
                    }
                    else if (RoleName == StrCmm.Accountant)
                    {
                        string url = (String.IsNullOrEmpty(ReturnUrl) ? Url.Action("Index", "Accountant") : ReturnUrl);
                        return Redirect(url);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    TempData["messageWarning"] = "Tài khoản hoặc mật khẩu không chính xác";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["Role"] != null)
            {
                Response.Cookies["Role"].Expires = DateTime.Now.AddDays(-1);
            }

            if (Request.Cookies["UserInfo"] != null)
            {
                Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
            }

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register(CreateFrontEndUserViewModels model)
        {
            if (model.RoleId != null)
            {
                ViewBag.isSelectedRole = model.RoleId;
                ViewBag.SelectedRoleName =  URes.GetAllRole().FirstOrDefault(x => x.RoleId == model.RoleId).RoleName;
            }
            if (!String.IsNullOrEmpty(model.Email))
            {
                ViewBag.RegisterLoginSocial = true;
                ViewBag.Email = model.Email;
            }

            ViewBag.RoleId = new SelectList(URes.GetAllRole().OrderByDescending(x => x.RoleId).Where(x => x.RoleName == StrCmm.Parent
            || x.RoleName == StrCmm.PreTutor || x.RoleName == StrCmm.Student), "RoleId", "RoleName", model.RoleId);
            ViewBag.ParentId = new SelectList(URes.GetAllParent(), "ParentId", "ParentName");

            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            }, "Value", "Text");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(CreateFrontEndUserViewModels model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (URes.isExistsUsername(model.Username) || URes.checkEmailLogin(model.Email) != null)
                {
                    if (model.RoleId != null)
                    {
                        ViewBag.isSelectedRole = model.RoleId;
                    }
                    if (!String.IsNullOrEmpty(model.Email))
                    {
                        ViewBag.RegisterLoginSocial = true;
                        ViewBag.Email = model.Email;
                    }

                    ViewBag.RoleId = new SelectList(URes.GetAllRole().OrderByDescending(x => x.RoleId).Where(x => x.RoleName == StrCmm.Parent
                    || x.RoleName == StrCmm.PreTutor || x.RoleName == StrCmm.Student), "RoleId", "RoleName", model.RoleId);
                    ViewBag.ParentId = new SelectList(URes.GetAllParent(), "ParentId", "ParentName");

                    ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Male", Value = "1"},
                        new SelectListItem {  Text = "Female", Value = "2"},
                    }, "Value", "Text");

                    return View(model);
                }
                string roleName = URes.GetAllRole().FirstOrDefault(x => x.RoleId == model.RoleId).RoleName;
                if (roleName == StrCmm.Student)
                {
                    Student temp = new Student();
                    temp.RoleId = (int)model.RoleId;
                    temp.Address = model.Address;
                    temp.BirthDate = model.BirthDate;
                    temp.City = model.City;
                    temp.Country = model.Country;
                    temp.Description = model.Description;
                    temp.FirstName = model.FirstName;
                    temp.Gender = model.Gender;
                    temp.LastName = model.LastName;
                    temp.ParentId = model.ParentId;
                    temp.Password = model.Password;
                    temp.PhoneNumber = model.PhoneNumber;
                    temp.Photo = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);
                    temp.PostalCode = model.PostalCode;
                    temp.SkypeId = model.SkypeId;
                    temp.UserName = model.Username;
                    temp.Email = model.Email;

                    URes.AddStudent(temp);                   
                } else if (roleName == StrCmm.Parent)
                {
                    Parent temp = new Parent();
                    temp.RoleId = (int)model.RoleId;
                    temp.Address = model.Address;
                    temp.BirthDate = model.BirthDate;
                    temp.City = model.City;
                    temp.Country = model.Country;
                    temp.Description = model.Description;
                    temp.FirstName = model.FirstName;
                    temp.Gender = model.Gender;
                    temp.LastName = model.LastName;
                    temp.Password = model.Password;
                    temp.PhoneNumber = model.PhoneNumber;
                    temp.Photo = FileUpload.UploadFile(file, FileUpload.TypeUpload.image);
                    temp.PostalCode = model.PostalCode;
                    temp.SkypeId = model.SkypeId;
                    temp.UserName = model.Username;
                    temp.Email = model.Email;

                    URes.AddParent(temp);
                    
                } else if (roleName == StrCmm.Tutor)
                {
                    Tutor temp = new Tutor();
                    temp.RoleId = (int)model.RoleId;
                    temp.Address = model.Address;
                    temp.BirthDate = model.BirthDate;
                    temp.City = model.City;
                    temp.Country = model.Country;
                    temp.Description = model.Description;
                    temp.FirstName = model.FirstName;
                    temp.Gender = model.Gender;
                    temp.LastName = model.LastName;
                    temp.Password = model.Password;
                    temp.PhoneNumber = model.PhoneNumber;
                    temp.Photo = FileUpload.UploadFile(file , FileUpload.TypeUpload.image);
                    temp.PostalCode = model.PostalCode;
                    temp.SkypeId = model.SkypeId;
                    temp.UserName = model.Username;
                    temp.Email = model.Email;
                    temp.BankName = model.BankName;
                    temp.BMemName = model.BMemName;
                    temp.BankId = model.BankID;

                    URes.AddTutor(temp);
                    
                }
                return RedirectToAction("Login", "Account");
            }

            if (model.RoleId != null)
            {
                ViewBag.isSelectedRole = model.RoleId;
            }
            if (!String.IsNullOrEmpty(model.Email))
            {
                ViewBag.RegisterLoginSocial = true;
                ViewBag.Email = model.Email;
            }

            ViewBag.RoleId = new SelectList(URes.GetAllRole().OrderByDescending(x => x.RoleId).Where(x => x.RoleName == StrCmm.Parent
            || x.RoleName == StrCmm.PreTutor || x.RoleName == StrCmm.Student), "RoleId", "RoleName", model.RoleId);
            ViewBag.ParentId = new SelectList(URes.GetAllParent(), "ParentId", "ParentName");

            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Male", Value = "1"},
                        new SelectListItem {  Text = "Female", Value = "2"},
                    }, "Value", "Text");

            return View(model);

        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}