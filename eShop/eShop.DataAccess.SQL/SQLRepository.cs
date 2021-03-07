using eShop.Core.Contracts;
using eShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;  //It will tell the road where database will store
        internal DbSet<T> dbSet;      //it will represent the model/table.


        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();

        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
           
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public T Find(string id)
        {
            return dbSet.Find(id);
        }


        public void Delete(string id)
        {
            var t = Find(id);
            if (context.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);

            }
            dbSet.Remove(t);
        }


    }   }
