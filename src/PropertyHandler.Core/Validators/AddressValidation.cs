using FluentValidation;
using PropertyHandler.Core.Entities;

namespace PropertyHandler.Core.Validators
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(x => x.Cep)
                .NotNull()
                .NotEmpty()
              .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.City)
                .NotNull()
                .NotEmpty()
              .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.District)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");

                RuleFor(x => x.State)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.LocationNumber)
                .NotEqual(0)
                .WithMessage("O campo {PropertyName} está inválido");

        }
    }
}
