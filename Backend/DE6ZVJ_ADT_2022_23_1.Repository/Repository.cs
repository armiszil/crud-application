using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected BookDbContext context;
        protected Repository(BookDbContext ctx)
        {
            this.context = ctx;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);

            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);

            context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>();
        }

        public abstract T GetOne(int id);


       
    }
}
