using Microsoft.AspNetCore.Http;
using PropertyHandler.Core.Entities;
using PropertyHandler.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<object>> GetProperties();

        Task<int> RegisterProperty(PropertyViewModel propertyViewModel, List<IFormFile> imagens);

        Task<object> GetProperty(int id);
    }
}
