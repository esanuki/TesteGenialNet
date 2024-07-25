using MediatR;

namespace TesteGenialNet.Business.Commands.Fornecedores
{
    public class UpdateProdutoFornecedorCommand : FornecedorCommand, IRequest<bool>
    {
    }
}
