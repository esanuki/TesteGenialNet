using System.Collections.Generic;
using TesteGenialNet.Business.Entity;
using TesteGenialNet.Business.Interop.Dtos;

namespace TesteGenialNet.Business.Dtos
{
    public class FornecedorDto : BaseDto
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public IList<ProdutoDto> Produtos { get; set; } = new List<ProdutoDto>();
    }
}
