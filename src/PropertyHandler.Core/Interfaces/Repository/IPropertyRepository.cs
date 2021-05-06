using PropertyHandler.Core.Entities;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces.Repository
{
    public interface IPropertyRepository: IRepository<Property>
    {
        Task<Property> GetWithFilters();
        Task<bool> UpdateAddressAndDetail(int addressId, int detailId, int propertyId);
    }
}
