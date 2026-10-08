using DesafioFinal.Application.DTO.Common.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Produto
{
    /// <summary>
    /// Essa classe reprensenta a estrutura Json referente ao produto usada no verbo PATCH
    /// </summary>
    public class ProdutoPatchDto
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        [JsonPropertyName("nome")]
        [StringLength(150, ErrorMessage = "O nome pode ter no máximo 150 caracteres.")]
        public string? Nome { get; set; }

        /// <summary>
        /// Valor do produto
        /// </summary>
        [JsonPropertyName("valor")]
        [JsonConverter(typeof(DecimalDuasCasasConverter))]
        [Range(0.01, 999999.99, ErrorMessage = "O valor deve ser entre 0.01 e 999999.99.")]
        public decimal Valor { get; set; }
    }
}
