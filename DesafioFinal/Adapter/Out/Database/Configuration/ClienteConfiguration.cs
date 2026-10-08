using DesafioFinal.Adapter.Out.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioFinal.Adapter.Out.Database.Configuration
{
    /// <summary>
    /// Configuração da tabela Clientes
    /// </summary>
    public class ClienteConfiguration : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Celular);
        }
    }
}
