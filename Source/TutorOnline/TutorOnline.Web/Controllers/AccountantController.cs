using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Entity;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;

namespace TutorOnline.Web.Controllers
{
    public class AccountantController : Controller
    {
        private AccountantRepository  AccRes = new AccountantRepository();
        
        // GET: Accountant
        public ActionResult Index(string searchString, string roleString, int? genderString, string yearString)
        {
            User user = new DataAccess.User();

            ViewBag.RoleString = new SelectList(AccRes.GetAllRole(), "Id", "RoleName", user.RoleID);

            var Trans = AccRes.GetAllTrans();
            if (searchString == null || roleString == null)
            {
                Trans = AccRes.GetAllTrans();
                ViewBag.totalRecord = Trans.Count();
                return View(Trans.ToList());
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                Trans = Trans.Where(s => s.User.Username.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                Trans = Trans.Where(s => s.User.Username == searchString);
            }
            
            if (!String.IsNullOrEmpty(roleString))
                Trans = Trans.Where(s => s.User.RoleID == int.Parse(roleString));

            ViewBag.totalRecord = Trans.Count();
            return View(Trans.ToList());
        }
    }
}