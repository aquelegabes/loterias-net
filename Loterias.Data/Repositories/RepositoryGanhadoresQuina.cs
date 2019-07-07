using Loterias.Data.Context;
using Loterias.Domain.Entities.Quina;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    public class RepositoryGanhadoresQuina : RepositoryBase<GanhadoresQuina>, IRepositoryGanhadoresQuina
    {
        public RepositoryGanhadoresQuina (LoteriasContext context) : base (context) { }
    }
}