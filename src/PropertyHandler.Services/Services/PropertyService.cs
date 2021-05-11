using PropertyHandler.Core.Entities;
using PropertyHandler.Core.Enums;
using PropertyHandler.Core.Helpers;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Repository;
using PropertyHandler.Core.Interfaces.Services;
using PropertyHandler.Core.Notifications;
using PropertyHandler.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<object>> GetProperties()
        {
            var lstProperties = await _propertyRepository.GetAll();

            var lstPropertyviewModels = from property in lstProperties
                                        select new
                                        {
                                            property.Id,
                                            Codigo = property.Code,
                                            ValorCondominio = property.CondominiumPrice,
                                            ValorAluguel = property.RentPrice,
                                            Descricao = property.Description,
                                            NomeProprietario = property.OwnerName,
                                            ValorIPTU = property.TaxPrice,
                                            Destaque = property.IsSpotlight,
                                            Tipo = (int)property.Type,
                                            Titulo = property.Title,
                                            ValorVenda = property.SalePrice,
                                            TipoEspecificoPropriedade = property.SpecificType,  
                                        };

            return lstPropertyviewModels;
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
            await _propertyRepository.UpdateAddressAndDetail(insertedAddressId, insertedDetailId, insertedPropertyId);

            return insertedPropertyId;
        }

        public async Task<PropertyViewModel> GetProperty(int id)
        {
            var property = await _propertyRepository.GetPerId(id);

            if (property == null)
            {
                _notifier.Handle(new Notification("Registro não encontrado.", ETypeError.NotFound));
                return null;
            }

            property.Address = await _addressRepository.GetPerPropetyId(id);
            property.Detail = await _detailsRepository.GetPerPropetyId(id);

            PropertyViewModel propertyViewModel = new()
            {
                Codigo = property.Code,
                ValorCondominio = property.CondominiumPrice,
                ValorAluguel = property.RentPrice,
                Descricao = property.Description,
                NomeProprietario = property.OwnerName,
                ValorIPTU = property.TaxPrice,
                Destaque = property.IsSpotlight,
                Tipo = (int)property.Type,
                Titulo = property.Title,
                ValorVenda = property.SalePrice,
                TipoEspecificoPropriedade = property.SpecificType,
                Detalhe = new DetailsViewModel
                {
                    QuantidadeBanheiros = property.Detail.BathRoomQuantity,
                    QuantidadeQuartos = property.Detail.BedRoomQuantity,
                    TamanhoDoLocal = property.Detail.PropertySize,
                    VagasCarros = property.Detail.CarVacancyQuantity
                },
                Endereco = new AddressViewModel
                {
                    Bairro = property.Address.District,
                    Cep = property.Address.Cep,
                    Cidade = property.Address.City,
                    Estado = property.Address.State,
                    NumeroRua = property.Address.LocationNumber,
                    Rua = property.Address.Street
                }
            };

            return propertyViewModel;
        }
    }
}
