using System;
using System.Linq;
using System.Collections.Generic;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;
using Loterias.Application.Interfaces;
using System.Threading.Tasks;
using System.Linq.Expressions;

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
        /// Get a list of ConcursoSena with a func where clause
        /// </summary>
        /// <param name="@where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        public Task<IEnumerable<ConcursoSena>> Where(Expression<Func<ConcursoSena, bool>> @where)
            => _sena.Where(where);

        /// <summary>
        /// Find first model with a func where clause
        /// </summary>
        /// <param name="@where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        public Task<ConcursoSena> FirstOrDefault(Expression<Func<ConcursoSena, bool>> @where)
            => _sena.FirstOrDefault(where);

        /// <summary>
        /// Get a ConcursoSena by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Task<ConcursoSena> GetById(int id) => _sena.GetById(id);
    }
}