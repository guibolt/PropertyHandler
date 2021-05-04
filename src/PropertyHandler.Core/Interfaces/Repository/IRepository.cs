using PropertyHandler.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<bool> Insert(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetPerId(int id);
    }
}
