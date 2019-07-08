using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Loterias.Domain.Interfaces.Repositories 
{
    public interface IRepositoryBase<TEntity>
            where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> @where);
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> @where);
        Task Add(TEntity model);
        Task Update(TEntity model);
        Task Remove(TEntity model);
        void Dispose();
    }
}