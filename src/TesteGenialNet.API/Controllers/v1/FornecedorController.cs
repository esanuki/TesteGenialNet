using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteGenialNet.Business.Commands.Fornecedores;
using TesteGenialNet.Business.Helpers;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Interfaces.Queries;
using TesteGenialNet.Business.Interop.ViewModels;
using TesteGenialNet.Business.Resources;
using TesteGenialNet.Business.Services;

namespace TesteGenialNet.API.Controllers.v1
{

    [Route("api/v1/[controller]")]
    public class FornecedorController : MainController
    {
        private readonly IMediator _mediator;
        private ConsultaCepService _consultaCepService;
        private readonly IFornecedorQuerie _fornecedorQuery;

        public FornecedorController(
            INotificator notificator,
            IMapper mapper,
            IMediator mediator,
            ConsultaCepService consultaCepService,
            IFornecedorQuerie fornecedorQuery) : base(notificator, mapper)
        {
            _mediator = mediator;
            _consultaCepService = consultaCepService;
            _fornecedorQuery = fornecedorQuery;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllFornecedor()
        {
            var result = await _fornecedorQuery.GetAll();

            return ReturnResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _fornecedorQuery.GetById(id);
            return ReturnResponse(result);
        }

        [HttpPost("adicionar")]
        public async Task<ActionResult> Post([FromBody] FornecedorViewModel fornecedor)
        {
            
            await fornecedor.Produtos.ProdutosValidation(_notificator);

            fornecedor.Endereco = await _consultaCepService.GetEndereco(fornecedor.CEP);
            await fornecedor.FornecedorValidation(_notificator);
            

            if (HasNotification())
                return ReturnResponse();

            var insert = _mapper.Map<InsertProdutoFornecedorCommand>(fornecedor);

            await _mediator.Send(insert);

            return ReturnResponse(Resource.MSG_SUCCESS);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult> Put([FromBody] FornecedorViewModel fornecedor)
        {
            
            await fornecedor.Produtos.ProdutosValidation(_notificator);

            fornecedor.Endereco = await _consultaCepService.GetEndereco(fornecedor.CEP);
            await fornecedor.FornecedorValidation(_notificator);

            if (HasNotification())
                return ReturnResponse();

            var update = _mapper.Map<UpdateProdutoFornecedorCommand>(fornecedor);

            await _mediator.Send(update);

            return ReturnResponse(Resource.MSG_SUCCESS);
        }

        [HttpDelete("excluirProdutos")]
        public async Task<ActionResult> DeleteProdutosByFornecedor([FromBody] FornecedorDeleteViewModel fornecedor)
        {
            var deleteProdutos = _mapper.Map<DeleteProdutosFornecedorCommand>(fornecedor);
            if (deleteProdutos.Id == 0)
                NotificationErrors("Fornecedor não informado!");

            if (deleteProdutos.Produtos.Count == 0)
                NotificationErrors("Produtos para exclusão não informado!");

            foreach (var produto in deleteProdutos.Produtos)
            {
                if (produto.Id == 0)
                    NotificationErrors("Id do produto não informado!");
            }

            if (HasNotification())
                return ReturnResponse();

            await _mediator.Send(deleteProdutos);

            return ReturnResponse(Resource.MSG_SUCCESS);
        }

        [HttpDelete("excluir/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                NotificationErrors("Fornecedor não informado!");
                return ReturnResponse();
            }
                
            await _mediator.Send(new DeleteFornecedorCommand { Id = id});

            return ReturnResponse(Resource.MSG_SUCCESS);
        }

    }
}
