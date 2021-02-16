using eShop.Core.Models;
using eShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;
        //these both are same context = new ProductRepository();

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategoryToEdit = context.Find(id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToEdit);
            }
        }
        [HttpPost]

        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productToEdit = context.Find(id);
            if (productToEdit == null)
            {
                return HttpNotFound();

            }
            else
            {
                productToEdit.Category = productCategory.Category;
              
                context.Commit();
                return RedirectToAction("Index");
            }


        }

        public ActionResult Delete(string id)
        {
            ProductCategory productToDelete = context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]

        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productToDelete = context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}