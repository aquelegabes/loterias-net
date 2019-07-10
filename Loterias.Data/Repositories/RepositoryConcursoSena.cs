using Loterias.Domain.Interfaces.Repositories;
using Loterias.Domain.Entities.Sena;
using Loterias.Data.Context;

namespace Loterias.Data.Repositories
{
    public class RepositoryConcursoSena : RepositoryBase<ConcursoSena>, IRepositoryConcursoSena
    {
        public RepositoryConcursoSena(LoteriasContext context) : base(context) { }
    }
}