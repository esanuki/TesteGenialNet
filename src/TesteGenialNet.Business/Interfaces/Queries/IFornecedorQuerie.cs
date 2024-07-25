using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Dtos;

namespace TesteGenialNet.Business.Interfaces.Queries
{
    public interface IFornecedorQuerie
    {
        Task<IList<FornecedorDto>> GetAll();
        Task<FornecedorDto> GetById(int id);
    }
}
