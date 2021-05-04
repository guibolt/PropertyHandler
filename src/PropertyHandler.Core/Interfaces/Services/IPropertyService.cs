using PropertyHandler.Core.Entities;
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
    }
}
