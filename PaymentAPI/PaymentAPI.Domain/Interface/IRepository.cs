
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentAPI.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task Add(T entity);

        Task Add(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        Task<IList<T>> All();

        Task<T> GetById(int id);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        IEnumerable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}

