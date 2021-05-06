using PropertyHandler.Core.Interfaces.Entities;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces.Repository
{
    public interface IComposeRepository<TEntity> where TEntity : IPropertyCompose
    {
        Task<TEntity> GetPerPropetyId(int propertyId);
    }
}
