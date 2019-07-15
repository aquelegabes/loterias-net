using System;
using System.Collections.Generic;
using System.Data.Common;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;
using Loterias.Application.Interfaces;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

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
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns><see cref="ConcursoSena"/></returns>
        public async Task<ConcursoSena> GetById(int id) => await _sena.GetById(id);

        /// <summary>
        /// Gets the by date.
        /// </summary>
        /// <returns>The by date.</returns>
        /// <param name="date">Date.</param>
        public async Task<ConcursoSena> GetByDate(DateTime date) => 
            await _sena.FirstOrDefault(f => f.Data.Date.Equals(date.Date));

        /// <summary>
        /// Get all the entities between the specified dates.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns>Entities</returns>
        public async Task<IEnumerable<ConcursoSena>> GetBetweenDates(DateTime date1, DateTime date2) =>
            await _sena.Where(w => w.Data.Date >= date1 && w.Data.Date <= date2);

        /// <summary>
        /// Get all the entities in the specified dates.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns>Entities</returns>
        public async Task<IEnumerable<ConcursoSena>> GetInDates(params DateTime[] dates)
            => await _sena.Where(w => dates.Any(a => a.Date.Equals(w.Data)));

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DbException"></exception>
        /// <returns><see cref="ConcursoSena" />Returns the model</returns>
        public async Task<ConcursoSena> Add(ConcursoSena model) => await _sena.Add(model);

        /// <summary>
        /// Updates an existing model
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EntryPointNotFoundException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbException"></exception>
        /// <returns> <see cref="ConcursoSena" />Returns the model</returns>
        public async Task<ConcursoSena> Update(ConcursoSena model) => await _sena.Update(model);
    }
}