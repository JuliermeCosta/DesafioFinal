using DesafioFinal.Adapter.Out.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioFinal.Adapter.Out.Database.Configuration
{
    /// <summary>
    /// Configuração da tabela Produtos
    /// </summary>
    public class ProdutoConfiguration : IEntityTypeConfiguration<ProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasPrecision(6, 2)
                .IsRequired();
        }
    }
}
