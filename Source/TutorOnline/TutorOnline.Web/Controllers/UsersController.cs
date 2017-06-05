using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tutor.DataAccess;

namespace Tutor.Web.Controllers
{
    public class UsersController : Controller
    {
        private TutorOnlineContext db = new TutorOnlineContext();

        // GET: Users
        public ActionResult Index(string searchString, string roleString, int? genderString, string yearString)
        {
            ViewBag.roleString = new SelectList(db.Roles, "RoleName", "RoleName");
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            },"Value","Text");

            var users = db.Users.Include(u => u.Role);
            if (searchString == null || roleString == null )
            {
                users = db.Users.Where(s => s.Username == "-1");
                return View(users.ToList());
            }    
                             
            if (!String.IsNullOrEmpty(searchString))
            {               
                users = users.Where(s => s.Username.Contains(searchString));               
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
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleID,ParentID,LastName,FirstName,BirthDate,Gender,Address,Email,SkypeID,City,PostalCode,Country,PhoneNumber,BankID,Salary,Wallet,Photo,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName", user.RoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleID,ParentID,LastName,FirstName,BirthDate,Gender,Address,Email,SkypeID,City,PostalCode,Country,PhoneNumber,BankID,Salary,Wallet,Photo,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
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
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
