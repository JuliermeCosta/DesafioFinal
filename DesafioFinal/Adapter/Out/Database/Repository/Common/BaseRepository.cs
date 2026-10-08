using DesafioFinal.Adapter.Out.Database.Entity.Common;
using DesafioFinal.Application.Port.Out.Common;
using DesafioFinal.Domain.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace DesafioFinal.Adapter.Out.Database.Repository.Common
{
    public abstract class BaseRepository<TEntity, TModel>(DbContext context) : IBaseRepository<TEntity, TModel>
        where TEntity : EntityBase
        where TModel : ModelBase
    {
        protected readonly DbContext Context = context;

        public virtual async Task<TModel?> ObterPorIdAsync(long id, CancellationToken cancellationToken = default)
        {
            TEntity? entity = id > 0 ? await Context.Set<TEntity>().FindAsync([id], cancellationToken) : null;
            return entity != null ? MapToModel(entity) : null;
        }

        public virtual async Task<List<TModel>> ObterTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await Context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return entities.Select(MapToModel).ToList();
        }

        public virtual async Task AdicionarAsync(TModel model, CancellationToken cancellationToken = default)
        {
            model.DataInclusao = DateTime.Now;
            var entity = MapToEntity(model);

            await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            model.Id = entity.Id;
        }

        public virtual async Task<int> ObterContagemAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .CountAsync(cancellationToken);
        }

        public virtual async Task RemoverAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = id > 0 ? await Context.Set<TEntity>().FindAsync([id], cancellationToken) : null;
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task AtualizarAsync(TModel model, CancellationToken cancellationToken = default)
        {
            model.DataAtualizacao = DateTime.Now;
            var entity = MapToEntity(model);

            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        //Métodos abstract para obrigar as filhas a usarem os seus mappers
        protected abstract TModel MapToModel(TEntity entity);
        protected abstract TEntity MapToEntity(TModel model);
    }
}
