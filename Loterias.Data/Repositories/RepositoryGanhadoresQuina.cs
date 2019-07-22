using Loterias.Data.Context;
using Loterias.Domain.Entities.Quina;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="GanhadoresQuina"/>.
    /// </summary>
    public class RepositoryGanhadoresQuina : RepositoryBase<GanhadoresQuina>, IRepositoryGanhadoresQuina
    {
        /// <summary>
        /// Initiate a new instance of a <see cref="RepositoryGanhadoresQuina"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="LoteriasContext"/> context.</param>
        public RepositoryGanhadoresQuina (LoteriasContext context) : base (context) { }
    }
}