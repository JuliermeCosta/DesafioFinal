using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Adapter.Out.Database.Mapper;
using DesafioFinal.Adapter.Out.Database.Repository.Common;
using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DesafioFinal.Adapter.Out.Database.Repository
{
    public class PedidoRepository(DbContext context) : BaseRepository<PedidoEntity, Pedido>(context), IPedidoRepository
    {
        protected override Pedido MapToModel(PedidoEntity entity) => PedidoPersistenceMapper.ToModel(entity);
        protected override PedidoEntity MapToEntity(Pedido model) => PedidoPersistenceMapper.ToEntity(model);

        public override async Task AdicionarAsync(Pedido model, CancellationToken cancellationToken = default)
        {
            model.DataInclusao = DateTime.Now;
            var entity = MapToEntity(model);

            await Context.Set<PedidoEntity>().AddAsync(entity, cancellationToken);

            //Garante que o EF não tente fazer INSERT no Cliente nem nos Produtos
            if (entity.Cliente != null)
                Context.Entry(entity.Cliente).State = EntityState.Unchanged;

            foreach (var produto in entity.Produtos)
            {
                Context.Entry(produto).State = EntityState.Unchanged;
            }

            await Context.SaveChangesAsync(cancellationToken);

            model.Id = entity.Id;
        }

        public override async Task RemoverAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = id > 0 ? await Context.Set<PedidoEntity>().FindAsync([id], cancellationToken) : null;
            if (entity != null)
            {
                Context.Set<PedidoEntity>().Remove(entity);

                if (entity.Cliente != null)
                    Context.Entry(entity.Cliente).State = EntityState.Unchanged;

                foreach (var produto in entity.Produtos)
                {
                    Context.Entry(produto).State = EntityState.Unchanged;
                }

                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public override async Task AtualizarAsync(Pedido model, CancellationToken cancellationToken = default)
        {
            model.DataAtualizacao = DateTime.Now;

            //1. Carrega o pedido do banco incluindo seus produtos atuais
            var entity = await Context.Set<PedidoEntity>()
                .AsTracking()
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            if (entity == null)
                return;

            entity.DataAtualizacao = model.DataAtualizacao;

            if (model.Cliente != null)
                entity.ClienteId = model.Cliente.Id;

            //2. IDs dos produtos enviados no request
            var idsProdutosNovos = model.Produtos.Select(p => p.Id).ToList();

            //3. REMOVE os produtos que NÃO vieram na requisição
            var produtosParaRemover = entity.Produtos
                .Where(p => !idsProdutosNovos.Contains(p.Id))
                .ToList();

            foreach (var produtoRemover in produtosParaRemover)
            {
                entity.Produtos.Remove(produtoRemover);
            }

            //4. ADICIONA os produtos que vieram no request mas ainda NÃO estão no pedido
            var idsProdutosAtuais = entity.Produtos.Select(p => p.Id).ToList();
            var idsParaAdicionar = idsProdutosNovos.Except(idsProdutosAtuais).ToList();

            foreach (var idNovo in idsParaAdicionar)
            {
                //Busca a entidade do produto no banco para associar a referência correta
                var produtoDb = await Context.Set<ProdutoEntity>()
                    .FirstOrDefaultAsync(p => p.Id == idNovo, cancellationToken);

                if (produtoDb != null)
                    entity.Produtos.Add(produtoDb);
            }

            //5. Salva no banco de dados
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Pedido>> ObterPorNumeroAsync(long numeroPedido, CancellationToken cancellationToken = default)
        {
            var entities = await base.Context.Set<PedidoEntity>()
                .AsNoTracking()
                .Where(pedido => pedido.Numero == numeroPedido)
                .ToListAsync(cancellationToken);

            return entities.Select(MapToModel).ToList();
        }

        public async Task<List<Pedido>> ObterPorClienteAsync(long clienteId, CancellationToken cancellationToken = default)
        {
            var entities = await base.Context.Set<PedidoEntity>()
                .AsNoTracking()
                .Where(pedido => pedido.ClienteId == clienteId)
                .ToListAsync(cancellationToken);

            return entities.Select(MapToModel).ToList();
        }

        public async Task<List<Pedido>> ObterPorProdutoAsync(long produtoId, CancellationToken cancellationToken = default)
        {
            var entities = await base.Context.Set<PedidoEntity>()
                .AsNoTracking()
                .Where(pedido => pedido.Produtos.Any(produto => produto.Id == produtoId))
                .ToListAsync(cancellationToken);

            return entities.Select(MapToModel).ToList();
        }
    }
}
