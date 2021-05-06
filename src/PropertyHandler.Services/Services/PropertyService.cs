using PropertyHandler.Core.Entities;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Repository;
using PropertyHandler.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHandler.Services.Services
{
    public class PropertyService : BaseService, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IDetailRepository _detailsRepository;

        public PropertyService(IPropertyRepository propertyRepository, IAddressRepository addressRepository,
            IDetailRepository detailsRepository, INotifier notifier) : base(notifier)
        {
            _propertyRepository = propertyRepository;
            _addressRepository = addressRepository;
            _detailsRepository = detailsRepository;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            var lstProperties =  await _propertyRepository.GetAll();

            foreach (var property in lstProperties)
            {
                property.Detail = await _detailsRepository.GetPerPropetyId(property.Id);
                property.Address = await _addressRepository.GetPerPropetyId(property.Id); 
               // property.Images = 
            }

            return lstProperties;
        }
    }
}
