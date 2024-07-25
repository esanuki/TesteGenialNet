using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using TesteGenialNet.Business.Entitys;
using System.Collections.Generic;
using TesteGenialNet.Business.Dtos;

namespace TesteGenialNet.Business.Interfaces.Repositorys
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> GetFornecedorWithProdutosByExpression(Expression<Func<Fornecedor, bool>> predicate);
        Task<FornecedorDto> GetFornecedorDto(Expression<Func<FornecedorDto, bool>> predicate);
        Task<IList<FornecedorDto>> GetAllFornecedorDto();
    }
}
