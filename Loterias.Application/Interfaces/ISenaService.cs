using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Loterias.Domain.Entities.Sena;
using System.Globalization;

namespace Loterias.Application.Interfaces
{
    public interface ISenaService
    {
        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns><see cref="ConcursoSena"/></returns>
        Task<ConcursoSena> GetById(int id);

        /// <summary>
        /// Gets the entity by date.
        /// </summary>
        /// <returns>The entity</returns>
        /// <param name="date">Date.</param>
        /// <exception cref="ArgumentNullException"></exception>
        Task<ConcursoSena> GetByDate(DateTime date);

        /// <summary>
        /// Gets all the entities between the specified dates.
        /// </summary>
        /// <param name="date1">Date 1.</param>
        /// <param name="date2">Date 2.</param>
        /// <returns>Entities</returns>
        /// <exception cref="ArgumentNullException" />
        Task<IEnumerable<ConcursoSena>> GetBetweenDates(DateTime date1, DateTime date2);

        /// <summary>
        /// Get the entities with the specified dates
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException" />
        Task<IEnumerable<ConcursoSena>> GetInDates(params DateTime[] dates);
        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DbException"></exception>
        /// <returns><see cref="ConcursoSena" />Returns the model</returns>
        Task<ConcursoSena> Add(ConcursoSena model);

        /// <summary>
        /// Updates an existing model
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="EntryPointNotFoundException" />
        /// <exception cref="DbUpdateException" />
        /// <exception cref="DbException" />
        /// <returns> <see cref="ConcursoSena" />Returns the model</returns>
        Task<ConcursoSena> Update(ConcursoSena model);
    }
}