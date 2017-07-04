using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Common;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    //[Authorize]
    public class CVController : Controller
    {
        private CVRepository CVRes = new CVRepository();
        // GET: CV
        public ActionResult Index(string button, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //Get Number of CV by status
            int allCV = CVRes.GetAllCV().Count();
            ViewBag.numAllCV = allCV;

            int newCV = CVRes.GetCVByStatus(false, false).Count();
            ViewBag.numNewCV = newCV;

            int approvedCV = CVRes.GetCVByStatus(true, true).Count();
            ViewBag.numApprovedCV = approvedCV;

            int rejectedCV = CVRes.GetCVByStatus(true, false).Count();
            ViewBag.numRejectedCV = rejectedCV;

            ViewBag.buttonB = button;
            //ViewBag.isReadB = isRead;
            //ViewBag.isApprovedB = isApproved;

            ViewBag.status = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {  Text = "--Lựa chọn--", Value = "0"},
                new SelectListItem {  Text = "Phê duyệt", Value = "1"},
                new SelectListItem {  Text = "Từ chối", Value = "2"},
            }, "Value", "Text");

            var cv = CVRes.GetAllCV();

            //Check which button was press
            if (button == "all")
                cv = CVRes.GetAllCV();
            if (button == "new")
                cv = CVRes.GetCVByStatus(false, false);
            if (button == "approved")
                cv = CVRes.GetCVByStatus(true, true);
            if (button == "rejected")
                cv = CVRes.GetCVByStatus(true, false);

            List<CVViewModels> result = new List<CVViewModels>();
            //Mapping Entity to ViewModel
            if (cv.Count() > 0)
            {
                foreach (var item in cv)
                {
                    CVViewModels model = new CVViewModels();
                    //Mapping Entity to ViewModel
                    model.CVId = item.CVId;
                    model.CVLink = item.CVLink;
                    //AddItem
                    result.Add(model);
                }
            }
            if (button == null)
                result = result.Where(x => x.CVId == 0).ToList();

            ViewBag.totalRecord = result.Count();
            return View(result.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult ChangeStatus(int? id, int? status)
        {
            var cv = CVRes.FindCV(id);
            //Check status to update Entity
            if (status == 0)
                TempData["message"] = new StringCommon().mustChangeCVStatus.ToString();
            if (status == 1)
            {
                cv.isRead = true;
                cv.isApproved = true;
                if (ModelState.IsValid)
                {
                    CVRes.ChangeStatus(cv);
                    TempData["message"] = new StringCommon().changeCVStatusSucessfully.ToString();
                }
            }
            if (status == 2)
            {
                cv.isRead = true;
                cv.isApproved = false;
                if (ModelState.IsValid)
                {
                    CVRes.ChangeStatus(cv);
                    TempData["message"] = new StringCommon().changeCVStatusSucessfully.ToString();
                }
            }

            return RedirectToAction("Index", "CV");
        }

        //[HttpGet]
        //public FileResult Download(long id)
        //{
        //    using (var mem = new MemoryStream())
        //    {
        //        // Create spreadsheet based on widgetId...
        //        // Or get the path for a file on the server...
        //        return File(mem, "application/pdf", "CV.pdf");
        //    }
        //}
    }
}