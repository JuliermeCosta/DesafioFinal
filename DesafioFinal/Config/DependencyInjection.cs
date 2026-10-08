using DesafioFinal.Adapter.Out.Database.Repository;
using DesafioFinal.Application.Port.In;
using DesafioFinal.Application.Port.Out;
using DesafioFinal.Application.Usecase;
using DesafioFinal.Domain.Service;

namespace DesafioFinal.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //1. Repositórios (Adapter Out)
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            //2. Serviços de Domínio / Validadores
            services.AddScoped<ClienteValidatorService>();
            services.AddScoped<ProdutoValidatorService>();
            services.AddScoped<PedidoValidatorService>();

            //3. Casos de Uso / Facades (Application / Ports In)
            services.AddScoped<IClienteFacade, ClienteFacade>();
            services.AddScoped<IProdutoFacade, ProdutoFacade>();
            services.AddScoped<IPedidoFacade, PedidoFacade>();

            return services;
        }
    }
}
