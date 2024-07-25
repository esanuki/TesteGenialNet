using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Dtos;

namespace TesteGenialNet.Business.Interfaces.Querys
{
    public interface IFornecedorQuery
    {
        Task<IList<FornecedorDto>> GetAll();
        Task<FornecedorDto> GetById(int id);
    }
}
