using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Pedido;
using DesafioFinal.Application.Mapper;
using DesafioFinal.Application.Port.In;
using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;
using DesafioFinal.Domain.Service;

namespace DesafioFinal.Application.Usecase
{
    public class PedidoFacade(IPedidoRepository pedidoRepository, PedidoValidatorService validatorService) : IPedidoFacade
    {
        public async Task<ResultResponse> CriarAsync(PedidoPostDto dto, CancellationToken cancellationToken)
        {
            //1. Mapeamento
            var pedido = PedidoMapper.ToModel(dto);

            //2. Gera o número do pedido
            pedido.GerarNumeroPedido();

            //3. Validação das Regras de Negócio
            var erros = await validatorService.ValidarPedido(pedido, cancellationToken);
            if (erros.Count > 0)
                return ResultResponse.CriarFalha(erros, "Erro ao cadastrar pedido.");

            //4. Persistência
            await pedidoRepository.AdicionarAsync(pedido, cancellationToken);

            //5. Retorno de Sucesso com o ID atualizado
            return ResultResponse.CriarSucesso($"ID: {pedido.Id}", "Pedido cadastrado com sucesso.");
        }

        public async Task<ResultResponse> AtualizarAsync(PedidoPatchPutDto dto, long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Pedido? pedido = await pedidoRepository.ObterPorIdAsync(id, cancellationToken);

            if (pedido == null)
                return ResultResponse.CriarFalha(["Pedido não encontrado"], "Erro ao atualizar pedido.");

            //2. Validação de Alteração
            if (dto.Produtos.Count == pedido.Produtos.Count)
            {
                var produtosOrdenadosDto = dto.Produtos.OrderBy(produto => produto).ToArray();
                var produtosOrdenadosModel = pedido.Produtos.Select(produto => produto.Id).OrderBy(produto => produto).ToArray();

                bool houveAlteracao = false;

                for (var i = 0; i < pedido.Produtos.Count; i++)
                {
                    if (produtosOrdenadosDto[i] != produtosOrdenadosModel[i])
                    {
                        houveAlteracao = true;
                        break;
                    }
                }

                if (!houveAlteracao)
                    return ResultResponse.CriarFalha(["Nenhum dado foi alterado."], "Erro ao atualizar pedido.");
            }

            //3. Modificação dos Campos
            pedido.RemoverTodosProdutos();
            dto.Produtos.ForEach(produto => pedido.AdicionarProduto(new Produto() { Id = produto }));

            //4. Validação das Regras de Negócio
            var erros = await validatorService.ValidarPedido(pedido, cancellationToken);
            if (erros.Count > 0)
                return ResultResponse.CriarFalha(erros, "Erro ao atualizar pedido.");

            //5. Persistência
            await pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            //6. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"ID: {pedido.Id}", "Pedido atualizado com sucesso.");
        }

        public async Task<ResultResponse> ApagarAsync(long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Pedido? pedido = await pedidoRepository.ObterPorIdAsync(id, cancellationToken);

            if (pedido == null)
                return ResultResponse.CriarFalha(["Pedido não encontrado"], "Erro ao atualizar pedido.");

            //2. Apaga o registro persistido
            await pedidoRepository.RemoverAsync(pedido.Id, cancellationToken);

            //3. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"ID: {pedido.Id}", "Pedido apagado com sucesso.");
        }

        public async Task<ResultResponse> ObterQuantidadeTotalAsync(CancellationToken cancellationToken)
        {
            //1. Obtém a contagem de pedidos
            int contagem = await pedidoRepository.ObterContagemAsync(cancellationToken);

            //2. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"Contagem: {contagem}", "Contagem de pedidos realizada com sucesso.");
        }

        public async Task<List<PedidoResponseDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            List<Pedido> listaPedidos = await pedidoRepository.ObterTodosAsync(cancellationToken);

            //2. Mapeia as entidades
            var listaRetorno = listaPedidos.Select(PedidoMapper.ToDto).ToList();

            //3. Retorno da lista de pedidos
            return listaRetorno;
        }

        public async Task<PedidoResponseDto> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Pedido? pedido = await pedidoRepository.ObterPorIdAsync(id, cancellationToken);

            //2. Mapeia e retorna o pedido
            return PedidoMapper.ToDto(pedido!);
        }

        public async Task<List<PedidoResponseDto>> ObterPorClienteAsync(long cliente, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            List<Pedido> listaPedidos = await pedidoRepository.ObterPorClienteAsync(cliente, cancellationToken);

            //2. Mapeia as entidades
            var listaRetorno = listaPedidos.Select(PedidoMapper.ToDto).ToList();

            //3. Retorno da lista de pedidos
            return listaRetorno;
        }

        public async Task<List<PedidoResponseDto>> ObterPorProdutoAsync(long pedido, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            List<Pedido> listaPedidos = await pedidoRepository.ObterPorProdutoAsync(pedido, cancellationToken);

            //2. Mapeia as entidades
            var listaRetorno = listaPedidos.Select(PedidoMapper.ToDto).ToList();

            //3. Retorno da lista de pedidos
            return listaRetorno;
        }
    }
}
