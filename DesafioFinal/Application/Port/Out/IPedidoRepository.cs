using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Application.Port.Out.Common;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Application.Port.Out
{
    public interface IPedidoRepository : IBaseRepository<PedidoEntity, Pedido>
    {
        Task<List<Pedido>> ObterPorNumeroAsync(long numeroPedido, CancellationToken cancellationToken = default);
        Task<List<Pedido>> ObterPorClienteAsync(long clienteId, CancellationToken cancellationToken = default);
        Task<List<Pedido>> ObterPorProdutoAsync(long produtoId, CancellationToken cancellationToken = default);
    }
}
