using Loterias.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loterias.Common.Exceptions;

namespace Loterias.Application.Interfaces
{
    /// <summary>
    /// A service interface responsible for getting concursos directly com caixa website
    /// </summary>
    public interface IConcursoService
    {
        /// <summary>
        /// Gets a concurso sena directly from caixa website
        /// </summary>
        /// <param name="concursoNumber">Concurso number</param>
        /// <returns>Returns the concurso</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<ConcursoSenaVm> GetSenaByConcursoNumber(int concursoNumber);

        /// <summary>
        /// Gets a concurso sena directly from caixa website
        /// </summary>
        /// <param name="concursoNumber">Concurso number</param>
        /// <returns>Returns the concurso</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<ConcursoLotofacilVm> GetLotofacilByConcursoNumber(int concursoNumber);

        /// <summary>
        /// Gets a concurso sena directly from caixa website
        /// </summary>
        /// <param name="concursoNumber">Concurso number</param>
        /// <returns>Returns the concurso</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        Task<ConcursoQuinaVm> GetQuinaByConcursoNumber(int concursoNumber);

        /// <summary>
        /// Populate the database on specified model
        /// </summary>
        /// <typeparam name="TModel">
        /// Where TModel must be one of these:
        /// <see cref="ConcursoQuinaVm"/>
        /// <see cref="ConcursoSenaVm"/>
        /// <see cref="ConcursoLotofacilVm"/>
        /// </typeparam>
        /// <param name="startingAt">Starting by concurso number</param>
        /// <param name="once">Populate once</param>
        /// <param name="checkNew">Check for new updates</param>
        /// <returns>No returns</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="DuplicateKeyException"></exception>
        Task<TModel> PopulateModel<TModel>(int startingAt = 1, bool once = false, bool checkNew = false);
    }
}
