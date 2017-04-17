using HappyCakeStore.Domain.Abstract;
using HappyCakeStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HappyCakeStore.Domain.Entities;

namespace HappyCakeStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository pRepository;
        private readonly ICategoriesRepository cRepository;

        public int PageSize = 4;
        public ProductController(IProductRepository pRepo, ICategoriesRepository cRepo)
        {
            pRepository = pRepo;
            cRepository = cRepo;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = pRepository.Products
                        .Where(p => category == null || cRepository.Categories.Single(c => c.CategoryID == p.CategoryID).CategoryName == category)
                        .OrderBy(p => p.ProductID)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                                pRepository.Products.Count() :
                                pRepository.Products.Where(p => cRepository.Categories.Single(c => c.CategoryID == p.CategoryID).CategoryName == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }
    }
}