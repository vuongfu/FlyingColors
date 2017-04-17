using HappyCakeStore.Domain.Abstract;
using HappyCakeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyCakeStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository pRepository;
        public AdminController(IProductRepository pRepo)
        {
            pRepository = pRepo;
        }
        public ActionResult Index()
        {
            return View(pRepository.Products);
        }

        public ViewResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                pRepository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved.", product.ProductName);
                return RedirectToAction("Index");
            }
            else
            {
                //there is some thing was wrong with the data values
                return View(product);
            }
        }
        public ViewResult Edit(int productId)
        {
            Product product = pRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                pRepository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved.", product.ProductName);
                return RedirectToAction("Index");
            }
            else
            {
                //there is some thing was wrong with the data values
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deleteProduct = pRepository.DeleteProduct(productId);
            
            if(deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted.", deleteProduct.ProductName);
            }

            return RedirectToAction("Index");
        }
    }
}