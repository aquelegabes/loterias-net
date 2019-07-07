using Loterias.Data.Context;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    public class RepositoryConcursoLotofacil : RepositoryBase<ConcursoLotofacil> , IRepositoryConcursoLotofacil
    {
        public RepositoryConcursoLotofacil (LoteriasContext context) : base (context) { }
    }
}