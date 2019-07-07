using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Loterias.Data.Context;
using Loterias.Domain.Interfaces.Repositories;
using System.Data.Common;

namespace Loterias.Data.Repositories
{
    public abstract class RepositoryBase<IEntity> : IDisposable, IRepositoryBase<IEntity>
            where IEntity : class
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
        public async Task<IEnumerable<IEntity>> GetAll() => await _context.Set<IEntity>().ToListAsync();

        /// <summary>
        /// Search for a entity based on id
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="IEntity"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public async Task<IEntity> GetById(int id) 
        { 
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id cannot be zero or lower.");

            return await _context.Set<IEntity>().FindAsync(id);
        }

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbException"></exception>
        /// <exception cref="Exception"></exception>
        
        public async Task Add(IEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot add a null reference");

            try 
            {
                await _context.Set<IEntity>().AddAsync(model);
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
        public async Task Update(IEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot update a null reference");
            
            try 
            {
                _context.Set<IEntity>().Update(model);
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
        public async Task Remove(IEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Cannot remove a null object");
            
            try 
            {
                _context.Set<IEntity>().Remove(model);
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