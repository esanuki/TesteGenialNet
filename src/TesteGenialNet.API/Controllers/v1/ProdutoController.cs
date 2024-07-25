using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Helpers;
using MediatR;
using TesteGenialNet.Business.Resources;
using TesteGenialNet.Business.Interfaces.Querys;
using TesteGenialNet.Business.Interop.ViewModels;
using TesteGenialNet.Business.Commands.Produtos;

namespace TesteGenialNet.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class ProdutoController : MainController
    {
        private IMediator _mediator;
        private IProdutoQuery _produtoQuery;

        public ProdutoController(
            INotificator notificator,
            IMapper mapper,
            IMediator mediator,
            IProdutoQuery produtoQuery) : base(notificator, mapper)
        {
            _mediator = mediator;
            _produtoQuery = produtoQuery;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
            => ReturnResponse(await _produtoQuery.GetAllProduto());

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
            => ReturnResponse(await _produtoQuery.GetProduto(id));

        [HttpPost("adicionar")]
        public async Task<ActionResult> Post([FromBody] ProdutoViewModel produtoViewModel)
        {
            await produtoViewModel.ProdutoValidation(_notificator);

            if (HasNotification())
                return ReturnResponse();

            var produtoCommand = _mapper.Map<InsertProdutoCommand>(produtoViewModel);

            await _mediator.Send(produtoCommand);

            return ReturnResponse(Resource.MSG_SUCCESS);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult> Put([FromBody] ProdutoViewModel produtoViewModel)
        {
            await produtoViewModel.ProdutoValidation(_notificator);

            if (HasNotification())
                return ReturnResponse();

            var produtoCommand = _mapper.Map<UpdateProdutoCommand>(produtoViewModel);

            await _mediator.Send(produtoCommand);

            return ReturnResponse(Resource.MSG_SUCCESS);
        }

        [HttpDelete("excluir/{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            await _mediator.Send(new DeleteProdutoCommand { Id = id});

            return ReturnResponse(Resource.MSG_SUCCESS);
        }
    }
}
