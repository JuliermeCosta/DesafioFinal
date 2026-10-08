using DesafioFinal.Domain.Model.Common;

namespace DesafioFinal.Domain.Model
{
    /// <summary>
    /// Classe que representa o cliente
    /// Essa classe é usada para regras de negócio
    /// </summary>
    public class Cliente : ModelBase
    {
        /// <summary>
        /// CPF do cliente
        /// </summary>
        public long Cpf { get; set; }

        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do cliente
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Celular do cliente (apenas números, com a máscara (XX) XXXXX-XXXX)
        /// </summary>
        public long? Celular { get; set; }
    }
}
