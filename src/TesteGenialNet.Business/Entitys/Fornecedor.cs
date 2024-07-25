using System.Collections.Generic;
using TesteGenialNet.Business.Entity;

namespace TesteGenialNet.Business.Entitys
{
    public class Fornecedor : BaseEntity
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public IList<ProdutoFornecedor> Produtos { get; set; } = new List<ProdutoFornecedor>();

    }
}
