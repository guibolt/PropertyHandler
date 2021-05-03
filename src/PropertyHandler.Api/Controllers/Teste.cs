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
    public class Teste : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger _logger;
        public Teste(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            _logger.Debug("Testando");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

           
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

        [HttpGet("File")]
        public async Task<FileResult> File()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appdata/arquivos", "24736a70-859d-41b6-9108-6786dd57de48_Gordura - 190121 - 2.png");

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return File(bytes, "image/png", "24736a70-859d-41b6-9108-6786dd57de48_Gordura - 190121 - 2.png");
        }
    }
}
