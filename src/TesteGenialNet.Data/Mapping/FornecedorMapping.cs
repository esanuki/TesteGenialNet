using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteGenialNet.Business.Entitys;

namespace TesteGenialNet.Data.Mapping
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(f => f.CNPJ)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(f => f.Endereco)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(f => f.Telefone)
                .IsRequired()
                .HasColumnType("varchar(20)");


            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor);

            builder.ToTable("Fornecedores");
        }
    }
}
