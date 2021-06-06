using FluentValidation;
using PropertyHandler.Core.Entities;

namespace PropertyHandler.Core.Validators
{
    public class PropertyValidation : AbstractValidator<Property>
    {
        public PropertyValidation()
        {
            RuleFor(x => x.Description)
              .NotNull()
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(10, 250)
              .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(x => x.Title)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
             .Length(10, 250)
             .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(x => x.TaxPrice)
             .NotEqual(0).WithMessage("O campo {PropertyName} está inválido!");

            RuleFor(x => x.OwnerName)
             .NotNull()
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
             .Length(10, 100)
             .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
