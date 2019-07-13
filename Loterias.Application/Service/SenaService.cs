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
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="IEnumerable{TEntity}"/></returns>
        public async Task<IEnumerable<ConcursoSena>> Where(Expression<Func<ConcursoSena, bool>> @where)
            => await _sena.Where(where);

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="TEntity"/></returns>
        public async Task<ConcursoSena> FirstOrDefault(Expression<Func<ConcursoSena, bool>> @where)
            => await _sena.FirstOrDefault(where);

        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns><see cref="TEntity"/></returns>
        public async Task<ConcursoSena> GetById(int id) => await _sena.GetById(id);

        /// <summary>
        /// Add a new model to the database
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DbException"></exception>
        /// <returns><see cref="ConcursoSena" />Returns the model</returns>
        public async Task<ConcursoSena> Add(ConcursoSena model) => await _sena.Add(model);
    }
}