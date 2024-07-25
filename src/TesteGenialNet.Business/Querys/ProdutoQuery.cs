using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Interfaces.Querys;
using TesteGenialNet.Business.Interfaces.Repositorys;
using TesteGenialNet.Business.Interop.Dtos;

namespace TesteGenialNet.Business.Querys
{
    public class ProdutoQuery : IProdutoQuery
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;
        private readonly INotificator _notificator;

        public ProdutoQuery(
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
