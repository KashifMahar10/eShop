﻿using eShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.InMemory
{
    class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products==null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Update(Product product)
        {
          Product productToUpdate = products.Find(p => p.Id == product.Id);
        if(productToUpdate!=null)
            {
                productToUpdate = product;
            }
        }
        
        public Product Find(string id)
        {
          Product product = products.Find(p => p.Id == id);
          if(product!=null)
            {
                return product;
            }
          else
            {
                throw new Exception("Product not Found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string id)
        {
            Product productToDelete = products.Find(p => p.Id == id);
            if(productToDelete!=null)
            {
                products.Remove(productToDelete);

            }
            else
            {
                throw new Exception("Product not Found");
            }

        }
    }
}
