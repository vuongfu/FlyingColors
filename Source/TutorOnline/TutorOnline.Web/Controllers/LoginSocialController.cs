using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using System.Net;
using TutorOnline.Web.Models;
using TutorOnline.Business.Repository;
using System.Web.Security;

namespace TutorOnline.Web.Controllers
{
    public class LoginSocialController : Controller
    {
        private UsersRepository URes = new UsersRepository();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void LoginWithGooglePlus()
        {
            GoogleConnect.ClientId = "250839049034-dbck1rk5nstqdiks4h8i9st55i463i0o.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "q9mPHYdNx8dsGmV7JmgKn5bl";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            GoogleConnect.Authorize("profile", "email");
        }

        [ActionName("LoginWithGooglePlus")]
        public ActionResult LoginWithGooglePlusConfirmed()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string code = Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);

                UserLoginInfo data = URes.checkEmailLogin(profile.Emails.Find(email => email.Type == "account").Value);
                if (data != null)
                {
                    FormsAuthentication.SetAuthCookie(data.UserName, false);
                    HttpCookie Role = new HttpCookie("Role");
                    Role["RoleId"] = data.RoleId.ToString();
                    Role["RoleName"] = data.RoleName;

                    HttpCookie UserInfo = new HttpCookie("UserInfo");
                    UserInfo["UserId"] = data.UserId.ToString();
                    UserInfo["UserName"] = data.UserName;
                    UserInfo.Expires.Add(new TimeSpan(0, 15, 0));
                    Response.Cookies.Add(UserInfo);

                    Role.Expires.Add(new TimeSpan(0, 15, 0));
                    Response.Cookies.Add(Role);
                    return RedirectToAction("Index", "Home");
                }
                else
                {                
                    CreateUserViewModels viewModel = new CreateUserViewModels();
                    viewModel.Email = profile.Emails.Find(email => email.Type == "account").Value;
                    return RedirectToAction("Register","Account",viewModel);
                }
                

                //if (URes.FindParentUser(profile.Id) != null)
                //{
                //    Session["khachhang"] = db.KhachHangs.Find(profile.Id);
                //    return RedirectToAction("Index", "Home");
                //}
                //KhachHang khachHang = new KhachHang()
                //{
                //    ID = profile.Id,
                //    Name = profile.DisplayName,
                //    Email = profile.Emails.Find(email => email.Type == "account").Value,
                //    Gender = profile.Gender,
                //    Type = profile.ObjectType,
                //    ImageURL = profile.Id + System.IO.Path.GetExtension(profile.Image.Url.Split('?')[0])
                //};
                //using (var client = new WebClient())
                //{
                //    client.DownloadFile(profile.Image.Url, Server.MapPath("~/Image/Users/" + khachHang.ImageURL));
                //}

            }
            if (Request.QueryString["error"] == "access_denied")
            {
                return Content("access_denied");
            }
            return RedirectToAction("Index", "Home");

        }
    }
}