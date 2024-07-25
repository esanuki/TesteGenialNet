using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Dtos;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Interfaces.Queries;
using TesteGenialNet.Business.Interfaces.Repositorys;

namespace TesteGenialNet.API.Queries
{
    public class FornecedorQuerie : IFornecedorQuerie
    {
        private readonly IFornecedorRepository _repository;
        private readonly INotificator _notificator;

        public FornecedorQuerie(IFornecedorRepository repository, INotificator notificator)
        {
            _repository = repository;
            _notificator = notificator;
        }

        public async Task<IList<FornecedorDto>> GetAll()
            => await _repository.GetAllFornecedorDto();

        public async Task<FornecedorDto> GetById(int id)
        {
            var result = await _repository.GetFornecedorDto(f => f.Id == id);

            if (result == null)
            {
                _notificator.NoticationErrors("Fornecedor não encontrado!");
                return null;
            }
            return result;
        }
    }
}
