using System.Threading.Tasks;
using TesteGenialNet.Business.Enums;
using TesteGenialNet.Business.Validations;

namespace TesteGenialNet.Business.Interop.ViewModels
{
    public class ProdutoViewModel : BaseViewModel
    {
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; }
        public decimal ValorCompra { get; set; }

        public async Task Validate()
        {
            ValidationResult = await new ProdutoViewModelValidation().ValidateAsync(this);
        }
    }
}
