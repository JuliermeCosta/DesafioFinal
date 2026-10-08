using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Adapter.Out.Database.Mapper
{
    public static class ClientePersistenceMapper
    {
        public static Cliente ToModel(ClienteEntity entity)
        {
            if (entity == null) return null!;

            return new Cliente
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Cpf = entity.Cpf,
                Email = entity.Email,
                Celular = entity.Celular,
                DataInclusao = entity.DataInclusao,
                DataAtualizacao = entity.DataAtualizacao
            };
        }

        public static ClienteEntity ToEntity(Cliente model)
        {
            if (model == null) return null!;

            return new ClienteEntity
            {
                Id = model.Id,
                Nome = model.Nome,
                Cpf = model.Cpf,
                Email = model.Email,
                Celular = model.Celular,
                DataInclusao = model.DataInclusao,
                DataAtualizacao = model.DataAtualizacao
            };
        }
    }
}
