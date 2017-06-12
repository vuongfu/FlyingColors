using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TutorOnline.Business.Repository;
using TutorOnline.Common;
using TutorOnline.DataAccess;
using TutorOnline.Web.Models;

namespace TutorOnline.Web.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        // GET: Categories
        private CategoriesRepository CRes = new CategoriesRepository();

        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.searchStr = searchString;

            var Categories = CRes.GetAllCategories();
            List<CategoriesViewModels> result = new List<CategoriesViewModels>();

            //Mapping Entity to ViewModel
            if (Categories.Count() > 0)
            {
                foreach (var item in Categories)
                {
                    CategoriesViewModels model = new CategoriesViewModels();
                    model.Id = item.Id;
                    model.CategoryName = item.CategoryName;
                    model.Description = item.Description;
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
                result = result.Where(x => CRes.SearchForString(x.CategoryName, searchString) ||
                                           CRes.SearchForString(x.Description, searchString)).ToList();
            }

            ViewBag.totalRecord = result.Count();
            return View(result.OrderBy(x => x.CategoryName).ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriesViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (CRes.isExistsCategoryName(model.CategoryName))
                {
                    TempData["message"] = new StringCommon().isExitCategoryName.ToString();
                    return View(model);
                }
                Category category = new Category();
                //Mapping Entity to ViewModel
                category.Id = model.Id;
                category.CategoryName = model.CategoryName;
                category.Description = model.Description;

                CRes.AddCategory(category);
                TempData["message"] = new StringCommon().addCategoriesSuccess.ToString();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = CRes.FindCategory(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            CategoriesViewModels model = new CategoriesViewModels();

            //Mapping Entity to ViewModel
            model.Id = category.Id;
            model.CategoryName = category.CategoryName;
            model.Description = category.Description;

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = CRes.FindCategory(id);
            CategoriesViewModels model = new CategoriesViewModels();
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                model.Id = category.Id;
                model.CategoryName = category.CategoryName;
                model.Description = category.Description;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriesViewModels model)
        {
            Category category = new Category();
            category.Id = model.Id;
            category.CategoryName = model.CategoryName;
            category.Description = model.Description;

            if (ModelState.IsValid)
            {
                CRes.EditCategory(category);
                return RedirectToAction("Details", new { id = model.Id });
            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = CRes.FindCategory(id);
            CategoriesViewModels model = new CategoriesViewModels();
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                model.Id = category.Id;
                model.CategoryName = category.CategoryName;
                model.Description = category.Description;
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            CRes.DeleteCategory(id);
            TempData["message"] = new StringCommon().deleteCategoriesSuccess.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CRes.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}