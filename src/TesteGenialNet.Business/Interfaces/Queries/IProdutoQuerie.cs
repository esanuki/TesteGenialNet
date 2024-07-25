using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Interop.Dtos;

namespace TesteGenialNet.Business.Interfaces.Queries
{
    public interface IProdutoQuerie
    {
        Task<IList<ProdutoDto>> GetAllProduto();
        Task<ProdutoDto> GetProduto(int id);

    }
}
