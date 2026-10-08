using DesafioFinal.Adapter.Out.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesafioFinal.Adapter.Out.Database.Context
{
    /// <summary>
    /// DbContext usado para conectar ao SQLite
    /// </summary>
    public class SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options) : DbContext(options)
    {
        public DbSet<ClienteEntity> Clientes { get; set; }
        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<ProdutoEntity> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Varre o projeto e aplica automaticamente todas as configurações que herdam IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlLiteDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //Define que NENHUMA consulta rastreará entidades por padrão
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            //Limpa o rastreador logo após gravar no banco
            ChangeTracker.Clear();

            return result;
        }
    }
}
