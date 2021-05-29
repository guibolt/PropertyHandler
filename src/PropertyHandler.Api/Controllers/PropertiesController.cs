using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyHandler.Core.Interfaces;
using PropertyHandler.Core.Interfaces.Services;
using PropertyHandler.Core.ViewModels;
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
        public async Task<IActionResult> Register([FromForm] PropertyViewModel propertyViewModel, List<IFormFile> imagens)
        {
            var insertedId = await _propertyService.RegisterProperty(propertyViewModel,imagens);
            return CustomResponse(insertedId);
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetPerId(int id)
        {
            var property = await _propertyService.GetProperty(id);
            return CustomResponse(property);
        }
    }
}
