using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Threading.Tasks;
using TesteGenialNet.Data;

namespace TesteGenialNet.Business.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TesteGenialNetContext _context;

        public UnitOfWork(TesteGenialNetContext context)
        {
            _context = context;
        }

        public DbConnection GetDbConnection() => _context.Database.GetDbConnection();

        public async Task BeginTransaction()
            => await _context.Database.BeginTransactionAsync();

        public async Task Commit()
            => await _context.Database.CommitTransactionAsync();

        public async Task Rollback()
            => await _context.Database.RollbackTransactionAsync();

    }
}
