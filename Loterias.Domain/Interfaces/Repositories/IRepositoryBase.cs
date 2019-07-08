using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loterias.Domain.Interfaces.Repositories 
{
    public interface IRepositoryBase<TEntity>
            where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Add(TEntity model);
        Task Update(TEntity model);
        Task Remove(TEntity model);
        void Dispose();
    }
}