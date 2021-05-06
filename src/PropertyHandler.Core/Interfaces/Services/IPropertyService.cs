using PropertyHandler.Core.Entities;
using PropertyHandler.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetProperties();

        Task<int> RegisterProperty(PropertyViewModel propertyViewModel);
    }
}
