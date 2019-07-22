using Loterias.Domain.Interfaces.Repositories;
using Loterias.Domain.Entities.Sena;
using Loterias.Data.Context;

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="ConcursoSena"/>.
    /// </summary>
    public class RepositoryConcursoSena : RepositoryBase<ConcursoSena>, IRepositoryConcursoSena
    {
        /// <summary>
        /// Initiate a new instance of a <see cref="ConcursoSena"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="LoteriasContext"/> context.</param>
        public RepositoryConcursoSena(LoteriasContext context) : base(context) { }
    }
}