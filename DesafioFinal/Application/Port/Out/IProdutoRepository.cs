using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Application.Port.Out.Common;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Application.Port.Out
{
    public interface IProdutoRepository : IBaseRepository<ProdutoEntity, Produto>
    {
        Task<List<Produto>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken = default);
    }
}
