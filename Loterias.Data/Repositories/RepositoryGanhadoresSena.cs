using Loterias.Data.Context;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Data.Repositories
{
    public class RepositoryGanhadoresSena : RepositoryBase<GanhadoresSena>, IRepositoryGanhadoresSena
    {
        public RepositoryGanhadoresSena (LoteriasContext context) : base (context) { }
    }
}