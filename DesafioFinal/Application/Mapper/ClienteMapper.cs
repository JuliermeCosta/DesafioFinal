using DesafioFinal.Application.DTO.Cliente;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Application.Mapper
{
    public static class ClienteMapper
    {
        public static Cliente ToModel(ClientePostDto dto)
        {
            if (dto == null) return null!;

            return new Cliente
            {
                Nome = dto.Nome!,
                Cpf = long.Parse(dto.Cpf!),
                Email = dto.Email!,
                Celular = dto.Celular
            };
        }

        public static ClienteResponseDto ToDto(Cliente model)
        {
            if (model == null) return null!;

            return new ClienteResponseDto
            {
                Id = model.Id,
                Nome = model.Nome,
                Cpf = model.Cpf.ToString(),
                Email = model.Email,
                Celular = model.Celular,
                DataInclusao = model.DataInclusao,
                DataAtualizacao = model.DataAtualizacao
            };
        }
    }
}
