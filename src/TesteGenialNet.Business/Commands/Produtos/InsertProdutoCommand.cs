using MediatR;

namespace TesteGenialNet.Business.Commands.Produtos
{
    public class InsertProdutoCommand : ProdutoCommand, IRequest<bool>
    {

    }
}
