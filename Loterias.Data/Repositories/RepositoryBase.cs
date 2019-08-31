using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Loterias.Data.Context;
using Loterias.Domain.Interfaces.Repositories;
using System.Data.Common;
using System.Linq.Expressions;
using System.Linq;

#pragma warning disable RCS1090

namespace Loterias.Data.Repositories
{
    /// <summary>
    /// Serves as a base repository for all database models
    /// </summary>
    /// <typeparam name="TEntity">An existing model/class</typeparam>
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity>
            where TEntity : class
    {
        /// <summary>
        /// Protected access to the context
        /// </summary>
        protected readonly LoteriasContext _context;

        /// <summary>
        /// Protected constructor for the base repository using a valid context
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected RepositoryBase(LoteriasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Cannot initialiaze a repository with a null context");
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Returns a <see cref="IEnumerable{TEntity}"/></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().ToListAsync();

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"><see cref="Expression{Func{TEntity,bool}}" /></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="TEntity"/></returns>
        public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where), "Predicate cannot be null.");
            try
            {
                return await _context.Set<TEntity>().FirstOrDefaultAsync(where);
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (ArgumentException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where">A valid <see cref="Expression{Func{TEntity,bool}}" /> predicate.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="IEnumerable{TEntity}"/></returns>
        public virtual async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where), "Predicate cannot be null.");

            try
            {
                return await _context.Set<TEntity>().Where(where).ToListAsync();
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (ArgumentException ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { where };
                throw;
            }
        }

        /// <summary>
        /// Returns the number of elements in a sequence that satisfy a condition.
        /// </summary>
        /// <param name="where">A valid <see cref="Expression{Func{TEntity,bool}}" /> predicate.</param>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        /// <exception cref="OverflowException">The number of elements in source is larger than <see cref="Int32.MaxValue" />.</exception>
        /// <returns><see cref="Int32" /> The number of elements in the input sequence.</returns>
        public async Task<int> Count(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(
                    message: "Predicate cannot be null.",
                    paramName: nameof(where));
            
            try 
            {
                return await _context.Set<TEntity>().CountAsync(where);
            }
            catch (ArgumentNullException ex)
            {
                ex.Data["params"] = new List<object> {where};
                throw;
            }
            catch (OverflowException ex)
            {
                ex.Data["params"] = new List<object> {where};
                throw;
            }
        }

        /// <summary>
        /// Search for a entity based on an id.
        /// </summary>
        /// <param name="id">(integer)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception" />
        /// <returns>Returns a <see cref="TEntity"/></returns>
        public virtual async Task<TEntity> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be zero or lower.", nameof(id));

            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> {id};
                throw;
            }
        }

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"><see cref="TEntity"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns><see cref="bool"/>Returns the model</returns>
        public virtual async Task<TEntity> Add(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot add a null reference");

            try
            {
                await _context.Set<TEntity>().AddAsync(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> { model };
                throw;
            }
        }

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
        public virtual async Task<TEntity> Update(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(
                    message: "Cannot update a null reference",
                    paramName: nameof(model));

            try
            {
                _context.Set<TEntity>().Update(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (EntryPointNotFoundException ex)
            {
                ex.Data["params"] = new List<object> {model};
                throw;
            }
            catch (DbUpdateException ex)
            {
                ex.Data["params"] = new List<object> {model};
                throw;
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> {model};
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> {model};
                throw;
            }
        }

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="model">A valid <see cref="TEntity"/> model.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns>Returns a <see cref="bool"/> true if removed, false if not</returns>
        public virtual async Task<bool> Remove(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(
                    message: "Cannot remove a null object",
                    paramName: nameof(model));

            try
            {
                _context.Set<TEntity>().Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                ex.Data["params"] = new List<object> {model};
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["params"] = new List<object> {model};
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}