using System;
using System.Linq;
using System.Collections.Generic;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;
using Loterias.Application.Interfaces;
using System.Threading.Tasks;

namespace Loterias.Application.Service
{
    public class SenaService : ISenaService
    {
        private readonly IRepositoryConcursoSena _sena;
        private readonly IRepositoryGanhadoresSena _ganhadoresSena;

        public SenaService(IRepositoryConcursoSena sena, IRepositoryGanhadoresSena ganhadoresSena)
        {
            _sena = sena;
            _ganhadoresSena = ganhadoresSena;
        }

        /// <summary>
        /// Get a list of ConcursoSena with a func clause
        /// </summary>
        /// <param name="@where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IEnumerable<ConcursoSena>> GetWhen(Func<ConcursoSena, bool> @where)
        {
            if (@where == null)
                throw new ArgumentNullException(nameof(@where), "Where clause cannot be null");
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find first model with a func model
        /// </summary>
        /// <param name="@where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<ConcursoSena> FindWhen(Func<ConcursoSena, bool> @where)
        {
            if (@where == null)
                throw new ArgumentNullException(nameof(@where), "Where clause cannot be null");
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a ConcursoSena by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ConcursoSena> GetById(int id) => _sena.GetById(id);
    }
}