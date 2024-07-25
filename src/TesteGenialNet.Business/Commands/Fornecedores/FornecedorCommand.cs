using System.Collections.Generic;
using TesteGenialNet.Business.Interop.ViewModels;

namespace TesteGenialNet.Business.Commands.Fornecedores
{
    public class FornecedorCommand : BaseCommand
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; } = "";
        public IList<ProdutoViewModel> Produtos { get; set; }

    }
}
