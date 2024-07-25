using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Interop.Dtos;

namespace TesteGenialNet.Business.Interfaces.Querys
{
    public interface IProdutoQuery
    {
        Task<IList<ProdutoDto>> GetAllProduto();
        Task<ProdutoDto> GetProduto(int id);
  
    }
}
