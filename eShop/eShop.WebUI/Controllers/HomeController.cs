using eShop.Core.Contracts;
using eShop.Core.ViewModels;
using eShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productcontext, IRepository<ProductCategory>productCategoriesContext)
        {
            context = productcontext;
            productCategories = productCategoriesContext;

        }
        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
               return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> Categories = productCategories.Collection().ToList();
            if(Category== null)
            {
                products = context.Collection().ToList();

            }
            else
            {
                 products = context.Collection().Where(p => p.Category == Category).ToList();
            }
            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = Categories;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}