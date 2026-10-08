using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Pedido
{
    /// <summary>
    /// Essa classe reprensenta a estrutura Json referente ao pedido usada no verbo POST
    /// </summary>
    public class PedidoPostDto
    {
        /// <summary>
        /// Cliente que constam no pedido, informar o ID do cliente
        /// </summary>
        [JsonPropertyName("cliente")]
        [Range(1, long.MaxValue, ErrorMessage = "O ID do cliente deve ser um número maior que zero.")]
        public long Cliente { get; set; }

        /// <summary>
        /// Lista de produtos que constam no pedido, informar os IDs de cada produto
        /// </summary>
        [JsonPropertyName("produtos")]
        [Required(ErrorMessage = "A lista de produtos é obrigatória.")]
        [MinLength(1, ErrorMessage = "Informar ao menos um produto.")]
        public List<long> Produtos { get; set; } = new();
    }
}
