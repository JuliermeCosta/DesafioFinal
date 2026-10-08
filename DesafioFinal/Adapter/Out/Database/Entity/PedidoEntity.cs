using DesafioFinal.Adapter.Out.Database.Entity.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioFinal.Adapter.Out.Database.Entity
{
    /// <summary>
    /// Essa classe reprensenta a tabela Pedidos no banco de dados
    /// </summary>
    [Table("Pedidos")]
    public class PedidoEntity : EntityBase
    {
        /// <summary>
        /// Número do pedido
        /// </summary>
        public long Numero { get; set; }

        /// <summary>
        /// Relacionamento 1:N com Cliente
        /// </summary>
        public long ClienteId { get; set; }

        /// <summary>
        /// Objeto com os dados do cliente com a ID do campo ClienteId
        /// </summary>
        public ClienteEntity Cliente { get; set; } = null!;

        /// <summary>
        /// Relacionamento direto N:N com Produtos
        /// </summary>
        public ICollection<ProdutoEntity> Produtos { get; set; } = new List<ProdutoEntity>();
    }
}
