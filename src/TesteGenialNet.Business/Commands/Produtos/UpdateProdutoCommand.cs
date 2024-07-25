using MediatR;

namespace TesteGenialNet.Business.Commands.Produtos
{
    public class UpdateProdutoCommand : ProdutoCommand, IRequest<bool>
    {
    }
}
