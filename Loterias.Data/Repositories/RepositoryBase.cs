using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Loterias.Data.Context;
using Loterias.Domain.Interfaces.Repositories;
using System.Data.Common;
using System.Linq.Expressions;
using System.Linq;

namespace Loterias.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity>
            where TEntity : class
    {
        private readonly LoteriasContext _context;

        public RepositoryBase(LoteriasContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <typeparam name="IEntity"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAll() => await _context.Set<TEntity>().ToListAsync();

        /// <summary>
        /// Returns the first entity that satisfies the condition or default value if no such is found.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where));
            try
            {
                return await _context.Set<TEntity>().FirstOrDefaultAsync(where);
            }
            catch (DbException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where));

            try
            {
                return await _context.Set<TEntity>().Where(where).ToListAsync();
            }
            catch (DbException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="IEntity"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public async Task<TEntity> GetById(int id) 
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id cannot be zero or lower.");

            return await _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        
        public async Task Add(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot add a null reference");

            try 
            {
                await _context.Set<TEntity>().AddAsync(model);
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task Update(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot update a null reference");
            
            try 
            {
                _context.Set<TEntity>().Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task Remove(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot remove a null object");
            
            try 
            {
                _context.Set<TEntity>().Remove(model);
                await _context.SaveChangesAsync();
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Dispose()
        {
        }
    }
}