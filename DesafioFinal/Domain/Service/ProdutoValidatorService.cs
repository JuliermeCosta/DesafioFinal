using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Domain.Service
{
    public class ProdutoValidatorService(IProdutoRepository produtoRepository)
    {        
        public async Task<List<string>> ValidarProduto(Produto produto, CancellationToken cancellationToken = default)
        {
            var erros = new List<string>();

            //1. Validar unicidade do nome no banco
            var produtoExistenteNome = await produtoRepository.ObterPorNomeAsync(produto.Nome, cancellationToken);
            if (produtoExistenteNome != null && produtoExistenteNome.Any(c => c.Id != produto.Id))
                erros.Add("Já existe um produto cadastrado com este nome.");

            return erros;
        }
    }
}
