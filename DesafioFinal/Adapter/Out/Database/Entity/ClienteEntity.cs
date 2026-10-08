using DesafioFinal.Adapter.Out.Database.Entity.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioFinal.Adapter.Out.Database.Entity
{
    /// <summary>
    /// Essa classe reprensenta a tabela Clientes no banco de dados
    /// </summary>
    [Table("Clientes")]
    public class ClienteEntity : EntityBase
    {
        /// <summary>
        /// CPF do cliente
        /// </summary>
        public long Cpf { get; set; }

        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do cliente
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Celular do cliente (apenas números, com a máscara (XX) XXXXX-XXXX)
        /// </summary>
        public long? Celular { get; set; }
    }
}
