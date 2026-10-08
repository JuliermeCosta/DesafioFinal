using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioFinal.Adapter.Out.Database.Entity.Common
{
    /// <summary>
    /// Classe abstrata com campos em comum entre as entidades
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// ID no banco de dados
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Data de inclusão das entidades
        /// </summary>
        public DateTime DataInclusao { get; set; }

        /// <summary>
        /// Data de atualização das entidades
        /// </summary>
        public DateTime? DataAtualizacao { get; set; }
    }
}
