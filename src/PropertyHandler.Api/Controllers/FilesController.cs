using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using KissLog;
using PropertyHandler.Core.Interfaces.Repository;
using PropertyHandler.Core.Helpers;
using System.Diagnostics;

namespace PropertyHandler.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IImageRepository _imageRepository;

        public FilesController(ILogger logger, IImageRepository imageRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
        }

        [HttpGet("GetPerId/{id:guid}")]
        public async Task<FileResult> ReturnFile(Guid id)
        {
            Stopwatch relo = new Stopwatch();

            relo.Start();
            var imageSeached = await _imageRepository.GetPerFileId(id);
            var fileExtension = FileHelper.GetFileExtension(imageSeached.FileType);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "appdata/arquivos", $"{id}.{fileExtension}");
            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            relo.Stop();
            return File(bytes, imageSeached.FileType, imageSeached.Name);
        }
    }
}
