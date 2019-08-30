using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Linq;
using Loterias.Common.Exceptions;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Domain.Interfaces.Repositories;
using Loterias.Application.Interfaces;
using Loterias.Common.Enums;
using Loterias.Common.Utils;
using System.Globalization;

namespace Loterias.Application.Service
{
    /// <summary>
    /// Service that implemente <see cref="ILotofacilService"/> wich is responsible for all lotofacil related methods.
    /// </summary>
    public class LotofacilService : ILotofacilService
    {
        /// <summary>
        /// Repository for all lotofacil concursos.
        /// </summary>
        private readonly IRepositoryConcursoLotofacil _concursos;

        /// <summary>
        /// Repository for all lotofacil winners.
        /// </summary>
        private readonly IRepositoryGanhadoresLotofacil _ganhadores;

        /// <summary>
        /// Initalize a new service using <see cref="IRepositoryConcursoLotofacilo"/> and <see cref="IRepositoryGanhadoresLotofacil"/>.
        /// </summary>
        /// <param name="concursos">Repository to get the <see cref="ConcursoLotofacil"/> entities.</param>
        /// <param name="ganhadores">Repository to get the <see cref="GanhadoresLotofacil"/> entities.</param>
        public LotofacilService(IRepositoryConcursoLotofacil concursos, IRepositoryGanhadoresLotofacil ganhadores)
        {
            _concursos = concursos;
            _ganhadores = ganhadores;
        }

        public Task<ConcursoLotofacil> Add(ConcursoLotofacil model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConcursoLotofacil>> GetBetweenDates(string culture, string date1, string date2)
        {
            throw new NotImplementedException();
        }

        public Task<ConcursoLotofacil> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the entities within the sorted specified numbers
        /// </summary>
        /// <param name="numbers">Numbers (integer)</param>
        /// <returns>Returns <see cref="IEnumerable{ConcursoLotofacil}"/> entities that matches the number.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        public async Task<IEnumerable<ConcursoLotofacil>> GetByNumbers(params int[] numbers)
        {
            if (numbers?.Length == 0 || numbers == default(int[]))
                throw new ArgumentNullException(nameof(numbers) ,"At least one number must be specified.");

            if (numbers.Any(num => num <= 0))
                throw new ArgumentException("Numbers must be higher than zero.", nameof(numbers));

            return await _concursos.Where(where => numbers.All(value => where.ResultadoOrdenado.Contains(value)));
        }

        public Task<IEnumerable<ConcursoLotofacil>> GetByStateWinners(params string[] states)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConcursoLotofacil>> GetInDates(string culture, params string[] dates)
        {
            throw new NotImplementedException();
        }

        public Task<ConcursoLotofacil> Update(ConcursoLotofacil model)
        {
            throw new NotImplementedException();
        }
    }
}