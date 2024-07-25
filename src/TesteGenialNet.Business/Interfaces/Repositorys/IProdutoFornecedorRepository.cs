using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TesteGenialNet.Business.Entitys;

namespace TesteGenialNet.Business.Interfaces.Repositorys
{
    public interface IProdutoFornecedorRepository
    {
        Task<bool> ExistsByExpression(Expression<Func<ProdutoFornecedor, bool>> predicate);
        Task SaveRange(List<ProdutoFornecedor> produtos);
        Task Delete(int produtoId, int fornecedorId);
    }
}
