using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteGenialNet.Business.Entity;

namespace TesteGenialNet.Data.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Descricao)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(p => p.UnidadeMedida)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnType("int");

            builder.ToTable("Produtos");
        }
    }
}
