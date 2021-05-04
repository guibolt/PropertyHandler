using KissLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Notifications;
using System.Linq;

namespace PropertyHandler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly INotifier _notifier;

        protected BaseController(ILogger logger, INotifier notifier)
        {
            _logger = logger;
            _notifier = notifier;
        }

        protected bool OperacaoValida() => !_notifier.HasNotifications();
        

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                _logger.Info(result);
                return Ok(new
                {
                    success = true,
                    data = result,
                });
            }

            _logger.Info(result);
            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificateInvalidModelError(modelState);
            return CustomResponse();
        }

        protected void NotificateInvalidModelError(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificateErro(errorMsg);
            }
        }

        protected void NotificateErro(string mensagem)
        {
            _logger.Error(mensagem);
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
