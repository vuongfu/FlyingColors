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
            GoogleConnect.ClientId = "250839049034-4bja10hd1vcfenuifcadct6q5b4askkm.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "RoVnqYSab9CnRokzBswoTlla";
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

                string username = URes.checkEmailLogin(profile.Emails.Find(email => email.Type == "account").Value);
                if (username != null)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login","Account");
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