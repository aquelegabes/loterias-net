using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loterias.Common.Exceptions;

namespace Loterias.Application.Interfaces
{
    /// <summary>
    /// Service interface responsible for all lottery service methods.
    /// </summary>
    public interface ILoteriasService <T> 
        where T : class
    {
        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id">Id (integer)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The <see cref="T"/> entity.</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Gets all the entities between the specified dates.
        /// </summary>
        /// <param name="culture">A valid culture info example: "en-US".</param>
        /// <param name="date1">Date 1.</param>
        /// <param name="date2">Date 2.</param>
        /// <returns>Returns the <see cref="T"/> entities.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="System.Globalization.CultureNotFoundException" />
        /// <exception cref="FormatException" />
        Task<IEnumerable<T>> GetBetweenDates(string culture, string date1, string date2);

        /// <summary>
        /// Get the entities in the specified dates
        /// </summary>
        /// <param name="culture">A valid culture info example: "en-US".</param>
        /// <param name="dates">A valid list of dates.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> entities that matches the dates.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="System.Globalization.CultureNotFoundException" />
        /// <exception cref="FormatException" />
        Task<IEnumerable<T>> GetInDates(string culture, params string[] dates);

        /// <summary>
        /// Get all the entities within the sorted specified numbers
        /// </summary>
        /// <param name="numbers">Numbers (integer)</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> entities that matches the number.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        Task<IEnumerable<T>> GetByNumbers(params int[] numbers);

        /// <summary>
        /// Get all the entities where winners must be on the specified states.
        /// </summary>
        /// <param name="states">States (two characters)</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> entities that matches the states.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        Task<IEnumerable<T>> GetByStateWinners(params string[] states);

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model">A valid <see cref="T"/> model.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="DuplicateKeyException"></exception>
        /// <returns>Return the <see cref="T" /> added model.</returns>
        Task<T> Add(T model);

        /// <summary>
        /// Updates an existing model
        /// </summary>
        /// <param name="id">A valid <see cref="int"> id.</param>
        /// <param name="model">A valid <see cref="T"/> model.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="EntryPointNotFoundException" />
        /// <exception cref="DbException" />
        /// <exception cref="DbException" />
        /// <returns>Returns the <see cref="T" /> updated model.</returns>
        Task<T> Update(int id, T model);
    }
}