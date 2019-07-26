using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Linq;
using Loterias.Common.Exceptions;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Interfaces.Repositories;
using Loterias.Application.Interfaces;
using Loterias.Common.Enums;
using Loterias.Common.Utils;
using System.Globalization;

#pragma warning disable RCS1090

namespace Loterias.Application.Service
{
    /// <summary>
    /// Service that implemente <see cref="ISenaService"/> wich is responsible for all mega-sena related methods.
    /// </summary>
    public class SenaService : ISenaService
    {
        private readonly IRepositoryConcursoSena _sena;
        private readonly IRepositoryGanhadoresSena _ganhadoresSena;

        /// <summary>
        /// Initalize a new service using <see cref="IRepositoryConcursoSena"/> and <see cref="IRepositoryGanhadoresSena"/>.
        /// </summary>
        /// <param name="sena">Repository to get the <see cref="ConcursoSena"/> entities.</param>
        /// <param name="ganhadoresSena">Repository to get the <see cref="GanhadoresSena"/> entities.</param>
        public SenaService(IRepositoryConcursoSena sena, IRepositoryGanhadoresSena ganhadoresSena)
        {
            _sena = sena;
            _ganhadoresSena = ganhadoresSena;
        }

        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id">Id (integer)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>Returns the <see cref="ConcursoSena"/> entity.</returns>
        public async Task<ConcursoSena> GetById(int id) => await _sena.GetById(id);

        /// <summary>
        /// Gets all the entities between the specified dates.
        /// </summary>
        /// <param name="date1">Date 1.</param>
        /// <param name="date2">Date 2.</param>
        /// <returns>Entities</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="CultureNotFoundException" />
        /// <exception cref="FormatException" />
        public async Task<IEnumerable<ConcursoSena>> GetBetweenDates(string culture, string date1, string date2)
        {
            if (string.IsNullOrWhiteSpace(culture) || string.IsNullOrWhiteSpace(date1) || string.IsNullOrWhiteSpace(date2))
            {
                throw new ArgumentNullException(
                    paramName: $"Parameters: {nameof(culture)}, {nameof(date1)}, {nameof(date2)}.",
                    message: "All parameters are required."
                );
            }

            if (!Utils.IsValidCulture(culture))
                throw new CultureNotFoundException("Wrong culture info specified, check https://lonewolfonline.net/list-net-culture-country-codes/ for a list containing all culture infos");

            var ci = CultureInfo.GetCultureInfo(culture);

            try
            {
                DateTime dateSearch1 = Convert.ToDateTime(date1, ci);
                DateTime dateSearch2 = Convert.ToDateTime(date2, ci);
                return await _sena.Where(w => w.Data.Date >= dateSearch1 && w.Data.Date <= dateSearch2);
            }
            catch (FormatException ex)
            {
                ex.Data["dates"] = new string[] { date1, date2 };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["param1"] = culture;
                ex.Data["param2"] = date1;
                ex.Data["param3"] = date2;
                throw;
            }
        }

        /// <summary>
        /// Get the entities in the specified dates
        /// </summary>
        /// <param name="dates"></param>
        /// <returns><see cref="ConcursoSena"/> Entities that matches the dates</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="CultureNotFoundException" />
        /// <exception cref="FormatException" />
        public async Task<IEnumerable<ConcursoSena>> GetInDates(string culture, params string[] dates)
        {
            if (dates.Length == 0 && dates.Any(value => !string.IsNullOrWhiteSpace(value)))
                throw new ArgumentNullException(nameof(dates) ,"At least one date must be specified.");

            if (string.IsNullOrWhiteSpace(culture))
                throw new ArgumentNullException(nameof(culture), "Culture type is required.");

            if (!Utils.IsValidCulture(culture))
                throw new CultureNotFoundException("Wrong culture info specified, check https://lonewolfonline.net/list-net-culture-country-codes/ for a list containing all culture infos");

            var ci = CultureInfo.GetCultureInfo(culture);

            try
            {
                var listDates = dates.Select(value => Convert.ToDateTime(value, ci)).ToArray();

                return await _sena.Where(w => listDates.Any(a => a.Date.Equals(w.Data)));
            }
            catch (FormatException ex)
            {
                ex.Data["dates"] = dates;
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["param1"] = culture;
                ex.Data["param2"] = dates;
                throw;
            }
        }

        /// <summary>
        /// Get all the entities within the sorted specified numbers
        /// </summary>
        /// <param name="numbers">Numbers (integer)</param>
        /// <returns><see cref="ConcursoSena"/> Entities that matches the number</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        public async Task<IEnumerable<ConcursoSena>> GetByNumbers(int[] numbers)
        {
            if (numbers?.Length == 0 || numbers == default(int[]))
                throw new ArgumentNullException(nameof(numbers) ,"At least one number must be specified.");

            if (numbers.Any(num => num <= 0))
                throw new ArgumentException("Numbers must be higher than zero.", nameof(numbers));

            return await _sena.Where(where => numbers.All(value => where.ResultadoOrdenado.Contains(value)));
        }

        /// <summary>
        /// Get all the entities where winners must be on the specified states.
        /// </summary>
        /// <param name="states">States (two characters)</param>
        /// <returns>Returns <see cref="IEnumerable{T}" /> entities that matches the states.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<ConcursoSena>> GetByStateWinners(params string[] states)
        {
            if (states?.Count() == 0 || default(string[]) == states)
                throw new ArgumentNullException(nameof(states), "At least one state must be specified.");

            var statesList = states.ToList();
            statesList.RemoveAll(r => string.IsNullOrWhiteSpace(r));

            if (statesList.Count == 0)
                throw new ArgumentException("States cannot have an empty value as parameter request.");

            statesList.ForEach(item =>
            {
                bool matches = Enum.GetNames(typeof(Estados))
                                .Any(value => value.Equals(item, StringComparison.OrdinalIgnoreCase));
                if (!matches)
                    throw new ArgumentException("Must be a valid two character state. See https://www.sogeografia.com.br/Conteudos/Estados/ for a list containing all states.");
            });

            // is it possible to optimize?
            return await _sena
                .Where(w =>
                    statesList.Any(state =>
                        w.GanhadoresModel.Any(winn =>
                            winn.EstadoUF.Equals(state, StringComparison.OrdinalIgnoreCase))));
        }

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model">A valid <see cref="ConcursoSena"/> model</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="DuplicateKeyException"></exception>
        /// <returns>Returns the <see cref="ConcursoSena" /> model</returns>
        public async Task<ConcursoSena> Add(ConcursoSena model)
        {
            var existingModel = await _sena.FirstOrDefault(f => f.Concurso.Equals(model.Concurso));

            if (existingModel != null)
                throw new DuplicateKeyException(existingModel, "Same concurso number already added to the database.");

            return await _sena.Add(model);
        }

        /// <summary>
        /// Updates an existing model
        /// </summary>
        /// <param name="model">A valid <see cref="ConcursoSena"/> model</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="EntryPointNotFoundException" />
        /// <exception cref="DbException" />
        /// <exception cref="DbException" />
        /// <returns>Returns the updated <see cref="ConcursoSena" /> model</returns>
        public async Task<ConcursoSena> Update(ConcursoSena model) => await _sena.Update(model);
    }
}