using Loterias.Data.Context;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    public class RepositoryGanhadoresFacil : RepositoryBase<GanhadoresFacil>, IRepositoryGanhadoresFacil
    {
        public RepositoryGanhadoresFacil (LoteriasContext context) : base (context) { }
    }
}