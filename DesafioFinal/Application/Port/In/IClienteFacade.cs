using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Cliente;

namespace DesafioFinal.Application.Port.In
{
    public interface IClienteFacade
    {
        Task<ResultResponse> CriarAsync(ClientePostDto dto, CancellationToken cancellationToken);
        Task<ResultResponse> AtualizarCompletoAsync(ClientePutDto dto, long id, CancellationToken cancellationToken);
        Task<ResultResponse> AtualizarParcialAsync(ClientePatchDto dto, long id, CancellationToken cancellationToken);
        Task<ResultResponse> ApagarAsync(long id, CancellationToken cancellationToken);
        Task<ResultResponse> ObterQuantidadeTotalAsync(CancellationToken cancellationToken);
        Task<List<ClienteResponseDto>> ObterTodosAsync(CancellationToken cancellationToken);
        Task<ClienteResponseDto> ObterPorIdAsync(long id, CancellationToken cancellationToken);
        Task<List<ClienteResponseDto>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken);
        Task<ClienteResponseDto> ObterPorCpfAsync(long cpf, CancellationToken cancellationToken);
    }
}
