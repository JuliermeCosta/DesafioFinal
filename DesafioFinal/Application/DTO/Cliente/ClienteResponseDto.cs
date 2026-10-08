using DesafioFinal.Application.DTO.Common;
using DesafioFinal.Application.DTO.Common.Converters;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Cliente
{
    /// <summary>
    /// Essa classe reprensenta a resposta Json referente ao cliente usado no verbo GET
    /// </summary>
    public class ClienteResponseDto : ResponseBaseDto
    {
        /// <summary>
        /// CPF do cliente
        /// </summary>
        [JsonPropertyName("cpf")]
        [JsonConverter(typeof(CpfConverter))]
        public string Cpf { get; set; } = string.Empty;

        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do cliente
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Celular do cliente (apenas números, seguindo esse formato: (XX) XXXXX-XXXX)
        /// </summary>
        [JsonPropertyName("celular")]
        public long? Celular { get; set; }
    }
}
