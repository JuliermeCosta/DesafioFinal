using DesafioFinal.Application.DTO.Produto;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Application.Mapper
{
    public static class ProdutoMapper
    {
        public static Produto ToModel(ProdutoPostPutDto dto)
        {
            if (dto == null) return null!;

            return new Produto
            {
                Nome = dto.Nome!,
                Valor = dto.Valor
            };
        }

        public static ProdutoResponseDto ToDto(Produto model)
        {
            if (model == null) return null!;

            return new ProdutoResponseDto
            {
                Id = model.Id,
                Nome = model.Nome,
                Valor = model.Valor,
                DataInclusao = model.DataInclusao,
                DataAtualizacao = model.DataAtualizacao,
            };
        }
    }
}
