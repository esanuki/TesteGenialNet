using MediatR;

namespace TesteGenialNet.Business.Commands.Fornecedores
{
    public class InsertProdutoFornecedorCommand : FornecedorCommand, IRequest<bool>
    {
    }
}
