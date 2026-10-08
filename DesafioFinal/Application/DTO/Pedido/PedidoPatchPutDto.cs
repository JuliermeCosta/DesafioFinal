using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Pedido
{
    /// <summary>
    /// Essa classe reprensenta a estrutura Json referente ao pedido usada no verbo PATCH e no verbo PUT
    /// </summary>
    public class PedidoPatchPutDto
    {
        /// <summary>
        /// Lista de produtos que constam no pedido, informar os IDs de cada produto
        /// </summary>
        [JsonPropertyName("produtos")]
        [Required(ErrorMessage = "A lista de produtos é obrigatória.")]
        [MinLength(1, ErrorMessage = "Informar ao menos um produto.")]
        public List<long> Produtos { get; set; } = new();
    }
}
