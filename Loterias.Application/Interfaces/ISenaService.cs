using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Loterias.Domain.Entities.Sena;

namespace Loterias.Application.Interfaces
{
    public interface ISenaService
    {
         /// <summary>
        /// Get a list of ConcursoSena with a func where clause
        /// </summary>
        /// <param name="@where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        Task<IEnumerable<ConcursoSena>> Where(Expression<Func<ConcursoSena, bool>> @where);
        /// <summary>
        /// Find first model with a func where clause
        /// </summary>
        /// <param name="@where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        Task<ConcursoSena> FirstOrDefault(Expression<Func<ConcursoSena,bool>> @where);
        /// <summary>
        /// Get a ConcursoSena by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<ConcursoSena> GetById(int id);
    }
}