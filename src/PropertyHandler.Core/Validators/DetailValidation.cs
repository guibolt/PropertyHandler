using FluentValidation;
using PropertyHandler.Core.Entities;

namespace PropertyHandler.Core.Validators
{
    public class DetailValidation : AbstractValidator<Detail>
    {
        public DetailValidation()
        {
            RuleFor(x => x.BedRoomQuantity)
                .NotEqual(0)
                .WithMessage("O campo {PropertyName} está inválido");

        }
    }
}
