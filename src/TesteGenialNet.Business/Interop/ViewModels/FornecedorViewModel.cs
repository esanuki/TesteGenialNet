using System.Collections.Generic;
using System.Threading.Tasks;
using TesteGenialNet.Business.Validations;

namespace TesteGenialNet.Business.Interop.ViewModels
{
    public class FornecedorViewModel : BaseViewModel
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public IList<ProdutoViewModel> Produtos { get; set; }

        public async Task Validate()
        {
            ValidationResult = await new FornecedorViewModelValidation().ValidateAsync(this);
        }
    }
}
