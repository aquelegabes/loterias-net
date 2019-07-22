using Loterias.Data.Context;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="GanhadoresFacil"/>.
    /// </summary>
    public class RepositoryGanhadoresFacil : RepositoryBase<GanhadoresFacil>, IRepositoryGanhadoresFacil
    {
        /// <summary>
        /// Initiate a new instance of a <see cref="RepositoryGanhadoresFacil"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="LoteriasContext"/> context.</param>
        public RepositoryGanhadoresFacil (LoteriasContext context) : base (context) { }
    }
}