using PropertyHandler.Core.Entities;
using PropertyHandler.Core.Helpers;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Repository;
using PropertyHandler.Core.Interfaces.Services;
using PropertyHandler.Core.ViewModels;
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

        public async Task<int> RegisterProperty(PropertyViewModel propertyViewModel)
        {
            var property = Mapper.PropertyMap(propertyViewModel);
            property.Detail = Mapper.DetailMap(propertyViewModel.Detalhe);
           
            property.Address = Mapper.AddressMap(propertyViewModel.Endereco);

            var insertedPropertyId = await _propertyRepository.Insert(property);

            property.Detail.PropertyId = insertedPropertyId;
            property.Address.PropertyId = insertedPropertyId;

            var insertedAddressId = await _addressRepository.Insert(property.Address);
            var insertedDetailId = await _detailsRepository.Insert(property.Detail);


            var updateIds = await _propertyRepository.UpdateAddressAndDetail(insertedAddressId, insertedDetailId, insertedPropertyId);

            return insertedPropertyId;
        }
    }
}
