using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Adapter.Out.Database.Mapper;
using DesafioFinal.Adapter.Out.Database.Repository.Common;
using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DesafioFinal.Adapter.Out.Database.Repository
{
    public class ClienteRepository(DbContext context) : BaseRepository<ClienteEntity, Cliente>(context), IClienteRepository
    {
        protected override Cliente MapToModel(ClienteEntity entity) => ClientePersistenceMapper.ToModel(entity);
        protected override ClienteEntity MapToEntity(Cliente model) => ClientePersistenceMapper.ToEntity(model);

        public async Task<Cliente?> ObterPorCpfAsync(long cpf, CancellationToken cancellationToken = default)
        {
            var entity = await base.Context.Set<ClienteEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(cliente => cliente.Cpf == cpf, cancellationToken);

            if (entity == null)
                return null;

            return MapToModel(entity);
        }

        public async Task<List<Cliente>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken = default)
        {
            var entities = await base.Context.Set<ClienteEntity>()
                .AsNoTracking()
                .Where(cliente => cliente.Nome.ToLower().Contains(nome.ToLower()))
                .ToListAsync(cancellationToken);

            return entities.Select(MapToModel).ToList();
        }

        public async Task<List<Cliente>> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var entities = await base.Context.Set<ClienteEntity>()
                .AsNoTracking()
                .Where(cliente => cliente.Email.ToLower().Contains(email.ToLower()))
                .ToListAsync(cancellationToken);

            return entities.Select(MapToModel).ToList();
        }
    }
}
