using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Adapter.Out.Database.Mapper;
using DesafioFinal.Adapter.Out.Database.Repository.Common;
using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DesafioFinal.Adapter.Out.Database.Repository
{
    public class ProdutoRepository(DbContext context) : BaseRepository<ProdutoEntity, Produto>(context), IProdutoRepository
    {
        protected override Produto MapToModel(ProdutoEntity entity) => ProdutoPersistenceMapper.ToModel(entity);
        protected override ProdutoEntity MapToEntity(Produto model) => ProdutoPersistenceMapper.ToEntity(model);

        public async Task<List<Produto>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken = default)
        {
            var entities = await base.Context.Set<ProdutoEntity>()
                .AsNoTracking()
                .Where(produto => produto.Nome.ToLower().Contains(nome.ToLower()))
                .ToListAsync(cancellationToken);

            return entities.Select(MapToModel).ToList();
        }
    }
}
