using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TutorOnline.Business.Repository;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AccountRepository AccRes = new AccountRepository();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var RoleName = AccRes.Authenticate(model.Username, model.Password);
                if (!string.IsNullOrEmpty(RoleName))
                {
                    int tempId;
                    HttpCookie Role = new HttpCookie("Role");
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    if(RoleName == "Parent")
                    {
                        var user  = AccRes.getCurrentUserTypeParent(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = user.Role.RoleName;
                        tempId = user.ParentId;
                    }
                    else if (RoleName == "Student")
                    {
                        var user = AccRes.getCurrentUserTypeStudent(model.Username);
                        Role["RoleId"] = user.RoleId.ToString();
                        Role["RoleName"] = user.Role.RoleName;
                        tempId = user.StudentId;
                    }
                    else if(RoleName == "Tutor")
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

                    return Redirect(returnUrl ?? Url.Action("Details", "Users" ,new { id = tempId, info = true }));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
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
    }
}