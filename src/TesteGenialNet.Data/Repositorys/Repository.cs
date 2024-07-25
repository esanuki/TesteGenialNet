using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TesteGenialNet.Business.Entity;
using TesteGenialNet.Business.Interfaces.Repositorys;

namespace TesteGenialNet.Data.Repositorys
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly TesteGenialNetContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(TesteGenialNetContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IList<T>> GetAll()
            => await _dbSet.AsNoTracking().ToListAsync();

        public virtual async Task<T> GetById(int id)
            => await _dbSet.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAllByExpression(Expression<Func<T, bool>> predicate)
            => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task<T> GetByExpression(Expression<Func<T, bool>> predicate)
         => await _dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();

        public virtual async Task Save(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _dbSet.Remove(new T { Id = id});
            await   _context.SaveChangesAsync();   
        }

        public async Task<bool> ExistsByExpression(Expression<Func<T, bool>> predicate)
         => await _dbSet.AsNoTracking().Where(predicate).AnyAsync();

        public void Dispose()
        {
            
        }
    }
}
