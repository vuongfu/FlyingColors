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
    //[Authorize]
    public class CVController : Controller
    {
        private CVRepository CVRes = new CVRepository();
        // GET: CV
        public ActionResult Index(bool? isRead, bool? isApproved, int? page)
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

            ViewBag.isReadB = isRead;
            ViewBag.isApprovedB = isApproved;

            var cv = CVRes.GetAllCV();
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
            //New CV
            if (isRead == false && isApproved == false)
                result = result.Where(x => (x.isRead == false && x.isApproved == false)).ToList();
            //Approved CV
            if (isRead == true && isApproved == true)
                result = result.Where(x => (x.isRead == true && x.isApproved == true)).ToList();
            //Rejected CV
            if (isRead == true && isApproved == false)
                result = result.Where(x => (x.isRead == true && x.isApproved == false)).ToList();

            ViewBag.totalRecord = result.Count();
            return View(result.ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}