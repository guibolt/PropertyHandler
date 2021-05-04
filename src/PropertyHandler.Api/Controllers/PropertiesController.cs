using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyHandler.Api.ViewModels;
using PropertyHandler.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyHandler.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertiesController : BaseController
    {
        public PropertiesController(ILogger logger, INotifier notifier) : base(logger, notifier)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> CadastrarImoveis([FromForm] PropertyViewModel propertyViewModel,List<IFormFile> formFiles)
        {


            return Ok();
        }
    }
}
