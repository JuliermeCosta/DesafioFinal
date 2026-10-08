namespace DesafioFinal.Domain.Model.Common
{
    /// <summary>
    /// Classe abstrata com campos em comum entre os models
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// ID do model
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Data de inclusão do model
        /// </summary>
        public DateTime DataInclusao { get; set; }

        /// <summary>
        /// Data de atualização do model
        /// </summary>
        public DateTime? DataAtualizacao { get; set; }
    }
}
