using MediatR;

namespace TesteGenialNet.Business.Commands.Fornecedores
{
    public class DeleteProdutosFornecedorCommand : FornecedorCommand, IRequest<bool>
    {
    }
}
