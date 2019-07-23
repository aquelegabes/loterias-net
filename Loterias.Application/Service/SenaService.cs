using System;
using System.Collections.Generic;
using System.Data.Common;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;
using Loterias.Application.Interfaces;
using System.Threading.Tasks;
using System.Linq;

#pragma warning disable RCS1090

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
        public async Task<ConcursoSena> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be zero or lower.", nameof(id));

            try {
                return await _sena.GetById(id);
            }
            catch (EntryPointNotFoundException)
            {
                throw;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        } 

        /// <summary>
        /// Gets the entity by date.
        /// </summary>
        /// <returns>The entity by date.</returns>
        /// <param name="date">Date.</param>
        /// <exception cref="ArgumentNullException"/>
        public async Task<ConcursoSena> GetByDate(DateTime date)
        {
            if (date == null || date == default(DateTime))
                throw new ArgumentNullException(nameof(date), "Date cannot be null");

            try 
            {
                return await _sena.FirstOrDefault(f => f.Data.Date.Equals(date.Date));
            }
            catch (DbException)
            {
                throw;
            }
        }
            

        /// <summary>
        /// Get all the entities between the specified dates.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns>Entities</returns>
        /// <exception cref="ArgumentNullException"/>
        public async Task<IEnumerable<ConcursoSena>> GetBetweenDates(DateTime date1, DateTime date2)
        {
            if (date1 == null || date1 == default(DateTime))
                throw new ArgumentNullException(nameof(date1), "Date1 cannot be null");
            
            if (date2 == null || date2 == default(DateTime))
                throw new ArgumentNullException(nameof(date2), "Date2 cannot be null");

            try
            {
                return await _sena.Where(w => w.Data.Date >= date1 && w.Data.Date <= date2);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get the entities in the specified dates
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public async Task<IEnumerable<ConcursoSena>> GetInDates(params DateTime[] dates)
        {
            if (dates.Any(a => a == default(DateTime)))
                throw new ArgumentNullException("Specified dates cannot be null");

            try
            {
                return await _sena.Where(w => dates.Any(a => a.Date.Equals(w.Data)));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get all the entities within the sorted specified numbers
        /// </summary>
        /// <param name="numbers">Numbers</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        public async Task<IEnumerable<ConcursoSena>> GetByNumbers(params int[] numbers)
        {
            if (numbers?.Any() == false || numbers == default(int[]))
                throw new ArgumentNullException(nameof(numbers), "At least one number must be specified.");
            
            if (numbers.Any(a => a.Equals(0)))
                throw new ArgumentException("Winners numbers cannot contain zero as value.", nameof(numbers));

            try 
            {
                return await _sena.Where(where => numbers.All(value => where.ResultadoOrdenado.Contains(value)));            
            }
            catch (Exception)
            {
                throw;
            }
        } 

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DbException"></exception>
        /// <returns><see cref="ConcursoSena" />Returns the model</returns>
        public async Task<ConcursoSena> Add(ConcursoSena model)
        { 
            if (model == null || model == default(ConcursoSena))
                throw new ArgumentNullException(nameof(model), "Cannot add a null reference.");

            try
            {
                return await _sena.Add(model);
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updates an existing model
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EntryPointNotFoundException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbException"></exception>
        /// <returns> <see cref="ConcursoSena" />Returns the model</returns>
        public async Task<ConcursoSena> Update(ConcursoSena model)
        {
            if (model == null || model == default(ConcursoSena))
                throw new ArgumentNullException(nameof(model), "Cannot update a null reference.");
            
            var findModel = await this.GetById(model.Id);

            if (findModel == null || findModel == default(ConcursoSena))
                throw new EntryPointNotFoundException("Could not find a model to update with specified Id.");

            try
            {
                return await _sena.Update(model);
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}