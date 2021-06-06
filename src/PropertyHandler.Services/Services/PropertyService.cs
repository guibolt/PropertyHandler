using Microsoft.AspNetCore.Http;
using PropertyHandler.Core.Entities;
using PropertyHandler.Core.Enums;
using PropertyHandler.Core.Helpers;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Repository;
using PropertyHandler.Core.Interfaces.Services;
using PropertyHandler.Core.Notifications;
using PropertyHandler.Core.Validators;
using PropertyHandler.Core.ViewModels;
using System;
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
        private readonly IImageRepository _imageRepository;

        public PropertyService(IPropertyRepository propertyRepository, IAddressRepository addressRepository,
            IDetailRepository detailsRepository, IImageRepository imageRepository, INotifier notifier) : base(notifier)
        {
            _propertyRepository = propertyRepository;
            _addressRepository = addressRepository;
            _detailsRepository = detailsRepository;
            _imageRepository = imageRepository;
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

        public async Task<int> RegisterProperty(PropertyViewModel propertyViewModel, List<IFormFile> imagens)
        {
            var property = Mapper.PropertyMap(propertyViewModel);
            property.Detail = Mapper.DetailMap(propertyViewModel.Detalhe);
            property.Address = Mapper.AddressMap(propertyViewModel.Endereco);

            if (!ExecuteValidation(new PropertyValidation(), property) 
                || !ExecuteValidation(new DetailValidation(), property.Detail)
                || !ExecuteValidation(new AddressValidation(), property.Address))
                return 0;

            var insertedPropertyId = await _propertyRepository.Insert(property);
            property.Detail.PropertyId = insertedPropertyId;
            property.Address.PropertyId = insertedPropertyId;

            var insertedAddressId = await _addressRepository.Insert(property.Address);
            var insertedDetailId = await _detailsRepository.Insert(property.Detail);
            await _propertyRepository.UpdateAddressAndDetail(insertedAddressId, insertedDetailId, insertedPropertyId);

            foreach (var arquivo in imagens)
            {
                PropertyImage novaImagem = new()
                {
                    Active = true,
                    FileId = Guid.NewGuid(),
                    FileType = arquivo.ContentType,
                    Name = arquivo.FileName,
                    PropertyId = insertedPropertyId,
                    RegisterDate = DateTime.Now
                };

                await FileHelper.CreateFile(arquivo, novaImagem.FileId.ToString());
                await _imageRepository.Insert(novaImagem);
            }

            return insertedPropertyId;
        }

        public async Task<object> GetProperty(int id)
        {
            var property = await _propertyRepository.GetPerId(id);

            if (property == null)
            {
                _notifier.Handle(new Notification("Registro não encontrado.", ETypeError.NotFound));
                return null;
            }

            property.Address = await _addressRepository.GetPerPropetyId(id);
            property.Detail = await _detailsRepository.GetPerPropetyId(id);
            property.Images = await _imageRepository.GetAllPerPropetyId(id);
            var propertyImages = await _imageRepository.GetAllPerPropetyId(property.Id);

            var returnedProperty = new
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
                },
                Imagens = from image in propertyImages
                          select new
                          {
                              IdArquivo = image.FileId,
                              NomeImagem = image.Name,
                              TipoArquivo = image.FileType,
                          }
            };

            return returnedProperty;
        }
    }
}
