using Loterias.Data.Context;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="GanhadoresLotofacil"/>.
    /// </summary>
    public class RepositoryGanhadoresLotofacil : RepositoryBase<GanhadoresLotofacil>, IRepositoryGanhadoresLotofacil
    {
        /// <summary>
        /// Initiate a new instance of a <see cref="RepositoryGanhadoresLotofacil"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="LoteriasContext"/> context.</param>
        public RepositoryGanhadoresLotofacil (LoteriasContext context) : base (context) { }
    }
}