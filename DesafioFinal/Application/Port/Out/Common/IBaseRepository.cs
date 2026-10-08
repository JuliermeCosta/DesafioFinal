using DesafioFinal.Adapter.Out.Database.Entity.Common;
using DesafioFinal.Domain.Model.Common;

namespace DesafioFinal.Application.Port.Out.Common
{
    public interface IBaseRepository<TEntity, TModel>
        where TEntity : EntityBase
        where TModel : ModelBase
    {
        Task<TModel?> ObterPorIdAsync(long id, CancellationToken cancellationToken = default);
        Task<List<TModel>> ObterTodosAsync(CancellationToken cancellationToken = default);
        Task AdicionarAsync(TModel model, CancellationToken cancellationToken = default);
        Task<int> ObterContagemAsync(CancellationToken cancellationToken = default);
        Task RemoverAsync(long id, CancellationToken cancellationToken = default);
        Task AtualizarAsync(TModel model, CancellationToken cancellationToken = default);
    }
}
