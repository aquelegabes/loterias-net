using Loterias.Data.Context;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="GanhadoresSena"/>.
    /// </summary>
    public class RepositoryGanhadoresSena : RepositoryBase<GanhadoresSena>, IRepositoryGanhadoresSena
    {
        /// <summary>
        /// Initiate a new instance of a <see cref="RepositoryGanhadoresSena"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="LoteriasContext"/> context.</param>
        public RepositoryGanhadoresSena (LoteriasContext context) : base (context) { }
    }
}