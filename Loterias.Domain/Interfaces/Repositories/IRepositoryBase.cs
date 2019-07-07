using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loterias.Domain.Interfaces.Repositories 
{
    public interface IRepositoryBase<IEntity>
            where IEntity : class
    {
        Task<IEntity> GetById(int id);
        Task<IEnumerable<IEntity>> GetAll();
        Task Add(IEntity model);
        Task Update(IEntity model);
        Task Remove(IEntity model);
        void Dispose();
    }
}