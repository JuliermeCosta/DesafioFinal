using DesafioFinal.Application.DTO.Cliente;
using DesafioFinal.Application.DTO.Common;
using DesafioFinal.Application.DTO.Produto;
using System.Text.Json.Serialization;

namespace DesafioFinal.Application.DTO.Pedido
{
    /// <summary>
    /// Essa classe reprensenta a resposta Json referente ao pedido usado no verbo GET
    /// </summary>
    public class PedidoResponseDto : ResponseBaseDto
    {
        /// <summary>
        /// Número do pedido
        /// </summary>
        [JsonPropertyName("numero")]
        public long Numero { get; set; }

        /// <summary>
        /// Cliente que constam no pedido
        /// </summary>
        [JsonPropertyName("cliente")]
        public ClienteResponseDto Cliente { get; set; } = new();

        /// <summary>
        /// Lista de produtos que constam no pedido
        /// </summary>
        [JsonPropertyName("produtos")]
        public List<ProdutoResponseDto> Produtos { get; set; } = new();

        /// <summary>
        /// Valor total do pedido
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
    }
}
