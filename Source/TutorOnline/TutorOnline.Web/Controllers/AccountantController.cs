﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using TutorOnline.Business.Repository;
using TutorOnline.DataAccess;
using PagedList;
using TutorOnline.Web.Models;
using System.Xml;
using OfficeOpenXml;
using TutorOnline.Common;
using System.Globalization;
using System.IO;
using ExcelDataReader;

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = UserCommonString.Accountant)]
    public class AccountantController : Controller
    {

        TranStringCommon TranString = new TranStringCommon();
        private AccountantRepository AccRes = new AccountantRepository();
        private UsersRepository URes = new UsersRepository();
        IndexUserViewModel user = new IndexUserViewModel();
        // GET: Accountant
        public ActionResult Index(string searchString, string roleString, String StartDate, String EndDate, int? page, String Export, String Search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Count = pageSize * (pageNumber - 1) + 1;

            ViewBag.SearchStr = searchString;
            ViewBag.RoleStr = roleString;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            ViewBag.RoleString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Học sinh", Value = "1"},
                new SelectListItem {  Text = "Phụ huynh", Value = "2"},
            }, "Value", "Text");

            List<TransactionListViewModels> ListTrans = new List<TransactionListViewModels>();

            if ((searchString == null || roleString == null || StartDate == null || EndDate == null) && page == null)
            {
                ViewBag.searchClick = false;
                ViewBag.totalRecord = ListTrans.Count();
                return View(ListTrans.ToPagedList(pageNumber, pageSize));
            }

            var Trans = AccRes.GetAllTrans();
            
            foreach (var record in Trans)
            {
                TransactionListViewModels temp = new TransactionListViewModels();
                temp.TransactionId = record.TransactionId;
                temp.Content = record.Content;
                temp.Amount = (int)record.Amount;
                temp.TranDate = record.TranDate;
                temp.UserID = record.UserID;
                temp.UserType = record.UserType;
                if(record.UserType==1)
                {
                    temp.UserTypeName = "Học sinh";
                } else
                {
                    temp.UserTypeName = "Gia sư";
                }

                ListTrans.Add(temp);
            }

            //Merges list student users and tutor users
            List<IndexUserViewModel> ListUsers = new List<IndexUserViewModel>();
            var Tutor = URes.GetAllTutorUser();
            var Student = URes.GetAllStudentUser();

            foreach (var record in Tutor)
            {
                IndexUserViewModel temp = new IndexUserViewModel();
                temp.Id = record.TutorId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.Gender = record.Gender;
                temp.PhoneNumber = record.PhoneNumber;

                ListUsers.Add(temp);
            }

            foreach (var record in Student)
            {
                IndexUserViewModel temp = new IndexUserViewModel();
                temp.Id = record.StudentId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.Gender = record.Gender;
                temp.PhoneNumber = record.PhoneNumber;

                ListUsers.Add(temp);
            }
            
            foreach (TransactionListViewModels item in ListTrans)
            {
                IndexUserViewModel temp = new IndexUserViewModel();
                if (item.UserType == 1)
                {
                    temp = ListUsers.Where(s => s.Id == item.UserID && s.RoleName == "Học sinh").FirstOrDefault();
                }
                else
                {
                    temp = ListUsers.Where(s => s.Id == item.UserID && s.RoleName == "Gia sư").FirstOrDefault();
                }
                item.UserName = temp.Username;
                item.Name = temp.LastName + " " + temp.FirstName;
            }

            if (!String.IsNullOrEmpty(roleString))
                ListTrans = ListTrans.Where(s => s.UserType == int.Parse(roleString)).ToList();
            try
            {
                    DateTime SDate = DateTime.ParseExact(StartDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                    new LogWriter("SDate = " + SDate.ToString());
                    ListTrans = ListTrans.Where(s => s.TranDate.Date >= SDate.Date).ToList();
            }
            catch(Exception e) {new LogWriter(e.ToString()); }

            try
            {
                    DateTime EDate = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    new LogWriter("EDate = " + EDate.ToString());
                    ListTrans = ListTrans.Where(s => s.TranDate.Date <= EDate.Date).ToList();
            }
            catch (Exception e) { new LogWriter(e.ToString()); }


            if (!String.IsNullOrEmpty(searchString))
            {

                ListTrans = ListTrans.Where(s => AccRes.SearchForString(s.UserName, searchString) || AccRes.SearchForString(s.Name, searchString)).ToList();
            }
            if (!String.IsNullOrEmpty(Export))
            {
                System.IO.MemoryStream memStream;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("New Sheet");

                    worksheet.Cells[1, 1].Value = "STT";
                    worksheet.Cells[1, 2].Value = TranString.TransId;
                    worksheet.Cells[1, 3].Value = TranString.Content;
                    worksheet.Cells[1, 4].Value = TranString.Amount;
                    worksheet.Cells[1, 5].Value = TranString.TransDate;
                    worksheet.Cells[1, 6].Value = TranString.Account;
                    worksheet.Cells[1, 7].Value = TranString.UserType;
                    worksheet.Cells[1, 8].Value = TranString.Name;
                    for (int i = 0; i < ListTrans.Count(); i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = i+1;
                        worksheet.Cells[i + 2, 2].Value = ListTrans[i].TransactionId.ToString();
                        worksheet.Cells[i + 2, 3].Value = ListTrans[i].Content.ToString();
                        worksheet.Cells[i + 2, 4].Value = ListTrans[i].Amount.ToString();
                        worksheet.Cells[i + 2, 5].Value = ListTrans[i].TranDate.ToString();
                        worksheet.Cells[i + 2, 6].Value = ListTrans[i].UserName.ToString();
                        worksheet.Cells[i + 2, 7].Value = ListTrans[i].UserTypeName.ToString();
                        worksheet.Cells[i + 2, 8].Value = ListTrans[i].Name.ToString();
                    }
                        memStream = new System.IO.MemoryStream(package.GetAsByteArray());
                }

                return File(memStream, "application/vnd.ms-excel", "Export.xlsx");
            }
            if (!String.IsNullOrEmpty(Search))
            {
                ViewBag.Search = true;
            }
            ViewBag.searchClick = true;
            ViewBag.totalRecord = "Số kết quả tìm được: " + ListTrans.Count();

            return View(ListTrans.OrderBy(x => x.TransactionId).ToPagedList(pageNumber, pageSize));
        }


        public FileResult PaySalary()
        {

            System.IO.MemoryStream memStream;
            List<Tutor> ListTutor = URes.GetAllTutorUser().Where(s => s.Balance > 0).ToList();

            List<TransactionViewModels> ListTrans = new List<TransactionViewModels>();

            foreach(Tutor item in ListTutor)
            {
                TransactionViewModels temp = new TransactionViewModels();
                temp.Amount = (int)item.Balance * -1;
                temp.Content = "Trả lương cho gia sư " + item.UserName;
                temp.Name = item.LastName + " " + item.FirstName;
                temp.TranDate = DateTime.Now;
                temp.UserName = item.UserName;
                temp.UserID = item.TutorId;
                temp.UserTypeName = UserCommonString.Tutor;
                ListTrans.Add(temp);

                Transaction Trans = new Transaction();
                Trans = MapCreateViewToTrans(temp);

                item.Balance = 0;

                URes.EditTutorUser(item);
                AccRes.Add(Trans);
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("New Sheet");

                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = TranString.Content;
                worksheet.Cells[1, 3].Value = TranString.Amount;
                worksheet.Cells[1, 4].Value = TranString.TransDate;
                worksheet.Cells[1, 5].Value = TranString.Account;
                worksheet.Cells[1, 6].Value = TranString.UserType;
                worksheet.Cells[1, 7].Value = TranString.Name;
                for (int i = 0; i < ListTrans.Count(); i++)
                {
                    worksheet.Cells[i + 2, 1].Value = i + 1;
                    worksheet.Cells[i + 2, 2].Value = ListTrans[i].Content.ToString();
                    worksheet.Cells[i + 2, 3].Value = ListTrans[i].Amount.ToString();
                    worksheet.Cells[i + 2, 4].Value = ListTrans[i].TranDate.ToString();
                    worksheet.Cells[i + 2, 5].Value = ListTrans[i].UserName.ToString();
                    worksheet.Cells[i + 2, 6].Value = ListTrans[i].UserTypeName.ToString();
                    worksheet.Cells[i + 2, 7].Value = ListTrans[i].Name.ToString();
                }
                memStream = new System.IO.MemoryStream(package.GetAsByteArray());
            }

            return File(memStream, "application/vnd.ms-excel", "Payment.xlsx");
        }

        public ActionResult Search(string searchString, string roleString, int? genderString, string yearString, int? page, string Search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Count = pageSize * (pageNumber - 1) + 1;

            ViewBag.SearchStr = searchString;
            ViewBag.RoleStr = roleString;
            ViewBag.GenderStr = genderString;

            ViewBag.Message = TempData["Message"] as string;

            ViewBag.roleString = new SelectList(URes.GetAllRole().Where(s => s.RoleName == "Gia sư" || s.RoleName == "Học sinh"), "RoleName", "RoleName");
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Nam", Value = "1"},
                new SelectListItem {  Text = "Nữ", Value = "2"},
            }, "Value", "Text");

            List<TransUserViewModel> ListUsers = new List<TransUserViewModel>();
            var Tutor = URes.GetAllTutorUser();
            var Student = URes.GetAllStudentUser();

            if ((searchString == null || roleString == null) && page == null)
            {
                ViewBag.searchClick = false;
                //users = URes.GetAllUser().Where(s => s.Username == "-1");
                ViewBag.totalRecord = ListUsers.Count();
                return View(ListUsers.ToPagedList(pageNumber, pageSize));
            }

            foreach (var record in Tutor)
            {
                TransUserViewModel temp = new TransUserViewModel();
                temp.Id = record.TutorId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.Gender = record.Gender;
                temp.PhoneNumber = record.PhoneNumber;
                temp.Balance = (int)record.Balance;

                ListUsers.Add(temp);
            }

            foreach (var record in Student)
            {
                TransUserViewModel temp = new TransUserViewModel();
                temp.Id = record.StudentId;
                temp.Email = record.Email;
                temp.FirstName = record.FirstName;
                temp.LastName = record.LastName;
                temp.RoleID = record.RoleId;
                temp.RoleName = record.Role.RoleName;
                temp.Username = record.UserName;
                temp.Gender = record.Gender;
                temp.PhoneNumber = record.PhoneNumber;
                temp.Balance = (int)record.Balance;

                ListUsers.Add(temp);
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                ListUsers = ListUsers.Where(s => URes.SearchForString(s.Username, searchString) ||
                         URes.SearchForString(s.FirstName, searchString) ||
                         URes.SearchForString(s.LastName, searchString)
                    ).ToList();
            }

            if (roleString == "" || roleString == null)
            {
                ListUsers = ListUsers.Where(s => s.RoleName == "Gia sư" || s.RoleName == "Học sinh").ToList();
            }

            if (!String.IsNullOrEmpty(roleString))
            {
                ListUsers = ListUsers.Where(s => s.RoleName == roleString).ToList();
            }



            if (genderString != null)
            {
                ListUsers = ListUsers.Where(s => s.Gender == genderString).ToList();
            }

            if (!String.IsNullOrEmpty(Search))
            {
                ViewBag.Search = true;
            }
            ViewBag.searchClick = true;
            ViewBag.totalRecord = "Số kết quả tìm được: " + ListUsers.Count();
            
            return View(ListUsers.OrderBy(x => x.Username).ToPagedList(pageNumber, pageSize));
        }

        // GET: Trans/Create
        public ActionResult Create(int id, String RoleName )
        {
            if (RoleName != "Học sinh" && RoleName != "Gia sư")

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<TransUserViewModel> ListUsers = new List<TransUserViewModel>();
            if(RoleName == "Học sinh")
            {
                var Student = URes.GetAllStudentUser();
                foreach (var record in Student)
                {
                    TransUserViewModel temp = new TransUserViewModel();
                    temp.Id = record.StudentId;
                    temp.Email = record.Email;
                    temp.FirstName = record.FirstName;
                    temp.LastName = record.LastName;
                    temp.RoleID = record.RoleId;
                    temp.RoleName = record.Role.RoleName;
                    temp.Username = record.UserName;
                    temp.Gender = record.Gender;
                    temp.PhoneNumber = record.PhoneNumber;
                    temp.Balance = (int)record.Balance;
                    ListUsers.Add(temp);
                }
            } else
            {
                var Tutor = URes.GetAllTutorUser();
                foreach (var record in Tutor)
                {
                    TransUserViewModel temp = new TransUserViewModel();
                    temp.Id = record.TutorId;
                    temp.Email = record.Email;
                    temp.FirstName = record.FirstName;
                    temp.LastName = record.LastName;
                    temp.RoleID = record.RoleId;
                    temp.RoleName = record.Role.RoleName;
                    temp.Username = record.UserName;
                    temp.Gender = record.Gender;
                    temp.PhoneNumber = record.PhoneNumber;
                    temp.Balance = (int)record.Balance;
                    ListUsers.Add(temp);
                }
            }

            TransUserViewModel user = ListUsers.Where(s => s.Id == id).FirstOrDefault();


            if (user == null)
            {
                return HttpNotFound();
            }
            TransactionViewModels trans = new TransactionViewModels();

            trans.UserID = id;
            trans.UserName = user.Username;
            trans.Name = user.LastName + " " + user.FirstName;
            trans.Balance = (int)user.Balance;
            trans.UserTypeName = user.RoleName;
            if(user.RoleName == "Học sinh") { trans.UserType = 1; }
            else { trans.UserType = 2; }
            return View(trans);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionViewModels TransactionViewModel)
        {
            if(TransactionViewModel.Content == null)
            {
                ViewBag.message = TranString.ContentNotNull;
                return View(TransactionViewModel);
            }
            
            if (ModelState.IsValid && TransactionViewModel.Balance + TransactionViewModel.Amount >= 0 && TransactionViewModel.Amount !=0)
            {
                if (TransactionViewModel.UserTypeName == "Học sinh")
                {
                    Student User = URes.FindStudentUser(TransactionViewModel.UserID);
                    User.Balance = User.Balance + TransactionViewModel.Amount;
                    URes.EditStudentUser(User);
                } else if (TransactionViewModel.UserTypeName == "Gia sư")
                {
                    Tutor User = URes.FindTutorUser(TransactionViewModel.UserID);
                    User.Balance = User.Balance + TransactionViewModel.Amount;
                    URes.EditTutorUser(User);
                }
                TransactionViewModel.TranDate = DateTime.Now;
                Transaction tran = MapCreateViewToTrans(TransactionViewModel);
                AccRes.Add(tran);

                TempData["Message"] = "Tạo giao dịch thành công.";
                return RedirectToAction("Search", new {Message = "Thành công."});
            }
            if (TransactionViewModel.Amount == 0)
            {
                ViewBag.message = TranString.WrongAmount;
                return View(TransactionViewModel);
            }
            if(TransactionViewModel.Balance + TransactionViewModel.Amount < 0)
            {
                ViewBag.message = "Số dư hiện tại không đủ.";
                return View(TransactionViewModel);
            }
            else { ViewBag.message = TranString.UpdateFailed; }
            return View(TransactionViewModel);
        }


        protected Transaction MapCreateViewToTrans(TransactionViewModels model)
        {
            Transaction temp = new Transaction();

            temp.UserID = model.UserID;
            temp.Content = model.Content;
            temp.Amount = model.Amount;
            temp.TranDate = model.TranDate;
            temp.UserType = model.UserType;
            //temp.User = model.User;
            return temp;
        }
        public ActionResult ImportExcel()
        {
            return View();
        }

        //install ExcelDataReader
        //install ExcelDataReader.AsAsDataSet
        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase file)
        {

            List<TransactionViewModels> ListTrans = new List<TransactionViewModels>();

            
            if (ModelState.IsValid)
            {
                DataSet ds = new DataSet();
                if (file != null && file.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = file.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader = null;


                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ViewBag.Message = "Định dạng file không hợp lệ.";
                        return View();
                    }



                    ds = reader.AsDataSet();
                    reader.Close();
                    var Tutor = URes.GetAllTutorUser();
                    var Student = URes.GetAllStudentUser();
                    for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TransactionViewModels trans = new TransactionViewModels();
                        try
                        {
                            trans.UserName = ds.Tables[0].Rows[i][0].ToString();
                            trans.UserTypeName = ds.Tables[0].Rows[i][1].ToString();
                            trans.Amount = int.Parse(ds.Tables[0].Rows[i][2].ToString());
                            trans.Content = ds.Tables[0].Rows[i][3].ToString();
                            if (trans.UserTypeName == UserCommonString.Student) { trans.UserType = 1; }
                            else if (trans.UserTypeName == UserCommonString.Tutor) { trans.UserType = 2; }
                            else {
                                ViewBag.Message = "Loại người dùng tại dòng " + (i + 1) + "không hợp lệ.";
                                return View();
                            }

                            if (trans.UserType == 1)
                            {
                                Student User = Student.Where(s => s.UserName.Equals(trans.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                                if(user == null)
                                {
                                    ViewBag.Message = "Tên người dùng tại dòng " + (i + 1) + "không tồn tại.";
                                    return View();
                                }
                                if (User.Balance + trans.Amount < 0)
                                {
                                    ViewBag.Message = TranString.WrongAmountWith + trans.UserName;
                                    return View();
                                }
                                trans.UserID = User.StudentId;
                            }
                            if (trans.UserType == 2)
                            {
                                Tutor User = Tutor.Where(s => s.UserName.Equals(trans.UserName, StringComparison.CurrentCultureIgnoreCase) && s.RoleId == 7).FirstOrDefault();
                                if (user == null)
                                {
                                    ViewBag.Message = "Tên người dùng tại dòng " + (i + 1) + " không tồn tại.";
                                    return View();
                                }
                                if (User.Balance + trans.Amount < 0)
                                {
                                    ViewBag.Message = TranString.WrongAmountWith + trans.UserName;
                                    return View();
                                }
                                trans.UserID = User.TutorId;
                            }
                        }
                        catch
                        {
                            ViewBag.Message = "Xảy ra lỗi tại dòng " + (i+1);
                            return View();
                        }

                        ListTrans.Add(trans);
                    }
                    foreach (var item in ListTrans)
                    {
                        if (item.UserType == 1)
                        {
                            Student User = URes.FindStudentUser(item.UserID);
                            User.Balance = User.Balance + item.Amount;
                            URes.EditStudentUser(User);
                        }
                        else if (item.UserType == 2)
                        {
                            Tutor User = URes.FindTutorUser(item.UserID);
                            User.Balance = User.Balance + item.Amount;
                            URes.EditTutorUser(User);
                        }
                        item.TranDate = DateTime.Now;
                        Transaction tran = MapCreateViewToTrans(item);
                        AccRes.Add(tran);
                    }
                    ViewBag.Message = TranString.SucceedImport;

                }
            
                else
                {
                    ViewBag.Message = "Bạn chưa chọn file tải lên.";
                }
            }

            return View();
        }
    }
}