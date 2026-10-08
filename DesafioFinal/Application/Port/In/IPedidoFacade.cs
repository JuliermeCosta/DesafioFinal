using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Pedido;

namespace DesafioFinal.Application.Port.In
{
    public interface IPedidoFacade
    {
        Task<ResultResponse> CriarAsync(PedidoPostDto dto, CancellationToken cancellationToken);
        Task<ResultResponse> AtualizarAsync(PedidoPatchPutDto dto, long id, CancellationToken cancellationToken);
        Task<ResultResponse> ApagarAsync(long id, CancellationToken cancellationToken);
        Task<ResultResponse> ObterQuantidadeTotalAsync(CancellationToken cancellationToken);
        Task<List<PedidoResponseDto>> ObterTodosAsync(CancellationToken cancellationToken);
        Task<PedidoResponseDto> ObterPorIdAsync(long id, CancellationToken cancellationToken);
        Task<List<PedidoResponseDto>> ObterPorClienteAsync(long cliente, CancellationToken cancellationToken);
        Task<List<PedidoResponseDto>> ObterPorProdutoAsync(long pedido, CancellationToken cancellationToken);
    }
}
