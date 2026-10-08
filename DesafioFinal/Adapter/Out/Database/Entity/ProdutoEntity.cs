using DesafioFinal.Adapter.Out.Database.Entity.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioFinal.Adapter.Out.Database.Entity
{
    /// <summary>
    /// Essa classe reprensenta a tabela Produtos no banco de dados
    /// </summary>
    [Table("Produtos")]
    public class ProdutoEntity : EntityBase
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Valor do produto
        /// </summary>
        public decimal Valor { get; set; }
    }
}
