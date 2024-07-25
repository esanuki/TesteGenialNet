using MediatR;

namespace TesteGenialNet.Business.Commands.Fornecedores
{
    public class DeleteFornecedorCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
