using TesteGenialNet.Business.Enums;

namespace TesteGenialNet.Business.Commands.Produtos
{
    public class ProdutoCommand : BaseCommand
    {
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; }
    }
}
