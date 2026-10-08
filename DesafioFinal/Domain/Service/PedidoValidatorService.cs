using DesafioFinal.Application.Port.Out;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Domain.Service
{
    public class PedidoValidatorService(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
    {
        public async Task<List<string>> ValidarPedido(Pedido pedido, CancellationToken cancellationToken = default)
        {
            var erros = new List<string>();

            //1. Validar unicidade do número no banco
            var pedidoExistenteNome = await pedidoRepository.ObterPorNumeroAsync(pedido.Numero, cancellationToken);
            if (pedidoExistenteNome != null && pedidoExistenteNome.Any(c => c.Id != pedido.Id))
                erros.Add("Já existe um pedido cadastrado com este número.");

            //2. Verifica se o ID do cliente é válido
            var cliente = await clienteRepository.ObterPorIdAsync(pedido.Cliente.Id, cancellationToken);
            if (cliente == null)
                erros.Add("Cliente inválido.");

            //3. Verifica se os IDs dos produtos são válidos
            int produtosInvalidos = 0;
            foreach (var produtoId in pedido.Produtos.Select(x => x.Id).Distinct())
            {
                var produto = await produtoRepository.ObterPorIdAsync(produtoId, cancellationToken);
                if (produto == null)
                    produtosInvalidos++;
            }

            if (produtosInvalidos > 0)
                erros.Add(produtosInvalidos == 1 ? "Há 1 produto ínválido." : $"Há {produtosInvalidos} produtos inválidos.");

            return erros;
        }
    }
}
