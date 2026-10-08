using DesafioFinal.Application.DTO;
using DesafioFinal.Application.DTO.Produto;
using DesafioFinal.Application.Mapper;
using DesafioFinal.Application.Port.In;
using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;
using DesafioFinal.Domain.Service;

namespace DesafioFinal.Application.Usecase
{
    public class ProdutoFacade(IProdutoRepository produtoRepository, ProdutoValidatorService validatorService) : IProdutoFacade
    {
        public async Task<ResultResponse> CriarAsync(ProdutoPostPutDto dto, CancellationToken cancellationToken)
        {
            //1. Mapeamento
            var produto = ProdutoMapper.ToModel(dto);

            //2. Validação das Regras de Negócio
            var erros = await validatorService.ValidarProduto(produto, cancellationToken);
            if (erros.Count > 0)
                return ResultResponse.CriarFalha(erros, "Erro ao cadastrar produto.");

            //3. Persistência
            await produtoRepository.AdicionarAsync(produto, cancellationToken);

            //4. Retorno de Sucesso com o ID atualizado
            return ResultResponse.CriarSucesso($"ID: {produto.Id}", "Produto cadastrado com sucesso.");
        }

        public async Task<ResultResponse> AtualizarCompletoAsync(ProdutoPostPutDto dto, long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Produto? produto = await produtoRepository.ObterPorIdAsync(id, cancellationToken);

            if (produto == null)
                return ResultResponse.CriarFalha(["Produto não encontrado"], "Erro ao atualizar produto.");

            //2. Validação de Alteração
            if (produto.Nome.Equals(dto.Nome) && produto.Valor == dto.Valor)
                return ResultResponse.CriarFalha(["Nenhum dado foi alterado."], "Erro ao atualizar produto.");

            //3. Modificação dos Campos
            produto.Nome = dto.Nome!;
            produto.Valor = dto.Valor;

            return await AtualizarAsync(produto, cancellationToken);
        }

        public async Task<ResultResponse> AtualizarParcialAsync(ProdutoPatchDto dto, long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente           
            Produto? produto = await produtoRepository.ObterPorIdAsync(id, cancellationToken);

            if (produto == null)
                return ResultResponse.CriarFalha(["Produto não encontrado"], "Erro ao atualizar produto.");

            bool houveAlteracao = false;

            //2. Modificação e Comparação dos Campos Informados
            if (!string.IsNullOrWhiteSpace(dto.Nome) && !dto.Nome.Equals(produto.Nome))
            {
                houveAlteracao = true;
                produto.Nome = dto.Nome;
            }

            if (dto.Valor != produto.Valor)
            {
                houveAlteracao = true;
                produto.Valor = dto.Valor;
            }

            if (!houveAlteracao)
                return ResultResponse.CriarFalha(["Não houve alteração"], "Erro ao atualizar produto.");

            return await AtualizarAsync(produto, cancellationToken);
        }

        private async Task<ResultResponse> AtualizarAsync(Produto Produto, CancellationToken cancellationToken)
        {
            //2. Validação das Regras de Negócio
            var erros = await validatorService.ValidarProduto(Produto, cancellationToken);
            if (erros.Count > 0)
                return ResultResponse.CriarFalha(erros, "Erro ao atualizar produto.");

            //3. Persistência
            await produtoRepository.AtualizarAsync(Produto, cancellationToken);

            //4. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"ID: {Produto.Id}", "Produto atualizado com sucesso.");
        }

        public async Task<ResultResponse> ApagarAsync(long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Produto? cliente = await produtoRepository.ObterPorIdAsync(id, cancellationToken);

            if (cliente == null)
                return ResultResponse.CriarFalha(["Produto não encontrado"], "Erro ao atualizar produto.");

            //2. Apaga o registro persistido
            await produtoRepository.RemoverAsync(cliente.Id, cancellationToken);

            //3. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"ID: {cliente.Id}", "Produto apagado com sucesso.");
        }

        public async Task<ResultResponse> ObterQuantidadeTotalAsync(CancellationToken cancellationToken)
        {
            //1. Obtém a contagem de produtos
            int contagem = await produtoRepository.ObterContagemAsync(cancellationToken);

            //2. Retorno de Sucesso
            return ResultResponse.CriarSucesso($"Contagem: {contagem}", "Contagem de produtos realizada com sucesso.");
        }

        public async Task<List<ProdutoResponseDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            List<Produto> listaProdutos = await produtoRepository.ObterTodosAsync(cancellationToken);

            //2. Mapeia as entidades
            var listaRetorno = listaProdutos.Select(ProdutoMapper.ToDto).ToList();

            //3. Retorno da lista de produtos
            return listaRetorno;
        }

        public async Task<ProdutoResponseDto> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            Produto? produto = await produtoRepository.ObterPorIdAsync(id, cancellationToken);

            //2. Mapeia e retorna o produto
            return ProdutoMapper.ToDto(produto!);
        }

        public async Task<List<ProdutoResponseDto>> ObterPorNomeAsync(string nome, CancellationToken cancellationToken)
        {
            //1. Busca da Entidade Existente
            List<Produto> listaProdutos = await produtoRepository.ObterPorNomeAsync(nome, cancellationToken);

            //2. Mapeia as entidades
            var listaRetorno = listaProdutos.Select(ProdutoMapper.ToDto).ToList();

            //3. Retorno da lista de produtos
            return listaRetorno;
        }
    }
}
