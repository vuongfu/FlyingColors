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
using PagedList;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
   // public class AccountantController : Controller
   // {
   //     private AccountantRepository  AccRes = new AccountantRepository();
   //     private UsersRepository URes = new UsersRepository();

   //     User user = new DataAccess.User();
   //     // GET: Accountant
   //     public ActionResult Index(string searchString, string roleString, int? genderString, String StartDate, String EndDate, int? page)
   //     {
   //         int pageSize = 3;
   //         int pageNumber = (page ?? 1);

   //         ViewBag.SearchStr = searchString;
   //         ViewBag.RoleStr = roleString;
   //         ViewBag.GenderStr = genderString;

   //         ViewBag.RoleString = new SelectList(AccRes.GetAllRole(), "Id", "RoleName", user.RoleID);

   //         var Trans = AccRes.GetAllTrans();
   //         if ((searchString == null || roleString == null || StartDate == null || EndDate == null) && page == null)
   //         {
   //             Trans = AccRes.GetAllTrans().Where(s => s.Id == -1);
   //             ViewBag.totalRecord = Trans.Count();
   //             return View(Trans.ToPagedList(pageNumber, pageSize));
   //         }
            
   //         if (!String.IsNullOrEmpty(searchString))
   //         {
   //             Trans = Trans.Where(s => s.User.Username == searchString);
   //         }

   //         if (!String.IsNullOrEmpty(roleString))
   //             Trans = Trans.Where(s => s.User.RoleID == int.Parse(roleString));
   //         try 
   //         {
   //             if (DateTime.Parse(StartDate) != null)
   //             {
   //                 Trans = Trans.Where(s => s.TranDate > DateTime.Parse(StartDate));
   //             }
   //         } catch(Exception e) { }

   //         try {
   //             if (DateTime.Parse(EndDate) != null)
   //             {
   //                 Trans = Trans.Where(s => s.TranDate < DateTime.Parse(EndDate));
   //             }
   //         } catch(Exception e) { }
            

   //         if (!String.IsNullOrEmpty(searchString))
   //         {
   //             Trans = Trans.Where(s => s.User.Username.Contains(searchString) || s.User.FirstName.Contains(searchString) || s.User.LastName.Contains(searchString));
   //         }


   //         ViewBag.totalRecord = Trans.Count();
   //         return View(Trans.OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize));
   //     }


   //     public ActionResult Search(string searchString, string roleString, int? genderString, string yearString, int? page)
   //     {
   //         int pageSize = 3;
   //         int pageNumber = (page ?? 1);

   //         ViewBag.SearchStr = searchString;
   //         ViewBag.RoleStr = roleString;
   //         ViewBag.GenderStr = genderString;

   //         ViewBag.roleString = new SelectList(URes.GetAllRole().Where(s => s.RoleName == "Tutor" || s.RoleName == "Student" || s.RoleName == "Parent"), "RoleName", "RoleName");
   //         ViewBag.genderString = new SelectList(new List<SelectListItem>
   //         {
   //             new SelectListItem {  Text = "Male", Value = "1"},
   //             new SelectListItem {  Text = "Female", Value = "2"},
   //         }, "Value", "Text");

   //         var users = URes.GetAllUser();
   //         if ((searchString == null || roleString == null) && page == null)
   //         {
   //             users = URes.GetAllUser().Where(s => s.Username == "-1");
   //             ViewBag.totalRecord = users.Count();
   //             return View(users.ToPagedList(pageNumber, pageSize));
   //         }

   //         if (!String.IsNullOrEmpty(searchString))
   //         {
   //             users = users.Where(s => s.Username.Contains(searchString) || s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
   //         }

   //         if (!String.IsNullOrEmpty(roleString))
   //         {
   //             users = users.Where(s => s.Role.RoleName == roleString);
   //         }

   //         if(roleString == "")
   //         {
   //             users = users.Where(s => s.Role.RoleName == "Tutor" || s.Role.RoleName == "Student" || s.Role.RoleName == "Parent");
   //         }

   //         if (genderString != null)
   //             users = users.Where(s => s.Gender == genderString);


   //         ViewBag.totalRecord = users.Count();
   //         return View(users.OrderBy(x => x.Username).ToPagedList(pageNumber, pageSize));
   //     }

   //     // GET: Trans/Create
   //     public ActionResult Create(int id)
   //     {
   //         if (id == null)
   //         {
   //             return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
   //         }
   //         User user = URes.Find(id);
   //         if (user == null)
   //         {
   //             return HttpNotFound();
   //         }
   //         TransactionViewModels trans = new TransactionViewModels();
			//trans.User = user;
   //         trans.UserID = id;
   //         trans.Username = user.Username;
   //         trans.FirstName = user.FirstName;
   //         trans.LastName = user.LastName;

   //         return View(trans);
   //     }

   //     // POST
   //     // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
   //     // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
   //     [HttpPost]
   //     [ValidateAntiForgeryToken]
   //     public ActionResult Create(TransactionViewModels TransactionViewModel)
   //     {
   //         if (ModelState.IsValid && user.Wallet + TransactionViewModel.Amount >= 0)
   //         {
   //             TransactionViewModel.TranDate = DateTime.Now;
   //             TransactionViewModel.Content = "Chinh sua so du cho nguoi dung " + TransactionViewModel.User.Username;
   //             Transaction tran = MapCreateViewToTrans(TransactionViewModel);
   //             AccRes.Add(tran);
   //             User user = URes.Find(TransactionViewModel.UserID);
   //             user.Wallet = user.Wallet + TransactionViewModel.Amount;
   //             URes.Edit(user);
   //             return RedirectToAction("Search");
   //         }
   //         if (user.Wallet + TransactionViewModel.Amount >= 0)
   //         {
   //             ViewBag.message = "Chỉnh sửa không hợp lý, yêu cầu nhập lại";
   //         }
   //         else { ViewBag.message = "Cập nhật thất bại"; }
   //         return View(TransactionViewModel);
   //     }


   //     protected Transaction MapCreateViewToTrans(TransactionViewModels model)
   //     {
   //         Transaction temp = new Transaction();

   //         temp.UserID = model.UserID;
   //         temp.Content = model.Content;
   //         temp.Amount = model.Amount;
   //         temp.TranDate = model.TranDate;

   //         //temp.User = model.User;
   //         return temp;
   //     }
   // }
}