using MediatR;

namespace TesteGenialNet.Business.Commands.Produtos
{
    public class DeleteProdutoCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
