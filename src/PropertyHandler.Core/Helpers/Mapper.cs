using PropertyHandler.Core.Entities;
using PropertyHandler.Core.ViewModels;

namespace PropertyHandler.Core.Helpers
{
    public static class Mapper
    {

        public static Property PropertyMap(PropertyViewModel propertyViewModel)
            => new()
            {
                Title = propertyViewModel.Titulo,
                Description = propertyViewModel.Descricao,
                SalePrice = propertyViewModel.ValorVenda,
                RentPrice = propertyViewModel.ValorAluguel,
                SpecificType = propertyViewModel.TipoEspecificoPropriedade,
                IsSpotlight = propertyViewModel.Destaque,
                Code = propertyViewModel.Codigo,
                CondominiumPrice = propertyViewModel.ValorCondominio,
                Type = propertyViewModel.Tipo == 1 ? Enums.EPropertyType.Commercial : Enums.EPropertyType.Residential,
                OwnerName = propertyViewModel.NomeProprietario,
                Status = Enums.EPropertyStatus.Opened,
                TaxPrice = propertyViewModel.ValorIPTU
            };

        public static Address AddressMap(AddressViewModel addressViewModel)
            => new()
            {
                Cep = addressViewModel.Cep,
                City = addressViewModel.Cidade,
                District = addressViewModel.Bairro,
                LocationNumber = addressViewModel.NumeroRua,
                State = addressViewModel.Estado,
                Street = addressViewModel.Rua
            };

        public static Detail DetailMap(DetailsViewModel detailsViewModel)
            => new()
            {
                BathRoomQuantity = detailsViewModel.QuantidadeBanheiros,
                BedRoomQuantity = detailsViewModel.QuantidadeQuartos,
                CarVacancyQuantity = detailsViewModel.VagasCarros,
                PropertySize = detailsViewModel.TamanhoDoLocal
            };
    }
}
