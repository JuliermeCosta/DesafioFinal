using DesafioFinal.Adapter.In.Filters;
using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Cliente;
using DesafioFinal.Application.Port.In;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace DesafioFinal.Adapter.In.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController(IClienteFacade clienteFacade) : ControllerBase
    {
        /// <summary>
        /// Cria um cliente.
        /// </summary>
        /// <param name="dto">Estrutura JSON com os dados do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarAsync([FromBody] ClientePostDto dto, CancellationToken cancellationToken)
        {
            var result = await clienteFacade.CriarAsync(dto, cancellationToken);

            if (!result.Sucesso)
                return BadRequest(result);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// Atualiza o cliente (todos os campos).
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <param name="dto">Estrutura JSON com os dados do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCompletoAsync(long id, [FromBody] ClientePutDto dto, CancellationToken cancellationToken)
        {
            var result = await clienteFacade.AtualizarCompletoAsync(dto, id, cancellationToken);

            if (!result.Sucesso)
            {
                //Verifica se a falha foi por conta do registro não ter sido encontrado
                if (result.Erros!.Contains("Cliente não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Atualiza o cliente (alguns campos).
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <param name="dto">Estrutura JSON com os dados do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpPatch("{id:long}")]
        [EnableBuffering]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarParcialAsync(long id, [FromBody] ClientePatchDto dto, CancellationToken cancellationToken)
        {
            if (HttpContext.Items.TryGetValue("RawRequestBody", out var rawBody) && rawBody is string bodyString)
                dto.CelularFoiInformado = Regex.IsMatch(bodyString, @"""celular""\s*:", RegexOptions.IgnoreCase);

            var result = await clienteFacade.AtualizarParcialAsync(dto, id, cancellationToken);

            if (!result.Sucesso)
            {
                if (result.Erros!.Contains("Cliente não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Apaga um cliente.
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApagarAsync(long id, CancellationToken cancellationToken)
        {
            var result = await clienteFacade.ApagarAsync(id, cancellationToken);

            if (!result.Sucesso)
            {
                if (result.Erros!.Contains("Cliente não encontrado"))
                    return NotFound(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Contagem dos clientes.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o resultado da requisição</returns>
        [HttpGet("contagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContagemAsync(CancellationToken cancellationToken)
        {
            var result = await clienteFacade.ObterQuantidadeTotalAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os clientes.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna todos os clientes</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var result = await clienteFacade.ObterTodosAsync(cancellationToken);

            if (result == null || result.Count == 0)
                return NotFound("Nenhum cliente encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém um cliente por ID.
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorno o cliente que corresponde ao ID</returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            var result = await clienteFacade.ObterPorIdAsync(id, cancellationToken);

            if (result == null)
                return NotFound("Cliente não encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém o cliente por nome.
        /// </summary>
        /// <param name="nome">Nome ou parte do nome do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna todos os clientes que contém parte do nome informado</returns>
        [HttpGet("busca-por-nome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorNomeAsync([FromQuery] string nome, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest(ResultResponse.CriarFalha(["O parâmetro 'nome' é obrigatório para a busca."], "Parâmetro inválido."));

            var result = await clienteFacade.ObterPorNomeAsync(nome, cancellationToken);

            if (result == null || result.Count == 0)
                return NotFound("Nenhum cliente encontrado");

            return Ok(result);
        }

        /// <summary>
        /// Obtém o cliente por CPF.
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorno o cliente que corresponde ao CPF</returns>
        [HttpGet("busca-por-cpf/{cpf:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorCpfAsync(long cpf, CancellationToken cancellationToken)
        {
            var result = await clienteFacade.ObterPorCpfAsync(cpf, cancellationToken);

            if (result == null)
                return NotFound("Cliente não encontrado");

            return Ok(result);
        }
    }
}