using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TesteGenialNet.Business.Entitys;
using TesteGenialNet.Business.Enums;

namespace TesteGenialNet.Business.Entity
{
    public class Produto : BaseEntity
    {
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; }
        public IList<ProdutoFornecedor> Fornecedores { get; set; }

        [NotMapped]
        public decimal ValorCompra { get; set; }
    }
}
