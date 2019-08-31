using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;

namespace Loterias.Application.Interfaces
{
    /// <summary>
    /// Service interface responsible for mega-sena service methods.
    /// </summary>
    public interface ISenaService : ILoteriasService<ConcursoSena>
    {
        
    }
}