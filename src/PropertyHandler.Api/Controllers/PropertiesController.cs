using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyHandler.Api.ViewModels;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHandler.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertiesController : BaseController
    {
        private readonly IPropertyService _propertyService;
        public PropertiesController(IPropertyService propertyService,ILogger logger, INotifier notifier) : base(logger, notifier)
        {
            _propertyService = propertyService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _propertyService.GetProperties();

            return CustomResponse(properties);
        }
        [HttpPost("register")]
        public async Task<IActionResult> CadastrarImoveis([FromForm] PropertyViewModel propertyViewModel,List<IFormFile> imagens)
        {


            return Ok();
        }
    }
}
