using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Common;

namespace Loterias.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface to serve as a base repository for all database models
    /// </summary>
    /// <typeparam name="TEntity">An existing model/class</typeparam>
    public interface IRepositoryBase<TEntity>
            where TEntity : class
    {
        /// <summary>
        /// Search for a entity based on an id.
        /// </summary>
        /// <param name="id">(integer)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>Returns a <see cref="TEntity"/></returns>
        Task<TEntity> GetById(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Returns a <see cref="IEnumerable{TEntity}"/></returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="TEntity"/></returns>
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> @where);

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where">A valid <see cref="Expression{Func{TEntity,bool}}" /> predica.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="IEnumerable{TEntity}"/></returns>
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> @where);

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns the added <see cref="TEntity"/> model.</returns>
        Task<TEntity> Add(TEntity model);

        /// <summary>
        /// Update the entity.
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EntryPointNotFoundException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns the updated <see cref="TEntity"/> entity.</returns>
        Task<TEntity> Update(TEntity model);

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="model">A valid <see cref="TEntity"/> model.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="bool"/> true if removed, false if not</returns>
        Task<bool> Remove(TEntity model);

        /// <summary>
        /// Releases all resource used by the <see cref="RepositoryBase"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="RepositoryBase"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="RepositoryBase"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="RepositoryBase"/> so the garbage collector can reclaim the memory
        /// that the <see cref="RepositoryBase"/> was occupying.</remarks>
        void Dispose();
    }
}