using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Adapter.Out.Database.Mapper
{
    public static class ProdutoPersistenceMapper
    {
        public static Produto ToModel(ProdutoEntity entity)
        {
            if (entity == null) return null!;

            return new Produto
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Valor = entity.Valor,
                DataInclusao = entity.DataInclusao,
                DataAtualizacao = entity.DataAtualizacao
            };
        }

        public static ProdutoEntity ToEntity(Produto model)
        {
            if (model == null) return null!;

            return new ProdutoEntity
            {
                Id = model.Id,
                Nome = model.Nome,
                Valor = model.Valor,
                DataInclusao = model.DataInclusao,
                DataAtualizacao = model.DataAtualizacao
            };
        }
    }
}
