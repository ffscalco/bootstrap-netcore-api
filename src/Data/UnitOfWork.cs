using System;
using System.Threading.Tasks;
using Core;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ImperiusDbContext _context;

        public UnitOfWork(ImperiusDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
