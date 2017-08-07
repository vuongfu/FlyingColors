using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        private TutorRepository TRes = new TutorRepository();

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
                            Rname = HttpUtility.UrlDecode(Request.Cookies["Role"]["RoleName"]);
                        }
                        if (Rname == UserCommonString.Parent || Rname == UserCommonString.Student || Rname == UserCommonString.Tutor || Rname == UserCommonString.PreTutor)
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
                    if (RoleName == UserCommonString.Parent)
                    {
                        var user = AccRes.getCurrentUserTypeParent(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = HttpUtility.UrlEncode(user.Role.RoleName);
                        tempId = user.ParentId;
                    }
                    else if (RoleName == UserCommonString.Student)
                    {
                        var user = AccRes.getCurrentUserTypeStudent(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = HttpUtility.UrlEncode(user.Role.RoleName);
                        tempId = user.StudentId;
                    }
                    else if (RoleName == UserCommonString.Tutor)
                    {
                        var user = AccRes.getCurrentUserTypeTutor(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = HttpUtility.UrlEncode(user.Role.RoleName);
                        tempId = user.TutorId;
                    }
                    else
                    {
                        var user = AccRes.getCurrentUserTypeBackEnd(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = HttpUtility.UrlEncode(user.Role.RoleName);
                        tempId = user.BackendUserId;
                    }


                    Role.Expires.Add(new TimeSpan(0, 15, 0));
                    Response.Cookies.Add(Role);

                    HttpCookie UserInfo = new HttpCookie("UserInfo");
                    UserInfo["UserId"] = tempId.ToString();
                    UserInfo["UserName"] = model.Username;
                    UserInfo.Expires.Add(new TimeSpan(0, 15, 0));
                    Response.Cookies.Add(UserInfo);

                    if(RoleName == UserCommonString.Manager)
                    {
                        string url = (String.IsNullOrEmpty(ReturnUrl) ? Url.Action("Index", "DashboardManager") : ReturnUrl);
                        return Redirect(url);
                    }else if(RoleName == UserCommonString.SysAdmin)
                    {
                        string url = (String.IsNullOrEmpty(ReturnUrl) ? Url.Action("Index", "Users") : ReturnUrl);
                        return Redirect(url);
                    }
                    else if (RoleName == UserCommonString.Accountant)
                    {
                        string url = (String.IsNullOrEmpty(ReturnUrl) ? Url.Action("Index", "Accountant") : ReturnUrl);
                        return Redirect(url);
                    }
                    else if(RoleName == UserCommonString.Tutor || RoleName == UserCommonString.PreTutor)
                    {
                        string url = (String.IsNullOrEmpty(ReturnUrl) ? Url.Action("ViewSchedule", "Tutor") : ReturnUrl);
                        return Redirect(url);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }
                else
                {

                    ViewBag.ErrorPassOrName = "Tên đăng nhập hoặc Mật khẩu không chính xác.";
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
        public ActionResult Register(string RoleId, string Email)
        {
            if (Request.Cookies["Role"] != null)
            {
                string Rname = null;
                string Rid = null;
                if (Request.Cookies["Role"]["RoleId"] != null)
                {
                    Rid = Request.Cookies["Role"]["RoleId"];
                    Rname = HttpUtility.UrlDecode(Request.Cookies["Role"]["RoleName"]);
                }
                if (Rname == UserCommonString.Parent || Rname == UserCommonString.Student || Rname == UserCommonString.Tutor || Rname == UserCommonString.PreTutor)
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Details", "Users", new { id = Rid, info = true });
            }

            if (!string.IsNullOrEmpty(RoleId))
            {
                ViewBag.isSelectedRole = RoleId;
                ViewBag.SelectedRoleName =  URes.GetAllRole().FirstOrDefault(x => x.RoleId == int.Parse(RoleId)).RoleName;
            }else if(RoleId == "")
            {
                TempData["messageWarning"] = "Bạn phải chọn chức vụ trước.";
            }

            if (Email != null)
            {
                ViewBag.RegisterLoginSocial = true;
                ViewBag.Email = Email;
            }


            ViewBag.RoleId = new SelectList(URes.GetAllRole().OrderByDescending(x => x.RoleId).Where(x => x.RoleName == UserCommonString.Parent
            || x.RoleName == UserCommonString.PreTutor || x.RoleName == UserCommonString.Student), "RoleId", "RoleName");
            ViewBag.ParentId = new SelectList(URes.GetAllParent(), "ParentId", "ParentName");
            ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key");
            ViewBag.Gender = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Nam", Value = "1"},
                new SelectListItem {  Text = "Nữ", Value = "2"},
            }, "Value", "Text");
            ViewBag.TutorSubjectId = new SelectList(URes.GetAllTutorSubject(), "SubjectId", "SubjectName");


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

                    ViewBag.RoleId = new SelectList(URes.GetAllRole().OrderByDescending(x => x.RoleId).Where(x => x.RoleName == UserCommonString.Parent
                    || x.RoleName == UserCommonString.PreTutor || x.RoleName == UserCommonString.Student), "RoleId", "RoleName", model.RoleId);
                    ViewBag.ParentId = new SelectList(URes.GetAllParent(), "ParentId", "ParentName");
                    ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key");
                    ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Male", Value = "1"},
                        new SelectListItem {  Text = "Female", Value = "2"},
                    }, "Value", "Text");
                    ViewBag.TutorSubjectId = new SelectList(URes.GetAllTutorSubject(), "SubjectId", "SubjectName");

                    return View(model);
                }
                string roleName = URes.GetAllRole().FirstOrDefault(x => x.RoleId == model.RoleId).RoleName;
                if (roleName == UserCommonString.Student)
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
                    temp.isActived = true;

                    URes.AddStudent(temp);                   
                } else if (roleName == UserCommonString.Parent)
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
                    temp.isActived = true;

                    URes.AddParent(temp);
                    
                } else if (roleName == UserCommonString.PreTutor)
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
                    temp.isActived = true;

                    URes.AddTutor(temp);

                    TutorSubject Ts = new TutorSubject();
                    Ts.Experience = model.Experience;
                    Ts.SubjectId = model.TutorSubjectId;
                    Ts.Status = 7;
                    Ts.TutorId = TRes.getTutorIdByUsername(model.Username);

                    TRes.AddTutorSubject(Ts);
                    
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

            ViewBag.RoleId = new SelectList(URes.GetAllRole().OrderByDescending(x => x.RoleId).Where(x => x.RoleName == UserCommonString.Parent
            || x.RoleName == UserCommonString.PreTutor || x.RoleName == UserCommonString.Student), "RoleId", "RoleName", model.RoleId);
            ViewBag.ParentId = new SelectList(URes.GetAllParent(), "ParentId", "ParentName");
            ViewBag.Country = new SelectList(GetAllCountries(), "Key", "Key");
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {  Text = "Male", Value = "1"},
                        new SelectListItem {  Text = "Female", Value = "2"},
                    }, "Value", "Text");
            ViewBag.TutorSubjectId = new SelectList(URes.GetAllTutorSubject(), "SubjectId", "SubjectName");

            return View(model);

        }

        public KeyValuePair<string, string>[] GetAllCountries()
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

        public ActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgotViewModel model)
        {
            UserLoginInfo data = URes.checkEmailLogin(model.Email);
            if(data != null)
            {
                string newpass = GeneratePassword();
                string strFrom = UserCommonString.EmailFrom;
                string strPass = UserCommonString.EmailPass;
                string strTo = model.Email;
                string strSubject = UserCommonString.EmailSubject;
                string strBody = string.Format(UserCommonString.EmailBaseBodyPass, newpass);

                ccMailClass.sendMail_UseGmail(strFrom, strPass, strTo, strSubject, strBody);
                if (data.RoleName == UserCommonString.Tutor || data.RoleName == UserCommonString.PreTutor)
                {
                    var user = URes.FindTutorUser(data.UserId);
                    user.Password = newpass;
                    URes.EditTutorUser(user);
                    
                }else if (data.RoleName == UserCommonString.Student)
                {
                    var user = URes.FindStudentUser(data.UserId);
                    user.Password = newpass;
                    URes.EditStudentUser(user);
                }
                else if (data.RoleName == UserCommonString.Parent)
                {
                    var user = URes.FindParentUser(data.UserId);
                    user.Password = newpass;
                    URes.EditParentUser(user);
                }
                else
                {
                    var user = URes.FindBackEndUser(data.UserId);
                    user.Password = newpass;
                    URes.EditBackEndUser(user);
                }
                TempData["message"] = UserCommonString.SendEmailSuccess;
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.ErrorMess = UserCommonString.EmailNotExist;
                return View(model);
            }
        }

        protected string GeneratePassword()
        {
            const string consonnants = "bcdfghjklmnpqrstvwxz";
            const string vowels = "aeiouy";

            string password = "";
            byte[] bytes = new byte[4];
            var rnd = new RNGCryptoServiceProvider();
            for (int i = 0; i < 3; i++)
            {
                rnd.GetNonZeroBytes(bytes);
                password += consonnants[bytes[0] * bytes[1] % consonnants.Length];
                password += vowels[bytes[2] * bytes[3] % vowels.Length];
            }

            rnd.GetBytes(bytes);
            password += (bytes[0] % 10).ToString() + (bytes[1] % 10).ToString();
            return password;
        }
    }
}