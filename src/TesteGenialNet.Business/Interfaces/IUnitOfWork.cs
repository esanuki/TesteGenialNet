using System.Data.Common;
using System.Threading.Tasks;

namespace TesteGenialNet.Business.Interfaces
{
    public interface IUnitOfWork
    {
        DbConnection GetDbConnection();
        Task BeginTransaction();
        Task Commit();
        Task Rollback();

    }
}
