using TesteGenialNet.Business.Entity;
using TesteGenialNet.Business.Interfaces.Repositorys;

namespace TesteGenialNet.Data.Repositorys
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(TesteGenialNetContext context) : base(context)
        {
        }
    }
}
