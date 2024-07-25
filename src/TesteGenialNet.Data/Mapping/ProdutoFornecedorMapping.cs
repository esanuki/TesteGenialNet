using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteGenialNet.Business.Entitys;

namespace TesteGenialNet.Data.Mapping
{
    public class ProdutoFornecedorMapping : IEntityTypeConfiguration<ProdutoFornecedor>
    {
        public void Configure(EntityTypeBuilder<ProdutoFornecedor> builder)
        {
            builder.HasKey(p => new {p.ProdutoId, p.FornecedorId});

            builder.Property(p => p.ValorCompra)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.HasOne(c => c.Produto)
                .WithMany(p => p.Fornecedores)
                .HasForeignKey(p => p.ProdutoId);

            builder.HasOne(c => c.Fornecedor)
               .WithMany(p => p.Produtos)
               .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("ProdutosFornecedores");
        }
    }
}
