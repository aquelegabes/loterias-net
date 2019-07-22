using Loterias.Data.Context;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a repository for <see cref="ConcursoLotofacil"/>.
    /// </summary>
    public class RepositoryConcursoLotofacil : RepositoryBase<ConcursoLotofacil> , IRepositoryConcursoLotofacil
    {
        /// <summary>
        /// Initiate a new instance of a <see cref="RepositoryConcursoLotofacil"/> repository.
        /// </summary>
        /// <param name="context">A valid <see cref="LoteriasContext"/> context.</param>
        public RepositoryConcursoLotofacil (LoteriasContext context) : base (context) { }
    }
}