
using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Infrastructure.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.Infrastructure.Data.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class, IEntity
    {
        public readonly ApplicationDbContext _context;
        public IRepository<T> Repository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Repository = new Repository<T>(context);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
