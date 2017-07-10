using System;
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

namespace TutorOnline.Web.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class AccountantController : Controller
    {

        TranStringCommon TranString = new TranStringCommon();
        private AccountantRepository AccRes = new AccountantRepository();
        private UsersRepository URes = new UsersRepository();

        IndexUserViewModel user = new IndexUserViewModel();
        // GET: Accountant
        public ActionResult Index(string searchString, string roleString, String StartDate, String EndDate, int? page, String Export)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.SearchStr = searchString;
            ViewBag.RoleStr = roleString;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            ViewBag.RoleString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Student", Value = "1"},
                new SelectListItem {  Text = "Tutor", Value = "2"},
            }, "Value", "Text");

            List<TransactionListViewModels> ListTrans = new List<TransactionListViewModels>();

            if ((searchString == null || roleString == null || StartDate == null || EndDate == null) && page == null)
            {

                ViewBag.totalRecord = ListTrans.Count();
                return View(ListTrans.ToPagedList(pageNumber, pageSize));
            }

            var Trans = AccRes.GetAllTrans();
            
            foreach (var record in Trans)
            {
                TransactionListViewModels temp = new TransactionListViewModels();
                temp.TransactionId = record.TransactionId;
                temp.Content = record.Content;
                temp.Amount = record.Amount;
                temp.TranDate = record.TranDate;
                temp.UserID = record.UserID;
                temp.UserType = record.UserType;
                if(record.UserType==1)
                {
                    temp.UserTypeName = "Student";
                } else
                {
                    temp.UserTypeName = "Tutor";
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
                    temp = ListUsers.Where(s => s.Id == item.UserID && s.RoleName == "Student").FirstOrDefault();
                }
                else
                {
                    temp = ListUsers.Where(s => s.Id == item.UserID && s.RoleName == "Tutor").FirstOrDefault();
                }
                item.UserName = temp.Username;
                item.Name = temp.LastName + " " + temp.FirstName;
            }

            if (!String.IsNullOrEmpty(roleString))
                ListTrans = ListTrans.Where(s => s.UserType == int.Parse(roleString)).ToList();
            try
            {
                if (DateTime.Parse(StartDate) != null)
                {
                    ListTrans = ListTrans.Where(s => s.TranDate > DateTime.Parse(StartDate)).ToList();
                }
            }
            catch (Exception e) { }

            try
            {
                if (DateTime.Parse(EndDate) != null)
                {
                    ListTrans = ListTrans.Where(s => s.TranDate < DateTime.Parse(EndDate)).ToList();
                }
            }
            catch (Exception e) { }


            if (!String.IsNullOrEmpty(searchString))
            {
                //ListUsers = ListUsers.Where(s => s.Username.Contains(searchString) || s.FirstName.Contains(searchString) || s.LastName.Contains(searchString)).ToList();
                //List<TransactionListViewModels> ListTransTemp = new List<TransactionListViewModels>();
                //TransactionListViewModels temp = new TransactionListViewModels();
                //foreach (IndexUserViewModel item in ListUsers)
                //{
                //    if (item.RoleName == "Student")
                //    {
                //        temp = ListTrans.Where(s => s.UserID == item.Id && s.UserType == 1).FirstOrDefault();
                //    }
                //    else
                //    {
                //        temp = ListTrans.Where(s => s.UserID == item.Id && s.UserType == 2).FirstOrDefault();
                //    }
                //    temp.UserName = item.Username;
                //    temp.Name = item.FirstName + " " + item.LastName;
                //    ListTransTemp.Add(temp);
                //}
                //ListTrans = ListTransTemp;

                ListTrans = ListTrans.Where(s => s.UserName.Contains(searchString) || s.Name.Contains(searchString)).ToList();
            }
            if (!String.IsNullOrEmpty(Export))
            {
                System.IO.MemoryStream memStream;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("New Sheet");

                    worksheet.Cells[1, 1].Value = TranString.TransId;
                    worksheet.Cells[1, 2].Value = TranString.Content;
                    worksheet.Cells[1, 3].Value = TranString.Amount;
                    worksheet.Cells[1, 4].Value = TranString.TransDate;
                    worksheet.Cells[1, 5].Value = TranString.Account;
                    worksheet.Cells[1, 6].Value = TranString.UserType;
                    worksheet.Cells[1, 7].Value = TranString.Name;
                    for (int i = 0; i < ListTrans.Count(); i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = ListTrans[i].TransactionId.ToString();
                        worksheet.Cells[i + 2, 2].Value = ListTrans[i].Content.ToString();
                        worksheet.Cells[i + 2, 3].Value = ListTrans[i].Amount.ToString();
                        worksheet.Cells[i + 2, 4].Value = ListTrans[i].TranDate.ToString();
                        worksheet.Cells[i + 2, 5].Value = ListTrans[i].UserName.ToString();
                        worksheet.Cells[i + 2, 6].Value = ListTrans[i].UserTypeName.ToString();
                        worksheet.Cells[i + 2, 7].Value = ListTrans[i].Name.ToString();
                    }
                        memStream = new System.IO.MemoryStream(package.GetAsByteArray());
                }

                return File(memStream, "application/vnd.ms-excel", "Export.xlsx");
            }

            ViewBag.totalRecord = ListTrans.Count();
            return View(ListTrans.OrderBy(x => x.TransactionId).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Search(string searchString, string roleString, int? genderString, string yearString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.SearchStr = searchString;
            ViewBag.RoleStr = roleString;
            ViewBag.GenderStr = genderString;

            ViewBag.roleString = new SelectList(URes.GetAllRole().Where(s => s.RoleName == "Tutor" || s.RoleName == "Student"), "RoleName", "RoleName");
            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            }, "Value", "Text");

            List<TransUserViewModel> ListUsers = new List<TransUserViewModel>();
            var Tutor = URes.GetAllTutorUser();
            var Student = URes.GetAllStudentUser();

            if ((searchString == null || roleString == null) && page == null)
            {
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
                temp.Balance = record.Balance;

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
                temp.Balance = record.Balance;

                ListUsers.Add(temp);
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                ListUsers = ListUsers.Where(s => s.Username.Contains(searchString) || s.FirstName.Contains(searchString) || s.LastName.Contains(searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(roleString))
            {
                ListUsers = ListUsers.Where(s => s.RoleName == roleString).ToList();
            }

            if (roleString == "")
            {
                ListUsers = ListUsers.Where(s => s.RoleName == "Tutor" || s.RoleName == "Student").ToList();
            }

            if (genderString != null)
                ListUsers = ListUsers.Where(s => s.Gender == genderString).ToList();


            ViewBag.totalRecord = ListUsers.Count();
            return View(ListUsers.OrderBy(x => x.Username).ToPagedList(pageNumber, pageSize));
        }

        // GET: Trans/Create
        public ActionResult Create(int id, String RoleName )
        {
            if (id == null || (RoleName != "Student" && RoleName != "Tutor"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<TransUserViewModel> ListUsers = new List<TransUserViewModel>();
            if(RoleName == "Student")
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
                    temp.Balance = record.Balance;
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
                    temp.Balance = record.Balance;
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
            trans.Name = user.LastName + user.FirstName;
            trans.Balance = user.Balance;
            trans.UserTypeName = user.RoleName;
            if(user.RoleName == "Student") { trans.UserType = 1; }
            else { trans.UserType = 2; }
            return View(trans);
        }

        // POST
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionViewModels TransactionViewModel)
        {

            if (ModelState.IsValid && TransactionViewModel.Balance + TransactionViewModel.Amount >= 0)
            {
                if (TransactionViewModel.UserTypeName == "Student")
                {
                    Student User = URes.FindStudentUser(TransactionViewModel.UserID);
                    User.Balance = User.Balance + TransactionViewModel.Amount;
                    URes.EditStudentUser(User);
                } else if (TransactionViewModel.UserTypeName == "Tutor")
                {
                    Tutor User = URes.FindTutorUser(TransactionViewModel.UserID);
                    User.Balance = User.Balance + TransactionViewModel.Amount;
                    URes.EditTutorUser(User);
                }
                TransactionViewModel.TranDate = DateTime.Now;
                Transaction tran = MapCreateViewToTrans(TransactionViewModel);
                AccRes.Add(tran);

                return RedirectToAction("Search");
            }
            if (TransactionViewModel.Balance + TransactionViewModel.Amount < 0)
            {
                ViewBag.message = TranString.WrongAmount;
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
        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase file)
        {

            List<TransactionViewModels> ListTrans = new List<TransactionViewModels>();

            DataSet ds = new DataSet();
            //if (Request.Files["file"].ContentLength > 0)
            //{
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                    excelConnection.Close();
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }

            var Tutor = URes.GetAllTutorUser();
            var Student = URes.GetAllStudentUser();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TransactionViewModels trans = new TransactionViewModels();
                    try
                    {
                        trans.UserName = ds.Tables[0].Rows[i][0].ToString();
                        trans.UserTypeName = ds.Tables[0].Rows[i][1].ToString();
                        trans.Amount = decimal.Parse(ds.Tables[0].Rows[i][2].ToString());
                        trans.Content = ds.Tables[0].Rows[i][3].ToString();
                        if (trans.UserTypeName == "Student") { trans.UserType = 1; }
                        else { trans.UserType = 2; }

                        if (trans.UserType == 1)
                        {
                            Student User = Student.Where(s => s.UserName == trans.UserName).FirstOrDefault();
                            if (User.Balance + trans.Amount < 0) { 
                            ViewBag.Message = TranString.WrongAmountWith + trans.UserName;
                            return View();
                            }
                            trans.UserID = User.StudentId;
                        }
                        if (trans.UserType == 2)
                        {
                            Tutor User = Tutor.Where(s => s.UserName == trans.UserName).FirstOrDefault();
                            if (User.Balance + trans.Amount < 0) {
                            ViewBag.Message = TranString.WrongAmountWith + trans.UserName;
                            return View();
                            }
                            trans.UserID = User.TutorId;
                    }
                    } catch(Exception e) {
                        ViewBag.Message = TranString.WrongInfo;
                        return View();
                    }
                     
                ListTrans.Add(trans);
                }
            foreach(var item in ListTrans)
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
            return View();
        }
    }
}