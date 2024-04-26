using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetOne(int id);

        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);







    }
}
