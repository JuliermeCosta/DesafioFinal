using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Produto;
using DesafioFinal.Application.Port.In;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFinal.Adapter.In.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController(IProdutoFacade produtoFacade) : ControllerBase
    {
        /// <summary>
        /// Cria um produto.
        /// </summary>
        /// <param name="dto">Estrutura JSON com os dados do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarAsync([FromBody] ProdutoPostPutDto dto, CancellationToken cancellationToken)
        {
            var result = await produtoFacade.CriarAsync(dto, cancellationToken);

            if (!result.Sucesso)
                return BadRequest(result);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// Atualiza o produto (todos os campos).
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="dto">Estrutura JSON com os dados do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCompletoAsync(long id, [FromBody] ProdutoPostPutDto dto, CancellationToken cancellationToken)
        {
            var result = await produtoFacade.AtualizarCompletoAsync(dto, id, cancellationToken);

            if (!result.Sucesso)
            {
                //Verifica se a falha foi por conta do registro não ter sido encontrado
                if (result.Erros!.Contains("Produto não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Atualiza o produto (alguns campos).
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="dto">Estrutura JSON com os dados do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPatch("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarParcialAsync(long id, [FromBody] ProdutoPatchDto dto, CancellationToken cancellationToken)
        {
            var result = await produtoFacade.AtualizarParcialAsync(dto, id, cancellationToken);

            if (!result.Sucesso)
            {
                if (result.Erros!.Contains("Produto não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Apaga um produto.
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApagarAsync(long id, CancellationToken cancellationToken)
        {
            var result = await produtoFacade.ApagarAsync(id, cancellationToken);

            if (!result.Sucesso)
            {
                if (result.Erros!.Contains("Produto não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Contagem dos produtos.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpGet("contagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContagemAsync(CancellationToken cancellationToken)
        {
            var result = await produtoFacade.ObterQuantidadeTotalAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna todos os produtos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var result = await produtoFacade.ObterTodosAsync(cancellationToken);

            if (result == null || result.Count == 0)
                return NotFound("Nenhum produto encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém um produto por ID.
        /// </summary>
        /// <param name="id">ID do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorno o produto que corresponde ao ID</returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            var result = await produtoFacade.ObterPorIdAsync(id, cancellationToken);

            if (result == null)
                return NotFound("Produto não encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém o produto por nome.
        /// </summary>
        /// <param name="nome">Nome ou parte do nome do produto</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna todos os produtos que contém parte do nome informado</returns>
        [HttpGet("busca-por-nome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorNomeAsync([FromQuery] string nome, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest(ResultResponse.CriarFalha(["O parâmetro 'nome' é obrigatório para a busca."], "Parâmetro inválido."));

            var result = await produtoFacade.ObterPorNomeAsync(nome, cancellationToken);

            if (result == null || result.Count == 0)
                return NotFound("Nenhum produto encontrado");

            return Ok(result);
        }
    }
}
