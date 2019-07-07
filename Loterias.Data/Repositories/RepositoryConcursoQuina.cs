using Loterias.Data.Context;
using Loterias.Domain.Entities.Quina;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    public class RepositoryConcursoQuina : RepositoryBase<ConcursoQuina>, IRepositoryConcursoQuina
    {
        public RepositoryConcursoQuina (LoteriasContext context) : base (context) { }
    }
}