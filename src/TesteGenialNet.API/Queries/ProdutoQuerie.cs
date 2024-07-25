using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Interfaces.Queries;
using TesteGenialNet.Business.Interfaces.Repositorys;
using TesteGenialNet.Business.Interop.Dtos;

namespace TesteGenialNet.API.Queries
{
    public class ProdutoQuerie : IProdutoQuerie
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;
        private readonly INotificator _notificator;

        public ProdutoQuerie(
            IProdutoRepository repository,
            IMapper mapper,
            INotificator notificator)
        {
            _repository = repository;
            _mapper = mapper;
            _notificator = notificator;
        }

        public async Task<IList<ProdutoDto>> GetAllProduto()
        {
            var result = await _repository.GetAll();

            return _mapper.Map<IList<ProdutoDto>>(result);
        }

        public async Task<ProdutoDto> GetProduto(int id)
        {
            var result = await _repository.GetByExpression(p => p.Id == id);

            if (result == null)
            {
                _notificator.NoticationErrors("Produto nao encontrado!");
                return null;
            }


            return _mapper.Map<ProdutoDto>(result);
        }
    }
}
