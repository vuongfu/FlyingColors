using HappyCakeStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyCakeStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private ICategoriesRepository cRepository;

        public NavController(ICategoriesRepository cRepo)
        {
            cRepository = cRepo;
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = cRepository.Categories
                .Select(c => c.CategoryName)
                .Distinct()
                .OrderBy(c => c);
            return PartialView(categories);
        }
    }
}