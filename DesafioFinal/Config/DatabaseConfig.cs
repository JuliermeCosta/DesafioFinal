using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using DesafioFinal.Adapter.Out.Database.Context;

namespace DesafioFinal.Config
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddInMemoryDatabaseConfiguration(this IServiceCollection services)
        {
            //1. Cria e abre uma conexão Sqlite In-Memory mantida em memória
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            //2. Registra a conexão aberta como Singleton para evitar que ela feche e perca os dados
            services.AddSingleton(sp => connection);

            //3. Registra o DbContext utilizando a conexão ativa
            services.AddDbContext<SqlLiteDbContext>((sp, options) =>
            {
                var conn = sp.GetRequiredService<SqliteConnection>();
                options.UseSqlite(conn);
            });

            //4. Redireciona o tipo base DbContext para a sua implementação concreta
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<SqlLiteDbContext>());

            return services;
        }

        public static IApplicationBuilder UseAutoMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SqlLiteDbContext>();

            //Garante que o banco e as tabelas sejam criados na memória ao iniciar a API
            context.Database.EnsureCreated();

            return app;
        }
    }
}
