using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;

namespace Tutor.Web.Controllers
{
    public class UsersController : Controller
    {
        private UsersRepository URes = new UsersRepository();

        // GET: Users
        public ActionResult Index(string searchString, string roleString, int? genderString, string yearString)
        {
            ViewBag.roleString = new SelectList(URes.GetAllRole(), "RoleName", "RoleName");
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            },"Value","Text");

            var users = URes.GetAllUser();
            if (searchString == null || roleString == null )
            {
                users = URes.GetAllUser().Where(s => s.Username == "-1");
                return View(users.ToList());
            }    
                             
            if (!String.IsNullOrEmpty(searchString))
            {               
                users = users.Where(s => s.Username.Contains(searchString) || s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));               
            }

            if (!String.IsNullOrEmpty(roleString))
            {
                users = users.Where(s => s.Role.RoleName == roleString);
            }

            if(genderString != null)
                users = users.Where(s => s.Gender == genderString);

            return View(users.ToList());
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
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "Id", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleID,Username,Password,LastName,FirstName,BirthDate,Gender,Address,Email,SkypeID,City,PostalCode,Country,PhoneNumber,BankID,Salary,Wallet,Photo,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                URes.Add(user);
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(URes.GetAllRole().Take(4), "Id", "RoleName", user.RoleID);
            return View(user);
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
    }
}
