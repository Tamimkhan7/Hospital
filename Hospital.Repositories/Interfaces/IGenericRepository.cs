using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hospital.Repositories.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
    {
        //IGenericRepository<T> This interface defines generic CRUD(Create, Read, Update, Delete)
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetById(object id);
        Task<T> GetByIdAsync(object id);

        void Add(T entity);
        Task<T> AddAsync(T entity);

        void Update(T entity);
        Task<T> UpdateAsync(T entity);

        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}
