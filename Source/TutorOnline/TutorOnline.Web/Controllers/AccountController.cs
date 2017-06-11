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
                if (AccRes.Authenticate(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    var user = AccRes.getCurrentUser(model.Username);
                    HttpCookie Role = new HttpCookie("Role");
                    Role["RoleId"] = user.Id.ToString();
                    Role["RoleName"] = user.Role.RoleName;
                    Role.Expires.Add(new TimeSpan(0, 15, 0));
                    Response.Cookies.Add(Role);            
                    return Redirect(returnUrl ?? Url.Action("Index", "Users" ,new { id = user.Id, info = true }));
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

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}