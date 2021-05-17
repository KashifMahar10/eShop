using eShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.SQL
{
    public class DataContext: DbContext   //It represent the database
    {

        public DataContext(): base("DefaultConnection")
        {
                
        }
        public DbSet<Product> Products { get; set; }  //Dbset represent the table or model nd make it in db

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
