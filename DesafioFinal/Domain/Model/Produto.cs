using DesafioFinal.Domain.Model.Common;

namespace DesafioFinal.Domain.Model
{
    /// <summary>
    /// Classe que representa o produto
    /// Essa classe é usada para regras de negócio
    /// </summary>
    public class Produto : ModelBase
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Valor do produto
        /// </summary>
        public decimal Valor { get; set; }
    }
}
