namespace TesteGenialNet.Business.Interop.Dtos
{
    public class ProdutoDto : BaseDto
    {
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal ValorCompra { get; set; }
    }
}
