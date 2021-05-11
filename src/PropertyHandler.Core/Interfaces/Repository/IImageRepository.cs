using PropertyHandler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHandler.Core.Interfaces.Repository
{
    public interface IImageRepository : IRepository<PropertyImage>
    {
        Task<IEnumerable<PropertyImage>> GetAllPerPropetyId(int propertyId);
        Task<PropertyImage> GetPerFileId(Guid fileId);
    }
}
