using DesafioFinal.Domain.Model.Common;

namespace DesafioFinal.Domain.Model
{
    /// <summary>
    /// Classe que representa o pedido de produtos realizados pelo cliente
    /// Essa classe é usada para regras de negócio
    /// </summary>
    public class Pedido : ModelBase
    {
        private readonly List<Produto> _produtos = new();
        /// <summary>
        /// Lista de produtos que constam no pedido
        /// </summary>
        public IReadOnlyCollection<Produto> Produtos { get { return _produtos.AsReadOnly(); } }

        /// <summary>
        /// Número do pedido
        /// </summary>
        public long Numero { get; set; }

        /// <summary>
        /// Cliente vinculado ao pedido
        /// </summary>
        public Cliente Cliente { get; set; } = null!;

        /// <summary>
        /// Adiciona um produto ao pedido
        /// </summary>
        /// <param name="produto">Produto que será adicionado ao pedido</param>
        public void AdicionarProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");

            _produtos.Add(produto);
        }

        /// <summary>
        /// Remove todos os produto do pedido
        /// </summary>
        public void RemoverTodosProdutos()
        {
            _produtos.Clear();
        }

        /// <summary>
        /// Gera o número do pedido
        /// </summary>
        public void GerarNumeroPedido()
        {
            Numero = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        }

        /// <summary>
        /// Obtem o total do pedido
        /// </summary>
        /// <returns>Total do pedido</returns>
        public decimal ObterTotalPedido()
        {
            return _produtos.Sum(x => x.Valor);
        }
    }
}
