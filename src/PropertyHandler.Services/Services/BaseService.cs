using FluentValidation;
using FluentValidation.Results;
using PropertyHandler.Core.Entities;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Notifications;

namespace PropertyHandler.Services.Services
{
    public abstract class BaseService
    {
        protected readonly INotifier _notifier;

        protected BaseService(INotifier notifier) =>   _notifier = notifier;
        
        protected void Notify(ValidationResult validationResult) => validationResult.Errors.ForEach(x => Notify(x.ErrorMessage));
        protected void Notify(string message) =>   _notifier.Handle(new Notification(message));
        
        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : BaseEntity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
