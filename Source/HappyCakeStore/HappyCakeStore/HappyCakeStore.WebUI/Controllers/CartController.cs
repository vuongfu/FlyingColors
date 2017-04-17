using HappyCakeStore.Domain.Abstract;
using HappyCakeStore.Domain.Entities;
using HappyCakeStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyCakeStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository pRepository;
        private IOrderProcessor oRepository;
        public CartController(IProductRepository pRepo, IOrderProcessor oRepo)
        {
            pRepository = pRepo;
            oRepository = oRepo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl});
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                oRepository.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                TempData["message"] = string.Format("Thank for your ordering, we sent a confirmation e-mail to you (^_^)");
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = pRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, String returnUrl)
        {
            Product product = pRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            
            if(product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}