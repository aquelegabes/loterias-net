using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Loterias.Domain.Entities.Sena;

namespace Loterias.Application.Interfaces
{
    public interface ISenaService
    {
        Task<IEnumerable<ConcursoSena>> GetWhen(Expression<Func<ConcursoSena, bool>> @where);
        Task<ConcursoSena> FindWhen(Expression<Func<ConcursoSena,bool>> @where);
        Task<ConcursoSena> GetById(int id);
    }
}