using Microsoft.EntityFrameworkCore;
using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Infrastructure.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public DbSet<T> Table { get; set; }

        public Repository(ApplicationDbContext dbContext)
        {
            Table = dbContext.Set<T>();
        }

        public async Task Add(T entity) => await Table.AddAsync(entity);

        public async Task Add(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task<IList<T>> All()
        {
          return  await Table.AsNoTracking().ToListAsync();
        }

        public void Delete(T entity)
        {
             Table.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
        }

        public async Task<T> GetById(int id)
        {
            return await Table.SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<T> Include(params Expression<Func<T, object>>[] includes) 
        {

            IEnumerable<T> query = null;
            foreach (var include in includes)
            {
                query = Table.Include(include);
            }

            return query ?? Table;
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            Table.UpdateRange(entities);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }
    }
}
