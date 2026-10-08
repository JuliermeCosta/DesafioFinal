using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Application.Port.Out.Common;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Application.Port.Out
{
    public interface IClienteRepository : IBaseRepository<ClienteEntity, Cliente>
    {
        Task<Cliente?> ObterPorCpfAsync(long cpf, CancellationToken cancellationToken = default);
        Task<List<Cliente>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken = default);
        Task<List<Cliente>> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
