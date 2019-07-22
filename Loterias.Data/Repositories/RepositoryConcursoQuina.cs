using Loterias.Data.Context;
using Loterias.Domain.Entities.Quina;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="ConcursoQuina"/>.
    /// </summary>
    public class RepositoryConcursoQuina : RepositoryBase<ConcursoQuina>, IRepositoryConcursoQuina
    {
        /// <summary>
        /// Initiate a new instance of <see cref="RepositoryConcursoQuina"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="LoteriasContext"/> context.</param>
        public RepositoryConcursoQuina (LoteriasContext context) : base (context) { }
    }
}