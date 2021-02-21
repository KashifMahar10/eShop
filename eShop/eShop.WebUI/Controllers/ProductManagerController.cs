﻿using eShop.Core.Contracts;
using eShop.Core.Models;
using eShop.Core.ViewModels;
using eShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        //these both are same context = new ProductRepository();

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext )
        {
            context = productContext;
            this.productCategories = productCategoryContext;
        }
        

        
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList(); 
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if(product==null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
               
            }
        }
        [HttpPost]

        public ActionResult Edit(Product product,string id)
        {
            Product productToEdit = context.Find(id);
            if(productToEdit==null)
            {
                return HttpNotFound();

            }
            else
            {
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();
                return RedirectToAction("Index");
            }
            

        }

        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id);
            if(productToDelete==null)
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
            Product productToDelete = context.Find(id);
            if(productToDelete==null)
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