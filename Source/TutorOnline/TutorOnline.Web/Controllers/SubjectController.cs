using System.Web.Mvc;
using TutorOnline.Business.Repository;

namespace TutorOnline.Web.UI.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectRepository SR = new SubjectRepository();
        // GET: Subject
        public ActionResult Index()
        {
            return View(SR.GetAll());
        }
    }
}