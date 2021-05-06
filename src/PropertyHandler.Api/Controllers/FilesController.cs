using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KissLog;

namespace PropertyHandler.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
      
        private readonly ILogger _logger;
        public FilesController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpPost("arquivo")]
        public IActionResult TesteArq(List<IFormFile> arquivos)
        {
            foreach (var arquivo in arquivos)
            {

                var imgPrefixo = Guid.NewGuid() + "_";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appdata/arquivos", imgPrefixo + arquivo.FileName);

                using (var stream = System.IO.File.Create(path))
                {
                    arquivo.CopyTo(stream);
                }
            }

            return Ok(arquivos);
        }

        [HttpGet("GetPerId/{id:guid}")]
        public async Task<FileResult> File(Guid id)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appdata/arquivos", "24736a70-859d-41b6-9108-6786dd57de48_Gordura - 190121 - 2.png");

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return File(bytes, "image/png", "24736a70-859d-41b6-9108-6786dd57de48_Gordura - 190121 - 2.png");
        }
    }
}
