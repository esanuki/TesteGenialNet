using System.Collections.Generic;

namespace TesteGenialNet.Business.Interop.ViewModels
{
    public class FornecedorDeleteViewModel : BaseViewModel
    {
        public IList<ProdutoDeleteViewModel> Produtos { get; set; }
    }
}
