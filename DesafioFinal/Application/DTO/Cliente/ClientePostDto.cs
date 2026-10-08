using DesafioFinal.Application.DTO.Common.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Cliente
{
    /// <summary>
    /// Essa classe reprensenta a estrutura Json referente ao cliente usada no verbo POST
    /// </summary>
    public class ClientePostDto
    {
        /// <summary>
        /// CPF do cliente
        /// </summary>
        [JsonPropertyName("cpf")]
        [JsonConverter(typeof(CpfConverter))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 dígitos (somente números).")]
        public string? Cpf { get; set; }

        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        [JsonPropertyName("nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome pode ter no máximo 150 caracteres.")]
        public string? Nome { get; set; }

        /// <summary>
        /// E-mail do cliente
        /// </summary>
        [JsonPropertyName("email")]        
        [Required(AllowEmptyStrings = false, ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail no formato inválido.")]
        [StringLength(150, ErrorMessage = "O e-mail pode ter no máximo 150 caracteres.")]
        public string? Email { get; set; }

        /// <summary>
        /// Celular do cliente (apenas números, seguindo esse formato: (XX) XXXXX-XXXX)
        /// </summary>
        [JsonPropertyName("celular")]
        [Range(1000000000, 99999999999, ErrorMessage = "O telefone informado é inválido.")]
        public long? Celular { get; set; }
    }
}
