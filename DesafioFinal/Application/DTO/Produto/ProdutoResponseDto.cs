using DesafioFinal.Application.DTO.Common;
using DesafioFinal.Application.DTO.Common.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Produto
{
    /// <summary>
    /// Essa classe reprensenta a resposta Json referente ao produto usado no verbo GET
    /// </summary>
    public class ProdutoResponseDto : ResponseBaseDto
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        [JsonPropertyName("nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome pode ter no máximo 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Valor do produto
        /// </summary>
        [JsonPropertyName("valor")]
        [JsonConverter(typeof(DecimalDuasCasasConverter))]
        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "O valor deve ser entre 0.01 e 999999.99.")]
        public decimal Valor { get; set; }
    }
}