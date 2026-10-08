using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Produto;

namespace DesafioFinal.Application.Port.In
{
    public interface IProdutoFacade
    {
        Task<ResultResponse> CriarAsync(ProdutoPostPutDto dto, CancellationToken cancellationToken);
        Task<ResultResponse> AtualizarCompletoAsync(ProdutoPostPutDto dto, long id, CancellationToken cancellationToken);
        Task<ResultResponse> AtualizarParcialAsync(ProdutoPatchDto dto, long id, CancellationToken cancellationToken);
        Task<ResultResponse> ApagarAsync(long id, CancellationToken cancellationToken);
        Task<ResultResponse> ObterQuantidadeTotalAsync(CancellationToken cancellationToken);
        Task<List<ProdutoResponseDto>> ObterTodosAsync(CancellationToken cancellationToken);
        Task<ProdutoResponseDto> ObterPorIdAsync(long id, CancellationToken cancellationToken);
        Task<List<ProdutoResponseDto>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken);
    }
}
