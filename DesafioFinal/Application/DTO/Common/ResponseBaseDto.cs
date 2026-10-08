using DesafioFinal.Application.DTO.Common.Converters;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Common
{
    /// <summary>
    /// Classe abstrata com campos em comum entre os DTOs
    /// </summary>
    public abstract class ResponseBaseDto
    {
        /// <summary>
        /// ID do DTO
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Data de inclusão do DTO
        /// </summary>
        [JsonPropertyName("dataInclusao")]
        [JsonConverter(typeof(DataFormatadaConverter))]
        public DateTime DataInclusao { get; set; }

        /// <summary>
        /// Data de atualização do DTO
        /// </summary>
        [JsonPropertyName("dataAtualizacao")]
        [JsonConverter(typeof(DataFormatadaConverter))]
        public DateTime? DataAtualizacao { get; set; }
    }
}
