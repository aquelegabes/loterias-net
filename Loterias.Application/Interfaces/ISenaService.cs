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
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{ConcursoSena,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="IEnumerable{ConcursoSena}"/></returns>
        Task<IEnumerable<ConcursoSena>> Where(Expression<Func<ConcursoSena, bool>> @where);

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{ConcursoSena,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="ConcursoSena"/></returns>
        Task<ConcursoSena> FirstOrDefault(Expression<Func<ConcursoSena,bool>> @where);

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
        /// <exception cref="FormatException"></exception>
        Task<ConcursoSena> GetByDate(DateTime date);

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