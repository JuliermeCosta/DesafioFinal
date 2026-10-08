using DesafioFinal.Application.DTO.Pedido;
using DesafioFinal.Application.Port.In;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFinal.Adapter.In.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidosController(IPedidoFacade pedidoFacade) : ControllerBase
    {
        /// <summary>
        /// Cria um pedido.
        /// </summary>
        /// <param name="dto">Estrutura JSON com os dados do pedido</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarAsync([FromBody] PedidoPostDto dto, CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.CriarAsync(dto, cancellationToken);

            if (!result.Sucesso)
                return BadRequest(result);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// Atualiza o pedido (todos os campos).
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="dto">Estrutura JSON com os dados do pedido</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCompletoAsync(long id, [FromBody] PedidoPatchPutDto dto, CancellationToken cancellationToken)
        {
            return await ProcessarAtualizacaoAsync(id, dto, cancellationToken);
        }

        /// <summary>
        /// Atualiza o pedido (alguns campos).
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="dto">Estrutura JSON com os dados do pedido</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPatch("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarParcialAsync(long id, [FromBody] PedidoPatchPutDto dto, CancellationToken cancellationToken)
        {
            return await ProcessarAtualizacaoAsync(id, dto, cancellationToken);
        }

        /// <summary>
        /// Atualiza o pedido (todos e alguns campos).
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="dto">Estrutura JSON com os dados do pedido</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        private async Task<IActionResult> ProcessarAtualizacaoAsync(long id, PedidoPatchPutDto dto, CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.AtualizarAsync(dto, id, cancellationToken);

            if (!result.Sucesso)
            {
                if (result.Erros!.Contains("Pedido não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Apaga um pedido.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApagarAsync(long id, CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.ApagarAsync(id, cancellationToken);

            if (!result.Sucesso)
            {
                if (result.Erros!.Contains("Pedido não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Contagem dos pedidos.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpGet("contagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContagemAsync(CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.ObterQuantidadeTotalAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os pedidos.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna todos os pedidos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.ObterTodosAsync(cancellationToken);

            if (result == null || result.Count == 0)
                return NotFound("Nenhum pedido encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém um pedido por ID.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorno o pedido que corresponde ao ID</returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.ObterPorIdAsync(id, cancellationToken);

            if (result == null)
                return NotFound("Pedido não encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém o pedido por cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna todos os pedidos do cliente</returns>
        [HttpGet("cliente/{clienteId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorClienteAsync(long clienteId, CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.ObterPorClienteAsync(clienteId, cancellationToken);

            if (result == null || result.Count == 0)
                return NotFound("Nenhum pedido encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém o produto por produto.
        /// </summary>
        /// <param name="produtoId">ID do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna todos os pedidos que contém esse produto</returns>
        [HttpGet("produto/{produtoId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorProdutoAsync(long produtoId, CancellationToken cancellationToken)
        {
            var result = await pedidoFacade.ObterPorProdutoAsync(produtoId, cancellationToken);

            if (result == null || result.Count == 0)
                return NotFound("Nenhum pedido encontrado");

            return Ok(result);
        }
    }
}
