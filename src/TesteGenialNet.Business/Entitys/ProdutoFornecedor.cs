using TesteGenialNet.Business.Entity;

namespace TesteGenialNet.Business.Entitys
{
    public class ProdutoFornecedor
    {
        public Fornecedor Fornecedor { get; set; }
        public int FornecedorId { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
        public decimal ValorCompra { get; set; }
    }
}
