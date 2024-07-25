using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TesteGenialNet.Business.Entity;

namespace TesteGenialNet.Business.Interfaces.Repositorys
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllByExpression(Expression<Func<T, bool>> predicate);
        Task<T> GetByExpression(Expression<Func<T, bool>> predicate);
        Task Save(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> ExistsByExpression(Expression<Func<T, bool>> predicate);
    }
}
