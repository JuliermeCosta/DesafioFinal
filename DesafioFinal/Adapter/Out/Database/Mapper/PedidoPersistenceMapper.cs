using DesafioFinal.Adapter.Out.Database.Entity;
using DesafioFinal.Domain.Model;

namespace DesafioFinal.Adapter.Out.Database.Mapper
{
    public static class PedidoPersistenceMapper
    {
        public static Pedido ToModel(PedidoEntity entity)
        {
            if (entity == null) return null!;

            var model = new Pedido
            {
                Id = entity.Id,
                Numero = entity.Numero,
                Cliente = ClientePersistenceMapper.ToModel(entity.Cliente),
                DataInclusao = entity.DataInclusao,
                DataAtualizacao = entity.DataAtualizacao
            };

            foreach (var produto in entity.Produtos)
            {
                model.AdicionarProduto(ProdutoPersistenceMapper.ToModel(produto));
            }

            return model;
        }

        public static PedidoEntity ToEntity(Pedido model)
        {
            if (model == null) return null!;

            var entity = new PedidoEntity
            {
                Id = model.Id,
                Numero = model.Numero,
                ClienteId = model.Cliente.Id,
                Cliente = ClientePersistenceMapper.ToEntity(model.Cliente),
                DataInclusao = model.DataInclusao,
                DataAtualizacao = model.DataAtualizacao
            };

            foreach (var produto in model.Produtos)
            {
                entity.Produtos.Add(ProdutoPersistenceMapper.ToEntity(produto));
            }

            return entity;
        }
    }
}
