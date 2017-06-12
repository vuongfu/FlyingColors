using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private UsersRepository URes = new UsersRepository();
        // GET: Manager
        public ActionResult Index(string searchString, int? genderString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;
            ViewBag.genderStr = genderString;

            ViewBag.genderString = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "Male", Value = "1"},
                new SelectListItem {  Text = "Female", Value = "2"},
            }, "Value", "Text");

            var Pretutor = URes.GetTutor(false);
            List<ApprovePretutorViewModel> result = new List<ApprovePretutorViewModel>();

            //Mapping Entity to ViewModel
            if (Pretutor.Count() > 0)
            {
                foreach (var item in Pretutor)
                {
                    ApprovePretutorViewModel model = new ApprovePretutorViewModel();

                    model.Id = item.Id;
                    model.FullName = item.FirstName + " " + item.LastName;
                    model.BirthDate = item.BirthDate;
                    model.Gender = item.Gender;
                    model.GenderStr = item.Gender == 1 ? "Nam" : "Nữ";
                    model.SkypeID = item.SkypeID;
                    model.Cv = item.CV;

                    result.Add(model);
                }
            }

            if (searchString == null && page == null)
            {
                result = result.Where(x => x.Id == 0).ToList();
                ViewBag.totalRecord = result.Count();
                return View(result.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => URes.SearchForString(x.FullName, searchString) ||
                                           URes.SearchForString(x.SkypeID, searchString)).ToList();
            }

            if (genderString != null)
                result = result.Where(x => x.Gender == genderString).ToList();

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.FullName).ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}