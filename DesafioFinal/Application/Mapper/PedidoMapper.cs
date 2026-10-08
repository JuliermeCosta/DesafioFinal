using DesafioFinal.Application.DTO.Pedido;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Application.Mapper
{
    public static class PedidoMapper
    {
        public static Pedido ToModel(PedidoPostDto dto)
        {
            if (dto == null) return null!;

            var model = new Pedido
            {
                Cliente = new Cliente() { Id = dto.Cliente },
            };

            foreach (var produto in dto.Produtos)
            {
                model.AdicionarProduto(new Produto() { Id = produto });
            }

            return model;
        }

        public static PedidoResponseDto ToDto(Pedido model)
        {
            if (model == null) return null!;

            var dto = new PedidoResponseDto
            {
                Id = model.Id,
                Numero = model.Numero,
                Cliente = ClienteMapper.ToDto(model.Cliente),
                DataInclusao = model.DataInclusao,
                DataAtualizacao = model.DataAtualizacao
            };

            foreach (var produto in model.Produtos)
            {
                dto.Produtos.Add(ProdutoMapper.ToDto(produto));
            }

            dto.Total = model.ObterTotalPedido();

            return dto;
        }
    }
}
