using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loterias.Domain.Entities.Sena;
using Loterias.Common.Exceptions;

namespace Loterias.Application.Interfaces
{
    /// <summary>
    /// Service interface responsible for all mega-sena related methods.
    /// </summary>
    public interface ISenaService
    {
        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id">Id (integer)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The <see cref="ConcursoSena"/> entity.</returns>
        Task<ConcursoSena> GetById(int id);

        /// <summary>
        /// Gets the entity by date.
        /// </summary>
        /// <returns>The entity <see cref="ConcursoSena"/></returns>
        /// <param name="date">Date.</param>
        /// <exception cref="ArgumentNullException"></exception>
        Task<ConcursoSena> GetByDate(DateTime date);

        /// <summary>
        /// Gets all the entities between the specified dates.
        /// </summary>
        /// <param name="date1">Date 1.</param>
        /// <param name="date2">Date 2.</param>
        /// <returns>Returns the <see cref="ConcursoSena"/> entities</returns>
        /// <exception cref="ArgumentNullException" />
        Task<IEnumerable<ConcursoSena>> GetBetweenDates(DateTime date1, DateTime date2);

        /// <summary>
        /// Get the entities in the specified dates
        /// </summary>
        /// <param name="dates"></param>
        /// <returns><see cref="ConcursoSena"/> Entities that matches the dates</returns>
        /// <exception cref="ArgumentNullException" />
        Task<IEnumerable<ConcursoSena>> GetInDates(params DateTime[] dates);

        /// <summary>
        /// Get all the entities within the sorted specified numbers
        /// </summary>
        /// <param name="numbers">Numbers (integer)</param>
        /// <returns><see cref="ConcursoSena"/> Entities that matches the number</returns>
        /// <exception cref="ArgumentNullException" />
        Task<IEnumerable<ConcursoSena>> GetByNumbers(params int[] numbers);

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model">A valid <see cref="ConcursoSena"/> model</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="DuplicateKeyException"></exception>
        /// <returns><see cref="ConcursoSena" />Returns the model</returns>
        Task<ConcursoSena> Add(ConcursoSena model);

        /// <summary>
        /// Updates an existing model
        /// </summary>
        /// <param name="model">A valid <see cref="ConcursoSena"/> model</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="EntryPointNotFoundException" />
        /// <exception cref="DbException" />
        /// <exception cref="DbException" />
        /// <returns>Returns the updated <see cref="ConcursoSena" /> model</returns>
        Task<ConcursoSena> Update(ConcursoSena model);
    }
}