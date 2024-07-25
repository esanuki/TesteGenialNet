using System.Threading.Tasks;
using TesteGenialNet.Business.Models;

namespace TesteGenialNet.Business.Interfaces.Services
{
    public interface IConsultaCepService
    {
        Task<Endereco> GetEndereco(string cep);
    }
}
