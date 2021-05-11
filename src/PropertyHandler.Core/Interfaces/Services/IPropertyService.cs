using PropertyHandler.Core.Entities;
using PropertyHandler.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<object>> GetProperties();

        Task<int> RegisterProperty(PropertyViewModel propertyViewModel);

        Task<PropertyViewModel> GetProperty(int id);
    }
}
