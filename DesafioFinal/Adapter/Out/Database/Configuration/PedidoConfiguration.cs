using DesafioFinal.Adapter.Out.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioFinal.Adapter.Out.Database.Configuration
{
    /// <summary>
    /// Configuração da tabela Pedidos
    /// </summary>
    public class PedidoConfiguration : IEntityTypeConfiguration<PedidoEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Numero)
                .IsRequired();

            //1. FK para Cliente (1 Pedido pertence a 1 Cliente)
            builder.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //2. FKs para Produtos via tabela de join automático
            builder.HasMany(p => p.Produtos)
                .WithMany()
                .UsingEntity("PedidosProdutos");

            //3. Configura para trazer sempre os dados relacionados por padrão
            builder.Navigation(p => p.Cliente).AutoInclude();
            builder.Navigation(p => p.Produtos).AutoInclude();
        }
    }
}
