using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Cliente
{
    /// <summary>
    /// Essa classe reprensenta a estrutura Json referente ao cliente usada no verbo PATCH
    /// </summary>
    public class ClientePatchDto
    {
        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        [JsonPropertyName("nome")]
        [StringLength(150, ErrorMessage = "O nome pode ter no máximo 150 caracteres.")]
        public string? Nome { get; set; }

        /// <summary>
        /// E-mail do cliente
        /// </summary>
        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "E-mail no formato inválido.")]
        [StringLength(150, ErrorMessage = "O e-mail pode ter no máximo 150 caracteres.")]
        public string? Email { get; set; }

        /// <summary>
        /// Celular do cliente (apenas números, seguindo esse formato: (XX) XXXXX-XXXX)
        /// </summary>
        [JsonPropertyName("celular")]
        [Range(1000000000, 99999999999, ErrorMessage = "O telefone informado é inválido.")]
        public long? Celular { get; set; }

        /// <summary>
        /// Propriedade interna para saber se a tag "celular" existia no JSON
        /// </summary>
        [JsonIgnore]
        public bool CelularFoiInformado { get; set; }
    }
}
